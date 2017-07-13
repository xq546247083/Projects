/************************************************************************
* 标题: 测试用
* 描述: 测试用
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using GameServer.DAL;
using System;

namespace GameServer.BLL
{
    using Tool.Log;

    /// <summary>
    /// 测试所用类
    /// </summary>
    public  class TestBLL : IConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //测试写日志傻吊
            Log.Write("dasdas", LogType.Error);

            Log.ZipLog(DateTime.Now);



            var sdas2 = PlayerDal.Insert(Guid.NewGuid(), "546247083", "xq", "123456", false, DateTime.Now);
            var sdas561 = PlayerDal.Insert(Guid.NewGuid(), "5462470831", "xq", "123456", false, DateTime.Now);
            var sdas = PlayerDal.GetAllList();

            var sdas1 = PlayerDal.Update(Guid.Parse(sdas.Rows[0][0].ToString()), "546247083t", "xqt", "123456", false, DateTime.Now);
            var sdas3 = PlayerDal.GetList(Guid.Parse(sdas.Rows[0][0].ToString()));
            var sdas4 = PlayerDal.Delete(Guid.Parse(sdas.Rows[0][0].ToString()));
        }
    }
}
