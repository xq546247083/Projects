/************************************************************************
* 标题: 角色类
* 描述: 角色类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;

namespace WebServer.BLL
{
    using WebServer.DAL;
    using WebServer.Model;

    /// <summary>
    /// 角色类
    /// </summary>
    public partial class SysRoleBLL : IInit
    {
        #region 属性

        /// <summary>
        /// 类名
        /// </summary>
        private const String mClassName = "SysRoleBLL";

        /// <summary>
        /// 角色数据集合
        /// key:角色id
        /// value:角色对象
        /// </summary>
        private static Dictionary<Int32, SysRole> mData = new Dictionary<Int32, SysRole>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //查询数据
            var dataList = BaseModelDal.ExecuteQuery<SysRole>();
            foreach (var dr in dataList)
            {
                mData[dr.RoleID] = dr;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Int32, SysRole> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个角色
        /// </summary>
        /// <param name="sysRoleId">角色id</param>
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>角色</returns>
        public static SysRole GetItem(Int32 sysRoleId, Boolean ifCastException = false)
        {
            if (GetData().ContainsKey(sysRoleId))
            {
                return mData[sysRoleId];
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("SysRole未找到roleId为{0}的角色", sysRoleId));
            }

            return null;
        }

        /// <summary>
        /// 更新角色数据
        /// </summary>
        /// <param name="sysRole">用户</param>
        /// <returns>用户</returns>
        public static void Update(SysRole sysRole)
        {
            SysRoleDAL.Update(sysRole.RoleID, sysRole.RoleName, sysRole.MenuIDS, sysRole.IsDefault, sysRole.IsSupper, sysRole.Notes);
        }

        /// <summary>
        /// 插入角色数据
        /// </summary>
        /// <param name="sysRole">用户</param>
        /// <returns>用户</returns>
        public static void Insert(SysRole sysRole)
        {
            SysRoleDAL.Insert(sysRole.RoleID, sysRole.RoleName, sysRole.MenuIDS, sysRole.IsDefault, sysRole.IsSupper, sysRole.Notes);
        }

        #endregion

        #region 组装客户端数据

        #endregion
    }
}
