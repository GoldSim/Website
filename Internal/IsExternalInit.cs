/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CheckNamespace
namespace System.Runtime.CompilerServices {

  /*============================================================================================================================
  | CLASS: IS EXTERNAL INIT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   The <see cref="IsExternalInit"/> class is made available as part of the .NET 5.0 CLR in order to enable init accessors.
  ///   As this is not available in .NET Standard, however, we must maintain this separate copy until we migrate to .NET 5.0.
  /// </summary>
  [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is instantiated by the framework.")]
  internal static class IsExternalInit;

} //Namespace