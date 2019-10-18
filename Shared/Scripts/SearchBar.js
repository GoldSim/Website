/**
 * (GOLDSIM WEB) SEARCH BAR SCRIPTS
 * @file Defines functionality associated with the expanding search bar in the upper right of the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function () {

    /**
     * Track site search queries
     */
    var
      isSearchTracked           = false,
      query                     = getQuerystringValue('SearchText');
    if (isSearchTracked === false && query.length) {
      ga('send', 'event', 'Site Search', 'Search', query);
      isSearchTracked = true;
    }

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

  /**
   * Determine and return the value for the requested querystring parameter
   */
  function getQuerystringValue(parameter) {
    parameter = parameter.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var
      regex = new RegExp('[\\?&]' + parameter + '=([^&#]*)'),
      results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
  }

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
