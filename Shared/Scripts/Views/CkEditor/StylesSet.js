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
  >-----------------------------------------------------------------------------------------------------------------------------
  { name                        : 'Heading 1',
    element                     : 'h1'
  },
  { name                        : 'Heading 2',
    element                     : 'h2'
  },
  { name                        : 'Heading 3',
    element                     : 'h3'
  },
  { name                        : 'Body',
    element                     : 'p'
  },
  {
    name                        : 'Preformatted Text',
    element                     : 'pre'
  },
  \---------------------------------------------------------------------------------------------------------------------------*/

  /*----------------------------------------------------------------------------------------------------------------------------
  | CUSTOM INLINE STYLES
  >-----------------------------------------------------------------------------------------------------------------------------
  | Styles that are specific to GoldSim's stylesheet, and frequently used across pages
  \---------------------------------------------------------------------------------------------------------------------------*/
  {
    name                        : 'Figure',
    element                     : 'figure',
    attributes                  : {
      'class'                   : 'illustration full'
    }
  },
  {
    name                        : 'Figure w/out Border',
    element                     : 'figure',
    attributes                  : {
      'class'                   : 'illustration full'
    }
  },
  {
    name                        : 'Help Reference',
    element                     : 'aside',
    attributes                  : {
      'class'                   : 'help'
    }
  },
  {
    name                        : 'Note',
    element                     : 'blockquote',
    attributes                  : {
      'class'                   : 'note'
    }
  },
  {
    name                        : 'Footnote',
    element                     : 'p',
    attributes                  : {
      'class'                   : 'footnote'
    }
  },

  /*----------------------------------------------------------------------------------------------------------------------------
  | CUSTOM OBJECT STYLES
  >-----------------------------------------------------------------------------------------------------------------------------
  | Object styles require that the element be added to the page first. Once that's done, they
  \---------------------------------------------------------------------------------------------------------------------------*/
  {
    name                        : 'Callout Quote',
    element                     : 'blockquote',
    attributes                  : {
      'class'                   : 'pull quote right'
    }
  },
  {
    name                        : 'Button',
    element                     : 'a',
    attributes                  : {
      'class'                   : 'button primary large'
    }
  },
  {
    name                        : 'Callout (Picture)',
    element                     : 'picture',
    attributes                  : {
      'class'                   : 'callout picture'
    }
  },
  {
    name                        : 'Callout (Thumbnail)',
    element                     : 'picture',
    attributes                  : {
      'class'                   : 'callout picture thumbnail small'
    }
  }

]);