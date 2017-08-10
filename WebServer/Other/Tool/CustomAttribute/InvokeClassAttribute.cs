/************************************************************************
* 标题: 调用类
* 描述: 调用类
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.CustomAttribute
{
    /// <summary>
    /// 调用类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InvokeClassAttribute : Attribute
    {
        #region 属性

        /// <summary>
        /// 描述内容
        /// </summary>
        public String Describe { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public String Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public String CreateDate { get; set; }

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="describe">描述</param>
        /// <param name="creator">创建者</param>
        /// <param name="createDate">创建日期</param>
        public InvokeClassAttribute(String describe, String creator, String createDate)
        {
            Describe = describe;
            Creator = creator;
            CreateDate = createDate;
        }
    }
}