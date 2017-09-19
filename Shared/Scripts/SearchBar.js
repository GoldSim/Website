/**
 * (GOLDSIM WEB) SEARCH BAR SCRIPTS
 * @file Defines functionality associated with the expanding search bar in the upper right of the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Add placeholder text to GCSE input
     */
    setTimeout(function () {
      $('#gsc-i-id1').attr('placeholder', 'Search');
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

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
