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
    | State/province lookup
    \-------------------------------------------------------------------------------------------------------------------------*/
    /**
      * If the United States is selected as the country, the state/province field should be selected from a metadata lookup;
      * otherwise, it should be entered as free text, since we don't have a lookup of all state/province names on a per country
      * basis. The state-lookup and province-input classes are matched instead of the field's name attribute, since the
      * underlying property may be nested (e.g. BindingModel.Address.Province) or top-level (e.g. BindingModel.Province)
      * depending on the form, and thus their name won't be consistent.
      */
    var countrySelect = $('select[name="BindingModel.Country"]');
    var stateField = $('.state-lookup');
    var provinceField = $('.province-input');
    var stateSelect = stateField.find('select');
    var provinceInput = provinceField.find('input');

    // Function to ensure that the correct state or province field is visible and enabled
    function syncStateProvince() {
      var isUnitedStates = countrySelect.val() === "United States";
      stateSelect.prop('disabled', !isUnitedStates);
      provinceInput.prop('disabled', isUnitedStates);
      stateField.toggleClass('is-hidden', !isUnitedStates);
      provinceField.toggleClass('is-hidden', isUnitedStates);
    }

    // On page load, both controls are already populated from the model (e.g., after a server-side validation error), so only
    // visibility and disabled state need to be synced
    countrySelect.on('change', function() {

      var isUnitedStates = countrySelect.val() === "United States";

      // Carry the current value over to whichever control is about to become active, since this is a deliberate user-driven
      // switch between the two representations
      if (isUnitedStates) {
        stateSelect.val(provinceInput.val());
      } else {
        provinceInput.val(stateSelect.val());
      }

      // Ensure correct field is visible and enabled
      syncStateProvince();

    });

    syncStateProvince();

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