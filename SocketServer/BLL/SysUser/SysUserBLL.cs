/************************************************************************
*  玩家类
*************************************************************************/
using System;
using System.Collections.Generic;

namespace SocketServer.BLL
{
    using Tool.CustomAttribute;
    using SocketServer.Model;

    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class SysUserBLL
    {
        #region 属性

        /// <summary>
        /// 玩家数据集合
        /// key:玩家id
        /// value:玩家对象
        /// </summary>
        private static Dictionary<Guid, SysUser> mData = new Dictionary<Guid, SysUser>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            var sysUser1 = new SysUser()
            {
                UserID = Guid.NewGuid(),
                UserName = "SysUser1",
                Password = "123456"
            };

            mData[sysUser1.UserID] = sysUser1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Guid, SysUser> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个玩家
        /// </summary>
        /// <param name="sysUserId">玩家id</param>
        /// <returns>玩家</returns>
        public static SysUser GetItem(Guid sysUserId)
        {
            if (GetData().ContainsKey(sysUserId))
            {
                return mData[sysUserId];
            }

            throw new Exception("用户不存在");
        }

        #endregion

        #region 调用方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        [InvokeMethod]
        public static void Login(Context context,String userName,String password)
        {

        }

        #endregion
    }
}
