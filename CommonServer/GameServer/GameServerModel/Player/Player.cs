/************************************************************************
* 标题: p_player
* 描述: p_player
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 用户
    /// </summary>
    [DataBaseTable("p_player")]
    public class Player
    {
        #region 属性

        /// <summary>
        /// id
        /// </summary>
        /// [PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 用户名name
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public String UserPwd { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Boolean Gend { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public Boolean IsOnline { get; set; }

        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime OnlieTime { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        #endregion

        /// <summary>
        /// Player
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="userName">用户Name</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="gend">性别</param>
        /// <param name="registerTime">注册时间</param>
        public Player(Guid id, String userId, String userName, String userPwd, Boolean gend, Boolean isOnline, DateTime onlieTime, DateTime registerTime)
        {
            this.Id = id;
            this.UserId = userId;
            this.UserName = userName;
            this.UserPwd = userPwd;
            this.Gend = gend;
            this.IsOnline = isOnline;
            this.OnlieTime = onlieTime;
            this.RegisterTime = registerTime;
        }
    }
}
