/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/**
 * BING SEARCH
 * @file Object for retrieving search results from the Bing Search API, and also managing the paging buttons so users can
 * retrieve subsequent records.
 */
;(function(window, document, goldSimWeb, $, undefined) {

  /*============================================================================================================================
  | ESTABLISH VARIABLES
  \---------------------------------------------------------------------------------------------------------------------------*/
  var pluginName                = "bingSearch";
  var defaults                  = {
    apiKey                      : null,
    customConfig                : null,
    queryStringParameter        : 'SearchText',
    previousButton              : 'PreviousPage',
    nextButton                  : 'NextPage',
    searchBox                   : 'SearchResultsSearchQuery'
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
    this._totalResults          = 0;

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

    /**
     * Set base query
     */
    this._searchQuery           = this.getQuerystringValue(this.options.queryStringParameter);
    this._baseApiUrl            = 'https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?q=' +
                                  encodeURIComponent(this._searchQuery) +
                                  '&customconfig=' + this.options.customConfig +
                                  '&responseFilter=Webpages&safesearch=Off';

    /**
     * Locate user interface elements
     */
    this._nextButton            = $('#' + this.options.nextButton);
    this._previousButton        = $('#' + this.options.previousButton);
    this._searchBox             = $('#' + this.options.searchBox);

    /**
    * Pre-populate results page search input value
    */
    if (this._searchBox) {
      this._searchBox.val(this._searchQuery);
    }

    /**
     * Build initial results set
     */
    this.getSearchResults();

    /**
     * Establish click handlers for paging buttons
     */
    if (this._previousButton) {
      this._previousButton.on('click', this.pageResults.bind(this));
    }
    if (this._nextButton) {
      this._nextButton.on('click', this.pageResults.bind(this));
    }

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
  | FUNCTION: GET SEARCH RESULTS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Given the record number to begin at, this function will call into the Bing Search API, and then bind the results to the
    * search results markup.
    */
  Plugin.prototype.getSearchResults = function(offset) {
    offset                      = offset? offset : 0;
    $.ajax({
      url                       : this._baseApiUrl + '&count=10&offset=' + offset + '&textDecorations=true&textFormat=HTML',
      headers                   : { 'Ocp-Apim-Subscription-Key': this.options.apiKey },
      success                   : this.bindSearchResults.bind(this)
    });

  };

  /*============================================================================================================================
  | FUNCTION: BIND SEARCH RESULTS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Given the record number to begin at, this function will call into the Bing Search API, and then bind the results to the
    * search results markup.
    */
  Plugin.prototype.bindSearchResults = function(result, status, xhr) {

    var searchResults           = result.webPages.value;

    // Clear current results
    $(this.element).html('');

    // Render search results
    for (var i                  = 0; i < searchResults.length; i++) {

      var title                 = searchResults[i].name;
      var url                   = searchResults[i].url;
      var displayUrl            = searchResults[i].displayUrl;
      var snippet               = searchResults[i].snippet;

      var searchResult          =
        '<div class="result">' +
        '  <a href="' + url + '" class="title">' + title + '</a><br />' +
        '  <small> ' + displayUrl + '</small>' +
        '  <p>' + snippet + '</p>' +
        '</div>';

      $(this.element).append(searchResult);

    }

    // Make updated estimated matches available to pagination
    this._totalResults          = result.webPages.totalEstimatedMatches;

    // Render pagination
    setTimeout(function () {
      this.setPagination(this._totalResults);
    }.bind(this), 1500);

  };

  /*============================================================================================================================
  | FUNCTION: SET PAGINATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
    * Update the pagination links based on the total estimated results.
    */
  Plugin.prototype.setPagination = function(totalResults) {

    var pageSize                = 10;
    var totalPages              = Math.ceil(totalResults / pageSize);
    var currentPageNumber       = Number(window.location.hash.length ? window.location.hash.substr(5) : 1);

    //Set previous button
    if (this._previousButton) {
      this._previousButton.attr("disabled", currentPageNumber <= 1).data("page", currentPageNumber - 1);
    }

    //Set next button
    if (this._nextButton) {
      this._nextButton.attr("disabled", currentPageNumber >= totalPages).data("page", currentPageNumber + 1);
    }

  };

  /*============================================================================================================================
  | FUNCTION: GET QUERY STRING VALUE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Determine and return the value for the requested querystring parameter
   */
  Plugin.prototype.getQuerystringValue = function(parameter) {
    var cleanParameter          = parameter.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex                   = new RegExp('[\\?&]' + cleanParameter + '=([^&#]*)');
    var results                 = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
  };

  /*============================================================================================================================
  | FUNCTION: PAGE RESULTS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /**
   * Determine and return the value for the requested querystring parameter
   */
  Plugin.prototype.pageResults = function() {

    event.preventDefault();

    var source                  = $(event.currentTarget);
    var pageNumber              = Number(source.data("page") || 1);
    window.location.hash        = "Page" + pageNumber;

    this.getSearchResults((pageNumber-1)*10);

  };

}(window, document, window.goldSimWeb = window.goldSimWeb || {}, jQuery));