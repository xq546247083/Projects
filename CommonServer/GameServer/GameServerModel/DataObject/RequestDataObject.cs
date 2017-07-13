/************************************************************************
* 标题: 请求数据对象
* 描述: 请求数据对象
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.Model
{
    /// <summary>
    /// 请求数据对象
    /// </summary>
    public class RequestDataObject
    {
        /// <summary>
        /// 请求的字符串
        /// </summary>
        public String RequestString { get; set; }

        /// <summary>
        /// 请求的模块名称
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// 请求的方法名称
        /// </summary>
        public String MethodName { get; set; }

        /// <summary>
        /// 服务器Id
        /// </summary>
        public Int32 ServerId { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public String PlayerId { get; set; }

        /// <summary>
        /// 玩家Token
        /// </summary>
        public String Token { get; set; }

        /// <summary>
        /// 游戏版本号
        /// </summary>
        public Int32 GameVersionId { get; set; }

        /// <summary>
        /// 客户端发送请求时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 请求的数据
        /// </summary>
        public Object[] Data { get; set; }
    }
}