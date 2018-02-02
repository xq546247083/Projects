/************************************************************************
* 系统用户表
*************************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatClient
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public class SysUser
    {
        #region 属性

        /// <summary>
        /// 登录ID
        /// </summary>
        public String UserID { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        public String NickName { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean Status { set; get; }

        /// <summary>
        /// 颜色
        /// </summary>
        [JsonIgnore]
        public String Color { set; get; }

        /// <summary>
        /// 消息列表
        /// </summary>
        [JsonIgnore]
        public List<Msg> MsgList = new List<Msg>();

        #endregion

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="sysUser">对象</param>
        public void Copy(SysUser sysUser)
        {
            this.UserID = sysUser.UserID;
            this.NickName = sysUser.NickName;
            this.Status = sysUser.Status;
            this.Color = sysUser.Color;
        }
    }
}
