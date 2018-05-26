using System.Web;
using System.Web.Optimization;

namespace Twitter
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/assets/global/css").Include(
                    "~/Content/assets/global/css/style.css",
                    "~/Content/assets/global/css/theme.css",
                    "~/Content/assets/global/css/ui.css",
                     "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/assets/admin/layout4/css").Include(
                   "~/Content/assets/admin/layout4/css/layout.css"
                   ));
            bundles.Add(new ScriptBundle("~/Content/assets/global/plugins/modernizr").Include(
                         "~/Content/assets/global/plugins/modernizr/modernizr-2.6.2-respond-1.1.0.min.js"));

            bundles.Add(new ScriptBundle("~/Content/assets/global/plugins/jquery").Include(
                        "~/Content/assets/global/plugins/jquery/jquery-3.1.0.min.js",
                        "~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js",
                         "~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.js"
            ));
            bundles.Add(new ScriptBundle("~/Content/assets/global").Include(
                        "~/Content/assets/global/plugins/gsap/main-gsap.min.js",
                        "~/Content/assets/global/plugins/tether/js/tether.min.js",
                         "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                         "~/Content/assets/global/plugins/appear/jquery.appear.js",
                          "~/Content/assets/global/plugins/jquery-cookies/jquery.cookies.min.js",
                          "~/Content/assets/global/plugins/jquery-block-ui/jquery.blockUI.min.js",
                          "~/Content/assets/global/plugins/bootbox/bootbox.min.js",
                          "~/Content/assets/global/plugins/mcustom-scrollbar/jquery.mCustomScrollbar.concat.min.js",
                          "~/Content/assets/global/plugins/bootstrap-dropdown/bootstrap-hover-dropdown.min.js",
                          "~/Content/assets/global/plugins/charts-sparkline/sparkline.min.js",
                          "~/Content/assets/global/plugins/retina/retina.min.js",
                          "~/Content/assets/global/plugins/select2/dist/js/select2.full.min.js",
                          "~/Content/assets/global/plugins/icheck/icheck.min.js",
                          "~/Content/assets/global/plugins/backstretch/backstretch.min.js",
                          "~/Content/assets/global/plugins/bootstrap-progressbar/bootstrap-progressbar.min.js",
                          "~/Content/assets/global/plugins/charts-chartjs/Chart.min.js",
                          "~/Content/assets/global/js/builder.js",
                          "~/Content/assets/global/js/sidebar_hover.js",
                          "~/Content/assets/global/js/widgets/notes.js",
                          "~/Content/assets/global/js/quickview.js",
                          "~/Content/assets/global/js/pages/search.js",
                          "~/Content/assets/global/js/plugins.js",
                          "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                          "~/Content/assets/global/js/application.js",
                          "~/Content/assets/admin/layout4/js/layout.js",
                          "~/Content/umgScript.js"
            ));
            bundles.Add(new StyleBundle("~/Content/login").Include(
                   "~/Content/assets/global/css/style.css",
                   "~/Content/assets/global/css/ui.css",
                   "~/Content/assets/global/plugins/bootstrap-loading/lada.min.css",
                   "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css"
                   ));
            bundles.Add(new ScriptBundle("~/Content/login_js").Include(
                   "~/Content/assets/global/plugins/jquery/jquery-3.1.0.min.js",
                   "~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js",
                   "~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.js",
                   "~/Content/assets/global/plugins/gsap/main-gsap.min.js",
                   "~/Content/assets/global/plugins/tether/js/tether.min.js",
                   "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                   "~/Content/assets/global/plugins/backstretch/backstretch.min.js",
                   "~/Content/assets/global/plugins/bootstrap-loading/lada.min.js",
                    "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                   "~/Content/assets/global/js/pages/login-v1.js",
                   "~/Content/umgScript.js"
                   ));
        }
    }
}
