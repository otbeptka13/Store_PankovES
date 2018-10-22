﻿using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery*"));

            bundles.Add(new StyleBundle("~/Content/css/awesome").Include(
                  "~/Content/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                 "~/Content/css/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/Content/js/bootstrap").Include(
                    "~/Content/js/bootstrap*"));
            bundles.Add(new StyleBundle("~/Content/css/my_css").Include(
                    "~/Content/css/style.css",
                    "~/Content/css/colors/dark-green.css",
                    "~/Content/css/owl.carousel.css",
                    "~/Content/css/owl.transitions.css",
                    "~/Content/css/animate.min.css"));

            bundles.Add(new ScriptBundle("~/Content/js/my_js").Include(
                        "~/Content/js/gmap3.min.js",
                        "~/Content/js/owl.carousel.min.js",
                        "~/Content/js/css_browser_selector.min.js",
                        "~/Content/js/echo.min.js",
                        "~/Content/js/wow.min.js",
                        "~/Content/js/buttons.js",
                        "~/Content/js/scripts.js",
                         "~/Content/js/custom.js"
                        ));

        }
    }
}
