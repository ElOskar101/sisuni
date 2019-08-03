using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Sisuni.App_Start {
    public class BundleConfig {
        public static void RegisterBundles (BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/carousel.css",
                "~/Content/css/animate.css",
                "~/Content/style.css",
                "~/Content/sb-admin.min.css"
                ));
                           bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/Content/js/jquery.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/carousel.js",
                "~/Content/js/animate.js",
                "~/Content/js/custom.js",
                "~/Content/js/videobg.js",
                "~/Content/js/sb-admin-min.js"
                ));
        }
    }
}