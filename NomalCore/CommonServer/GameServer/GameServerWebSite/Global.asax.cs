/************************************************************************
* 标题: 网站启动页面
* 描述: 网站启动页面
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace GameServerWebSite
{
    using Tool.Common;
    using GameServer.BLL;

    /// <summary>
    /// 网站启动页面
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 服务器启动
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            Log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

            //启动服务器
            Log.Write("服务器开始启动", LogType.Info);
            WorldBLL.Start();
            Log.Write(String.Format("服务器启动成功，ProcessId={0}, ThreadId={1}", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId), LogType.Info);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}