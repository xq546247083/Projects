/************************************************************************
* 系统用户表
*************************************************************************/

using System;

namespace SocketServer.Model
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public class SysUser
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public String UserID { set; get; }

        /// <summary>
        /// 登录ID
        /// </summary>
        public String NickName { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean Status { set; get; }

        #endregion
    }
}
