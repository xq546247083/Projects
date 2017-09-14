/************************************************************************
* 标题: sys_user的DAL
* 描述: sys_user的DAL
* 数据表:sys_user
* 作者：徐敏荣
* 日期：2017/8/17 10:18:33
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// sys_user的DAL
    /// </summary>
    public class SysUserDAL : BaseDal
    {
        #region 属性

        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "sys_user";

        #endregion

        #region 方法

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>获取所有数据</returns>
        public static DataTable GetAllList()
        {
            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetAllList]);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="userID">主键</param> 
        /// <returns>获取数据</returns>
        public static DataTable GetList(Guid userID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.UserID,userID),
			};

            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="userName">用户名</param> 
        /// <returns>/// 获取数据</returns>
        public static DataTable GetListByUserName(String userName)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.UserName,userName),
			};

            return ExecuteDataTable(SqlConst.GetPlayerListByUserName, mySqlParameter);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="email">邮箱</param> 
        /// <returns>/// 获取数据</returns>
        public static DataTable GetListByUserEmail(Guid email)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.Email,email),
			};

            return ExecuteDataTable(SqlConst.GetPlayerListByUserEmail, mySqlParameter);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userID">主键</param>
        /// <returns>删除</returns>
        public static Int32 Delete(Guid userID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.UserID,userID),
			};

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="userID">主键</param>
        /// <param name="userName">登录ID</param>
        /// <param name="fullName">用户真实姓名</param>
        /// <param name="password">登陆密码</param>
        /// <param name="pwdExpiredTime">密码过期时间</param>
        /// <param name="sex">性别 1男0女</param>
        /// <param name="phone">工作电话</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="status">状态 1 启用 2禁用 3已删</param>
        /// <param name="loginCount">登录次数</param>
        /// <param name="lastLoginTime">最后登录时间</param>
        /// <param name="lastLoginIP">公司ID</param>
        /// <param name="roleIDs">角色ID（可以多个）</param>
        /// <param name="createTime">创建日期</param>
        /// <returns>更新</returns>
        public static Int32 Insert(Guid userID, String userName, String fullName, String password, DateTime pwdExpiredTime, Boolean sex, String phone, String email, Int32 status, Int32 loginCount, DateTime lastLoginTime, String lastLoginIP, String roleIDs, DateTime createTime)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.UserID,userID),
				new MySqlParameter(FiledConst.UserName,userName),
				new MySqlParameter(FiledConst.FullName,fullName),
				new MySqlParameter(FiledConst.Password,password),
				new MySqlParameter(FiledConst.PwdExpiredTime,pwdExpiredTime),
				new MySqlParameter(FiledConst.Sex,sex),
				new MySqlParameter(FiledConst.Phone,phone),
				new MySqlParameter(FiledConst.Email,email),
				new MySqlParameter(FiledConst.Status,status),
				new MySqlParameter(FiledConst.LoginCount,loginCount),
				new MySqlParameter(FiledConst.LastLoginTime,lastLoginTime),
				new MySqlParameter(FiledConst.LastLoginIP,lastLoginIP),
				new MySqlParameter(FiledConst.RoleIDs,roleIDs),
				new MySqlParameter(FiledConst.CreateTime,createTime),
			};

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userID">主键</param>
        /// <param name="userName">登录ID</param>
        /// <param name="fullName">用户真实姓名</param>
        /// <param name="password">登陆密码</param>
        /// <param name="pwdExpiredTime">密码过期时间</param>
        /// <param name="sex">性别 1男0女</param>
        /// <param name="phone">工作电话</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="status">状态 1 启用 2禁用 3已删</param>
        /// <param name="loginCount">登录次数</param>
        /// <param name="lastLoginTime">最后登录时间</param>
        /// <param name="lastLoginIP">公司ID</param>
        /// <param name="roleIDs">角色ID（可以多个）</param>
        /// <param name="createTime">创建日期</param>
        /// <returns>更新</returns>
        public static Int32 Update(Guid userID, String userName, String fullName, String password, DateTime pwdExpiredTime, Boolean sex, String phone, String email, Int32 status, Int32 loginCount, DateTime lastLoginTime, String lastLoginIP, String roleIDs, DateTime createTime)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
			{
				new MySqlParameter(FiledConst.UserID,userID),
				new MySqlParameter(FiledConst.UserName,userName),
				new MySqlParameter(FiledConst.FullName,fullName),
				new MySqlParameter(FiledConst.Password,password),
				new MySqlParameter(FiledConst.PwdExpiredTime,pwdExpiredTime),
				new MySqlParameter(FiledConst.Sex,sex),
				new MySqlParameter(FiledConst.Phone,phone),
				new MySqlParameter(FiledConst.Email,email),
				new MySqlParameter(FiledConst.Status,status),
				new MySqlParameter(FiledConst.LoginCount,loginCount),
				new MySqlParameter(FiledConst.LastLoginTime,lastLoginTime),
				new MySqlParameter(FiledConst.LastLoginIP,lastLoginIP),
				new MySqlParameter(FiledConst.RoleIDs,roleIDs),
				new MySqlParameter(FiledConst.CreateTime,createTime),
			};

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        #endregion
    }
}
