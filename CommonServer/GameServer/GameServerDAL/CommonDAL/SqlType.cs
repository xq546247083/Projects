/************************************************************************
* 标题: 数据库语句类型
* 描述: 数据库语句类型
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
namespace GameServer.DAL
{
    /// <summary>
    /// 数据库语句类型
    /// </summary>
    public enum SqlType
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        GetList = 0,

        /// <summary>
        /// 获取所有列表
        /// </summary>
        GetAllList = 1,

        /// <summary>
        /// 插入数据
        /// </summary>
        Insert = 2,

        /// <summary>
        /// 更新数据
        /// </summary>
        Update = 3,

        /// <summary>
        /// 删除数据
        /// </summary>
        Delete = 4
    }
}
