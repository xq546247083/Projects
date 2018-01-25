//***********************************************************************************
// WebSocket管理对象
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
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
                mLockObj.EnterWriteLock();
                try
                {
                    foreach (var userID in mConnectionData.Keys.ToList())
                    {
                        if (mConnectionData[userID].CheckIfTimeout())
                        {
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
        /// 注册连接
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="userID">玩家Id</param>
        public static void Register(IConnection connection, Guid userID)
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
        public static void UnRegister(IConnection connection, Guid userID)
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
