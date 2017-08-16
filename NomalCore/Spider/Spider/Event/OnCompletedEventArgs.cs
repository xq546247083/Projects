using System;

namespace Spider.Event
{
    /// <summary>
    /// 爬虫完成事件
    /// </summary>
    public class OnCompletedEventArgs
    {
        /// <summary>
        /// 爬虫URL地址
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// 任务线程ID
        /// </summary>
        public int ThreadId { get; private set; }

        /// <summary>
        /// 页面源代码
        /// </summary>
        public string PageSource { get; private set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Milliseconds { get; private set; }

        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="uri">爬虫URL地址</param>
        /// <param name="threadId">任务线程ID</param>
        /// <param name="milliseconds">页面源代码</param>
        /// <param name="pageSource">时间</param>
        public OnCompletedEventArgs(Uri uri, int threadId, long milliseconds, string pageSource)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
        }
    }
}
