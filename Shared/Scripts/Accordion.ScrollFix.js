(function(goldSimWeb, $, undefined) {
  $(".accordion").on("up.zf.accordion", function (event) {
    var accordion = $(this);
    setTimeout(function () {
      $('html,body').animate({ scrollTop: $(accordion).find('.is-active').offset().top }, 'slow');
    },
    250); //Set to slideSpeed
  });
} (window.goldSimWeb = window.goldSimWeb || {}, jQuery));
