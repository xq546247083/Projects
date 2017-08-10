/************************************************************************
* 标题: 表的主键属性
* 描述: 表的主键属性
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.CustomAttribute
{
    /// <summary>
    /// 表的主键属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        
    }
}