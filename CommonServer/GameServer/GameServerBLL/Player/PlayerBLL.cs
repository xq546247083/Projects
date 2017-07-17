using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.BLL.Common;
using GameServer.DAL;
using GameServer.Model;

namespace GameServer.BLL
{
    /// <summary>
    /// 玩家类
    /// </summary>
    public partial class PlayerBLL:IInit
    {
        #region 属性
        
        /// <summary>
        /// 玩家数据集合
        /// key:玩家id
        /// value:玩家对象
        /// </summary>
        private static Dictionary<Guid, Player> data = new Dictionary<Guid, Player>();
        
        #endregion  

        #region 初始化

        public void Init()
        {
            data=PlayerDal.GetAllList().ToDic<Guid,Player>("Id");
            var  tempData = new Dictionary<string, Player>();
            foreach (var dr in dt.Rows)
            {

                tempData
            }
        }

        #endregion

        #region 方法



        #endregion
    }
}
