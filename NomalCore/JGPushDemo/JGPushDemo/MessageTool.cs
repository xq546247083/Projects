//***********************************************************************************
// 文件名称：处理消息发送的助手类.cs
// 功能描述：极光推送
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:26:05
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace JGPushDemo
{
    using JGPushDemo.Model;
    using Moqikaka.Util;
    using Moqikaka.Util.Json;
    using Moqikaka.Util.Log;
    using Moqikaka.Util.OS;
    using Moqikaka.Util.Web;

    /// <summary>
    /// 处理消息发送的助手类
    /// </summary>
    public static class MessageTool
    {
        #region 属性

        /// <summary>
        /// 锁
        /// </summary>
        private static Object mLockObj = new Object();

        /// <summary>
        /// 线程助手对象
        /// </summary>
        private static ThreadUtil mThreadUtilObj = null;

        /// <summary>
        /// 消息队列
        /// </summary>
        private static Queue<MessageObject> mMessageQueue = new Queue<MessageObject>();

        /// <summary>
        /// 当前处理的消息数量
        /// </summary>
        private static Int32 mCurHandleMessageCount = 0;

        /// <summary>
        /// 每分钟处理的消息数量
        /// </summary>
        private static Int32 mMessageCountPerMinute = 0;

        /// <summary>
        /// 保存失败消息的文件夹
        /// </summary>
        private static String mMessageFolder = String.Empty;

        /// <summary>
        /// 内容类型
        /// </summary>
        private static String mContentType = String.Empty;

        #endregion

        #region 私有方法

        /// <summary>
        ///  获取消息现有的数量
        /// </summary>
        private static Int32 GetMessageCount()
        {
            lock (mLockObj)
            {
                return mMessageQueue.Count;
            }
        }

        /// <summary>
        ///  出队消息
        /// </summary>
        private static MessageObject DequeueMessage()
        {
            MessageObject message = null;
            lock (mLockObj)
            {
                if (mMessageQueue.Count > 0)
                {
                    message = mMessageQueue.Dequeue();
                }
            }

            return message;
        }

        /// <summary>
        /// 定时执行的方法(1分钟一次)
        /// </summary>
        private static void HandleMessageCountTask()
        {
            while (true)
            {
                try
                {
                    mCurHandleMessageCount = 0;

                    // 调整线程数
                    var messageCount = GetMessageCount();
                    mThreadUtilObj.DynamicAdjustThread(messageCount);

                    // 获取初始化线程数
                    Int32 initedThreadCount = mThreadUtilObj.InitedThreadCount;
                    LogUtil.Write(String.Format("已初始化的线程数量为{0}，队列中未处理的数量为{1}，平均每个线程需处理数量为{2}", initedThreadCount, mMessageQueue.Count, (initedThreadCount == 0) ? 0 : (mMessageQueue.Count / initedThreadCount)), LogType.Debug, true);

                    if (messageCount > mMessageCountPerMinute)
                    {
                        for (Int32 i = 0; i < messageCount - mMessageCountPerMinute; i++)
                        {
                            var message = DequeueMessage();
                            if (message != null)
                            {
                                SaveMessage(message);
                            }
                        }
                    }
                    else
                    {
                        // 获取未推送的消息列表
                        var fileNameList = FileUtil.GetFileNameList(mMessageFolder);
                        if (fileNameList != null && fileNameList.Length != 0)
                        {
                            // 循环消息不够的数量
                            Int32 j = 0;
                            while (j < fileNameList.Length && j < mMessageCountPerMinute - messageCount)
                            {
                                // 读取文件，并把消息入队到队列
                                String fileName = fileNameList[j];
                                String strMessage = FileUtil.ReadFile(fileName);
                                if (!String.IsNullOrEmpty(strMessage))
                                {
                                    MessageObject inMessage = JsonUtil.Deserialize<MessageObject>(strMessage);
                                    lock (mLockObj)
                                    {
                                        mMessageQueue.Enqueue(inMessage);
                                    }
                                }
                                FileUtil.DeleteFile(fileName);
                                j++;
                            }
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    LogUtil.Write(ex.ToString(), LogType.Error, true);
                }
                finally
                {
                    Thread.Sleep(60000);
                }
            }
        }

        /// <summary>
        /// 消耗消息
        /// </summary>
        private static void ConsumeMessage()
        {
            while (true)
            {
                try
                {
                    lock (mLockObj)
                    {
                        if (mMessageQueue.Count == 0)
                        {
                            Monitor.Wait(mLockObj);
                        }
                    }

                    var message = DequeueMessage();
                    if (message != null)
                    {
                        HandleMessage(message);
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    LogUtil.Write(ex.ToString(), LogType.Error, true);
                }
            }
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        private static void HandleMessage(MessageObject message)
        {
            var messageStr = String.Empty;
            try
            {
                var returnStr = String.Empty;
                Interlocked.Add(ref mCurHandleMessageCount, 1);
                messageStr = JsonUtil.Serialize(message);

                if (message.IsPost)
                {
                    returnStr = WebUtil.PostWebData(message.Url, message.Message, DataCompress.NotCompress, message.Headers, 0, mContentType, "", null, 0);
                }
                else
                {
                    returnStr = WebUtil.GetWebData(message.Url, message.Message, DataCompress.NotCompress, message.Headers, 0, mContentType, "", null);
                }

                if (!String.IsNullOrEmpty(returnStr))
                {
                    LogUtil.Write($"消息工具推送返回消息：{returnStr}", LogType.Debug, true);
                }
            }
            catch (WebException webEx)
            {
                using (WebResponse response = webEx.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    if (response != null)
                    {
                        // 获取返回的消息
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            var responeInfo = reader.ReadToEnd();
                            LogUtil.Write($"消息工具推送失败：message{messageStr},webEx:{webEx},ResponeInfo:{responeInfo}", LogType.Error, true);
                        }
                    }
                    else
                    {
                        LogUtil.Write($"消息工具推送失败：message{messageStr},webEx:{webEx}", LogType.Error, true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Write($"消息工具推送失败：message{messageStr},ex:{ex}", LogType.Error, true);
            }
            finally
            {
                if (!message.IsThrowAway)
                {
                    SaveMessage(message);
                }
            }
        }

        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="message">消息</param>
        private static void SaveMessage(MessageObject message)
        {
            FileUtil.WriteFile(mMessageFolder, Guid.NewGuid().ToString(), false, new String[]
            {
                JsonUtil.Serialize(message)
            });
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="messageFolder">存储消息的目录</param>
        /// <param name="minThreadCount">最小的线程数量[10,100]</param>
        /// <param name="maxThreadCount">最大的线程数量[minThreadCount, 500]</param>
        /// <param name="messageCountPerThread">每个线程平均需要处理的数量</param>
        /// <param name="messageCountPerMinute">每分钟发送的消息数量</param>
        /// <param name="contentType">内容类型</param>
        public static void SetParam(String messageFolder, Int32 minThreadCount, Int32 maxThreadCount, Int32 messageCountPerThread, Int32 messageCountPerMinute, String contentType)
        {
            // 控制推送线程数量
            lock (mLockObj)
            {
                if (mThreadUtilObj != null) { return; }
                mThreadUtilObj = new ThreadUtil(minThreadCount, maxThreadCount, messageCountPerThread, new ThreadStart(ConsumeMessage));

                mCurHandleMessageCount = 0;
                mMessageCountPerMinute = messageCountPerMinute;
                mMessageFolder = messageFolder;
                mContentType = contentType;
            }
            LogUtil.Write(String.Format("MessageFolder={0},MinThreadCount={1},MaxThreadCount={2}", messageFolder, minThreadCount, maxThreadCount), LogType.Debug, true);

            // 开启定时任务
            var t = new Thread(new ThreadStart(HandleMessageCountTask)) { IsBackground = true };
            t.Start();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        public static void SendMessage(MessageObject message)
        {
            if (message == null)
            {
                return;
            }

            lock (mLockObj)
            {
                // 如果消息处理数量大于0每分钟能处理的数量
                if (mCurHandleMessageCount > mMessageCountPerMinute)
                {
                    SaveMessage(message);
                }
                else
                {
                    mMessageQueue.Enqueue(message);
                    Monitor.Pulse(mLockObj);
                }
            }
        }

        /// <summary>
        /// 将队列中尚未发送的数据保存到文件中
        /// </summary>
        public static void SaveMessage()
        {
            LogUtil.Write(String.Format("开始保存消息，数量={0}", mMessageQueue.Count), LogType.Debug, true);

            while (true)
            {
                try
                {
                    lock (mLockObj)
                    {
                        if (mMessageQueue.Count == 0)
                        {
                            break;
                        }
                    }

                    var message = DequeueMessage();
                    if (message != null)
                    {
                        SaveMessage(message);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Write(ex.ToString(), LogType.Error, true);
                }
            }

            LogUtil.Write("保存消息成功.", LogType.Debug, true);
        }

        #endregion
    }
}
