/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | VIEW MODEL: METADATA LOOKUP
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views related to the <see cref="MetadataLookupViewModel"/>.
  /// </summary>
  public class MetadataLookupViewModel {

    public SelectList Options { get; set; }
    public string DefaultText { get; set; }

  } // Class
} // Namespace
