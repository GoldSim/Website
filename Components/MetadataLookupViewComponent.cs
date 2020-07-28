/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using GoldSim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnTopic;
using OnTopic.Collections;
using OnTopic.Mapping.Annotations;
using OnTopic.Repositories;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: METADATA LOOKUP VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a dropdown list of metadata from an <see
  ///   cref="ITopicRepository"/> based on attributes of a <see cref="ModelExpression"/>.
  /// </summary>
  public class MetadataLookupViewComponent: ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="MetadataLookupViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <remarks>
    ///   While this implementation satisfies GoldSim's current requirements, it has a major design flaw that necessitates it
    ///   be placed within a container with <c>for</c> or <c>asp-for</c>, such as <see cref="HtmlHelperEditorExtensions.
    ///   EditorFor"/>. That's because the view is relying on the <see cref="ViewComponent.ViewData"/> to be populated as part
    ///   of that process.  Ideally, this should be able to fill that information in on its own, but that mandates further
    ///   exploration. Further, because objects in the view aren't being bound against the original property, they're not able
    ///   to correctly wireup any validator attributes. That said, this isn't currently a show stopper as we're already nesting
    ///   the call under an editor template and can compensate for validation on the server.
    /// </remarks>
    /// <returns>A <see cref="MetadataLookupViewComponent"/>.</returns>
    public MetadataLookupViewComponent(ITopicRepository topicRepository) {
      _topicRepository = topicRepository?? throw new ArgumentNullException(nameof(topicRepository));
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a dropdown list of metadata associated with the bound property.
    /// </summary>
    public IViewComponentResult Invoke(
      ModelExpression aspFor,
      string htmlFieldPrefix
    )
    {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### TODO JJC20191119: Ideally, these would be configured as optional parameters. Unfortunately, the tag helper approach
      //to calling view components doesn't (yet) support optional parameters. These should be reevaluated if that's fixed. For
      //now, it's not strictly required by current requirements that these be overwritten by the views.
      var defaultText           = "Select one…";
      var valueField            = nameof(Topic.Title);
      var textField             = nameof(Topic.Title);

      /*------------------------------------------------------------------------------------------------------------------------
      | Get metadata attribute
      \-----------------------------------------------------------------------------------------------------------------------*/
      var modelMetadata         = aspFor.Metadata;
      var currentProperty       = modelMetadata.ContainerType.GetProperty(modelMetadata.Name);
      var propertyAttributes    = currentProperty.GetCustomAttributes(typeof(MetadataAttribute), true);
      var metadataAttribute     = propertyAttributes.FirstOrDefault() as MetadataAttribute;
      var metadataKey           = $"Configuration:Metadata:{metadataAttribute?.Key}:LookupList";

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML field prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      var templateInfo = ViewData.TemplateInfo;
      if (String.IsNullOrEmpty(templateInfo.HtmlFieldPrefix)) {
        templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      }
      if (!templateInfo.HtmlFieldPrefix.EndsWith(modelMetadata.Name, StringComparison.InvariantCulture)) {
        templateInfo.HtmlFieldPrefix = templateInfo.GetFullHtmlFieldName(modelMetadata.Name);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Lookup metadata values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var metadataList          = _topicRepository.Load(metadataKey)?.Children?? new TopicCollection();
      var selectList            = new SelectList(metadataList, valueField, textField, aspFor.Model?.ToString());

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate metadata
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (metadataAttribute == null) {
        throw new InvalidOperationException(
          $"The {aspFor.Metadata.PropertyName} must be decorated with the [Metadata()] attribute."
        );
      }
      else if (metadataList.Count == 0) {
        throw new InvalidOperationException(
          $"The {aspFor.Metadata.PropertyName} is bound to the {metadataAttribute.Key} metadata, but the lookup list " +
          $"contains no topics."
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Create view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new MetadataLookupViewModel() {
        Options = selectList,
        DefaultText = defaultText,
        Value = aspFor.Model?.ToString(),
        IsRequired = modelMetadata.IsRequired
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class

} // Namespace