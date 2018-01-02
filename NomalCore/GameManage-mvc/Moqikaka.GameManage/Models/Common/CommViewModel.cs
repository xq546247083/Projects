/************************************************************************
* 描述:页面公共model
*************************************************************************/
using System;
using System.Collections.Generic;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 页面公共model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommViewModel<T> where T : class,new()
    {
        /// <summary>
        /// 页面单模型
        /// </summary>
        public T ViewModel { get; set; }

        /// <summary>
        /// 页面集合模型
        /// </summary>
        public List<T> ViewModelList { get; set; }

        /// <summary>
        /// 服务器id
        /// </summary>
        public Int32 ServerGroupId { get; set; }

        /// <summary>
        /// 玩家名字
        /// </summary>
        public String PlayerNames { get; set; }

        /// <summary>
        /// 页面index
        /// </summary>
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 页面数量
        /// </summary>
        public Int32 PageCount { get; set; }

        public CommViewModel()
        {
            PageIndex = 1;
            PageSize = 20;
            ViewModel = new T();
            ViewModelList = new List<T>();
        }
    }
}