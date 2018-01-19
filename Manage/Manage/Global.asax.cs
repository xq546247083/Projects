/************************************************************************
* 描述: 启动
*************************************************************************/
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Manage
{
    using Manage.Common;
    using Manage.DAL;

    /// <summary>
    /// 启动
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动
        /// </summary>
        protected void Application_Start()
        {
            Register();
            InitData();
        }

        /// <summary>
        /// 注册
        /// </summary>
        void Register()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            // 检测配置是否配置
            WebConfig.Check();

            // 构造数据库语句
            SqlFactory.BuildCommond();
        }

        /// <summary>
        /// 结束
        /// </summary>
        protected void Application_End()
        {
        }
    }
}
