/************************************************************************
* 标题: sys_role
* 描述: sys_role
* 数据表:sys_role
* 作者：xiaoqiang
* 日期：2017/9/8 19:11:41
* 版本：V1.0
*************************************************************************/

using System;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// sys_role
    /// </summary>
    [DataBaseTable("sys_role")]
    public class SysRole
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKeyAttribute]
        public Int32 RoleID { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }

        /// <summary>
        /// 菜单id（用,隔开）
        /// </summary>
        public String MenuIDS { set; get; }

        /// <summary>
        /// 是否默认角色
        /// </summary>
        public Boolean IsDefault { set; get; }

        /// <summary>
        /// 是否是超级管理员角色
        /// </summary>
        public Boolean IsSupper { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Notes { set; get; }

        #endregion
    }
}
