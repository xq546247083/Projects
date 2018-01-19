/************************************************************************
* 描述: 主键属性
*************************************************************************/
using System;

namespace Manage.Common
{
    /// <summary>
    /// 表的主键属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        
    }
}