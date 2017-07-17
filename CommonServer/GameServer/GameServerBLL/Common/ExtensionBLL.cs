using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static List<T> ToList<T>(this DataTable dt)
        {
            
        }

        /// <summary>
        /// 扩展方法类
        /// </summary>
        /// <typeparam name="TKey">key类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dt">数据</param>
        /// <param name="columnName">key的列名</param>
        /// <returns>返回List</returns>
        public static Dictionary<TKey, TValue> ToDic<TKey, TValue>(this DataTable dt, String columnName)
        {

        }
    }
}
