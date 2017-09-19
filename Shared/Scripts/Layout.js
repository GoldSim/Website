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
      $paneFullHeight           = ($window.height() - $siteHeader.height());

    /**
     * Set elements marked with class "js-full-height" to 100% of the viewport height, minus the top bar
     */
    $('.js-full-height').each(function () {
      var $this = $(this);
      if ($window.width() > 768) {
        $this.innerHeight($paneFullHeight);
      }
    });

    // ### HACK KLT 20170807: Temporary workaround for stylesheet prototype
    $('</section>').prependTo($('.panel.body section.panel.accordion'));

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
