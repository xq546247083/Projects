/************************************************************************
* 描述:ui
*************************************************************************/
using System.Web.Mvc;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// ui
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// DIV加载
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString DivLoadingStations(this HtmlHelper htmlHelper, string id)
        {
            TagBuilder div = new TagBuilder("div");
            div.Attributes["style"] = "display: none;";
            div.Attributes["id"] = id;
            div.InnerHtml = " <div class=\"layui-layer-shade\"  style=\"z-index:99998; opacity:0.5; filter:alpha(opacity=1);\"></div><div class=\"layui-layer layui-layer-dialog layui-layer-msg layer-anim\" style=\"z-index: 99999; top: 40%; left: 45%;\"><div class=\"layui-layer-content layui-layer-padding\"><i class=\"layui-layer-ico layui-layer-ico16\"></i>加载中..</div></div>";

            return MvcHtmlString.Create(div.ToString());
        }
    }
}