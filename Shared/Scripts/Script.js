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
      sceneController           = new ScrollMagic.Controller(),
      fileTypes                 = ['pdf', 'exe', 'zip', 'doc', 'docx', 'ppt', 'pptx', 'xls', 'xlsx'];

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function() {
      var $this                 = $(this);
      $this.innerHeight($paneFullHeight);
    });

    /**
     * Sets up fixing/unfixing of CTAs panel
     */
    $(window).scroll(function () {
      var
        $footerPosition         = $('#SiteFooter').offset().top,
        $footerHeight           = $('#SiteFooter').outerHeight(),
        $windowHeight           = $(window).height(),
        $windowScrollTop        = $(this).scrollTop();

      if ($windowScrollTop > ($footerPosition - $windowHeight)) {
        $('#CTAs').css('position', 'static');
      }
      else {
        $('#CTAs').css('position', 'fixed');
      }

    });

    /**
     * Works around need to double-click primary navigation parent links on touchscreen devices
     */
    $('.is-dropdown-submenu-parent > a').on('click touchend', function (e) {
      var
        $clickedLink            = $(this),
        linkHref                = $clickedLink.attr('href');
      window.location           = linkHref;
    });

    /**
     * Appends icon font elements to links
     */
    $(fileTypes).each(function (index, value) {
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
      $('a[href $=".' + fileType + '"][class*="button"]').append('<i class="fa fa-' + $iconClass + '" aria-hidden="true"></i>');
    });

    /**
     * Adds placeholder text to GCSE input
     */
    setTimeout(function () {
      $('#gsc-i-id1').attr('placeholder', 'Search'); //.val('').focus().blur()
    }, 2000);

    /**
     * Capture search button trigger, set open/closed state
     */
    $('.search.form .buttons button').mousedown(function (event) {
      var $searchBar = $('div.search.form');
      if ($searchBar.hasClass('closed')) {
        event.preventDefault();
        $('input.gsc-input').focus();
      }
    });
    $('.search.form').on('focus', 'input.gsc-input', function (event) {
      $('div.search.form').addClass('open').removeClass('closed');
    });
    $('.search.form').on('blur', 'input.gsc-input', function (event) {
      $('div.search.form').addClass('closed').removeClass('open');
    });

    // ### HACK KLT 20170807: Temporary workaround for stylesheet prototype
    $('</section>').prependTo($('.panel.body section.panel.accordion'));

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
