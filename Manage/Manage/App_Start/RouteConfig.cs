/************************************************************************
* 描述:路由
*************************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace Manage
{
    /// <summary>
    /// 路由配置
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
