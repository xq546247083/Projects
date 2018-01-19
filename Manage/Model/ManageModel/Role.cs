/************************************************************************
* 描述: 角色
*************************************************************************/
using System;

namespace Manage.Model
{
    using Manage.Common;

    /// <summary>
    /// 角色
    /// </summary>
    [DataBaseTable("role")]
    public sealed class Role
    {
        #region 属性

        /// <summary>
        /// 角色Id
        /// </summary>
        [PrimaryKey]
        public Int32 ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public String RolesName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 角色具有的权限
        /// </summary>
        public String Page { get; set; }

        #endregion
    }
}
