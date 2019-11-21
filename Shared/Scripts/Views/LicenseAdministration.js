$(function () {

  /**
    * Instantiates the dataTable plugin for the pending requests; $requestsTableRows finds all rows (even those hidden by
    * paging) for the dataTable.
    */
  var $requestsTable = $('table[id*="PendingRequests"]').DataTable({
    pageLength                  : 75,
    //'paging'                  : false,
    lengthMenu                  : [[10, 25, 50, -1], [10, 25, 50, "All"]],
    order                       : [[4, 'asc']],
    stateSave                   : false
  });
  var $requestsTableRows = $requestsTable.rows().nodes();

  /**
    * Finds all rows in the table and selects all checkboxes, depending on state of table header checkbox; also sets/resets
    * the hidden field value of selected/checked requests
    */
  $('#SelectAllRecords').change(function () {

    if (!$(this).is(':checked')) {
      $('input[type="checkbox"]', $requestsTableRows).prop('checked', false);
      $(this).removeClass('all-selected');
    }
    else {
      $('input[type="checkbox"]', $requestsTableRows).prop('checked', true);
      $(this).addClass('all-selected');
    }

  });

  /**
    * De-checks the "select all" checkbox in the event an individual row checkbox is toggled; also sets/resets the hidden
    * field value of selected/checked requests
    */
  $('.Request-Row td input[type="checkbox"]').change(function () {

    var $selectAllCheckbox = $('#SelectAllRecords');

    // Update "select all" checkbox
    if ($($selectAllCheckbox).hasClass('all-selected')) {
      $($selectAllCheckbox).prop('checked', false);
    }
    $($selectAllCheckbox).toggleClass('all-selected');

  });

});

/**
  * Provide a warning and confirmation when user chooses to delete a Topic
  */
function confirmDelete() {
  return confirm('Are you sure you want to delete the selected license requests? This action cannot be undone.');
}