/************************************************************************
* 标题: model接口，用于构造对象
* 描述: model接口，用于构造对象
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System.Data;

namespace GameServer.Model
{
    /// <summary>
    /// model接口，用于构造对象
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="dr">要构造的数据行</param>
        void Construct(DataRow dr);
    }
}
