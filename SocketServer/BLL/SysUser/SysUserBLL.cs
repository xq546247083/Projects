/************************************************************************
*  玩家类
*************************************************************************/
using System;
using System.Collections.Generic;

namespace SocketServer.BLL
{
    using SocketServer.Model;
    using Tool.CustomAttribute;

    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class SysUserBLL : INeedInit
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
                UserID = Guid.Parse("c3c3825c-b479-4b42-88db-352bab1b4381"),
                UserName = "SysUser1",
                Password = "123456"
            };

            var sysUser2 = new SysUser()
            {
                UserID = Guid.Parse("c3c3825c-b479-4b42-88db-352bab1b4382"),
                UserName = "SysUser2",
                Password = "123456"
            };

            var sysUser3 = new SysUser()
            {
                UserID = Guid.Parse("c3c3825c-b479-4b42-88db-352bab1b4383"),
                UserName = "SysUser3",
                Password = "123456"
            };

            mData[sysUser1.UserID] = sysUser1;
            mData[sysUser2.UserID] = sysUser2;
            mData[sysUser3.UserID] = sysUser3;
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
        /// <param name="ifCastExeption">是否跑出错误</param>
        /// <returns>玩家</returns>
        public static SysUser GetItem(Guid sysUserId, Boolean ifCastExeption = true)
        {
            if (GetData().ContainsKey(sysUserId))
            {
                return mData[sysUserId];
            }

            if (ifCastExeption)
            {
                throw new Exception("用户不存在");
            }

            return null;
        }

        #endregion

        #region 调用方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="userID">用户名</param>
        /// <param name="password">密码</param>
        [InvokeMethod]
        public static ReturnObject C_Login(Context context, Guid userID, String password)
        {
            var result = new ReturnObject() { Code = -1 };

            var sysUser = GetItem(userID, false);
            if (sysUser == null)
            {
                result.Message = "用户不存在";
                return result;
            }

            if (sysUser.Password != password)
            {
                result.Message = "密码不正确";
                return result;
            }

            // 登录成功，注册连接
            ConnectionManager.Register(context.Connection, userID);

            result.Code = 0;
            result.Message = "登录成功";
            return result;
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="message">消息</param>
        [InvokeMethod]
        public static ReturnObject C_Broadcast(Context context, String message)
        {
            var result = new ReturnObject() { Code = -1 };

            // 循环用户广播
            var users = GetData();
            foreach (var item in users.Values)
            {
                PushTool.Send(item.UserID, message);
            }

            result.Code = 0;
            result.Message = "广播成功";
            return result;
        }

        #endregion
    }
}
