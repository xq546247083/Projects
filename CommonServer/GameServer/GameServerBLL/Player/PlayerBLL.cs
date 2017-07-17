/************************************************************************
* 标题: 玩家类
* 描述: 玩家类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Tool.Common;

namespace GameServer.BLL
{
    using GameServer.DAL;
    using GameServer.Model;

    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class PlayerBLL : IInit
    {
        #region 属性

        private const String mClassName = "PlayerBLL";

        /// <summary>
        /// 玩家数据集合
        /// key:玩家id
        /// value:玩家对象
        /// </summary>
        private static Dictionary<Guid, Player> mData = new Dictionary<Guid, Player>();

        /// <summary>
        /// 读写锁
        /// </summary>
        private static ReaderWriterLockTool readerWriterLockTool = new ReaderWriterLockTool();

        #endregion

        #region 初始化

        public void Init()
        {
            //查询数据
            var dataList = PlayerDal.GetAllList().ToList<Player>();
            //赋值
            var dataTemp = new Dictionary<Guid, Player>();

            foreach (var dr in dataList)
            {
                dataTemp[dr.Id] = dr;
            }

            mData = dataTemp;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Guid, Player> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个玩家
        /// </summary>
        /// <param name="playerId">playerId</param>
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>玩家</returns>
        public static Player GetItem(Guid playerId, Boolean ifCastException = false)
        {
            using (readerWriterLockTool.GetLock(mClassName, ReaderWriterLockTool.LockTypeEnum.Reader, 0))
            {
                if (GetData().ContainsKey(playerId))
                {
                    return mData[playerId];
                }
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("p_player未找到id为{0}的玩家", playerId));
            }

            return null;
        }

        /// <summary>
        /// 获取在线玩家列表
        /// </summary>
        /// <returns>在线玩家</returns>
        public static List<Player> GetOnlinePlayer()
        {
            using (readerWriterLockTool.GetLock(mClassName, ReaderWriterLockTool.LockTypeEnum.Reader, 0))
            {
                return GetData().Values.Where(r => r.IsOnline).ToList();
            }
        }

        #endregion
    }
}
