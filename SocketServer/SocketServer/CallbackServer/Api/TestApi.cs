/************************************************************************
* 描述:测试API
*************************************************************************/
namespace Manage.Api
{
    /// <summary>
    /// 测试API
    /// </summary>
    public class TestApi : BaseApi
    {
        /// <summary>
        /// 具体处理
        /// </summary>
        public override void Process()
        {
            this.Response.Write("调用成功了噢:{0}" + this.RequestData["aasdasd"]);
        }

        /// <summary>
        /// 类名
        /// </summary>
        /// <returns>返回类名</returns>
        public override string GetClassName()
        {
            return "TestApi";
        }
    }
}
