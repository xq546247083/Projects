//***********************************************************************************
// 请求处理的上下文对象
//***********************************************************************************
using System;
using System.Collections.Specialized;

namespace SocketServer.Model
{
    /// <summary>
    /// 上下文对象
    /// </summary>
    public class Context
    {
        /// <summary>
        /// 请求对象
        /// </summary>
        public NameValueCollection Request { get; private set; }

        /// <summary>
        /// 链接对象
        /// </summary>
        public IConnection Connection { get; set; }

        /// <summary>
        /// 用户对象
        /// </summary>
        public SysUser SysUser { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="connection">连接对象</param>
        /// <param name="sysUser">用户对象</param>
        public Context(NameValueCollection request, IConnection connection, SysUser sysUser)
        {
            this.Request = request;
            this.Connection = connection;
            this.SysUser = sysUser;
        }
    }
}
