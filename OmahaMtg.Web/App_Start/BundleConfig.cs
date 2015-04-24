using System.Web;
using System.Web.Optimization;

namespace OmahaMtg.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.widget.min.js",
                         "~/Scripts/metro-tab-control.js",
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/marked").Include(
                        "~/Scripts/bootstrap-markdown.js",
                      "~/Scripts/marked.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/anytimejs").Include(
                      "~/Scripts/anytime.5.0.5.min.js"));

            bundles.Add(new StyleBundle("~/Content/anytimecss").Include(
               "~/Content/anytime.5.0.5.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/metro-bootstrap.min.css",
                      "~/Content/metro-bootstrap-responsive.min.css",
                     // "~/Content/iconFont.min.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-markdown.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/textillate").Include(
          "~/Scripts/jquery.lettering.js", "~/Scripts/jquery.textillate.js"));

            bundles.Add(new StyleBundle("~/Content/textillate").Include(
               "~/Content/animate.min.css"
                ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
