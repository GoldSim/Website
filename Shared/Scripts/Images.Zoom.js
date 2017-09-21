/**
 * (GOLDSIM WEB) IMAGES ZOOM SCRIPTS
 * @file Defines functionality and presentation elements needed for zooming in on an image on the GoldSim website.
 * @namespace goldSimWeb
 */
(function (goldSimWeb, $, undefined) {
  'use strict';

  $(document).ready(function() {

    /**
     * Loop through all zoomable items on the page in order to: wrap and append zoom link; add modal element for full-size
     * image.
     */
    $('.zoomable').each(function() {

      // Establish variables
      var
        zoomableItem            = $(this),
        zoomableImage           = $(zoomableItem).find('img').attr('src'),
        imageNameStart          = (zoomableImage.lastIndexOf('/') + 1),
        imageNameEnd            = zoomableImage.lastIndexOf('.'),
        imageName               = zoomableImage.substring(imageNameStart, imageNameEnd),
        imageModal              = '<div class="reveal" id="' + imageName + '" data-reveal data-close-on-click="true"><img src="' + zoomableImage + '" alt="*" /><button class="close-button" data-close aria-label="Close Modal" type="button"><span aria-hidden="true">&times;</span></button></div>',
        wrapper                 = '<div class="zoomable wrapper"></div>';
      if ($(zoomableItem).hasClass('callout')) {
        wrapper                 = '<div class="callout zoomable wrapper"></div>';
      }

      // Add wrapper and button to zoomable item
      $(zoomableItem).wrap(wrapper).after('<button class="js-zoom" data-open="' + imageName + '" data-toggle="' + imageName + '">Zoom In <i class="fa fa-search-plus"></i></button>');

      // Add modal based on zoomable item's image
      $('body').append(imageModal);

    });

    /**
     * Open Foundation Reveal on modal button click
     */
    $('button.js-zoom').click(function () {
      var
        modalToOpen             = $(this).data('open');
        revealModal             = new Foundation.Reveal($('#' + modalToOpen));
        revealModal.open();
    });

  });

}(window.goldSimWeb = window.goldSimWeb || {}, jQuery));
