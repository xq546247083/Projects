/************************************************************************
* 标题: 博客类型类
* 描述: 博客类型类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.BLL
{
    using WebServer.DAL;
    using WebServer.Model;

    /// <summary>
    /// 博客类型类
    /// </summary>
    public partial class UBlogTypeBLL : IInit
    {
        #region 属性

        /// <summary>
        /// 博客类型数据集合
        /// key:博客类型id
        /// value:博客类型对象
        /// </summary>
        private static Dictionary<Int32, UBlogType> mData = new Dictionary<Int32, UBlogType>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //查询数据
            var dataList = BaseModelDal.ExecuteQuery<UBlogType>();
            foreach (var dr in dataList)
            {
                mData[dr.ID] = dr;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Int32, UBlogType> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个博客类型
        /// </summary>
        /// <param name="uBlogTypeId">博客类型id</param>
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>博客类型</returns>
        public static UBlogType GetItem(Int32 uBlogTypeId, Boolean ifCastException = false)
        {
            if (GetData().ContainsKey(uBlogTypeId))
            {
                return mData[uBlogTypeId];
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("UBlogType未找到Id为{0}的博客类型", uBlogTypeId));
            }

            return null;
        }

        #endregion
    }
}
