using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace SoftAutoUpdate
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class CommonUtil
    {
        /// <summary>
        /// 获取配置文件的值(如果不存在，返回空字符串)
        /// </summary>
        /// <param name="keyStr">key</param>
        /// <returns></returns>
        public static string GetConfigValue(String keyStr)
        {
            var result = String.Empty;

            try
            {
                result = ConfigurationManager.AppSettings[keyStr];
            }
            catch
            {
                // 一般要记录错误日志
            }

            return result;
        }

        /// <summary>
        /// 是否存在进程
        /// </summary>
        public static Boolean ExistProcess(String processValue)
        {
            //获取所有进程
            var allProcess = Process.GetProcesses();
            foreach (var p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == processValue.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 杀掉进程
        /// </summary>
        public static void KillProcess(String processValue)
        {
            //获取所有进程
            var allProcess = Process.GetProcesses();
            foreach (var p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == processValue.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++) p.Threads[i].Dispose();
                    p.Kill();
                }
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <returns>是否创建成果</returns>
        public static Boolean CreateDir(FileInfo fileInfo)
        {
            try
            {
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
            }
            catch
            {
                // 一般要记录错误日志
                return false;
            }

            return true;
        }
    }
}
