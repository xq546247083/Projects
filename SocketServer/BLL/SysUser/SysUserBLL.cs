/************************************************************************
*  玩家类
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocketServer.BLL
{
    using SocketServer.Enum;
    using SocketServer.Model;
    using System.Threading;
    using Tool.CustomAttribute;

    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class SysUserBLL : INeedInit
    {
        #region 属性

        /// <summary>
        /// 锁对象
        /// </summary>
        private static ReaderWriterLockSlim mLockObj = new ReaderWriterLockSlim();

        /// <summary>
        /// 玩家数据集合
        /// key:玩家id
        /// value:玩家对象
        /// </summary>
        private static Dictionary<String, SysUser> mData = new Dictionary<String, SysUser>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<String, SysUser> GetDataForCopy()
        {
            return new Dictionary<String, SysUser>(mData);
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns>数据</returns>
        public static List<SysUser> GetOnlineUser()
        {
            return GetDataForCopy().Values.Where(r => r.Status).ToList();
        }

        /// <summary>
        /// 获取某一个玩家
        /// </summary>
        /// <param name="sysUserId">玩家id</param>
        /// <param name="ifCastExeption">是否跑出错误</param>
        /// <returns>玩家</returns>
        public static SysUser GetItem(String sysUserId, Boolean ifCastExeption = true)
        {
            mLockObj.EnterReadLock();
            try
            {
                if (mData.ContainsKey(sysUserId))
                {
                    return mData[sysUserId];
                }
            }
            finally
            {
                mLockObj.ExitReadLock();
            }

            if (ifCastExeption)
            {
                throw new Exception("用户不存在");
            }

            return null;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUser">用户</param>
        /// <returns>添加</returns>
        public static void UpdateUser(SysUser sysUser)
        {
            mLockObj.EnterWriteLock();
            try
            {
                mData[sysUser.UserID] = sysUser;
            }
            finally
            {
                mLockObj.ExitWriteLock();
            }
        }

        #endregion

        #region 推送方法

        /// <summary>
        /// 给所有人推送消息（不包括自己）
        /// </summary>
        /// <param name="sysUser">自己</param>
        /// <param name="clientCmdEnum">消息</param>
        /// <param name="data">数据</param>
        public static void PushToAll(SysUser sysUser, ClientCmdEnum clientCmdEnum, Object data)
        {
            // 推送数据
            var users = GetDataForCopy();
            foreach (var item in users.Values)
            {
                if (item.UserID != sysUser.UserID && item.Status)
                {
                    PushTool.Send(item.UserID, clientCmdEnum, 0, data);
                }
            }
        }

        #endregion

        #region 调用方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="userID">用户名</param>
        /// <param name="nickName">昵称</param>
        [InvokeMethod]
        public static ReturnObject C_Login(Context context, String userID, String nickName)
        {
            var result = new ReturnObject() { Code = -1, Cmd = ClientCmdEnum.Login };

            if (String.IsNullOrEmpty(userID))
            {
                result.Message = "用户ID不能为空";
                return result;
            }

            if (String.IsNullOrEmpty(nickName))
            {
                result.Message = "昵称不能为空";
                return result;
            }

            // 更新用户
            var sysUser = new SysUser { UserID = userID, NickName = nickName, Status = true };
            UpdateUser(sysUser);

            // 登录成功，注册连接
            ConnectionManager.Register(context.Connection, userID);

            // 广播给其他用户自己登录了
            PushToAll(sysUser, ClientCmdEnum.Push_Login, sysUser);

            result.Code = 0;
            result.Message = "登录成功";
            result.Data = GetOnlineUser().Where(r => r.UserID != sysUser.UserID).ToList();
            return result;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="context">上下文</param>
        [InvokeMethod]
        public static ReturnObject C_Logout(Context context)
        {
            var result = new ReturnObject() { Code = -1, Cmd = ClientCmdEnum.Logout };

            // 更新登录状态
            context.SysUser.Status = false;
            ConnectionManager.UnRegister(context.Connection, context.SysUser.UserID);

            // 广播给其他用户自己退出了
            PushToAll(context.SysUser, ClientCmdEnum.Push_Logout, context.SysUser.UserID);

            result.Code = 0;
            result.Message = "退出成功";
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
            var result = new ReturnObject() { Code = -1, Cmd = ClientCmdEnum.Broadcast };

            var resultValue = new Dictionary<String, Object>
            {
                ["FromUserID"] = context.SysUser.UserID,
                ["Message"] = message
            };

            // 广播消息
            PushToAll(context.SysUser, ClientCmdEnum.Push_Broadcast, resultValue);

            result.Code = 0;
            result.Message = "发送成功";
            return result;
        }

        /// <summary>
        /// 私聊消息
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="toUserID">目标用户Id</param>
        /// <param name="message">消息</param>
        [InvokeMethod]
        public static ReturnObject C_Chat(Context context, String toUserID, String message)
        {
            var result = new ReturnObject() { Code = -1, Cmd = ClientCmdEnum.Chat };

            var toUser = GetItem(toUserID, false);
            if (toUser == null)
            {
                result.Message = "目标玩家不存在!";
                return result;
            }

            if (!toUser.Status)
            {
                result.Message = "目标玩家不在线!";
                return result;
            }

            var resultValue = new Dictionary<String, Object>
            {
                ["FromUserID"] = context.SysUser.UserID,
                ["Message"] = message
            };
            PushTool.Send(toUserID, ClientCmdEnum.Push_Chat, 0, resultValue);

            result.Code = 0;
            result.Message = "发送成功";
            return result;
        }

        #endregion
    }
}
