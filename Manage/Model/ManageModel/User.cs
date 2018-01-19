/************************************************************************
* 描述: 用户
*************************************************************************/
using System;

namespace Manage.Model
{
    using Manage.Common;

    /// <summary>
    /// 用户
    /// </summary>
    [DataBaseTable("user")]
    public sealed class User
    {
        #region 属性

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [PrimaryKey]
        public Int32 UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public String UserPwd { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Crdate { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        public String LastLoginIP { get; set; }

        /// <summary>
        /// 是否超级用户
        /// </summary>
        public Boolean IfSuper { get; set; }

        /// <summary>
        /// 状态(0-正常，-1-锁定)
        /// </summary>
        public Int32 Status { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public Int32 UserRole { get; set; }

        #endregion
    }
}
