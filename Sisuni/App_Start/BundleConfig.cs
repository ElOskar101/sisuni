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
                "~/Content/style.css"
                ));

           // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
             //          "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/Content/js/jquery.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/carousel.js",
                "~/Content/js/animate.js",
                "~/Content/js/custom.js",
                "~/Content/js/videobg.js",
                "~/Content/js/validate/jquery.validate*"
                ));

           
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css")
            .Include(
            "~/Content/admin/css/bootstrap.min.css",
            "~/Content/css/font-awesome.min.css",
            "~/Content/admin/css/ionicons.min.css",
            "~/Content/admin/css/AdminLTE.min.css",
            "~/Content/admin/css/_all-skins.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                .Include(
                "~/Content/admin/js/jquery.min.js",
                "~/Content/admin/js/bootstrap.min.js",
                //"~/Content/admin/js/jquery.slimscroll.min.js",
                //"~/Content/admin/js/fastclick.js",
                "~/Content/admin/js/adminlte.min.js"
                //"~/Content/admin/js/demo.js"

                ));
           // bundles.Add(new ScriptBundle("~/bundles/js").Include(
             //         "~/Content/js/validate/jquery-{version}.js"));

        }
    }
}