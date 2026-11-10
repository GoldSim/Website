/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * FORM SCRIPTS
 * @file A collection of scripts for use on the forms, mostly for handling special validation rules.
 */
;(function(window, document, goldSimWeb, $, undefined) {

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function() {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Module exclusivity
    \-------------------------------------------------------------------------------------------------------------------------*/
    /**
      * Treat Radionuclide and Contaminant transport modules as exclusive selections
      */
    var rtCheckbox = $("#BindingModel_Modules_RadionuclideTransport");
    var ctCheckbox = $("#BindingModel_Modules_ContaminantTransport");

    rtCheckbox.on('change', function(e) {
      ctCheckbox.prop('checked', false);
    });
    ctCheckbox.on('change', function(e) {
      rtCheckbox.prop('checked', false);
    });

    /*--------------------------------------------------------------------------------------------------------------------------
    | State lookup
    \-------------------------------------------------------------------------------------------------------------------------*/
    /**
      * The state field is selected from a metadata lookup of U.S. states, and is thus only applicable if the United States is
      * selected as the country. Otherwise, it is disabled, instead of hidden, so that subsequent fields don't shift position.
      * The state-lookup class is matched instead of the field's name attribute, since the underlying property may be nested
      * (e.g. BindingModel.Address.Province) or top-level (e.g. BindingModel.Province) depending on the form, and thus its name
      * won't be consistent.
      */
    var countrySelect = $('select[name="BindingModel.Country"]');
    var stateSelect = $('.state-lookup').find('select');

    // Function to ensure that the state field is only enabled, and populated, for the United States
    function syncState() {

      var isUnitedStates = countrySelect.val() === "United States";

      // Clear any previously selected state, since it doesn't apply outside of the United States
      if (!isUnitedStates) {
        stateSelect.val('');
      }

      // Ensure state field is only enabled for the United States
      stateSelect.prop('disabled', !isUnitedStates);

    }

    // Sync on page load and whenever the country changes
    countrySelect.on('change', syncState);
    syncState();

    /*--------------------------------------------------------------------------------------------------------------------------
    | Enable checkbox validation
    \-------------------------------------------------------------------------------------------------------------------------*/
    /**
      * By default, values of checkboxes are returned as strings. This makes them incompatible with validation rules requiring a
      * true value, as provided for by server-side validation rules configured via jQuery Unobtrusive. This can be fixed by
      * updating the validator to return a boolean value for checkbox elements.
      */
    //
    var defaultRangeValidator = $.validator.methods.range;
    $.validator.methods.range = function (value, element, param) {
      if (element.type === 'checkbox') {
        //If it's a checkbox, return its checked state
        return element.checked;
      } else {
        //Otherwise, run the default validator
        return defaultRangeValidator.call(this, value, element, param);
      }
    };
  });

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));