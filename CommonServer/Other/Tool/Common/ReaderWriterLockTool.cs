using System;
using System.Collections.Generic;
using System.Threading;

namespace Tool.Common
{
    /// <summary>
    /// 读写锁工具
    /// </summary>
    public class ReaderWriterLockTool
    {
        #region 属性

        /// <summary>
        /// lockObjData同步对象
        /// </summary>
        private object mLockObj = new object();

        /// <summary>
        /// 锁集合
        /// </summary>
        private Dictionary<string, ReaderWriterLockSlim> mLockSlimDic = new Dictionary<string, ReaderWriterLockSlim>();

        #endregion

        #region 方法

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="lockType">锁类型</param>
        /// <param name="millisecondsTimeout">等待的毫秒数；&lt;=0表示无限期等待。</param>
        /// <returns>返回锁</returns>
        public IDisposable GetLock(string key, LockTypeEnum lockType, int millisecondsTimeout = 100)
        {
            ReaderWriterLockSlim lockSlim = this.GetLockSlim(key);
            return GetLock(lockSlim, lockType, millisecondsTimeout);
        }

        /// <summary>
        /// 主动释放锁资源，避免长久驻留内存
        /// </summary>
        /// <param name="key">锁的唯一标识</param>
        public void ReleaseLock(string key)
        {
            lock (this.mLockObj)
            {
                this.mLockSlimDic.Remove(key);
            }
        }

        /// <summary>
        /// 主动清空所有锁资源，避免长久驻留内存
        /// </summary>
        public void ReleaseAllLock()
        {
            lock (this.mLockObj)
            {
                this.mLockSlimDic.Clear();
            }
        }

        /// <summary>
        /// 获取锁对象，获取过程会死等。直到获取到锁对象
        /// </summary>
        /// <param name="lockObj">锁对象</param>
        /// <param name="lockType">锁类型</param>
        /// <returns>返回锁对象</returns>
        private static IDisposable GetLockSlimByInfiniteWait(ReaderWriterLockSlim lockObj, LockTypeEnum lockType)
        {
            switch (lockType)
            {
                case LockTypeEnum.Reader:
                    if (!lockObj.IsReadLockHeld)
                    {
                        lockObj.EnterReadLock();
                    }
                    return new CustomMonitor(lockObj, lockType);
                case LockTypeEnum.Writer:
                    if (!lockObj.IsWriteLockHeld)
                    {
                        lockObj.EnterWriteLock();
                    }
                    return new CustomMonitor(lockObj, lockType);
                case LockTypeEnum.EnterUpgradeableReader:
                    if (!lockObj.IsUpgradeableReadLockHeld)
                    {
                        lockObj.EnterUpgradeableReadLock();
                    }
                    return new CustomMonitor(lockObj, lockType);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取锁对象信息
        /// </summary>
        /// <param name="key">锁的唯一标识</param>
        /// <returns>返回锁对象</returns>
        private ReaderWriterLockSlim GetLockSlim(string key)
        {
            ReaderWriterLockSlim result;
            lock (this.mLockObj)
            {
                if (!this.mLockSlimDic.ContainsKey(key))
                {
                    ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
                    this.mLockSlimDic[key] = readerWriterLockSlim;
                    result = readerWriterLockSlim;
                }
                else
                {
                    result = this.mLockSlimDic[key];
                }
            }
            return result;
        }

        /// <summary>
        /// 获取锁对象
        /// </summary>
        /// <param name="lockSlimObj">锁对象</param>
        /// <param name="lockType">锁类型</param>
        /// <param name="millisecondsTimeout">等待的毫秒数；&lt;=0表示无限期等待。</param>
        /// <returns>返回锁对象</returns>
        /// <exception cref="T:TimeoutException">获取锁对象超时时，抛出此异常</exception>
        /// <remarks>
        /// 对同一个锁操作时，读的代码块内不能包含写的代码块，也不能在写的代码块包含读的代码块，这会导致内部异常,如果确实想要写，请使用<see cref="F:Moqikaka.Util.Lock.LockTypeEnum.EnterUpgradeableReader" />，比如：
        /// <code>
        /// using("test", LockTypeEnum.EnterUpgradeableReader)
        /// {
        ///     using("test",LockTypeEnum.Write)
        ///     {
        ///         // do something
        ///     }
        /// }
        /// </code>
        /// </remarks>
        private static IDisposable GetLock(ReaderWriterLockSlim lockSlimObj, LockTypeEnum lockType, int millisecondsTimeout = 100)
        {
            if (millisecondsTimeout <= 0)
            {
                return GetLockSlimByInfiniteWait(lockSlimObj, lockType);
            }

            switch (lockType)
            {
                case LockTypeEnum.Reader:
                    if (lockSlimObj.IsUpgradeableReadLockHeld || lockSlimObj.IsReadLockHeld || lockSlimObj.TryEnterReadLock(millisecondsTimeout))
                    {
                        return new CustomMonitor(lockSlimObj, lockType);
                    }
                    throw new TimeoutException("等待读锁超时");
                case LockTypeEnum.Writer:
                    if (lockSlimObj.IsWriteLockHeld || lockSlimObj.TryEnterWriteLock(millisecondsTimeout))
                    {
                        return new CustomMonitor(lockSlimObj, lockType);
                    }
                    throw new TimeoutException("等待写锁超时");
                case LockTypeEnum.EnterUpgradeableReader:
                    if (lockSlimObj.IsUpgradeableReadLockHeld || lockSlimObj.TryEnterUpgradeableReadLock(millisecondsTimeout))
                    {
                        return new CustomMonitor(lockSlimObj, lockType);
                    }
                    throw new TimeoutException("等待读锁超时");
                default:
                    return null;
            }
        }

        #endregion

        /// <summary>
        /// 锁类型枚举
        /// </summary>
        public enum LockTypeEnum
        {
            /// <summary>
            /// 读,在此方式下，如果要切换到写。则会报异常
            /// </summary>
            Reader,
            /// <summary>
            /// 写
            /// </summary>
            Writer,
            /// <summary>
            /// 可升级的读，在读中可能需要切换到写锁，用此方式，此方式性能比Writer高
            /// </summary>
            EnterUpgradeableReader
        }

        /// <summary>
        /// 自定义锁对象
        /// </summary>
        private class CustomMonitor : IDisposable
        {
            /// <summary>
            /// 锁对象
            /// </summary>
            private ReaderWriterLockSlim rwLockObj;

            /// <summary>
            /// 锁类型
            /// </summary>
            private LockTypeEnum lockType;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="rwLockObj">读写锁对象</param>
            /// <param name="lockType">获取方式</param>
            public CustomMonitor(ReaderWriterLockSlim rwLockObj, LockTypeEnum lockType)
            {
                this.rwLockObj = rwLockObj;
                this.lockType = lockType;
            }

            /// <summary>
            /// 锁释放
            /// </summary>
            public void Dispose()
            {
                if (this.lockType == LockTypeEnum.Reader && this.rwLockObj.IsReadLockHeld)
                {
                    this.rwLockObj.ExitReadLock();
                    return;
                }
                if (this.lockType == LockTypeEnum.Writer && this.rwLockObj.IsWriteLockHeld)
                {
                    this.rwLockObj.ExitWriteLock();
                    return;
                }
                if (this.lockType == LockTypeEnum.EnterUpgradeableReader && this.rwLockObj.IsUpgradeableReadLockHeld)
                {
                    this.rwLockObj.ExitUpgradeableReadLock();
                }
            }
        }
    }
}
