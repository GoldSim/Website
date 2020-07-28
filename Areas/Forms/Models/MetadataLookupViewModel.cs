/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | VIEW MODEL: METADATA LOOKUP
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views related to the <see cref="MetadataLookupViewModel"/>.
  /// </summary>
  public class MetadataLookupViewModel {

    public SelectList Options { get; set; }
    public string DefaultText { get; set; }
    public string Value { get; set; }
    public bool IsRequired { get; set; }

  } // Class
} // Namespace
