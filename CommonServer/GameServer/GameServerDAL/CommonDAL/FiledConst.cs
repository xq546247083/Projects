/************************************************************************
* 标题: 数据库字段
* 描述: 数据库字段
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.DAL
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    public static class FiledConst
    {
        public const String Id = "@Id";
        public const String UserId = "@UserId";
        public const String UserName = "@UserName";
        public const String UserPwd = "@UserPwd";
        public const String Gend = "@Gend";
        public const String IsOnline = "@IsOnline";
        public const String OnlieTime = "@OnlieTime";
        public const String RegisterTime = "@RegisterTime";
    }
}
