/************************************************************************
* 标题: 忽视属性
* 描述: 忽视属性
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.CustomAttribute
{
    /// <summary>
    /// 忽视属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class IgnoreAttribute : Attribute
    {

    }
}