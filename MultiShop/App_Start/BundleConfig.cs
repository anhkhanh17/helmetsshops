using System.Web;
using System.Web.Optimization;
using MultiShop.Utils;

namespace MultiShop
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/js/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/nn-style.css",
                      "~/Content/css/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/LTE211/jquery").Include(
                        "~/Themes/Admin/LTE/211/plugins/jQuery/jQuery-2.1.4.min.js",
                         "~/Themes/Admin/LTE/211/jQueryUI/1.11.2/jquery-ui.min.js",
                        "~/Themes/Admin/LTE/211/bootstrap/js/bootstrap.min.js",
                        "~/Themes/Admin/LTE/211/raphael/2.1.0/raphael-min.js",
                        "~/Themes/Admin/LTE/211/plugins/morris/morris.min.js",
                        "~/Themes/Admin/LTE/211/plugins/sparkline/jquery.sparkline.min.js",
                        "~/Themes/Admin/LTE/211/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                        "~/Themes/Admin/LTE/211/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                        "~/Themes/Admin/LTE/211/plugins/knob/jquery.knob.js",
                        "~/Themes/Admin/LTE/211/moment/2.10.0/moment.min.js",
                        "~/Themes/Admin/LTE/211/plugins/daterangepicker/daterangepicker.js",
                        "~/Themes/Admin/LTE/211/plugins/datepicker/bootstrap-datepicker.js",
                        "~/Themes/Admin/LTE/211/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                        "~/Themes/Admin/LTE/211/plugins/slimScroll/jquery.slimscroll.min.js",
                        //"~/Themes/Admin/LTE/211/plugins/fastclick/fastclick.min.js",
                        "~/Themes/Admin/LTE/211/dist/js/app.min.js",
                        //"~/Themes/Admin/LTE/211/dist/js/pages/dashboard.js",
                        "~/Scripts/ckeditor/ckeditor.js",
                        "~/Themes/Admin/LTE/211/plugins/iCheck/icheck.min.js",
                        "~/Themes/Admin/LTE/211/dist/js/demo.js"

                       ).ForceOrdered());
            bundles.Add(new StyleBundle("~/Jtable/css").Include(
                     "~/Scripts/DataTable/media/css/jquery.dataTables.min.css",
                     "~/Scripts/DataTable/extensions/colvis/css/dataTables.colvis.jqueryui.css",
                    "~/Scripts/DataTable/extensions/colvis/css/dataTables.colVis.min.css",
                     "~/Scripts/DataTable/extensions/ColReorder/css/dataTables.colReorder.min.css",
                     "~/Scripts/DataTable/extensions/TableTools/css/dataTables.tableTools.min.css"

                    ).ForceOrdered());

            bundles.Add(new ScriptBundle("~/Jtable/jquery").Include(
               "~/Scripts/DataTable/media/js/jquery.dataTables.min.js",
               "~/Scripts/DataTable/extensions/colvis/js/dataTables.colVis.min.js",
                "~/Scripts/DataTable/extensions/ColReorder/js/dataTables.colReorder.min.js",
                 "~/Scripts/DataTable/extensions/TableTools/js/dataTables.tableTools.min.js"
              ).ForceOrdered());

            bundles.Add(new StyleBundle("~/LTE211/css").Include(
                     "~/Themes/Admin/LTE/211/bootstrap/css/bootstrap.min.css",
                     "~/Themes/Admin/LTE/211/font-awesome/4.3.0/css/font-awesome.min.css",
                     "~/Themes/Admin/LTE/211/ionicons/2.0.1/css/ionicons.min.css",
                     "~/Themes/Admin/LTE/211/dist/css/AdminLTE.min.css",
                     "~/Themes/Admin/LTE/211/dist/css/skins/_all-skins.min.css",
                     "~/Themes/Admin/LTE/211/plugins/iCheck/flat/blue.css",
                     "~/Themes/Admin/LTE/211/plugins/morris/morris.css",
                     "~/Themes/Admin/LTE/211/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                     "~/Themes/Admin/LTE/211/plugins/datepicker/datepicker3.css",
                     "~/Themes/Admin/LTE/211/plugins/daterangepicker/daterangepicker-bs3.css",
                     "~/Themes/Admin/LTE/211/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                     "~/Themes/Admin/LTE/211/plugins/iCheck/square/blue.css",
                     "~/Content/AdminSite.css"
                     ).ForceOrdered());


            bundles.Add(new StyleBundle("~/BootstrapValidator/css").Include(
             //"~/Scripts/BootstrapValidator/css/bootstrapValidator.min.css"
            ));
            bundles.Add(new ScriptBundle("~/BootstrapValidator/jquery").Include(
              "~/Scripts/BootstrapValidator/js/bootstrapValidator.min.js"

              ));
            bundles.Add(new StyleBundle("~/Helmet/css").Include(
                      "~/Themes/User/Helmet/css/bootstrap.css",
                      "~/Themes/User/Helmet/css/nn-style.css",
                      "~/Themes/User/Helmet/css/jquery-ui.css",
                      "~/Themes/User/Helmet/css/bootstrap-theme.css",
                      "~/Themes/User/Helmet/css/bootstrap-theme.css.map",
                      "~/Themes/User/Helmet/css/bootstrap-theme.min.css",
                      "~/Themes/User/Helmet/cssj/bootstrap.css.map",
                      "~/Themes/User/Helmet/css/bootstrap.min.css").ForceOrdered());

            bundles.Add(new ScriptBundle("~/Helmet/js").Include(
                      "~/Themes/User/Helmet/js/bootstrap.js",
                      "~/Themes/User/Helmet/js/jquery-1.10.2.js",
                      "~/Themes/User/Helmet/js/jquery-1.10.2.min.js",
                      "~/Themes/User/Helmet/js/jquery-1.10.2.min.map",
                      "~/Themes/User/Helmet/js/jquery-1.7.1.min.js",
                      "~/Themes/User/Helmet/js/jquery-scrollbox.jquery.json",
                      "~/Themes/User/Helmet/js/jquery-ui-1.8.16.custom.min.js",
                      "~/Themes/User/Helmet/js/jquery-ui.js",
                      "~/Themes/User/Helmet/js/jquery-ui.min.js",
                      "~/Themes/User/Helmet/js/jquery.scrollbox.js").ForceOrdered());

        }
    }
}