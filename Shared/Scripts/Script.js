/**
 * (GOLDSIM WEB) SCRIPTS
 * @file Defines functions, primarily presentation-oriented, related to the GoldSim website.
 * @namespace goldSimWeb
 */

/**
 * jQuery functionality
 */
(function(goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function() {

    /**
     * Establish variables
     */
    var
      $window                   = $(window),
      $siteHeader               = $('header.site.header'),
      $paneFullHeight           = ($window.height() - $siteHeader.height()),
      $fileTypes                = ['pdf', 'exe', 'zip', 'doc', 'docx', 'ppt', 'pptx', 'xls', 'xlsx'];

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function() {
      var $this                 = $(this);
      $this.innerHeight($paneFullHeight);
    });

    /**
     * Appends icon font elements to links
     */
    $($fileTypes).each(function (index, value) {
      var
        fileType                = value,
        $iconClass              = "file-pdf-o";

      switch (fileType) {
        case    'pdf'           :
          break;
        case    'exe'           :
          $iconClass            = 'download';
          break;
        case    'zip'           :
          $iconClass            = 'file-archive-o';
          break;
        case    'doc'           :
          $iconClass            = 'file-word-o';
          break;
        case    'docx'          :
          $iconClass            = 'file-word-o';
          break;
        case 'ppt'              :
          $iconClass            = 'file-powerpoint-o';
          break;
        case 'pptx'             :
          $iconClass            = 'file-powerpoint-o';
          break;
        case 'xls'              :
          $iconClass            = 'file-excel-o';
          break;
        case 'xlsx'             :
          $iconClass            = 'file-excel-o';
          break;
        default:
          break;
      }

      $('a[href $=".' + fileType + '"]').not('[class*="button"]').after('<i class="fa fa-' + $iconClass + '" aria-hidden="true"></i>');
    });

    /**
     * Adds placeholder text to GCSE input
     */
    $('#gsc-i-id1').attr('placeholder', 'Search').val('').focus().blur();

    /**
     * Capture search button trigger, set open/closed state
     */
    $('.search.form .buttons button').click(function (event) {
      if ($(this).hasClass('closed')) {
        $(this).addClass('open').removeClass('closed');
      }
      else {
        $(this).addClass('closed').removeClass('open');
      }
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
