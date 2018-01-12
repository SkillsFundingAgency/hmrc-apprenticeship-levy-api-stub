using System.Web;
using System.Web.Mvc;

namespace HMRC.ESFA.Levy.Api.Stub
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
