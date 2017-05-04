/************************************************************************
* 标题: 服务启动类
* 描述: 服务启动类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.BLL
{
    using Tool.Log;

    /// <summary>
    /// 初始化整个服务器的数据
    /// </summary>
    public static class WorldBLL
    {
        public static void Start()
        {
            //开启配置
            ConfigBLL.Start();

            //声明并启动定时线程
            Task.Run(() => RunThread());
        }

        /// <summary>
        /// 运行线程
        /// </summary>
        private static void RunThread()
        {
            while (true)
            {
                //获取当前时间的小时和分钟数，以避免在执行代码的过程当中改变
                DateTime dtNow = DateTime.Now;
                Int32 hour = dtNow.Hour;
                Int32 minute = dtNow.Minute;

                //整点执行
                if (minute % 60 == 0)
                {
                    //1点执行：压缩日志等
                    if (hour == 1)
                    {
                        ThreadPool.QueueUserWorkItem(OneAmMethod, dtNow);
                    }
                }

                //休眠到整分数
                Thread.Sleep((60 - DateTime.Now.Second) * 1000);
            }
        }

        /// <summary>
        /// 凌晨1点执行的方法：压缩日志等
        /// </summary>
        /// <param name="state">参数</param>
        private static void OneAmMethod(Object state)
        {
            DateTime dtNow = (DateTime)state;

            //每个月初压缩日志
            if (dtNow.Day == 1)
            {
                Log.ZipLog(dtNow);
            }
        }
    }
}
