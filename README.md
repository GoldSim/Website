## GoldSim.com
This repository contains the source code for the public-facing [GoldSim website](https://www.goldsim.com).

### Dependencies
Built using:

- [Ignia Topic Library](https://github.com/Ignia/Topics-Library)
- [Microsoft ASP.NET Core 3.0](https://www.asp.net/core)

### Subsites
- [Administration](Areas/Administration/): Administrative tools
- [Courses](Areas/Courses/): GoldSim online courses
- [Forms](Areas/Forms/): Customer request forms
- [Payments](Areas/Payments/): Credit card processing for invoices

### Components
- [`CallsToActionViewComponent`](Components/CallsToActionViewComponent.cs)
- [`FooterViewComponent`](Components/FooterViewComponent.cs)
- [`MenuViewComponent`](Components/MenuViewComponent.cs)
- [`PageLevelNavigationViewComponent`](Components/PageLevelNavigationViewComponent.cs)

In addition, some subsites contain their own components.

### Key Files
- [`Startup`](Startup.cs): Establishes overall application configuration, including authorization, file types, and routes.
- [`GoldSimActivator`](GoldSimActivator.cs): The _composition root_ for dependency injection.
- [`GoldSimTopicViewModelLookupService`](Services/GoldSimTopicViewModelLookupService.cs): Provides a lookup service for mapping topics to view components.
- [`GulpFile`](gulpFile.js): Entry point for precompiling, consolidating, minimizing, and distributing client-side JavaScript and SCSS dependencies.
- [`rewriteRules`](Configuration/rewriteRules.config): Configuration file for URL rewrite module, used for ensuring backward compatible URLs.

### Status
Ongoing development