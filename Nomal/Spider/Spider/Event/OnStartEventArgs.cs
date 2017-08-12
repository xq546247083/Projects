using System;

namespace Spider.Event
{
    /// <summary>
    /// 爬虫启动事件
    /// </summary>
    public class OnStartEventArgs
    {
        public Uri Uri { get; set; }

        /// <summary>
        /// 开始事件
        /// </summary>
        /// <param name="uri"></param>
        public OnStartEventArgs(Uri uri)
        {
            this.Uri = uri;
        }
    }
}
