using System.Web.Mvc;

namespace GoldSim.Web{
  public class FilterConfig {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
      filters.Add(new ErrorHandler.AiHandleErrorAttribute());
    }
  }
}