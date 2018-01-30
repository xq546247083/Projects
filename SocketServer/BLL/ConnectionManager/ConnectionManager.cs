//***********************************************************************************
// WebSocket管理对象
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer.BLL
{
    using SocketServer.Model;
    using Tool.Common;

    /// <summary>
    /// WebSocket管理对象
    /// </summary>
    public class ConnectionManager
    {
        #region 字段

        /// <summary>
        /// 连接适配器对象
        /// </summary>
        private static Dictionary<String, IConnection> mConnectionData = new Dictionary<String, IConnection>();

        /// <summary>
        /// 锁对象
        /// </summary>
        private static ReaderWriterLockSlim mLockObj = new ReaderWriterLockSlim();

        /// <summary>
        /// 处理任务
        /// </summary>
        private static Task mainTask = null;

        #endregion

        #region 私有方法

        /// <summary>
        /// 处理连接,移除过期连接
        /// </summary>
        private static void HandleConnect()
        {
            while (true)
            {
                Thread.Sleep(1000 * 60 * 5);

                // 循环所有适配器对象，移除过期的项
                mLockObj.EnterWriteLock();
                try
                {
                    foreach (var userID in mConnectionData.Keys.ToList())
                    {
                        if (mConnectionData[userID].CheckIfTimeout())
                        {
                            mConnectionData[userID].UnRegister();
                            mConnectionData.Remove(userID);
                        }
                    }
                }
                finally
                {
                    mLockObj.ExitWriteLock();
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (mainTask != null)
            {
                throw new Exception("不能重复创建处理线程");
            }

            mainTask = Task.Factory.StartNew(HandleConnect);
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="message">数据</param>
        public static void HandleMessage(IConnection connection, byte[] message)
        {
            var result = new ReturnObject() { Code = -1 };

            try
            {
                connection.KeepAlive();
                // 处理获得的数据
                var request = RequestTool.ConverToNameValueCollection(Encoding.UTF8.GetString(message), false, Encoding.UTF8);

                // 获取用户,如果没登录，用户为null
                SysUser sysUser = null;
                if (!String.IsNullOrEmpty(connection.UserID))
                {
                    sysUser = SysUserBLL.GetItem(connection.UserID);
                }

                // 组装上下文
                var context = new Context(request, connection, sysUser);

                // 调用方法返回
                result = MethodManager.Call(context);
            }
            catch (Exception ex)
            {
                Log.Error($"处理数据异常:{ex}");
                result.Message = ex.Message;
            }
            finally
            {
                connection.SendData(result);
            }
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="userID">玩家Id</param>
        /// <returns>连接</returns>
        public static IConnection GetConnection(String userID)
        {
            mLockObj.EnterReadLock();
            try
            {
                if (mConnectionData.ContainsKey(userID))
                {
                    return mConnectionData[userID];
                }
            }
            finally
            {
                mLockObj.ExitReadLock();
            }

            return null;
        }

        /// <summary>
        /// 注册连接
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="userID">玩家Id</param>
        public static void Register(IConnection connection, String userID)
        {
            // 如果之前的登录过，注销用户
            mLockObj.EnterReadLock();
            try
            {
                if (mConnectionData.ContainsKey(userID))
                {
                    mConnectionData[userID].UnRegister();
                }
            }
            finally
            {
                mLockObj.ExitReadLock();
            }

            // 注册连接到登录用户
            mLockObj.EnterWriteLock();
            try
            {
                mConnectionData[userID] = connection;
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }

            // 连接注册其用户
            connection.Register(userID);
        }

        /// <summary>
        /// 注销连接
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="userID">玩家Id</param>
        public static void UnRegister(IConnection connection, String userID)
        {
            // 注销用户
            mLockObj.EnterWriteLock();
            try
            {
                if (mConnectionData.ContainsKey(userID))
                {
                    mConnectionData[userID].UnRegister();
                    mConnectionData.Remove(userID);
                }
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }

        }

        #endregion
    }
}
