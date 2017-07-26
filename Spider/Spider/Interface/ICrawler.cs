using System;
using System.Threading.Tasks;
using Spider.Event;

namespace Spider.Interface
{
    public interface ICrawler
    {
        /// <summary>
        /// 爬虫启动事件
        /// </summary>
        event EventHandler<OnStartEventArgs> OnStart;

        /// <summary>
        /// 爬虫完成事件
        /// </summary>
        event EventHandler<OnCompletedEventArgs> OnCompleted;

        /// <summary>
        /// 爬虫出错事件
        /// </summary>
        event EventHandler<OnErrorEventArgs> OnError;

        /// <summary>
        /// 异步爬虫
        /// </summary>
        /// <param name="uri">地址</param>
        /// <param name="proxy">代理地址</param>
        /// <returns>信息</returns>
        Task<string> Start(Uri uri, string proxy);
    }
}
