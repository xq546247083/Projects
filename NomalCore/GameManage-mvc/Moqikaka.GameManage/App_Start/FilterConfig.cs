/************************************************************************
* 描述:过滤
*************************************************************************/
using System.Web.Mvc;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 过滤
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="filters">过滤条件</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
