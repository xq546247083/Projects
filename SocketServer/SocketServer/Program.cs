/************************************************************************
* 启动
*************************************************************************/
using System;
using System.IO;
using System.Threading;

namespace SocketServer
{
    using CallbackServer;
    using Tool.Common;
    using WebSocketServer;

    /// <summary>
    /// 启动
    /// </summary>
    class Program
    {
        #region 属性

        /// <summary>
        /// 停止处理信号量
        /// </summary>
        private static Semaphore mStopSemaphore = new Semaphore(0, 1);

        #endregion

        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 初始化成功则开启服务器！
            if (Init())
            {
                StartServer();
            }

            mStopSemaphore.WaitOne();
        }

        #region 方法

        /// <summary>
        /// 服务器初始化
        /// </summary>
        static Boolean Init()
        {
            try
            {
                // 配置文件检测
                SocketServerConfig.Check();

                // 日志设置
                Log.Set(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"), SocketServerConfig.LogInfoFlag, SocketServerConfig.LogDebugFlag, SocketServerConfig.LogWarnFlag, SocketServerConfig.LogErrorFlag);

                // 设置邮件信息
                EmailTool.SetSenderInfo(SocketServerConfig.EmailHost, SocketServerConfig.EmailAddress, SocketServerConfig.EmailPass);

                Console.WriteLine("服务器初始化成功！");
                Log.Info("服务器初始化成功！");

                return true;
            }
            catch (Exception ex)
            {
                var exStr = $"服务器初始化失败。错误信息：{ex}";
                Console.WriteLine(exStr);
                Log.Error(exStr);

                return false;
            }
        }

        /// <summary>
        /// 开启服务器
        /// </summary>
        static void StartServer()
        {
            try
            {
                // 连接管理器初始化
                ConnectionManager.Init();

                // 启动websocket服务器
                WebSocketServer.Start(SocketServerConfig.WebSocketServerUrl);

                // 启动回调服务器
                CallbackServer.Start(SocketServerConfig.CallbackServerUrl);

                Console.WriteLine("服务器启动成功！");
                Log.Info("服务器启动成功！");
            }
            catch (Exception ex)
            {
                var exStr = $"服务器启动失败。错误信息：{ex}";
                Console.WriteLine(exStr);
                Log.Error(exStr);
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 程序出现异常退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(((Exception)e.ExceptionObject).ToString());
            mStopSemaphore.Release();
        }

        #endregion
    }
}
