using System.Web;
using System.Web.Optimization;

namespace ChicadresseSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/bootstrap-select.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/app/lib/bootstrap/dist/css/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/hover-min.css",
                      "~/Content/css/animate.min.css",
                       "~/app/lib/bootstrap-select/dist/css/bootstrap-select.css",
                      "~/Content/css/menu.css",
                      "~/app/lib/font-awesome/css/font-awesome.min.css",
                      "~/Content/css/demo.css",
                      "~/app/lib/bootstrap-carousel-touch-slider-master/bootstrap-touch-slider.css",
                      "~/Content/css/icon.css",
                      "~/Content/css/style.css",
                      "~/Content/css/responsive.css"));
        }
    }
}
