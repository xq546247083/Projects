/************************************************************************
* 描述: 对事物进行处理的类
*************************************************************************/
using System;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Manage.BLL
{
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
        public static void Handle(Action tranAction, Action acAction = null)
        {
            //开启数据库事务
            using (TransactionScope sope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
            {
                try
                {
                    tranAction?.Invoke();

                    sope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //如果事务方法执行成功，则执行一般方法
            acAction?.Invoke();
        }
    }
}
