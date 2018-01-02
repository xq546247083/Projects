/************************************************************************
* 描述: 数据库自定义查询语句
*************************************************************************/
using System;

namespace Moqikaka.GameManage.DAL
{
    /// <summary>
    /// 数据库语句
    /// </summary>
    public static class SqlConst
    {
        #region 自定义sql

        public static readonly String GetPlayerListByUserName = "SELECT ` UserID `,` UserName `,` FullName `,` Password `,` PwdExpiredTime `,` Sex `,` Phone `,` Email `,` Status `,` LoginCount `,` LastLoginTime `,` LastLoginIP `,` RoleIDs `,` CreateTime `  FROM` sys_user `  UserName=@UserName;";
        public static readonly String GetPlayerListByUserEmail = "SELECT ` UserID `,` UserName `,` FullName `,` Password `,` PwdExpiredTime `,` Sex `,` Phone `,` Email `,` Status `,` LoginCount `,` LastLoginTime `,` LastLoginIP `,` RoleIDs `,` CreateTime `  FROM` sys_user `  Email=@Email;";

        #endregion
    }
}
