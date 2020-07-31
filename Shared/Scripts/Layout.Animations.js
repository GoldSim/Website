/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * LAYOUT ANIMATIONS SCRIPTS
 * @file Defines animation functionality related to the GoldSim website header, primary (desktop) navigation menu, and bottom
 * area Calls To Action panel.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function () {

    /**
     * Handles initial scroll CTAs trigger
     */
    $(window).on('scroll.callsToAction', function () {
      if ($(this).scrollTop() > 25) {
        $('#CallsToAction').removeClass('off-screen').addClass('on-screen');
        $(window).off('scroll.callsToAction');
      }
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));