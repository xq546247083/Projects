﻿/************************************************************************
* 标题: 数据库自定义查询语句
* 描述: 数据库自定义查询语句
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace WebServer.DAL
{
    /// <summary>
    /// 数据库语句
    /// </summary>
    public static class SqlConst
    {
        #region p_player

        public static readonly String GetPlayerListByUserName = "SELECT ` UserID `,` UserName `,` FullName `,` Password `,` PwdExpiredTime `,` Sex `,` Phone `,` Email `,` Status `,` LoginCount `,` LastLoginTime `,` LastLoginIP `,` RoleIDs `,` CreateTime `  FROM` sys_user `  UserName=@UserName;";
        public static readonly String GetPlayerListByUserEmail = "SELECT ` UserID `,` UserName `,` FullName `,` Password `,` PwdExpiredTime `,` Sex `,` Phone `,` Email `,` Status `,` LoginCount `,` LastLoginTime `,` LastLoginIP `,` RoleIDs `,` CreateTime `  FROM` sys_user `  Email=@Email;";

        #endregion
    }
}
