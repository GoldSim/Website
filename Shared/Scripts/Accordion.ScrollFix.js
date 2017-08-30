(function (goldSimWeb, $, undefined) {

  var _previousAccordion = null;
  var _previousHeight = 0;
  var _previousTop = window.innerHeight;

  $(".accordion").on("down.zf.accordion", function (event) {
    _previousAccordion = $(this).find(".is-active .accordion-content");
    _previousHeight = _previousAccordion.outerHeight();
    _previousTop = _previousAccordion.offset().top;
  });

  $("li.accordion-item").on("click", function (event) {
    var accordion = $(this);
    var offset = 0;
    if (!accordion.hasClass("is-active")) {
      _previousTop = window.innerHeight;
      return;
    }
    if (_previousTop < accordion.offset().top) {
      offset = _previousHeight;
    }
    $('html,body').animate({ scrollTop: $(accordion).offset().top - offset }, 'fast');
  });
} (window.goldSimWeb = window.goldSimWeb || {}, jQuery));
