﻿/************************************************************************
* 描述:缓存
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Manage
{
    /// <summary>
    /// 缓存接口的实现
    /// </summary>
    public partial class MemoryCacheManager
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private static ObjectCache Cache => MemoryCache.Default;

        /// <summary>
        /// 获取已经设置的缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>指定的键对应的值</returns>
        public static T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// 添加对象到缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        /// <param name="cacheTime">保存时间</param>
        public static void Set(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 指定的键是否存在缓存中
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>结果</returns>
        public static Boolean IsSet(string key)
        {
            if (Cache.Contains(key))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 删除缓存中指定的键
        /// </summary>
        /// <param name="key">/键</param>
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 通过正则表达式模式删除
        /// </summary>
        /// <param name="pattern">正则表达式模式</param>
        public static void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
            {
                if (regex.IsMatch(item.Key))
                {
                    keysToRemove.Add(item.Key);
                }
            }

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 删除所有的缓存数据
        /// </summary>
        public static void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}