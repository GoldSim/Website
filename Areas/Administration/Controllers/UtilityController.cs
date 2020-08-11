/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.Collections;
using OnTopic.Querying;
using OnTopic.Repositories;

namespace GoldSim.Web.Administration.Controllers {

  /*============================================================================================================================
  | CLASS: REPORT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows GoldSim to report on embedded images and links.
  /// </summary>
  [Authorize]
  [Area("Administration")]
  public class UtilityController: Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IWebHostEnvironment             _hostingEnvironment;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref ="UtilityController"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public UtilityController(
      ITopicRepository topicRepository,
      IWebHostEnvironment hostingEnvironment
    ) {
      _topicRepository          = topicRepository;
      _hostingEnvironment       = hostingEnvironment;
    }


  } // Class
} // Namespace