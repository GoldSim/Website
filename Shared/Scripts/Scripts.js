function trackEvent(e,o,n,i){"use strict";null==o&&(o="Link"),null==n&&(n="Click"),null==i&&(i=e.href),ga("send","event",o,n,i),setTimeout(function(){"_blank"===e.target?window.open(e.href,"_blank"):window.location.href=e.href},200)}!function(e,o,n){"use strict";o(document).ready(function(){var e=o(window),n=o("header.site.header"),i=(e.width(),o("#PrimaryNavigation").length?o("#PrimaryNavigation").height():0),t=e.height()-n.height()-i;o(".js-full-height").each(function(){var n=o(this);e.width()>768&&n.outerHeight(t)}),o("</section>").prependTo(o(".panel.body section.panel.accordion"))})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";o(document).ready(function(){o(window).on("scroll.callsToAction",function(){o(this).scrollTop()>25&&(o("#CallsToAction").removeClass("off-screen").addClass("on-screen"),o(window).off("scroll.callsToAction"))})});var i=document.getElementById("PrimaryNavigation"),t=o(window).width(),a=o("#SiteHeader").outerHeight();t<1024&&(i=document.getElementById("SiteHeader"),a=0);new Headroom(i,{offset:a,tolerance:{up:5,down:0},classes:{initial:"animated",pinned:"animated-pinned",unpinned:"animated-unpinned"}})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";function i(e){e=e.replace(/[\[]/,"\\[").replace(/[\]]/,"\\]");var o=new RegExp("[\\?&]"+e+"=([^&#]*)").exec(location.search);return null===o?"":decodeURIComponent(o[1].replace(/\+/g," "))}o(document).ready(function(){var e=!1,n=i("SearchText");!1===e&&n.length&&(ga("send","event","Site Search","Search",n),e=!0),o(".search.form .buttons button").mousedown(function(e){o("div.search.form").hasClass("closed")?(e.preventDefault(),o("div.search.form").addClass("open").removeClass("closed"),o('.search.form input[type="search"]').focus()):o("div.search.form").addClass("closed").removeClass("open")}),o(".search.form").on("blur",'input[type="search"]',function(e){o("div.search.form").addClass("closed").removeClass("open")})})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";o(document).ready(function(){o(".is-dropdown-submenu-parent > a").on("click touchend",function(e){var n=o(this).attr("href");window.location=n}),o("#PageNavigationSmallScreen select").change(function(){var e=o(this).val();window.location.href=e})})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";o(document).ready(function(){o.each({pdf:"file-pdf-o",exe:"download",zip:"file-archive-o",doc:"file-word-o",docx:"file-word-o",ppt:"file-powerpoint-o",pptx:"file-powerpoint-o",xls:"file-excel-o",xlsx:"file-excel-o"},function(e,n){o('a[href $=".'+e+'"]').not('[class*="button"]').after('<i class="fa fa-'+n+'" aria-hidden="true"></i>'),o('a[href $=".'+e+'"][class*="button"]').append('<i class="fa fa-'+n+'" aria-hidden="true"></i>')})})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";o(document).ready(function(){o(".zoomable").each(function(){var e=o(this),n=o(e).find("img").attr("src"),i=n.lastIndexOf("/")+1,t=n.lastIndexOf("."),a=n.substring(i,t),d='<div class="reveal" id="'+a+'" data-reveal data-close-on-click="true"><img src="'+n+'" alt="*" /><button class="close-button" data-close aria-label="Close Modal" type="button"><span aria-hidden="true">&times;</span></button></div>',c='<div class="zoomable wrapper"></div>';o(e).hasClass("callout")&&(c='<div class="callout zoomable wrapper"></div>'),o(e).wrap(c).after('<button class="js-zoom" data-open="'+a+'">Zoom In <i class="fa fa-search-plus"></i></button>'),o("body").append(d),o("#"+a).foundation()}),o("button.js-zoom").each(function(){o(this).foundation()})})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){var i=null,t=null,a=0,d=window.innerHeight;o(".accordion").on("down.zf.accordion",function(e){i=o(this),t=o(this).find(".is-active .accordion-content"),a=t.innerHeight(),d=t.offset().top}),o("li.accordion-item").on("click",function(e){var n=o(this),t=o(n).offset().top,c=0;n.hasClass("is-active")?(i&&d<t&&i.has(n).length&&(c=a),o("html,body").animate({scrollTop:t-c},"fast"),setTimeout(function(){o("html,body").animate({scrollTop:t-c},"fast")},200)):d=window.innerHeight})}(window.goldSimWeb=window.goldSimWeb||{},jQuery),function(e,o,n){"use strict";function i(){o(".fixed-column.table-wrapper table:not(.cloned)"),o(window).width()}o(document).ready(function(){i(),o(window).resize(function(){i()})})}(window.goldSimWeb=window.goldSimWeb||{},jQuery);