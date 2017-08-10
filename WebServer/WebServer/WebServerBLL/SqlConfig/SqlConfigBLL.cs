/************************************************************************
* 标题: 初始化数据库语句
* 描述: 初始化数据库语句
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using WebServer.DAL;

namespace WebServer.BLL
{
    /// <summary>
    /// 初始化数据库语句
    /// </summary>
    public class SqlConfigBLL : IConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            SqlFactory.BuildCommond();
        }
    }
}
