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

});
