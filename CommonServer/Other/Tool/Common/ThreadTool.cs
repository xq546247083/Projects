/************************************************************************
* 标题: 线程工具
* 描述: 线程工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Tool.Common
{
    using Tool.Log;

    /// <summary>
    /// 用队列控制线程数量
    /// </summary>
    public class ThreadTool
    {
        /// <summary>
        /// 线程队列
        /// </summary>
        private ConcurrentQueue<Thread> mThreadQueue = new ConcurrentQueue<Thread>();

        /// <summary>
        /// 已经初始化的线程数量
        /// </summary>
        public int InitedThreadCount
        {
            get
            {
                return this.mThreadQueue.Count;
            }
        }

        /// <summary>
        /// 最小的线程数量
        /// </summary>
        public int MinThreadCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 最大的线程数量
        /// </summary>
        public int MaxThreadCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 每个线程平均需要处理的数量
        /// </summary>
        public int MessageCountPerThread
        {
            get;
            private set;
        }

        /// <summary>
        /// 供初始化线程的方法
        /// </summary>
        public ThreadStart ThreadStartFunc
        {
            get;
            private set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="minThreadCount">最小的线程数量[10,100]</param>
        /// <param name="maxThreadCount">最大的线程数量[minThreadCount, 500]</param>
        /// <param name="messageCountPerThread">每个线程平均需要处理的数量[10,100]</param>
        /// <param name="threadStartFunc">FuncForThread</param>
        public ThreadTool(int minThreadCount, int maxThreadCount, int messageCountPerThread, ThreadStart threadStartFunc)
        {
            if (minThreadCount < 1)
            {
                throw new Exception("minThreadCount不能小于1");
            }

            if (minThreadCount > 100)
            {
                throw new Exception("minThreadCount不能大于100");
            }

            if (maxThreadCount < minThreadCount)
            {
                throw new Exception(String.Format("maxThreadCount:{0}比minThreadCount：{1}小", maxThreadCount, minThreadCount));
            }

            if (maxThreadCount > 500)
            {
                throw new Exception("maxThreadCount不能大于500");
            }

            if (messageCountPerThread < 10)
            {
                throw new Exception("messageCountPerThread不能小于10");
            }

            if (messageCountPerThread > 100)
            {
                throw new Exception("messageCountPerThread不能大于100");
            }

            if (threadStartFunc == null)
            {
                throw new Exception("threadStartFunc不能为空");
            }

            this.MinThreadCount = minThreadCount;
            this.MaxThreadCount = maxThreadCount;
            this.MessageCountPerThread = messageCountPerThread;
            this.ThreadStartFunc = threadStartFunc;
            this.InitNewThread(this.MinThreadCount);
        }

        /// <summary>
        /// 初始化新线程
        /// </summary>
        /// <param name="count">线程数量，大于0表示增加，小于0表示删除</param>
        private void InitNewThread(int count)
        {
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (this.mThreadQueue.Count >= this.MaxThreadCount)
                    {
                        return;
                    }

                    Thread thread = new Thread(this.ThreadStartFunc)
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    this.mThreadQueue.Enqueue(thread);
                }
                return;
            }

            count *= -1;
            for (int j = 0; j < count; j++)
            {
                if (this.mThreadQueue.Count <= this.MinThreadCount)
                {
                    return;
                }
                Thread thread2 = null;
                if (this.mThreadQueue.TryDequeue(out thread2))
                {
                    try
                    {
                        thread2.Abort();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 动态调整线程数量
        /// </summary>
        /// <param name="queueCount">队列中数据的数量</param>
        public void DynamicAdjustThread(int queueCount)
        {
            try
            {
                int num = queueCount / this.MessageCountPerThread - this.mThreadQueue.Count;
                if (num != 0)
                {
                    this.InitNewThread(num);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Log.Write((ex.StackTrace == null) ? ex.Message : (ex.StackTrace + System.Environment.NewLine + ex.Message), LogType.Error);
            }
        }
    }
}
