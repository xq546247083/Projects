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
        /// 读写锁
        /// </summary>
        private static ReaderWriterLockTool readerWriterLockTool = new ReaderWriterLockTool();

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
            using (readerWriterLockTool.GetLock(mClassName, ReaderWriterLockTool.LockTypeEnum.Reader, 0))
            {
                if (GetData().ContainsKey(sysUserId))
                {
                    return mData[sysUserId];
                }
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
        /// <param name="userId">userId</param>
        /// <returns>玩家</returns>
        public static SysUser GetItemByUserName(String userName)
        {
            using (readerWriterLockTool.GetLock(mClassName, ReaderWriterLockTool.LockTypeEnum.Reader, 0))
            {
                return GetData().Values.FirstOrDefault(r => r.UserName == userName);
            }
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

        #endregion

        #region 组装客户端数据

        /// <summary>
        /// 组装客户端数据
        /// </summary>
        /// <param name="SysUser">玩家对象</param>
        /// <returns>客户端数据</returns>
        public static Dictionary<String, Object> AssembleToClient(SysUser SysUser)
        {
            Dictionary<String, Object> clientInfo = new Dictionary<String, Object>();

            clientInfo[PropertyConst.UserName] = SysUser.UserName;
            clientInfo[PropertyConst.FullName] = SysUser.FullName;
            clientInfo[PropertyConst.Sex] = SysUser.Sex;
            clientInfo[PropertyConst.LastLoginTime] = SysUser.LastLoginTime;
            clientInfo[PropertyConst.CreateTime] = SysUser.CreateTime;

            return clientInfo;
        }

        #endregion
    }
}
