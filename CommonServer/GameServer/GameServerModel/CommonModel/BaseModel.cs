using System.Data;

namespace GameServer.Model.CommonModel
{
    public interface IModel
    {
        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="dr">要构造的数据行</param>
        void Construct(DataRow dr);
    }
}
