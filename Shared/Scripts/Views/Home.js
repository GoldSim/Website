/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/*==============================================================================================================================
| METHOD: INIT
\-----------------------------------------------------------------------------------------------------------------------------*/
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

/*==============================================================================================================================
| JQUERY: WIRE UP ACTIONS
\-----------------------------------------------------------------------------------------------------------------------------*/
$(function() {

  /**
    * Establish variables
    */
  var
    isVideoLoaded           = false,
    introductionPanelPosition = ($('#Introduction').offset().top - 24),
    isSafari                = !!navigator.userAgent.match(/Version\/[\d\.]+.*Safari/),
    isIos                   = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream,
    isIe11                  = !!navigator.userAgent.match(/Trident\/7\./);

  /**
    * Handle splash screen arrow scroll click
    */
  $('img.arrow.scroll').click(function() {
    $('html,body').animate({
      scrollTop: introductionPanelPosition
    }, 1000);
  });

  /**
    * Instantiate carousel for large and small screens
    */
  $('.owl-carousel.large').owlCarousel({
    items: 1,
    margin: 0,
    autoHeight: true,
    nav: true,
    navText: [
      '<i class="fa fa-caret-left"></i>',
      '<i class="fa fa-caret-right"></i>'
    ],
    loop: false
  });
  $('.owl-carousel.small').owlCarousel({
    items: 1,
    margin: 0,
    autoHeight: false,
    nav: true,
    navText: [
      '<i class="fa fa-caret-left"></i>',
      '<i class="fa fa-caret-right"></i>'
    ],
    loop: false
  });

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

  /**
    * Handle video playback based on Foundation Reveal events
    */
  $(document).on('open.zf.reveal', '[data-reveal]', function () {
    if (isVideoLoaded === false) {
      var
        manifestUrl         = 'https://media.GoldSim.com/Videos/Home/GoldSim_Overview_dash.mpd',
        fallbackUrl         = 'https://31cac97e830ee523d21d-3991774b1862aed7fa3658c502b53d27.ssl.cf1.rackcdn.com/GoldSimm_Overview_082718.mp4',
        dashPlayer          = dashjs.MediaPlayer().create();

      //Configure dashPlayer
      dashPlayer.updateSettings({
        'streaming'         : {
          'retryAttemps'    : {
            'MPD'           : 2,
            'XLinkExpansion': 2,
            'IndexSegment'  : 3,
            'InitializationSegment': 3,
            'BitstreamSwitchingSegment': 3
          },
          'abr'             : {
            'autoSwitchBitrate': {
              'audio'       : false,
              'video'       : true
            }
          }
        }
      });

      // Only initialize DASH for supported browsers
      if (!(isIos && isSafari) && !isIe11) {
        dashPlayer.initialize(document.querySelector('#IntroductionVideo'), manifestUrl, true);
      }
      // Otherwise, use fallback / original video
      else {
        $('#IntroductionVideo source').attr('src', fallbackUrl);
        $('#IntroductionVideo')[0].play();
      }

      // Toggle video loaded
      isVideoLoaded = true;

      // Track video play event
      ga('send', 'event', 'Video', 'Play', 'Hompage Splash Video');
    }
    else {
      $('#IntroductionVideo')[0].play();
    }
  });
  $(document).on('closed.zf.reveal', '[data-reveal]', function () {
    $('#IntroductionVideo')[0].pause();
  });

});