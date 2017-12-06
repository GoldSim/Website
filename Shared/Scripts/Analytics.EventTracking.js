/**
 * (GOLDSIM WEB) GOOGLE ANALYTICS EVENT TRACKING SCRIPTS
 * @file Centralizes the call to Google Analytics' "send" functionality for tracking a "hitType" of "event".
 * @namespace goldSimWeb
 */
function trackEvent(link, category, action, label) {
  'use strict';

  /**
   * Set variable defaults
   */
  if (category == null) category = 'Link';
  if (action == null) action = 'Click';
  if (label == null) label = link.href;

  /**
   * Send the event to Google Analytics
   */
  ga('send', 'event', category, action, label);

  /**
   * Complete link interaction
   */
  setTimeout(function () {
    if (link.target === '_blank') {
      window.open(link.href, '_blank');
    }
    else {
      window.location.href = link.href;
    }
  }, 200);
};