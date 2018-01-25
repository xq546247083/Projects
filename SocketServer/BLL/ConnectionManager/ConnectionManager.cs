//***********************************************************************************
// WebSocket管理对象
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer.BLL
{
    using SocketServer.Model;

    /// <summary>
    /// WebSocket管理对象
    /// </summary>
    public class ConnectionManager
    {
        #region 字段

        /// <summary>
        /// 连接适配器对象
        /// </summary>
        private static Dictionary<Guid, IConnection> mConnectionData = new Dictionary<Guid, IConnection>();

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
                try
                {
                    mLockObj.EnterWriteLock();

                    foreach (var item in mConnectionData)
                    {
                        if (item.Value.CheckIfTimeout())
                        {
                            mConnectionData.Remove(item.Key);
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
        /// 获取连接
        /// </summary>
        /// <param name="userID">玩家Id</param>
        /// <returns>连接</returns>
        public static IConnection GetConnection(Guid userID)
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
        /// 增加连接
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="userID">玩家Id</param>
        public static void Register(IConnection connection, Guid userID)
        {
            // 注册连接
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

        #endregion
    }
}
