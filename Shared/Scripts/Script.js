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
      $screenSize               = $window.width(),
      $paneFullHeight           = ($window.height() - $siteHeader.height()),
      sceneController           = new ScrollMagic.Controller(),
      lastScrollTop             = 0,
      fileTypes                 = ['pdf', 'exe', 'zip', 'doc', 'docx', 'ppt', 'pptx', 'xls', 'xlsx'];

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function() {
      var $this = $(this);
      if ($window.width() > 640) {
        $this.innerHeight($paneFullHeight);
      }
    });

    /**
     * Sets up fixing/unfixing of CTAs panel as well as animation for primary (desktop) navigation
     */
    $(window).scroll(function () {
      var
        $navigationTriggerHook  = ($('#PrimaryNavigation').offset().top + $('#PrimaryNavigation').outerHeight()),
        $footerPosition         = $('#SiteFooter').offset().top,
        $footerHeight           = $('#SiteFooter').outerHeight(),
        $windowHeight           = $window.height(),
        $windowScrollTop        = $(this).scrollTop();

      // Control stickiness of CTAs panel
      if ($windowScrollTop > ($footerPosition - $windowHeight)) {
        $('#CTAs').css('position', 'static');
      }
      else {
        $('#CTAs').css('position', 'fixed');
      }

      // Control animation for primary (desktop) navigation
      if ($screenSize > 1024) {
        if ($windowScrollTop < lastScrollTop) {
        //console.log('scrolling up');
        //console.log('nav pos: ' + $navigationTriggerHook + ' - wST: ' + $windowScrollTop);
        } else {
        //console.log('scrolling down');
        }
      }
      lastScrollTop             = $windowScrollTop;

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
     * Handles page-level navigation changes for small screens
     */
    $('#PageNavigationSmallScreen select').change(function() {
      var $newUrl               = $(this).val();
      window.location.href      = $newUrl;
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
        $('div.search.form').addClass('open').removeClass('closed');
        $('input.gsc-input').focus();
      }
      else {
        $('div.search.form').addClass('closed').removeClass('open');
      }
    });
    $('.search.form').on('blur', 'input.gsc-input', function (event) {
      $('div.search.form').addClass('closed').removeClass('open');
    });

    // ### HACK KLT 20170807: Temporary workaround for stylesheet prototype
    $('</section>').prependTo($('.panel.body section.panel.accordion'));

  });

  /**
   * Handles animation for #PrimaryNavigation and/or #SiteHeader (depending on screen size)
   */
  var
    didScroll,
    lastScrollTop               = 0,
    delta                       = 5,
    $screenSize                 = $(window).width(),
    $navbarHeight               = ($('#SiteHeader').outerHeight()), // + $('#PrimaryNavigation').outerHeight()
    $fixedElement               = $('#PrimaryNavigation');
    if ($screenSize < 1024) {
      $navbarHeight             = $('#SiteHeader').outerHeight();
      $fixedElement             = $('#SiteHeader');
    }

  $(window).scroll(function (event) {
    didScroll                   = true;
  });

  setInterval(function () {
    if (didScroll) {
      hasScrolled();
      didScroll                 = false;
    }
  }, 250);

  function hasScrolled() {
    var $scrollTop              = $(window).scrollTop();

    console.log('$scrollTop: ' + $scrollTop);
    console.log('lastScrollTop: ' + lastScrollTop);
    console.log('$navbarHeight: ' + $navbarHeight);
    console.log('$scrollTop > lastScrollTop && $scrollTop > $navbarHeight: ' + ($scrollTop > lastScrollTop && $scrollTop > $navbarHeight));

    // Do nothing unless the scroll is more than the delta
    if (Math.abs(lastScrollTop - $scrollTop) <= delta) return;

    // If they scrolled down and are past the navbar, set class .static-primary-nav; otherwise, set class fixed-primary-nav
    if ($scrollTop > lastScrollTop && $scrollTop > $navbarHeight) {
      // Scrolling down
      $($fixedElement).removeClass('fixed-primary-nav').addClass('static-primary-nav');
    }
    else {
      // if ($scrollTop + $(window).height() < $(document).height()) {
      // Scrolling up
      if ($scrollTop >= $navbarHeight) {
        $($fixedElement).removeClass('static-primary-nav').addClass('fixed-primary-nav');
      }
    }

    lastScrollTop               = $scrollTop;
  }

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
