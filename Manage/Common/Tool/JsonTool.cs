/************************************************************************
* 标题: Json工具
*************************************************************************/
using System;
using System.Collections.Generic;

namespace Manage.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Json工具
    /// </summary>
    public static class JsonTool
    {
        /// <summary>
		/// 序列化对象为Json数据
		/// </summary>
		/// <param name="obj">序列对象</param>
		/// <exception cref="T:System.ArgumentException"></exception>
		/// <exception cref="T:System.ArgumentNullException"></exception>
		/// <exception cref="T:System.InvalidOperationException"></exception>
		/// <returns>序列化后的字符串</returns>
		public static string Serialize(object obj)
        {
            if (obj == null)
            {
                throw new System.ArgumentNullException("Error", "obj can't be null.");
            }
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化Json数据为指定对象
        /// </summary>
        /// <typeparam name="T">指定返回对象</typeparam>
        /// <param name="str">Json字符串</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        /// <returns>反序列化后的T对象</returns>
        public static T Deserialize<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Error", "str can't be empty or null.");
            }
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 反序列化Json数据为Dictionary对象
        /// </summary>
        /// <param name="str">Json字符串</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        /// <returns>反序列化后的Dictionary对象</returns>
        public static Dictionary<string, object> Deserialize(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Error", "str can't be empty or null.");
            }
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
        }
    }
}
