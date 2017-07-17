using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;

namespace GameServer.BLL.Common
{
    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class ExtensionBLL
    {
        /// <summary>
        /// 扩展方法类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">数据</param>
        /// <returns>返回List</returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class,IModel, new()
        {
            List<T> resultList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //构造对象
                T tempObject = new T();
                tempObject.Construct(dr);

                resultList.Add(tempObject);
            }

            return resultList;
        }

        /// <summary>
        /// 扩展方法类
        /// </summary>>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="dt">数据</param>
        /// <param name="columnName">key的列名</param>
        /// <returns>返回List</returns>
        public static Dictionary<String, T> ToDic<T>(this DataTable dt, String columnName) where T : class, IModel, new()
        {
            Dictionary<String, T> resultDic = new Dictionary<String, T>();
            foreach (DataRow dr in dt.Rows)
            {
                //构造对象
                T tempObject = new T();
                tempObject.Construct(dr);

                var keyStr = dr[columnName].ToString();
                resultDic[keyStr] = tempObject;
            }

            return resultDic;
        }
    }
}
