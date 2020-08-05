/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * HOME SCRIPTS
 * @file A collection of scripts for wiring up events relevant to the homepage, such as opening the video in a modal window,
 * playing the video, and wiring up the carousel.
 */
;(function(window, document, goldSimWeb, $, undefined) {

  /*============================================================================================================================
  | FUNCTION: INIT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Provides lazy loading for images so that the page can load quickly, with images being filled in after-the-fact. This is
   * useful for the homepage given how long it is, and especially with the carousels, as those don't affect the initial user
   * experience of the page.
   */
  function init() {
    var deferredImages = document.getElementsByTagName('img');
    for (var i = 0; i < deferredImages.length; i++) {
      if (deferredImages[i].getAttribute('data-src')) {
        deferredImages[i].setAttribute('src', deferredImages[i].getAttribute('data-src'));
      }
    }
  }
  window.onload = init;

  /*============================================================================================================================
  | FUNCTION: INIT VIDEO
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Dynamically loads the MPEG-DASH video player upon request. This is done dynamically as we don't want the video to begin
   * until the user clicks on the popup, as most users will just pass over it.
   */
  function initVideo() {

    /*--------------------------------------------------------------------------------------------------------------------------
    | DECLARE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    var
      manifestUrl               = 'https://media.GoldSim.com/Videos/Home/GoldSim_Overview_dash.mpd',
      fallbackUrl               = 'https://31cac97e830ee523d21d-3991774b1862aed7fa3658c502b53d27.ssl.cf1.rackcdn.com/GoldSimm_Overview_082718.mp4',
      dashPlayer                = dashjs.MediaPlayer().create(),
      isSafari                  = !!navigator.userAgent.match(/Version\/[\d\.]+.*Safari/),
      isIos                     = /iPhone|iPod/.test(navigator.userAgent) && !window.MSStream,
      isIe11                    = !!navigator.userAgent.match(/Trident\/7\./);

    /*--------------------------------------------------------------------------------------------------------------------------
    | CONFIGURE PLAYER
    \-------------------------------------------------------------------------------------------------------------------------*/
    dashPlayer.updateSettings({
      'streaming'               : {
        'retryAttemps'          : {
          'MPD'                 : 2,
          'XLinkExpansion'      : 2,
          'IndexSegment'        : 3,
          'InitializationSegment' : 3,
          'BitstreamSwitchingSegment': 3
        },
        'abr'                   : {
          'autoSwitchBitrate'   : {
            'audio'             : false,
            'video'             : true
          }
        }
      }
    });

    /*--------------------------------------------------------------------------------------------------------------------------
    | INITIALIZE PLAYER
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (!(isIos && isSafari) && !isIe11) {
      dashPlayer.initialize(document.querySelector('#IntroductionVideo'), manifestUrl, true);
    }

    /*--------------------------------------------------------------------------------------------------------------------------
    | HANDLE UNSUPPORTED BROWSERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    else {
      $('#IntroductionVideo source').attr('src', fallbackUrl);
      $('#IntroductionVideo')[0].play();
    }

  }

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/

  /*----------------------------------------------------------------------------------------------------------------------------
  | ESTABLISH VARIABLES
  \---------------------------------------------------------------------------------------------------------------------------*/
  var isVideoLoaded             = false;
  var introductionPanelPosition = ($('#Introduction').offset().top - 24);

  /*----------------------------------------------------------------------------------------------------------------------------
  | HANDLE SCROLL CLICK
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('img.arrow.scroll').click(function() {
    $('html,body').animate({
      scrollTop                 : introductionPanelPosition
    }, 1000);
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | INITIALIZE CAROUSEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('.owl-carousel.large').owlCarousel({
    items                       : 1,
    margin                      : 0,
    autoHeight                  : true,
    nav                         : true,
    navText                     : [
      '<i class="fa fa-caret-left"></i>',
      '<i class="fa fa-caret-right"></i>'
    ],
    loop                        : false
  });
  $('.owl-carousel.small').owlCarousel({
    items                       : 1,
    margin                      : 0,
    autoHeight                  : false,
    nav                         : true,
    navText                     : [
      '<i class="fa fa-caret-left"></i>',
      '<i class="fa fa-caret-right"></i>'
    ],
    loop                        : false
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | CAROUSEL: HANDLE SCROLLING
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Handle toggling of carousel slide text / screenshot
    */
  $.fn.toggleText = function (textOriginal, textToggled) {
    if (this.text() == textOriginal) {
      this.text(textToggled);
    }
    else {
      this.text(textOriginal);
    }
    return this;
  };
  $('a.js-toggle-slide').click(function () {
    $(this).siblings('.description, .screenshot').toggleClass('is-hidden');
    $(this).toggleText('View Screenshot', 'View Description');
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | INITIALIZE VIDEO
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Handle video playback based on Foundation Reveal events
    */
  $(document).on('open.zf.reveal', '[data-reveal]', function () {

    // Play, if already loaded
    if (isVideoLoaded === true) {
      $('#IntroductionVideo')[0].play();
      return;
    }

    // Initialize video
    initVideo();

    // Toggle video loaded
    isVideoLoaded = true;

    // Track video play event
    ga('send', 'event', 'Video', 'Play', 'Hompage Splash Video');

  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | CLOSE VIDEO
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).on('closed.zf.reveal', '[data-reveal]', function () {
    $('#IntroductionVideo')[0].pause();
  });

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));