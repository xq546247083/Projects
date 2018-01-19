/************************************************************************
* 标题: 日志工具
*************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Manage.Common
{
    /// <summary>
    /// 日志工具
    /// </summary>
    public static class Log
    {
        #region 属性

        /// <summary>
        /// 是否写Info日志
        /// </summary>
        public static Boolean LogInfoFlag { get; set; }

        /// <summary>
        /// 是否写Debug日志
        /// </summary>
        public static Boolean LogDebugFlag { get; set; }

        /// <summary>
        /// 是否写Warn日志
        /// </summary>
        public static Boolean LogWarnFlag { get; set; }

        /// <summary>
        /// 是否写Error日志
        /// </summary>
        public static Boolean LogErrorFlag { get; set; }

        /// <summary>
        /// 日志地址
        /// </summary>
        public static String LogPath { get; set; }

        /// <summary>
        /// 记录Info日志队列
        /// </summary>
        private static readonly ConcurrentQueue<String> mLogInfoQueue = new ConcurrentQueue<String>();

        /// <summary>
        /// 记录Debug日志队列
        /// </summary>
        private static readonly ConcurrentQueue<String> mLogDebugQueue = new ConcurrentQueue<String>();

        /// <summary>
        /// 记录Warn日志队列
        /// </summary>
        private static readonly ConcurrentQueue<String> mLogWarnQueue = new ConcurrentQueue<String>();

        /// <summary>
        /// 记录Error日志队列
        /// </summary>
        private static readonly ConcurrentQueue<String> mLogErrorQueue = new ConcurrentQueue<String>();

        /// <summary>
        /// 记录Info日志信号量
        /// </summary>
        private static AutoResetEvent mLogInfoResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 记录Debug日志信号量
        /// </summary>
        private static AutoResetEvent mLogDebugResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 记录Warn日志信号量
        /// </summary>
        private static AutoResetEvent mLogWarnResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 记录Error日志信号量
        /// </summary>
        private static AutoResetEvent mLogErrorResetEvent = new AutoResetEvent(true);

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        static Log()
        {
            LogInfoFlag = WebConfig.LogInfoFlag;
            LogDebugFlag = WebConfig.LogDebugFlag;
            LogWarnFlag = WebConfig.LogWarnFlag;
            LogErrorFlag = WebConfig.LogErrorFlag;

            //开始写日志
            RunWriteLog();
        }

        /// <summary>
        /// 开启写日志方法
        /// </summary>
        private static void RunWriteLog()
        {
            //运行写日志队列
            Task.Run(() => RunLogInfoQueue());
            Task.Run(() => RunLogDebugQueue());
            Task.Run(() => RunLogWarnQueue());
            Task.Run(() => RunLogErrorQueue());
        }

        /// <summary>
        /// 运行写info日志
        /// </summary>
        private static void RunLogInfoQueue()
        {
            while (true)
            {
                try
                {
                    String content = null;
                    while (mLogInfoQueue.TryDequeue(out content))
                    {
                        WriteLog(content, LogType.Info);
                    }
                }
                catch
                {
                    // ignored
                }

                //处理完日志，等待1s
                mLogInfoResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 运行写Debug日志
        /// </summary>
        private static void RunLogDebugQueue()
        {
            while (true)
            {
                try
                {
                    String content = null;
                    while (mLogDebugQueue.TryDequeue(out content))
                    {
                        WriteLog(content, LogType.Debug);
                    }
                }
                catch
                {
                    // ignored
                }

                //处理完日志，等待1s
                mLogDebugResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 运行写Warn日志
        /// </summary>
        private static void RunLogWarnQueue()
        {
            while (true)
            {
                try
                {
                    String content = null;
                    while (mLogWarnQueue.TryDequeue(out content))
                    {
                        WriteLog(content, LogType.Warn);
                    }
                }
                catch
                {
                    // ignored
                }

                //处理完日志，等待1s
                mLogWarnResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 运行写Error日志
        /// </summary>
        private static void RunLogErrorQueue()
        {
            while (true)
            {
                try
                {
                    String content = null;
                    while (mLogErrorQueue.TryDequeue(out content))
                    {
                        WriteLog(content, LogType.Error);
                    }
                }
                catch
                {
                    // ignored
                }

                //处理完日志，等待1s
                mLogErrorResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="logType">写日志方式</param>
        private static void WriteLog(String content, LogType logType)
        {
            if (String.IsNullOrEmpty(LogPath))
            {
                throw new ArgumentNullException("LogPath", "未初始化LogPath.");
            }

            DateTime now = DateTime.Now;
            String filePath = String.Format("{0}\\{1}\\{2}", LogPath, now.Year, now.Month < 10 ? String.Format("0{0}", now.Month) : now.Month.ToString());
            String fileName = String.Format("{0}-{1}.{2}.{3}", new object[] { DateTimeTool.GetShortGreenWichTime(now), now.Hour.ToString(), Enum.GetName(typeof(LogType), logType), "txt" });
            try
            {
                content = String.Format("{0}{1} {2}", "#", DateTimeTool.GetGreenWichTime(now), content);
                FileTool.WriteFile(filePath, fileName, true, new String[]
                {
                    content,
                    "---------------------------------------------------------------------"
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="logType">日志类型</param>
        public static void Write(String content, LogType logType)
        {
            //把文件加入日志
            switch (logType)
            {
                case LogType.Info:
                    if (LogInfoFlag)
                    {
                        mLogInfoQueue.Enqueue(content);
                        mLogInfoResetEvent.Set();
                    }
                    break;
                case LogType.Debug:
                    if (LogDebugFlag)
                    {
                        mLogDebugQueue.Enqueue(content);
                        mLogDebugResetEvent.Set();
                    }
                    break;
                case LogType.Warn:
                    if (LogWarnFlag)
                    {
                        mLogWarnQueue.Enqueue(content);
                        mLogWarnResetEvent.Set();
                    }
                    break;
                case LogType.Error:
                    if (LogErrorFlag)
                    {
                        mLogErrorQueue.Enqueue(content);
                        mLogErrorResetEvent.Set();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="param">参数</param>
        public static void Error(String content, params String[] param)
        {
            if (param == null || param.Length <= 0)
            {
                Write(content, LogType.Error);
                return;
            }

            Write(String.Format(content, param), LogType.Error);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="param">参数</param>
        public static void Warn(String content, params String[] param)
        {
            if (param == null || param.Length <= 0)
            {
                Write(content, LogType.Warn);
                return;
            }

            Write(String.Format(content, param), LogType.Warn);
        }

        /// <summary>
        /// 写Debug日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="param">参数</param>
        public static void Debug(String content, params String[] param)
        {
            if (param == null || param.Length <= 0)
            {
                Write(content, LogType.Debug);
                return;
            }

            Write(String.Format(content, param), LogType.Debug);
        }

        /// <summary>
        /// 写消息日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="param">参数</param>
        public static void Info(String content, params String[] param)
        {
            if (param == null || param.Length <= 0)
            {
                Write(content, LogType.Info);
                return;
            }

            Write(String.Format(content, param), LogType.Info);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType : byte
    {
        /// <summary>
        /// Info
        /// </summary>
        Info = 0,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 2,

        /// <summary>
        /// Warn
        /// </summary>
        Warn = 1,

        /// <summary>
        /// Error
        /// </summary>
        Error = 3,
    }
}
