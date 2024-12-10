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
  internal class MetadataLookupViewModel {

    internal SelectList Options { get; set; }
    internal string DefaultText { get; set; }
    internal string Value { get; set; }
    internal bool IsRequired { get; set; }

  } // Class
} // Namespace