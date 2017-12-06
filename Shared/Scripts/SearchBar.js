﻿/**
 * (GOLDSIM WEB) SEARCH BAR SCRIPTS
 * @file Defines functionality associated with the expanding search bar in the upper right of the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Capture search button trigger, set open/closed state
     */
    $('.search.form .buttons button').mousedown(function (event) {
      var $searchBar = $('div.search.form');
      if ($searchBar.hasClass('closed')) {
        event.preventDefault();
        $('div.search.form').addClass('open').removeClass('closed');
        $('.search.form input[type="search"]').focus();
      }
      else {
        $('div.search.form').addClass('closed').removeClass('open');
      }
    });
    $('.search.form').on('blur', 'input[type="search"]', function (event) {
      $('div.search.form').addClass('closed').removeClass('open');
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
