﻿using System.Web;
using System.Web.Optimization;

namespace ChicadresseSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/hover-min.css",
                      "~/Content/css/animate.min.css",
                      "~/Content/css/bootstrap-select.min.css",
                      "~/Content/css/menu.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/demo.css",
                      "~/Content/css/bootstrap-touch-slider.css",
                      "~/Content/css/icon.css",
                      "~/Content/css/style.css",
                      "~/Content/css/responsive.css"));
        }
    }
}