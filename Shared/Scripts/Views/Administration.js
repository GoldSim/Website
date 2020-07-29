/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * ADMINISTRATION SCRIPTS
 */
$(function () {

  /*============================================================================================================================
  | FUNCTION: TOGGLE ALL RECORDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Finds all rows in the table and selects all checkboxes, depending on state of table header checkbox; also sets/resets
    * the hidden field value of selected/checked records
    */
  $('#SelectAllRecords').change(function() {

    if (!$(this).is(':checked')) {
      $('tr.record input[type="checkbox"]').prop('checked', false);
      $(this).removeClass('all-selected');
    }
    else {
      $('tr.record input[type="checkbox"]').prop('checked', true);
      $(this).addClass('all-selected');
    }

  });

  /*============================================================================================================================
  | FUNCTION: TOGGLE RECORD
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * De-checks the "select all" checkbox in the event an individual row checkbox is toggled; also sets/resets the hidden
    * field value of selected/checked records.
    */
  $('tr.record td input[type="checkbox"]').change(function() {

    var $selectAllCheckbox = $('#SelectAllRecords');

    // Update "select all" checkbox
    if ($($selectAllCheckbox).hasClass('all-selected')) {
      $($selectAllCheckbox).prop('checked', false);
    }
    $($selectAllCheckbox).toggleClass('all-selected');

  });
  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/

  /**
    * De-checks the "select all" checkbox in the event an individual row checkbox is toggled; also sets/resets the hidden
    * field value of selected/checked records.
    */
  $('tr[data-href].record:not(td.js-no-click)').click(function() {
    window.location = $(this).attr("data-href");
  }).find(".js-no-click").click(function(e) {
    e.stopPropagation();
  });

})(jQuery);

/*==============================================================================================================================
| METHOD: CONFIRM DELETE
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
  * Provide a warning and confirmation when user chooses to delete a Topic
  * @return {object} Confirmation prompt confirming the user's selection
  */
function confirmDelete() {
  return confirm('Are you sure you want to delete the selected records? This action cannot be undone.');
}