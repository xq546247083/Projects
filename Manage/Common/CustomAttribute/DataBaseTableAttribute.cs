/************************************************************************
* 描述: 数据库表描述
*************************************************************************/
using System;

namespace Manage.Common
{
    /// <summary>
    /// 数据库表描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DataBaseTableAttribute : Attribute
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataBaseType Type { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">表名</param>
        public DataBaseTableAttribute(String name)
            : this(name, DataBaseType.Table)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">执行的名称</param>
        /// <param name="type">执行的类型</param>
        public DataBaseTableAttribute(String name, DataBaseType type)
        {
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// 操作的类型
        /// </summary>
        public enum DataBaseType
        {
            /// <summary>
            /// 表明
            /// </summary>
            Table,

            /// <summary>
            /// 存储过程
            /// </summary>
            StoredProcedure
        }
    }
}