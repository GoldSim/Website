/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

/*==============================================================================================================================
| METHOD: ADD (STYLE SET)
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Provides configuration settings for a custom CKEditor style set.
 */
CKEDITOR.stylesSet.add( 'OnTopicStyleSet', [

  /*----------------------------------------------------------------------------------------------------------------------------
  | FORMAT STYLES
  >-----------------------------------------------------------------------------------------------------------------------------
  | These are normally found in the format dropdown box. By reproducing them here, we can optionally remove the format dropdown.
  \---------------------------------------------------------------------------------------------------------------------------*/
  { name                : 'Heading 2',
    element             : 'h2'
  },
  { name                : 'Heading 3',
    element             : 'h3'
  },
  { name                : 'Body',
    element             : 'p'
  },
  { name: 'Preformatted Text', element: 'pre' }


]);