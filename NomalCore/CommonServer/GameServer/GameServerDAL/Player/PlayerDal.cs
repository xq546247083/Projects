/************************************************************************
* 标题: 玩家dal
* 描述: 玩家dal
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Data;

namespace GameServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// 玩家查询dal
    /// </summary>
    public class PlayerDal : BaseDal
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "p_player";

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
        /// <returns>/// 获取数据</returns>
        public static DataTable GetList(Guid id)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.Id,id)    
            };

            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="gend">性别</param>
        /// <param name="isOnline">是否在线</param>
        /// <param name="onlieTime">在线时间</param>
        /// <param name="registerTime">注册时间</param>
        /// <returns>更新</returns>
        public static Int32 Update(Guid id, String userId, String userName, String userPwd, Boolean gend, Boolean isOnline, DateTime onlieTime, DateTime registerTime)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.Id,id),
                new MySqlParameter(FiledConst.UserId,userId),
                new MySqlParameter(FiledConst.UserName,userName),
                new MySqlParameter(FiledConst.UserPwd,userPwd),
                new MySqlParameter(FiledConst.Gend,gend),
                new MySqlParameter(FiledConst.IsOnline,isOnline),
                new MySqlParameter(FiledConst.OnlieTime,onlieTime),
                new MySqlParameter(FiledConst.RegisterTime,registerTime)
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>删除</returns>
        public static Int32 Delete(Guid id)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.Id,id)       
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="gend">性别</param>
        /// <param name="isOnline">是否在线</param>
        /// <param name="onlieTime">在线时间</param>
        /// <param name="registerTime">注册时间</param>
        /// <returns></returns>
        public static Int32 Insert(Guid id, String userId, String userName, String userPwd, Boolean gend, Boolean isOnline, DateTime onlieTime, DateTime registerTime)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.Id,id),
                new MySqlParameter(FiledConst.UserId,userId),
                new MySqlParameter(FiledConst.UserName,userName),
                new MySqlParameter(FiledConst.UserPwd,userPwd),
                new MySqlParameter(FiledConst.Gend,gend),
                new MySqlParameter(FiledConst.IsOnline,isOnline),
                new MySqlParameter(FiledConst.OnlieTime,onlieTime),
                new MySqlParameter(FiledConst.RegisterTime,registerTime)
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>获取用户信息</returns>
        public static DataTable GetListByUserId(String userId)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.UserId,userId)       
            };

            return ExecuteDataTable(SqlConst.GetPlayerListByUserId, mySqlParameter);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>获取用户信息</returns>
        public static DataTable GetListById(String id)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.Id,id)
            };
            mySqlParameter[0].Value = id;

            return ExecuteDataTable(SqlConst.GetPlayerListById, mySqlParameter);
        }
    }
}
