/************************************************************************
* 标题: 博客类
* 描述: 博客类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.BLL
{
    using Tool.Common;
    using WebServer.DAL;
    using WebServer.Model;

    /// <summary>
    /// 博客类
    /// </summary>
    public partial class UBlogBLL : IInit
    {
        #region 属性

        /// <summary>
        /// 博客数据集合
        /// key:博客id
        /// value:博客对象
        /// </summary>
        private static Dictionary<Guid, UBlog> mData = new Dictionary<Guid, UBlog>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //查询数据
            var dataList = BaseModelDal.ExecuteQuery<UBlog>();
            foreach (var dr in dataList)
            {
                //不缓存博客内容，以免内存爆炸。内容的信息都执行数据库操作。
                var length = 20;
                if (length > dr.Content.Length) length = dr.Content.Length;
                dr.Content = dr.Content.Substring(0, length);

                mData[dr.ID] = dr;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Guid, UBlog> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个博客
        /// </summary>
        /// <param name="uBlogId">博客id</param>
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>博客</returns>
        public static UBlog GetItem(Guid uBlogId, Boolean ifCastException = false)
        {
            if (GetData().ContainsKey(uBlogId))
            {
                return mData[uBlogId];
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("UBlog未找到Id为{0}的博客", uBlogId));
            }

            return null;
        }

        /// <summary>
        /// 获取玩家博客列表
        /// </summary>
        /// <param name="sysUser">用户对象</param>
        /// <returns>博客列表</returns>
        public static List<UBlog> GetBlogByUser(SysUser sysUser)
        {
            var data = GetData();
            return data.Values.Where(r => r.UserId == sysUser.UserID).ToList();
        }

        /// <summary>
        /// 更新博客数据
        /// </summary>
        /// <param name="uBlog">用户</param>
        /// <returns>用户</returns>
        public static void Update(UBlog uBlog)
        {
            UBlogDAL.Update(uBlog.ID, uBlog.UserId, uBlog.Title, uBlog.Content, uBlog.Tag, uBlog.ATUsers, uBlog.BlogType, uBlog.Status, uBlog.CrDate, uBlog.ReDate);
        }

        /// <summary>
        /// 插入博客数据
        /// </summary>
        /// <param name="uBlog">用户</param>
        /// <returns>用户</returns>
        public static void Insert(UBlog uBlog)
        {
            UBlogDAL.Insert(uBlog.ID, uBlog.UserId, uBlog.Title, uBlog.Content, uBlog.Tag, uBlog.ATUsers, uBlog.BlogType, uBlog.Status, uBlog.CrDate, uBlog.ReDate);
        }

        #endregion

        #region 私有方法

        #endregion

        #region 组装客户端数据

        /// <summary>
        /// 组装客户端数据
        /// </summary>
        /// <param name="sysUser">用户对象</param>
        /// <param name="blogType">博客类型</param>
        /// <param name="status">状态</param>
        /// <param name="tagInfo">标签</param>
        /// <returns>客户端数据</returns>
        public static List<Dictionary<String, Object>> AssembleToClient(SysUser sysUser, Int32 blogTypeID, Int32 status, String tagInfo)
        {
            List<Dictionary<String, Object>> clientInfo = new List<Dictionary<String, Object>>();

            var data = GetBlogByUser(sysUser);

            var resultList = new List<UBlog>();
            var blogType = UBlogTypeBLL.GetItem(blogTypeID);
            //如果没有这个类型的博客，那么则用状态筛选
            if (blogType == null)
            {
                resultList = data.Where(r => r.Status == status).ToList();
            }
            else
            {
                resultList = data.Where(r => r.BlogType == blogTypeID && status == (Int32)BlogStatusEnum.Common).ToList();
            }

            //筛选标签
            var tagList = StringTool.SplitToStrList(tagInfo);
            if (tagList.Count > 0)
            {
                resultList = resultList.Where(r => StringTool.SplitToStrList(r.Tag).Any(k => tagList.Contains(k))).ToList();
            }

            foreach (var item in resultList)
            {
                Dictionary<String, Object> singleInfo = new Dictionary<String, Object>();
                singleInfo[PropertyConst.ID] = item.ID;
                singleInfo[PropertyConst.Title] = item.Title;
                singleInfo[PropertyConst.Content] = item.Content;
                singleInfo[PropertyConst.Tag] = item.Tag;
                singleInfo[PropertyConst.ATUsers] = item.ATUsers;
                singleInfo[PropertyConst.BlogType] = item.BlogType;
                singleInfo[PropertyConst.Status] = item.Status;
                singleInfo[PropertyConst.CrDate] = item.CrDate;
                singleInfo[PropertyConst.ReDate] = item.ReDate;

                clientInfo.Add(singleInfo);
            }

            return clientInfo;
        }

        #endregion
    }
}
