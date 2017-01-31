using System.Web;
using System.Web.Optimization;

namespace VMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/ExtendedNav.css"));

            bundles.Add(new ScriptBundle("~/bundles/multiple-select").Include(
                      "~/Scripts/jquery1.11.min.js",
                      "~/Scripts/multiple-select.js"));
            bundles.Add(new StyleBundle("~/Content/multiple-select-css").Include(
                      "~/Content/multiple-select.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                      "~/Scripts/bootstrap-datepicker.min.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap-datepickercss").Include(
                      "~/Content/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/sif").Include(
                      "~/Scripts/Shared/sif.js"));

            bundles.Add(new StyleBundle("~/bundles/enlistment-css").Include(
                "~/Content/Members/Enlistment.css"));
            bundles.Add(new ScriptBundle("~/bundles/enlistment-js").Include(
                "~/Scripts/Members/Enlistment.js"));
        }
    }
}
