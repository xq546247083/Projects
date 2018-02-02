using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ChatClient
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public static class SysUserManager
    {
        /// <summary>
        /// 大厅用户
        /// </summary>
        public static SysUser HallUser = new SysUser() { UserID = "1c076550-f3d2-413a-a4b0-078f587be4db", NickName = "大厅" };

        /// <summary>
        /// 用户列表
        /// </summary>
        private static List<SysUser> data = new List<SysUser>() { HallUser };

        /// <summary>
        /// 锁对象
        /// </summary>
        private static ReaderWriterLockSlim mLockObj = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public static List<SysUser> GetCopyData()
        {
            mLockObj.EnterReadLock();
            try
            {
                return new List<SysUser>(data);
            }
            finally
            {
                mLockObj.ExitReadLock();
            }
        }

        /// <summary>
        /// 添加或者更新用户
        /// </summary>
        /// <param name="sysUserList">用户列表</param>
        public static void AddOrUpdateUsers(List<SysUser> sysUserList)
        {
            mLockObj.EnterWriteLock();
            try
            {
                foreach (var sysUser in sysUserList)
                {
                    // 处理颜色
                    sysUser.Color = sysUser.Status ? "LightBlue" : "Gray";

                    // 更新数据
                    var sysUserTemp = data.FirstOrDefault(r => r.UserID == sysUser.UserID);
                    if (sysUserTemp == null)
                    {
                        data.Add(sysUser);
                    }
                    else
                    {
                        sysUserTemp.Copy(sysUser);
                    }
                }
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }
        }
    }
}
