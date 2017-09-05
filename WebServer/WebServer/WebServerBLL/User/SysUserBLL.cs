/************************************************************************
* 标题: 玩家类
* 描述: 玩家类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Tool.Extension;

namespace WebServer.BLL
{
    using WebServer.DAL;
    using WebServer.Model;
    using Tool.Common;

    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class SysUserBLL : IInit
    {
        #region 属性

        /// <summary>
        /// 类名
        /// </summary>
        private const String mClassName = "SysUserBLL";

        /// <summary>
        /// 玩家数据集合
        /// key:玩家id
        /// value:玩家对象
        /// </summary>
        private static Dictionary<Guid, SysUser> mData = new Dictionary<Guid, SysUser>();

        /// <summary>
        /// 玩家数据集合
        /// key:邮箱地址
        /// value:验证码,上次发送验证码时间
        /// </summary>
        private static Dictionary<String, Tuple<String, DateTime>> mEmailData = new Dictionary<String, Tuple<String, DateTime>>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //赋值
            var dataTemp = new Dictionary<Guid, SysUser>();

            //查询数据
            var dataList = BaseModelDal.ExecuteQuery<SysUser>();
            foreach (var dr in dataList)
            {
                dataTemp[dr.UserID] = dr;
            }

            mData = dataTemp;
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
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>玩家</returns>
        public static SysUser GetItem(Guid sysUserId, Boolean ifCastException = false)
        {
            if (GetData().ContainsKey(sysUserId))
            {
                return mData[sysUserId];
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("SysUser未找到id为{0}的玩家", sysUserId));
            }

            return null;
        }

        /// <summary>
        /// 获取某一个玩家
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>玩家</returns>
        public static SysUser GetItemByUserName(String userName)
        {
            var userInfo = GetData().Values.FirstOrDefault(r => r.UserName == userName);

            //todo xiqoaing 这里要检测用户信息是否存在，如果不存在，用数据库获取用户信息，这里要好好思考下咋获取
            return userInfo;
        }

        /// <summary>
        /// 获取某一个玩家,通过邮箱
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>玩家</returns>
        public static SysUser GetItemByEmail(String email)
        {
            return GetData().Values.FirstOrDefault(r => r.Email == email);
        }

        /// <summary>
        /// 获取某一个玩家,通过邮箱或者用户名
        /// </summary>
        /// <param name="userNameOrEmail">email</param>
        /// <returns>玩家</returns>
        public static SysUser GetItemByUserNameOrEmail(String userNameOrEmail)
        {
            SysUser sysUser = null;
            if (userNameOrEmail.IsValidEmail())
            {
                sysUser = GetItemByEmail(userNameOrEmail);
            }
            else
            {
                sysUser = GetItemByUserName(userNameOrEmail);
            }

            return sysUser;
        }

        /// <summary>
        /// 获取某一个玩家,通过邮箱
        /// </summary>
        /// <param name="userNameOrEmail">email</param>
        /// <returns>玩家</returns>
        public static void UpdatePwdExpiredTime(String userNameOrEmail)
        {
            SysUser sysUser = null;
            if (userNameOrEmail.IsValidEmail())
            {
                sysUser = GetItemByEmail(userNameOrEmail);
            }
            else
            {
                sysUser = GetItemByUserName(userNameOrEmail);
            }

            if (sysUser != null)
            {
                sysUser.PwdExpiredTime = DateTime.Now.AddHours(WebConfig.PwdExpiredTime);

                Update(sysUser);
            }
        }

        /// <summary>
        /// 检测玩家是否过期
        /// </summary>
        /// <param name="userNameOrEmail">email</param>
        /// <returns>玩家</returns>
        public static Boolean CheckPwdExpiredTime(String userNameOrEmail)
        {
            SysUser sysUser = null;
            if (userNameOrEmail.IsValidEmail())
            {
                sysUser = GetItemByEmail(userNameOrEmail);
            }
            else
            {
                sysUser = GetItemByUserName(userNameOrEmail);
            }

            return sysUser.PwdExpiredTime < DateTime.Now;
        }



        /// <summary>
        /// 更新玩家数据
        /// </summary>
        /// <param name="sysUser">用户</param>
        /// <returns>用户</returns>
        public static void Update(SysUser sysUser)
        {
            SysUserDAL.Update(sysUser.UserID, sysUser.UserName, sysUser.FullName, sysUser.Password, sysUser.PwdExpiredTime, sysUser.Sex, sysUser.Phone, sysUser.Email, sysUser.Status, sysUser.LoginCount, sysUser.LastLoginTime, sysUser.LastLoginIP, sysUser.RoleIDs, sysUser.CreateTime);
        }

        /// <summary>
        /// 插入玩家数据
        /// </summary>
        /// <param name="sysUser">用户</param>
        /// <returns>用户</returns>
        public static void Insert(SysUser sysUser)
        {
            SysUserDAL.Insert(sysUser.UserID, sysUser.UserName, sysUser.FullName, sysUser.Password, sysUser.PwdExpiredTime, sysUser.Sex, sysUser.Phone, sysUser.Email, sysUser.Status, sysUser.LoginCount, sysUser.LastLoginTime, sysUser.LastLoginIP, sysUser.RoleIDs, sysUser.CreateTime);

            //更新内存
            var data = GetData();
            data[sysUser.UserID] = sysUser;
        }

        #endregion

        #region 组装客户端数据

        /// <summary>
        /// 组装客户端数据
        /// </summary>
        /// <param name="sysUser">玩家对象</param>
        /// <returns>客户端数据</returns>
        public static Dictionary<String, Object> AssembleToClient(SysUser sysUser)
        {
            Dictionary<String, Object> clientInfo = new Dictionary<String, Object>();

            clientInfo[PropertyConst.UserName] = sysUser.UserName;
            clientInfo[PropertyConst.FullName] = sysUser.FullName;
            clientInfo[PropertyConst.Sex] = sysUser.Sex;
            clientInfo[PropertyConst.Phone] = sysUser.Phone;
            clientInfo[PropertyConst.Email] = sysUser.Email;
            clientInfo[PropertyConst.LastLoginTime] = sysUser.LastLoginTime;
            clientInfo[PropertyConst.LastLoginIP] = sysUser.LastLoginIP;
            clientInfo[PropertyConst.LoginCount] = sysUser.LoginCount;
            clientInfo[PropertyConst.Status] = sysUser.Status;
            clientInfo[PropertyConst.CreateTime] = sysUser.CreateTime;
            clientInfo[PropertyConst.PwdExpiredTime] = DateTimeTool.GetUnixTime(sysUser.PwdExpiredTime);

            return clientInfo;
        }

        #endregion
    }
}
