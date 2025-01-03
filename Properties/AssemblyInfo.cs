/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| USING DIRECTIVES (GLOBAL)
\-----------------------------------------------------------------------------------------------------------------------------*/
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Mvc;
global using OnTopic.Attributes;
global using OnTopic.Internal.Diagnostics;
global using OnTopic.Mapping.Annotations;
global using OnTopic.Repositories;
global using OnTopic.ViewModels;

/*==============================================================================================================================
| USING DIRECTIVES (LOCAL)
\-----------------------------------------------------------------------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/*==============================================================================================================================
| DEFINE ASSEMBLY ATTRIBUTES
>===============================================================================================================================
| Declare and define attributes used in the compiling of the finished assembly.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: Guid("70A25A53-63AF-42CA-87AB-E8358600E7F7")]

/*==============================================================================================================================
| DEFINE SUPPRESSIONS
>===============================================================================================================================
| Declare suppressions to code analysis rules that should be applied.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: SuppressMessage("Design", "CA1024")]
[assembly: SuppressMessage("Design", "CA1812")]
[assembly: SuppressMessage("Maintainability", "CA1515", Scope = "NamespaceAndDescendants", Target = "~N:GoldSim.Web.Components")]
[assembly: SuppressMessage("Maintainability", "CA1515", Scope = "NamespaceAndDescendants", Target = "~N:GoldSim.Web.Controllers")]
[assembly: SuppressMessage("Maintainability", "CA1515", Scope = "NamespaceAndDescendants", Target = "~N:GoldSim.Web.Models")]
[assembly: SuppressMessage("Maintainability", "CA1515", Scope = "NamespaceAndDescendants", Target = "~N:GoldSim.Web.Areas")]
[assembly: SuppressMessage("Style", "IDE0305", Justification = "Aesthetic preference")]