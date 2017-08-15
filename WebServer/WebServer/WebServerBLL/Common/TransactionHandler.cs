/************************************************************************
* 标题: 事务处理类
* 描述: 对事物进行处理的类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Transactions;

namespace WebServer.BLL
{
    using Tool.Common;
    using Tool.Extension;

    /// <summary>
    /// 事务提交类
    /// </summary>
    public class TransactionHandler
    {
        /// <summary>
        /// 处理事务
        /// </summary>
        /// <param name="tranAction">事务方法</param>
        /// <param name="acAction">一般方法</param>
        public static void Handle(Action tranAction, Action acAction)
        {
            //开启数据库事务
            using (TransactionScope sope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
            {
                try
                {
                    if (tranAction != null)
                    {
                        tranAction();
                    }

                    sope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //如果事务方法执行成功，则执行一般方法
            if (acAction != null)
            {
                acAction();
            }
        }
    }
}
