/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * FIXED-COLUMN TABLES SCRIPTS
 * @file Allows the first column of a table to remain fixed while subsequent columns scroll. This permits wide table layouts
 * while continuing to provide context.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function () {
    setFixedColumnTables();
    $(window).resize(function () {
      setFixedColumnTables();
    });
  });

  /*============================================================================================================================
  | FUNCTION: SET FIXED COLUMN TABLES
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Clone the first column of a table and position it on top of the original table, such that the original table slides
   * underneath the fixed first column.
   */
  function setFixedColumnTables() {
    var
      fixedColumnTable          = $('.fixed-column.table-wrapper table:not(.cloned)'),
      firstColumnWidth          = ($(window).width() * 0.5);

    // Only perform the column fixing for small screens
    /*
    if ($(window).width() < 768 && ($(window).width() < $(window).height())) {

      $(fixedColumnTable).each(function (index, element) {
        var
          originalTable         = $(this),
          clonedTable           = $(originalTable).clone().insertBefore($(originalTable)).addClass('cloned');

        // Update the width of the first column if there are only two columns in the table
        if ($(originalTable).find('thead td').length <= 2) {
          firstColumnWidth      = ($(window).width() * 0.5);
        }

        // Remove everything in the table clone except for first column
        clonedTable.find('th:not(:first-child),td:not(:first-child)').remove();

        // Set a consistent width for the first columns (cloned and original tables)
        $(originalTable).find('tr td:first-child').css('width', firstColumnWidth + 'px').css('min-width', firstColumnWidth + 'px');
        $(clonedTable).find('tr td:first-child').css('width', firstColumnWidth + 'px').css('min-width', firstColumnWidth + 'px');

        // Match the height of the rows in the fixed column (cloned) table to that of the original table's
        clonedTable.find('tr').each(function (index, element) {
          var rowHeight = originalTable.find('tr:eq(' + index + ')').innerHeight();
          $(this).outerHeight(rowHeight);
        });

      });

    }
    */

  }

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));