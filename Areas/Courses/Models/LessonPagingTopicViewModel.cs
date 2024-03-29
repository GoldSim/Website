﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: LESSON PAGING TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for handling next/back paging buttons on the Lesson pages.
  /// </summary>
  public record LessonPagingTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string Title { get; init; }

    /*==========================================================================================================================
    | PROPERTY: WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string WebPath { get; init; }

    /*==========================================================================================================================
    | LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string Label { get; init; }

    /*==========================================================================================================================
    | MOVE NEXT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    public bool MoveNext { get; init; }

  } // Class
} // Namespace