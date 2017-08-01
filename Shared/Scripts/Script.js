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
      $paneFullHeight           = ($window.height() - $siteHeader.height());

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function() {
      var $this                 = $(this);
      $this.innerHeight($paneFullHeight);
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
