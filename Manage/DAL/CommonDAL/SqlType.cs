/************************************************************************
* 描述: 数据库语句类型
*************************************************************************/
namespace Manage.DAL
{
    /// <summary>
    /// 数据库语句类型
    /// </summary>
    public enum SqlType
    {
        /// <summary>
        /// 获取所有列表
        /// </summary>
        GetAllList = 0,

        /// <summary>
        /// 获取列表
        /// </summary>
        GetList = 1,

        /// <summary>
        /// 获取自定义筛选列表
        /// </summary>
        GeDefinedList = 2,

        /// <summary>
        /// 获取数量
        /// </summary>
        GetDefinedCount = 3,

        /// <summary>
        /// 插入数据
        /// </summary>
        Insert = 4,

        /// <summary>
        /// 更新数据
        /// </summary>
        Update = 5,

        /// <summary>
        /// 删除数据
        /// </summary>
        Delete = 6,
    }
}
