/**
 * (GOLDSIM WEB) FILE ICONS SCRIPTS
 * @file Defines functionality that appends (Font Awesome) file icons to button or text links pointing to files.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Establish variables
     */
    var fileTypes = {
      'pdf'                     : 'file-pdf-o',
      'exe'                     : 'download',
      'zip'                     : 'file-archive-o',
      'doc'                     : 'file-word-o',
      'docx'                    : 'file-word-o',
      'ppt'                     : 'file-powerpoint-o',
      'pptx'                    : 'file-powerpoint-o',
      'xls'                     : 'file-excel-o',
      'xlsx'                    : 'file-excel-o'
    };

    /**
     * Append icon font elements to links
     */
    $.each(fileTypes, function (key, value) {
      $('a[href $=".' + key + '"]').not('[class*="button"]').after('<i class="fab fa-' + value + '" aria-hidden="true"></i>');
      $('a[href $=".' + key + '"][class*="button"]').append('<i class="fab fa-' + value + '" aria-hidden="true"></i>');
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
