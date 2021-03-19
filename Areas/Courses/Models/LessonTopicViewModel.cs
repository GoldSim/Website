﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: LESSON TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Lesson</c> topic.
  /// </summary>
  public record LessonTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | UNIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    [AttributeKey("Parent")]
    [Include(AssociationTypes.Parents)]
    public UnitTopicViewModel Unit { get; init; }

  } // Class
} // Namespace