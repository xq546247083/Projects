/************************************************************************
* 标题: ModelHandler
* 描述: ModelHandler,用来处理model的
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System.Collections.Generic;

namespace WebServer.Model
{
    /// <summary>
    /// ModelHandler
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public static class ModelHandler<T> where T : class, new()
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        public static List<T> Execute()
        {
            return BaseModelDal.ExecuteQuery<T>();
        }
    }
}
