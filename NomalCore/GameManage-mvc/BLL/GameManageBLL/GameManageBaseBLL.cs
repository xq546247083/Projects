/************************************************************************
* 描述:后台数据库BLL
*************************************************************************/
using System;
using System.Collections.Generic;

namespace Moqikaka.GameManage.BLL
{
    using Moqikaka.GameManage.DAL;
    using Moqikaka.Util.Log;

    /// <summary>
    /// 后台数据库BLL
    /// </summary>
    public class GameManageBaseBLL
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
                return GameManageBaseDAL<T>.GetList(paramObj);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "GetList", "Null", e.StackTrace, e.Message), LogType.Error);

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
        /// <param name="orderStr">排序字符串</param>
        /// <returns>数据</returns>
        public static List<T> GetDefinedList<T>(T paramObj = null, Int32 pageNo = -1, Int32 pageSize = -1, Boolean isLike = true, String orderStr = "") where T : class
        {
            try
            {
                return GameManageBaseDAL<T>.GetDefinedList(paramObj, pageNo, pageSize, isLike, orderStr);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "GetDefinedList", "Null", e.StackTrace, e.Message), LogType.Error);

                return new List<T>();
            }
        }

        /// <summary>
        /// 获得自定义筛选的数据量（只支持字符串与数字筛选,like）
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <param name="isLike">是否使用like查询</param>
        /// <returns>受影响的函数</returns>
        public static Int32 GetDefinedCount<T>(T paramObj = null, Boolean isLike = true) where T : class
        {
            try
            {
                return GameManageBaseDAL<T>.GetDefinedCount(paramObj, isLike);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "GetCount", "Null", e.StackTrace, e.Message), LogType.Error);

                return 0;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Insert<T>(T paramObj) where T : class
        {
            try
            {
                return GameManageBaseDAL<T>.Insert(paramObj);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "Instert", "Null", e.StackTrace, e.Message), LogType.Error);

                return 0;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Update<T>(T paramObj) where T : class
        {
            try
            {
                return GameManageBaseDAL<T>.Update(paramObj);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "Update", "Null", e.StackTrace, e.Message), LogType.Error);

                return 0;
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
                return GameManageBaseDAL<T>.Delete(paramObj);
            }
            catch (Exception e)
            {
                LogUtil.Write(String.Format("{0}Model:{1}{0}Method:{2}{0}Data:{3}{0}stackTrace:{0}{4}{0}Message:{0}{5}{0}", Environment.NewLine, typeof(T).Name, "Delete", "Null", e.StackTrace, e.Message), LogType.Error);

                return 0;
            }
        }
    }
}
