/************************************************************************
* 描述:PageHelper
*************************************************************************/
using System;
using System.Web;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// PageHelper
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// 获取缓存查询添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">缓存key</param>
        /// <param name="linkOpen">是否打开</param>
        /// <param name="clickSearch">是否操作</param>
        /// <param name="model">模型</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static T GetSearch<T>(string sessionKey, bool linkOpen, bool clickSearch, T model, ref int pageSize)
        {
            sessionKey = sessionKey + "_CacheKey";
            var retModel = model;

            if (linkOpen == false)
            {
                retModel = (T)HttpContext.Current.Session[sessionKey];
            }
            else
            {
                HttpContext.Current.Session.Remove(sessionKey);
                HttpContext.Current.Session.Add(sessionKey, model);
            }

            if (clickSearch)
            {
                pageSize = Convert.ToInt32(HttpContext.Current.Session[sessionKey + "_pageSize"]);
            }

            HttpContext.Current.Session.Add(sessionKey + "_pageSize", pageSize);

            return retModel;
        }
    }
}