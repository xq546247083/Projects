/************************************************************************
* 描述:内部Api
*************************************************************************/
using System;
using System.Web.Mvc;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 内部Api
    /// </summary>
    public class InnerApiController : Controller
    {
        #region Api

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns>测试</returns>
        public String Test()
        {
            return "测试成功！";
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// Ifs the ip allowed.
        /// </summary>
        /// <param name="ips">允许的ip列表</param>
        /// <returns></returns>
        private Boolean IfIpAllowed(String[] ips)
        {
#if DEBUG

            return true;

#else
            // 如果为空，直接返回
            if (ips == null || ips.Length == 0)
            {
                return false;
            }

            String ip = Request.UserHostAddress;
            if (!ips.Contains(ip))
            {
                return false;
            }

            return true;
#endif
        }

        #endregion
    }
}