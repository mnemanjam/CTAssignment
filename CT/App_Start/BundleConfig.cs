using System.Web;
using System.Web.Optimization;

namespace CT
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-hover-dropdown.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Css/jquery.combobox.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                         "~/Scripts/json2.js",
                        "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                        "~/Scripts/jquery.blockUI-{version}.js",
                        "~/Scripts/jquery.simplePagination.js",
                        "~/Scripts/jquery.uploadfile.min.js",
                        "~/Scripts/Locales/ui.datepicker-sr-SR.min.js",
                        "~/Scripts/jquery.contextmenu.r2.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.numeric.js",
                        "~/Scripts/jquery.bs_pagination.min.js",
                        "~/Scripts/Locale/bs_pagination-sr.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.getUrlParam.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/globalize/globalize.js",
                        "~/Scripts/globalize/cultures/globalize.culture.sr-Latn-RS.js",
                        "~/Scripts/scripts-1.2.0.js",
                        "~/Scripts/jquery.number.min.js",
                        "~/Scripts/jquery.cookie.js"
                       ));
        }
    }
}
