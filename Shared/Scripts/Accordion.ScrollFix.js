(function (goldSimWeb, $, undefined) {

  var _previousAccordion = null;
  var _previousAccordionItem = null;
  var _previousHeight = 0;
  var _previousTop = window.innerHeight;

  $(".accordion").on("down.zf.accordion", function (event) {
    _previousAccordion = $(this);
    _previousAccordionItem = $(this).find(".is-active .accordion-content");
    _previousHeight = _previousAccordionItem.outerHeight();
    _previousTop = _previousAccordionItem.offset().top;
  });

  $("li.accordion-item").on("click", function (event) {
    var accordionItem = $(this);
    var offset = 0;
    if (!accordionItem.hasClass("is-active")) {
      _previousTop = window.innerHeight;
      return;
    }
    if (_previousTop < accordionItem.offset().top && _previousAccordion && _previousAccordion.has(accordionItem).length) {
      offset = _previousHeight;
    }
    $('html,body').animate({ scrollTop: $(accordionItem).offset().top - offset }, 'fast');
  });
} (window.goldSimWeb = window.goldSimWeb || {}, jQuery));
