/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>TechnicalPaper</c> topic.
  /// </summary>
  public record TechnicalPaperTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="TechnicalPaperTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public TechnicalPaperTopicViewModel(AttributeDictionary attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Category                  = attributes.GetValue(nameof(Category));
      Authors                   = attributes.GetValue(nameof(Authors));
      Publication               = attributes.GetValue(nameof(Publication));
      PublicationUrl            = attributes.GetUri(nameof(PublicationUrl));
      PublicationDate           = attributes.GetDateTime(nameof(PublicationDate))?? PublicationDate;
      LearnMoreUrl              = attributes.GetUri(nameof(LearnMoreUrl));
      DownloadLabel             = attributes.GetValue(nameof(DownloadLabel));
      Description               = attributes.GetValue(nameof(Description));
    }

    /// <summary>
    ///   Initializes a new <see cref="TechnicalPaperTopicViewModel"/> with no parameters.
    /// </summary>
    public TechnicalPaperTopicViewModel() { }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the topic's <see cref="Key"/> attribute, the primary text identifier for the <see cref="Topic"/>.
    /// </summary>
    public string Key { get; init; }

    /*==========================================================================================================================
    | TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the Title attribute, which represents the friendly name of the topic.
    /// </summary>
    public string Title { get; init; }

    /*==========================================================================================================================
    | CATEGORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the category that the content item should be grouped under.
    /// </summary>
    public string Category { get; init; }

    /*==========================================================================================================================
    | AUTHORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of authors associated with the paper.
    /// </summary>
    public string Authors { get; init; }

    /*==========================================================================================================================
    | PUBLICATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the name of the publication or journal that the technical paper was originally published in.
    /// </summary>
    public string Publication { get; init; }

    /*==========================================================================================================================
    | PUBLICATION (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the URL for the publication or journal that the technical paper was originally published in.
    /// </summary>
    public Uri PublicationUrl { get; init; }

    /*==========================================================================================================================
    | PUBLICATION DATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the date that the technical paper was originally published on.
    /// </summary>
    public DateTime PublicationDate { get; init; }

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional link for the <see cref="AssociatedTopicViewModel"/>.
    /// </summary>
    public Uri LearnMoreUrl { get; init; }

    /*==========================================================================================================================
    | DOWNLOAD (LABEL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional label for the download link.
    /// </summary>
    public string DownloadLabel { get; init; }

    /*==========================================================================================================================
    | DESCRIPTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the description; this is effectively the body.
    /// </summary>
    public string Description { get; init; }

  } // Class
} // Namespace