/**
 * (GOLDSIM WEB) LAYOUT ANIMATIONS SCRIPTS
 * @file Defines animation functionality related to the GoldSim website header, primary (desktop) navigation menu, and bottom
 * area Calls To Action panel.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

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

  /**
   * Handles animation for #PrimaryNavigation and/or #SiteHeader (depending on screen size)
   */

  // Establish variables
  var
    headroomElement             = document.getElementById('PrimaryNavigation'),
    $screenSize                 = $(window).width(),
    $siteHeaderHeight           = $('#SiteHeader').outerHeight(),
    $offset                     = $siteHeaderHeight;
  if ($screenSize < 1024) {
    headroomElement             = document.getElementById('SiteHeader');
    $offset                     = 0;
  }

  // Instantiate Headroom
  var headroom = new Headroom(headroomElement, {
    'offset'                    : $offset,
    'tolerance'                 : {
      up                        : 5,
      down                      : 0
    },
    'classes'                   : {
      'initial'                 : 'animated',
      'pinned'                  : 'animated-pinned',
      'unpinned'                : 'animated-unpinned'
    }
  });
  headroom.init();

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
