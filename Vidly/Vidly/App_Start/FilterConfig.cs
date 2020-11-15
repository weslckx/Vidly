using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); //redirects user to filterpage when error
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); // no longer accesible without httpsacces 
        }
    }
}
