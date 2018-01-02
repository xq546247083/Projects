/************************************************************************
* 描述:绑定js
*************************************************************************/
using System.Web.Optimization;

namespace Moqikaka.GameManage
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //select2 css
            bundles.Add(new StyleBundle("~/Content/select2-master/css").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/select2-master/css/select2.min.css"
            ));
            //select2 js
            bundles.Add(new ScriptBundle("~/Content/select2-master/js/select2").Include(
                "~/Content/bootstrap/js/bootstrap.min.js",
                "~/Content/select2-master/js/select2.full.min.js"
            ));
            //datetimepicker
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap/css/bootstrap-datetimepicker.min.css"
            ));
            //datetimepicker
            bundles.Add(new ScriptBundle("~/Content/select2-master/js/select2").Include(
                "~/Content/bootstrap/js/bootstrap.min.js",
                "~/Content/bootstrap/js/bootstrap-datetimepicker.js",
                "~/Content/bootstrap/js/bootstrap-datetimepicker.zh-CN.js"
            ));
            bundles.Add(new ScriptBundle("~/Content/custom/css").Include(
                "~/Content/custom.css"
            ));
        }
    }
}
