/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * LAYOUT SCRIPTS
 * @file Defines presentation-oriented functionality related layout concerns for the GoldSim website.
 * @namespace goldSimWeb
 */
;(function(window, document, goldSimWeb, $, undefined) {
  'use strict';

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function () {

    /**
     * Handles the cookie consent notice:
     *  - checks for cookie setting to determine whether to display the notice;
     *  - removes the body spacing buffer CSS class when closing the fixed (at top) notice / alert box;
     *  - sets a cookie to prevent the alert from being shown again.
     */
    var cookiesConsentMatch     = document.cookie.match(new RegExp('(^| )CookiesConsent=([^;]+)'));
    if (cookiesConsentMatch && cookiesConsentMatch[2] === 'Agreed') {
      $('#CookiesNotice').slideUp(500);
    }
    else {
      $('#CookiesNotice').slideDown(500, 'linear');
    }
    $('#CookiesNoticeCloseButton').click(function () {
      var expiryDate            = new Date();

      // Set display cookie
      expiryDate.setFullYear(expiryDate.getFullYear() + 1);
      document.cookie           = 'CookiesConsent=Agreed;expires=' + expiryDate.toGMTString() + ';path=/';

      // Handle body spacing buffer
      $('#CookiesNotice').slideUp(500);

    });

    // ### HACK KLT 20170807: Temporary workaround for stylesheet prototype
    $('</section>').prependTo($('.panel.body section.panel.accordion'));

  });

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));