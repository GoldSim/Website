/**
 * (GOLDSIM WEB) MENU SCRIPTS
 * @file Defines functionality associated with the primary (desktop) navigation menu, the (mobile) hamburger menu, or the
 * (mobile) page-level navigation on the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Work around need to double-click primary navigation parent links on touchscreen devices
     */
    $('.is-dropdown-submenu-parent > a').on('click touchend', function (e) {
      var
        $clickedLink            = $(this),
        linkHref                = $clickedLink.attr('href');
      window.location           = linkHref;
    });

    /**
     * Handle page-level navigation changes for small screens
     */
    $('#PageNavigationSmallScreen select').change(function () {
      var $newUrl               = $(this).val();
      window.location.href      = $newUrl;
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
