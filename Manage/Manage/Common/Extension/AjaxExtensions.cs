/************************************************************************
* 描述:ajax扩展
*************************************************************************/
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Manage
{
    public static class AjaxExtensions
    {
        /// <summary>
        /// ActionLink包装
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkPackaging(this AjaxHelper ajaxHelper, string linkText, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string mark = "mark";
            var link = ajaxHelper.ActionLink(mark, actionName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(link.ToString().Replace(mark, linkText));
        }


        /// <summary>
        /// ActionLink包装
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkPackaging(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string mark = "mark";
            var link = ajaxHelper.ActionLink(mark, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(link.ToString().Replace(mark, linkText));
        }

        /// <summary>
        /// ActionLink包装
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkPackaging(this AjaxHelper ajaxHelper, string linkText, string actionName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            string mark = "mark";
            var link = ajaxHelper.ActionLink(mark, actionName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(link.ToString().Replace(mark, linkText));
        }
    }
}