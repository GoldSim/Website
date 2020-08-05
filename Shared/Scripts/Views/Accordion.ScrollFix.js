/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * ACCORDION: SCROLL FIX
 * @file By default, when the accordion expands, it doesn't scroll the page. This can cause problem with longer accordion
 * content, however, since any previous panels will collapse. As a result, the top of the panel that was just opened may not be
 * visible—and, in fact, the entire panel may not even be visible in some extreme scenarios. To mitigate this, this script will
 * automatically scroll to the top of the panel that was just opened. It does this by projecting where the top of the panel will
 * be so that it can animate to that location in parallel to the accordion animation for toggling panel visibility. This is
 * preferred to doing this sequentially, which would otherwise have the effect of slowing down the transition, or resulting in
 * an abrupt jump at the end.
 */
;(function(window, document, goldSimWeb, $, undefined) {

  /*============================================================================================================================
  | JQUERY: WIRE UP ACTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(document).ready(function() {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Establish variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    var _previousAccordion      = null;
    var _previousAccordionItem  = null;
    var _previousHeight         = 0;
    var _previousTop            = window.innerHeight;

    /*--------------------------------------------------------------------------------------------------------------------------
    | Initialize variables on open
    \-------------------------------------------------------------------------------------------------------------------------*/
    $(".accordion").on("down.zf.accordion", function (event) {
      _previousAccordion        = $(this);
      _previousAccordionItem    = $(this).find(".is-active .accordion-content");
      _previousHeight           = _previousAccordionItem.innerHeight();
      _previousTop              = _previousAccordionItem.offset().top;
    });

    /*--------------------------------------------------------------------------------------------------------------------------
    | Scroll to the projected top of the panel
    \-------------------------------------------------------------------------------------------------------------------------*/
    $("li.accordion-item").on("click", function (event) {
      var accordionItem         = $(this);
      var top                   = $(accordionItem).offset().top;
      var offset                = 0;
      if (!accordionItem.hasClass("is-active")) {
        _previousTop            = window.innerHeight;
        return;
      }
      if (_previousAccordion && _previousTop < top && _previousAccordion.has(accordionItem).length) {
        offset                  = _previousHeight;
      }
      $('html,body').animate({ scrollTop: top - offset }, 'fast');
      setTimeout(
        function () {
          $('html,body').animate({ scrollTop: top - offset }, 'fast');
        },
        200
      );
    });

  });

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));