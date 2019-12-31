﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using GoldSim.Web.Models.Forms;

namespace GoldSim.Web.Administration.Models.Licenses {

  /*============================================================================================================================
  | CLASS: LICENSE REQUEST VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering a license request.
  /// </summary>
  public class LicenseRequestTopicViewModel: CoreContact {

    /*==========================================================================================================================
    | ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The topic's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /*==========================================================================================================================
    | LAST MODIFIED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The date the request was submitted.
    /// </summary>
    public DateTime LastModified { get; set; }

  } // Class
} // Namespace