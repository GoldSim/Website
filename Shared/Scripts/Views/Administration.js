/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * ADMINISTRATION SCRIPTS
 * @file A collection of scripts for use on the administration pages, mostly for handling record management. The types of
 * records will vary by administrative section, but the basic interactions can be generalized.
 */
;(function(window, document, goldSimWeb, $, undefined) {

  /*============================================================================================================================
  | FUNCTION: TOGGLE ALL RECORDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Finds all rows in the table and selects all checkboxes, depending on state of table header checkbox; also sets/resets
    * the hidden field value of selected/checked records
    */
  var toggleAllRecords = function () {

    if (!$(this).is(':checked')) {
      $('tr.record input[type="checkbox"]').prop('checked', false);
      $(this).removeClass('all-selected');
    }
    else {
      $('tr.record input[type="checkbox"]').prop('checked', true);
      $(this).addClass('all-selected');
    }

  };

  /*============================================================================================================================
  | FUNCTION: TOGGLE RECORD
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * De-checks the "select all" checkbox in the event an individual row checkbox is toggled; also sets/resets the hidden
    * field value of selected/checked records.
    */
  var toggleRecord = function () {

    var $selectAllCheckbox = $('#SelectAllRecords');

    // Update "select all" checkbox
    if ($($selectAllCheckbox).hasClass('all-selected')) {
      $($selectAllCheckbox).prop('checked', false);
    }
    $($selectAllCheckbox).toggleClass('all-selected');

  };

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function() {

    $('#SelectAllRecords').change(toggleAllRecords);
    $('tr.record td input[type="checkbox"]').change(toggleRecord);

    /**
      * Sometimes, records will be clickable, allowing a user to e.g. drill down to a detail page. This should not happen if the
      * user is instead attempting to select a record via a checkbox. In that case, disable the link.
      */
    $('tr[data-href].record:not(td.js-no-click)').click(function() {
      window.location = $(this).attr("data-href");
    }).find(".js-no-click").click(function(e) {
      e.stopPropagation();
    });

  });

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));

/*==============================================================================================================================
| METHOD: CONFIRM DELETE
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
  * Provide a warning and confirmation when user chooses to delete a record.
  * @return {object} Confirmation prompt confirming the user's selection
  */
function confirmDelete() {
  return confirm('Are you sure you want to delete the selected records? This action cannot be undone.');
}