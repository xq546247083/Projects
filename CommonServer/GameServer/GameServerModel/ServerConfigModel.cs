/************************************************************************
* 标题: m_ServerConfig
* 描述: m_ServerConfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 服务器配置表
    /// </summary>
    [DataBaseTable("m_ServerConfig")]
    public class ServerConfigModel
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 服务器id
        /// </summary>
        public String ServerId { get; set; }
    }
}
