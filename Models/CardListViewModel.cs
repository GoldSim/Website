/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | CLASS: CARD LIST VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering a list of card objects.
  /// </summary>
  public class CardListViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     IEnumerable<ICardViewModel>     _cards                          = null;
    private                     string                          _className                      = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Card List View Model with appropriate dependencies.
    /// </summary>
    /// <returns>A card list view model.</returns>
    public CardListViewModel(IEnumerable<ICardViewModel> cards, string className="") {
      _cards                    = cards;
      _className                = className;
    }

    /*==========================================================================================================================
    | CARDS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of cards to be rendered as part of the card list.
    /// </summary>
    /// <returns>A <see cref="List{T}"/> of <see cref="Ignia.Topics.Topic"/>, each representing a unique card.</returns>
    public IEnumerable<ICardViewModel> Cards {
      get {
        return _cards;
      }
    }

    /*==========================================================================================================================
    | CLASS NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the (optional) CSS class to be associated with each card.
    /// </summary>
    /// <returns>A CSS class name.</returns>
    public string ClassName {
      get {
        return _className;
      }
    }

  } // Class

} // Namespace
