## HubSpot Contact Sync

Form submissions are optionally synced to HubSpot as CRM contacts, alongside the existing email receipt and, if configured, OnTopic save. Sync is configured per-form based on JSON manifest files—no code changes are needed to add a field or wire up a new form once a manifest exists.

### Manifest Location and Naming

Manifests live under `Areas/Forms/HubSpot/Mappings/` and are loaded once, at startup, by `HubSpotMappingRegistry`. Each file is deserialized into a `HubSpotFormManifest` and keyed by its `FormIdentifier`, which must match the form's binding model name with the `BindingModel` suffix removed (e.g. `TrialFormBindingModel` becomes `TrialForm`). `FormsController` skips the HubSpot sync for any forms that don't have a matching manifest.

A manifest's filename doesn't matter to the registry, but naming it after its `FormIdentifier` (e.g. `TrialForm.json`) keeps the folder easy to scan. Manifests are loaded and validated once, at startup, so any of the following also throws an exception then rather than on first use: 
- Two manifests sharing the same `FormIdentifier`
- A manifest that fails to deserialize as valid JSON, 
- A manifest with no field (or more than one) marked `isUniqueKey`
- A field that sets more than one, or none, of `sourceAttribute`, `constantValue`, and `sourceFlags`
- A field whose `sourceFlags` names a lookup that isn't defined, or whose `hubSpotType` isn't `enumerationSet`
- A field that sets `isAppended` with a `hubSpotType` other than `enumerationSet`

These are considered configuration issues and throw exceptions so that authors can identify and resolve them at design time. 

### Adding a Field

Each entry in a manifest's `fields` array maps one HubSpot property to either a topic attribute or a fixed value:

```json
{
  "sourceAttribute": "Email",
  "hubSpotProperty": "email",
  "hubSpotType": "string",
  "isRequired": true,
  "isUniqueKey": true
}
```

- `sourceAttribute`, `constantValue`, _or_ `sourceFlags`: `sourceAttribute` reads a `Topic` attribute; `constantValue` writes the same fixed value on every submission; `sourceFlags` combines several boolean attributes into a checkbox list (see below). Setting more than one, or none, fails manifest validation at startup.
- `hubSpotProperty`: The internal HubSpot property name.
- `hubSpotType`: `string`, `enumeration` (dropdown), or `enumerationSet` (checkbox list).
- `isRequired`: If the resolved value is empty, a required field throws an exception before any HubSpot call is made. Optional fields, by contrast, are simply omitted from the payload if they're empty.
- `isUniqueKey`: Exactly one field per manifest must set this to `true`. Its resolved value becomes the `id`/`idProperty` HubSpot uses to decide whether to create or update a contact.
- `isAppended`: Optional; for `enumerationSet` fields, adds the values to any already selected on the contact instead of replacing them (see below).
- `valueMap`: Optional; see below.

Empty optional fields are omitted from the payload rather than sent as null, since an explicit null would overwrite an existing HubSpot value on update.

### Enumeration Values and `valueMap`

HubSpot's `enumeration` and `enumerationSet` fields use internal option values that don't necessarily match the source topic's values. `valueMap` translates one to the other:

```json
{
  "sourceAttribute": "Industry",
  "hubSpotProperty": "industry",
  "hubSpotType": "enumeration",
  "valueMap": {
    "Software": "software_internal_value",
    "Manufacturing": "manufacturing_internal_value"
  }
}
```

If `valueMap` is omitted, the source value is sent unchanged. If it's present and a source value has no entry, `HubSpotPayloadBuilder` throws—an unmapped value is never silently dropped or passed through, since HubSpot rejects unknown option values for the entire contact "upsert", not just the offending field. When adding an enumeration field, populate `valueMap` with _every_ value the source can actually produce, or leave it unset entirely if the source and HubSpot values already match verbatim.

For `enumerationSet`, the source value is a single string with individual tokens separated by `HubSpotFieldMapping.MultiValueDelimiter` (a comma); each token is mapped independently through the same `valueMap`, then joined with HubSpot's delimiter (a semicolon) on write.

By default, an `enumerationSet` value _replaces_ every option already selected on the contact. Setting `isAppended` prefixes the value with a semicolon, which HubSpot treats as "add these to the existing selections"; options that are already selected aren't duplicated. This matters for properties shared with other products, such as `datacor_product`, where replacing the value would remove another product's selection. (HubSpot also accepts this syntax on single-select `enumeration` properties, but stores the combined values in a field that expects one, which is why `isAppended` is limited to `enumerationSet`.)

### Boolean Flags and Shared Lookups

Some forms store a checkbox list as several boolean attributes (e.g. `ModulesReliability`, `ModulesContaminantTransport`) rather than a single delimited value. `sourceFlags` maps these to a single HubSpot `enumerationSet` by naming a shared lookup that pairs each attribute with its HubSpot option value:

```json
{
  "sourceFlags": "Modules",
  "hubSpotProperty": "goldsim_info_addon_modules",
  "hubSpotType": "enumerationSet"
}
```

Shared lookups live in `Mappings/Shared/Lookups.json`, keyed by name, so that the same attribute-to-option pairs aren't repeated, and potentially drift, across manifests:

```json
{
  "Modules": {
    "ModulesReliability": "Reliability Module",
    "ModulesContaminantTransport": "Contaminant Transport (CT) Module"
  }
}
```

Each attribute that's set to true has its option value included; if none are set, the field is treated as empty. A lookup may list attributes that a given form never sets (e.g. the Trial form only offers the transport modules), but any attribute that _is_ set must map to an option the target HubSpot property accepts. Since `HubSpotMappingRegistry` only loads manifests from the top level of `Mappings/`, the `Shared/` folder isn't mistaken for a manifest.

### Missing Tokens and Sync Failures

`HubSpotContactSyncService.SyncAsync()` never throws. If no `HubSpot:AccessToken` is configured, it returns immediately with a skipped result and makes no HTTP call—this is a supported, expected runtime state, not an error. Any other failure (an unreachable host, a non-2xx response, or a manifest/payload error such as a missing required field) is caught and reported via the returned `HubSpotSyncResult` instead of propagating. A HubSpot outage, an invalid token, or a missing token never blocks a form submission and never prevents the application from starting.

The access token is read from configuration at `HubSpot:AccessToken`, which maps to the `HubSpot__AccessToken` environment variable in Azure App Service.