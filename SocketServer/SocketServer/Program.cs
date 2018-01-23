/************************************************************************
* 启动
*************************************************************************/
using System;
using System.IO;

namespace SocketServer
{
    using Tool.Common;

    /// <summary>
    /// 启动
    /// </summary>
    class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Init();
            Console.ReadKey();
        }

        /// <summary>
        /// 服务器启动初始化
        /// </summary>
        static void Init()
        {
            try
            {
                // 配置文件检测
                SocketServerConfig.Check();

                // 日志设置
                Log.Set(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"), CommonWebSiteConfig.LogInfoFlag, CommonWebSiteConfig.LogDebugFlag, CommonWebSiteConfig.LogWarnFlag, CommonWebSiteConfig.LogErrorFlag);

                // 设置邮件信息
                EmailTool.SetSenderInfo(SocketServerConfig.EmailHost, SocketServerConfig.EmailAddress, SocketServerConfig.EmailPass);

                //CallbackServerManager.Start();
            }
            catch (Exception ex)
            {
                Log.Error($"服务器初始化失败。错误信息：{ex}");
            }
        }
    }
}
