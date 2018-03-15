/************************************************************************
* 描述:后台数据库BLL
*************************************************************************/
using System;
using System.Collections.Generic;

namespace Manage.BLL
{
    using Manage.Common;
    using Manage.DAL;

    /// <summary>
    /// 后台数据库BLL
    /// </summary>
    public class ManageBaseBLL
    {
        /// <summary>
        /// 查询列表（只支持主键筛选）
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>数据</returns>
        public static List<T> GetList<T>(T paramObj = null) where T : class
        {
            try
            {
                return ManageBaseDAL<T>.GetList(paramObj);
            }
            catch (Exception ex)
            {
                Log.Write($"查询列表失败，ex:{ex}", LogType.Error);

                return new List<T>();
            }
        }

        /// <summary>
        /// 查询列表（只支持字符串与数字筛选）
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="paramObj">参数</param>
        /// <param name="isLike">是否使用like查询</param>
        /// <param name="filterStr">自定义筛选字段</param>
        /// <param name="orderStr">排序字符串</param>
        /// <returns>数据</returns>
        public static List<T> GetDefinedList<T>(T paramObj = null, Int32 pageNo = -1, Int32 pageSize = -1, Boolean isLike = false, String filterStr = "", String orderStr = "") where T : class
        {
            try
            {
                return ManageBaseDAL<T>.GetDefinedList(paramObj, pageNo, pageSize, isLike, filterStr, orderStr);
            }
            catch (Exception ex)
            {
                Log.Write($"获得自定义查询列表失败，ex:{ex}", LogType.Error);

                return new List<T>();
            }
        }

        /// <summary>
        /// 获得自定义筛选的数据量（只支持字符串与数字筛选,like）
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <param name="isLike">是否使用like查询</param>
        /// <param name="filterStr">自定义筛选字段</param>
        /// <returns>受影响的函数</returns>
        public static Int32 GetDefinedCount<T>(T paramObj = null, Boolean isLike = false, String filterStr = "") where T : class
        {
            try
            {
                return ManageBaseDAL<T>.GetDefinedCount(paramObj, isLike, filterStr);
            }
            catch (Exception ex)
            {
                Log.Write($"获得自定义筛选的数据量失败，ex:{ex}", LogType.Error);

                return 0;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Insert<T>(T paramObj, Boolean ifThrowException = false) where T : class
        {
            try
            {
                return ManageBaseDAL<T>.Insert(paramObj);
            }
            catch (Exception ex)
            {
                Log.Write($"插入数据失败，ex:{ex}", LogType.Error);

                if (ifThrowException)
                {
                    throw ex;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Update<T>(T paramObj, Boolean ifThrowException = false) where T : class
        {
            try
            {
                return ManageBaseDAL<T>.Update(paramObj);
            }
            catch (Exception ex)
            {
                Log.Write($"更新数据失败，ex:{ex}", LogType.Error);

                if (ifThrowException)
                {
                    throw ex;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Delete<T>(T paramObj) where T : class
        {
            try
            {
                return ManageBaseDAL<T>.Delete(paramObj);
            }
            catch (Exception ex)
            {
                Log.Write($"删除数据失败，ex:{ex}", LogType.Error);

                return 0;
            }
        }
    }
}
