/************************************************************************
* 标题: 数据库字段
* 描述: 数据库字段
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace WebServer.DAL
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    public static class FiledConst
    {
        public const String UserID = "@UserID";
        public const String UserName = "@UserName";
        public const String FullName = "@FullName";
        public const String Password = "@Password";
        public const String PwdExpiredTime = "@PwdExpiredTime";
        public const String Sex = "@Sex";
        public const String Phone = "@Phone";
        public const String Email = "@Email";
        public const String Status = "@Status";
        public const String LoginCount = "@LoginCount";
        public const String LastLoginTime = "@LastLoginTime";
        public const String LastLoginIP = "@LastLoginIP";
        public const String RoleIDs = "@RoleIDs";
        public const String CreateTime = "@CreateTime";
    }
}
