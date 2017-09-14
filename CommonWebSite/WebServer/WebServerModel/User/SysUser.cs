/************************************************************************
* 标题: 系统用户表
* 描述: 系统用户表
* 数据表:sys_user
* 作者：徐敏荣
* 日期：2017/8/20 11:43:55
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 系统用户表
    /// </summary>
    [DataBaseTable("sys_user")]
    public class SysUser
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKeyAttribute]
        public Guid UserID { set; get; }

        /// <summary>
        /// 登录ID
        /// </summary>
        public String UserName { set; get; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public String FullName { set; get; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public String Password { set; get; }

        /// <summary>
        /// 密码过期时间
        /// </summary>
        public DateTime PwdExpiredTime { set; get; }

        /// <summary>
        /// 性别 1男0女
        /// </summary>
        public Boolean Sex { set; get; }

        /// <summary>
        /// 工作电话
        /// </summary>
        public String Phone { set; get; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public String Email { set; get; }

        /// <summary>
        /// 状态 1 启用 2禁用 3已删
        /// </summary>
        public Int32 Status { set; get; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public Int32 LoginCount { set; get; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { set; get; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public String LastLoginIP { set; get; }

        /// <summary>
        /// 角色ID（可以多个）
        /// </summary>
        public String RoleIDs { set; get; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { set; get; }

        #endregion
    }
}
