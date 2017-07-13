/************************************************************************
* 标题: 方法描述
* 描述: 方法描述
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.CustomAttribute
{
    /// <summary>
    /// 方法描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodDescribeAttribute : Attribute
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

        /// <summary>
        /// 参数描述
        /// </summary>
        public String ParameterDescribe { get; set; }

        /// <summary>
        /// 返回值描述
        /// </summary>
        public String ReturnDescribe { get; set; }


        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="describe">描述</param>
        /// <param name="creator">创建者</param>
        /// <param name="createDate">创建日期</param>
        /// <param name="parameterDescribe">参数描述</param>
        /// <param name="returnDescribe">返回值描述</param>
        public MethodDescribeAttribute(String describe, String creator, String createDate, String parameterDescribe, String returnDescribe)
        {
            Describe = describe;
            Creator = creator;
            CreateDate = createDate;
            ParameterDescribe = parameterDescribe;
            ReturnDescribe = ReturnDescribe;
        }
    }
}