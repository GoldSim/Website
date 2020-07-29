/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * FILE ICONS SCRIPTS
 * @file Defines functionality that appends (Font Awesome) file icons to button or text links pointing to files.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  /*============================================================================================================================
  | DECLARE FILE TYPES
  \---------------------------------------------------------------------------------------------------------------------------*/
  var fileTypes = {
    'pdf'                       : 'file-pdf',
    'exe'                       : 'download',
    'zip'                       : 'file-archive',
    'doc'                       : 'file-word',
    'docx'                      : 'file-word',
    'ppt'                       : 'file-powerpoint',
    'pptx'                      : 'file-powerpoint',
    'xls'                       : 'file-excel',
    'xlsx'                      : 'file-excel'
  };

  /*============================================================================================================================
  | FUNCTION: APPEND ICONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  var appendIcons = function (key, value) {
    $('a[href $=".' + key + '"]').not('[class*="button"]').after('<i class="fas fa-' + value + '" aria-hidden="true"></i>');
    $('a[href $=".' + key + '"][class*="button"]').append('<i class="fas fa-' + value + '" aria-hidden="true"></i>');
  };

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(

    $.each(fileTypes, appendIcons)

  );

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));