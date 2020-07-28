/**
 * (GOLDSIM WEB) LAYOUT SCRIPTS
 * @file Defines presentation-oriented functionality related layout concerns for the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Establish variables
     */
    var
      $window                   = $(window),
      $siteHeader               = $('header.site.header'),
      $screenSize               = $window.width(),
      $primaryNavHeight         = ($('#PrimaryNavigation').length ? $('#PrimaryNavigation').height() : 0),
      $paneFullHeight           = ($window.height() - $siteHeader.height() - $primaryNavHeight);

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function () {
      var $this = $(this);
      if ($window.width() > 768) {
        $this.outerHeight($paneFullHeight);
      }
    });

    /**
     * Handles the cookie consent notice:
     *  - checks for cookie setting to determine whether to display the notice;
     *  - removes the body spacing buffer CSS class when closing the fixed (at top) notice / alert box;
     *  - sets a cookie to prevent the alert from being shown again.
     */
    var cookiesConsentMatch     = document.cookie.match(new RegExp('(^| )CookiesConsent=([^;]+)'));
    if (cookiesConsentMatch && cookiesConsentMatch[2] === 'Agreed') {
      $('#CookiesNotice').hide();
      $('body').removeClass('has-notice');
    }
    else {
      $('#CookiesNotice').removeClass('hidden');
      $('body').addClass('has-notice');
    }
    $('#CookiesNoticeCloseButton').click(function () {
      var expiryDate            = new Date();

      // Set display cookie
      expiryDate.setFullYear(expiryDate.getFullYear() + 1);
      document.cookie           = 'CookiesConsent=Agreed;expires=' + expiryDate.toGMTString() + ';path=/';

      // Handle body spacing buffer
      $('body').removeClass('has-notice');

    });

    // ### HACK KLT 20170807: Temporary workaround for stylesheet prototype
    $('</section>').prependTo($('.panel.body section.panel.accordion'));

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));