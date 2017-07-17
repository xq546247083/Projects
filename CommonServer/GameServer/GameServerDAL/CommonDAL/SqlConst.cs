/************************************************************************
* 标题: 数据库自定义查询语句
* 描述: 数据库自定义查询语句
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.DAL
{
    /// <summary>
    /// 数据库语句
    /// </summary>
    public static class SqlConst
    {
        #region p_player

        public static readonly String GetPlayerListById = "SELECT  Id,UserId,UserName,UserPwd,Gend,RegisterTime  FROM p_player where Id=@Id;";
        public static readonly String GetPlayerListByUserId = "SELECT  Id,UserId,UserName,UserPwd,Gend,RegisterTime  FROM p_player where UserId=@UserId;";

        #endregion
    }
}
