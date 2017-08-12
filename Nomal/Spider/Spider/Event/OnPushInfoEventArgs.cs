using System;

namespace Spider.Event
{
    /// <summary>
    /// 通知主界面消息
    /// </summary>
    public class OnPushInfoEventArgs
    {
        /// <summary>
        /// 消息
        /// 1:放消息
        /// 2：url队列增加
        /// 3：url队列减少
        /// 4：img队列增加
        /// 5：img队列减少
        /// </summary>
        public Int32 Type { get; private set; }

        /// <summary>
        /// 消息
        /// </summary>
        public String Info { get; private set; }

        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="info">消息</param>
        public OnPushInfoEventArgs(Int32 type,String info)
        {
            this.Type = type;
            this.Info = info;
        }
    }
}
