using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ChatClient
{
    /// <summary>
    /// 消息管理
    /// </summary>
    public static class MsgManager
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static ReaderWriterLockSlim mLockObj = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public static List<Msg> GetCopyData(String userId)
        {
            mLockObj.EnterReadLock();
            try
            {
                return new List<Msg>(SysUserManager.GetCopyData().FirstOrDefault(r => r.UserID == userId).MsgList);
            }
            finally
            {
                mLockObj.ExitReadLock();
            }
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void AddMsg(Msg msg)
        {
            var sysUser = SysUserManager.GetCopyData().FirstOrDefault(r => r.UserID == msg.FromUserID);
            if (sysUser == null)
            {
                return;
            }

            mLockObj.EnterWriteLock();
            try
            {
                sysUser.MsgList.Add(msg);
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }
        }

        /// <summary>
        /// 添加广播消息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void AddBrocastMsg(Msg msg)
        {
            mLockObj.EnterWriteLock();
            try
            {
                SysUserManager.HallUser.MsgList.Add(msg);
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }
        }
    }
}
