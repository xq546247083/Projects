/************************************************************************
* 标题: sys_role
* 描述: sys_role
* 数据表:sys_role
* 作者：徐敏荣
* 日期：2017/9/7 12:23:43
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

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
        public Guid RoleID { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Notes { set; get; }

        #endregion
    }
}
