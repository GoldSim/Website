/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * FORM SCRIPTS
 */
$(function () {

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/

  /*----------------------------------------------------------------------------------------------------------------------------
  | Module exclusivity
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Treate Radionuclide and Contaminant transport modules as exclusive selections
    */
  var rtCheckbox = $("#BindingModel_Modules_RadionuclideTransport");
  var ctCheckbox = $("#BindingModel_Modules_ContaminantTransport");

  rtCheckbox.on('change', function(e) {
    ctCheckbox.prop('checked', false);
  });
  ctCheckbox.on('change', function(e) {
    rtCheckbox.prop('checked', false);
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Enable checkbox validation
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Extend the range validator return a boolean
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