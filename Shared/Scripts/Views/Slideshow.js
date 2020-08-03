/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * SLIDESHOW
 * @file Object for initializing the slideshow carousel.
 */
;(function($, window, document, undefined) {

  /*============================================================================================================================
  | ESTABLISH VARIABLES
  \---------------------------------------------------------------------------------------------------------------------------*/
  var pluginName                = "slideshow";
  var defaults                  = {
  };

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Constructor for initializing the jQuery plugin.
    */
  function Plugin(element, options) {

    //Public properties
    this.element                = element;
    this.options                = $.extend({}, defaults, options);

    //Private properties
    this._defaults              = defaults;
    this._name                  = pluginName;

    //Initialize
    this.init();

  }

  /*============================================================================================================================
  | INITIALIZER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Initialize the plug.
    */
  Plugin.prototype.init = function () {

    var carousel                = $(this.element);

    carousel.owlCarousel({
      items                     : 1,
      lazyLoad                  : true,
      URLhashListener           : true,
      margin                    : 10,
      autoHeight                : false,
      startPosition             : 'URLHash',
      nav                       : true,
      navText                   : [
        '<button class="button large primary">Prev</button>',
        '<button class="button large primary">Next</button>',
      ],
      dots                      : true,
      dotsEach                  : 1
    });

    /**
      * Differentiate top and bottom nav
      */
    $('.owl-nav:not(.bottom)').addClass('top');

    /**
      * Recalculate carousel stage height
      */
    setTimeout(function() {
      this.adjustStageHeight('.owl-item.active');
    }.bind(this), 1000);
    carousel.on('translated.owl.carousel', function (event) {
      this.adjustStageHeight('.owl-item.active');
    }.bind(this));

    /**
      * Reflect navigation disabled state
      */
    $('.owl-prev.custom').addClass('disabled');
    carousel.on('translated.owl.carousel', function (event) {
      this.reflectDisabledState('.owl-prev');
      this.reflectDisabledState('.owl-next');
    }.bind(this));

    /**
      * Manually trigger bottom navigation
      */
    $('.owl-prev.custom').click(function () {
      carousel.trigger('prev.owl.carousel');
      // Scroll back to top of content
      $('html, body').animate({
        scrollTop: $('article[itemprop="mainContentOfPage"]').offset().top
      }, 'slow');
    });
    $('.owl-next.custom').click(function () {
      carousel.trigger('next.owl.carousel');
      // Scroll back to top of content
      $('html, body').animate({
        scrollTop: $('article[itemprop="mainContentOfPage"]').offset().top
      }, 'slow');
    });

    /**
      * Clear navigation button focus
      */
    $('.owl-prev, .owl-next').click(function () {
      $(this).find('button').blur();
    });


  };

  /*============================================================================================================================
  | PUBLIC CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Public interface for constructing a new instance of the plugin. This helps prevent multiple instances of the object from
    * being instantiated on the same object.
    */
  $.fn[pluginName] = function (options) {
    return this.each(function () {
      if (!$.data(this, "plugin_" + pluginName)) {
        $.data(
          this,
          "plugin_" + pluginName,
          new Plugin(this, options)
        );
      }
    });
  };

  /*============================================================================================================================
  | METHOD: ADJUST STAGE HEIGHT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Adjusts carousel stage height based on height of active slide
   */
  Plugin.prototype.adjustStageHeight = function(activeSlide) {
    var totalHeight = 0;
    $(activeSlide).children().each(function() {
      totalHeight += $(this).outerHeight(true);
    });
    $('div.owl-stage-outer').height(totalHeight);
  };

  /*============================================================================================================================
  | METHOD: REFLECT DISABLED STATE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Reflects disabled state in bottom navigation
   */
  Plugin.prototype.reflectDisabledState = function(navButton) {
    $('.owl-prev.custom, .owl-next.custom').removeClass('disabled');
    if ($('.owl-nav.top ' + navButton).hasClass('disabled')) {
      $(navButton + '.custom').addClass('disabled');
    }
  };

})(jQuery, window, document);