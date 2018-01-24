/************************************************************************
* 描述:测试API
*************************************************************************/
namespace CallbackServer.Api
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
            var name = RequestData["Name"];
            this.Response.Write($"{name}调用成功了噢!");
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
