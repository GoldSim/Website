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