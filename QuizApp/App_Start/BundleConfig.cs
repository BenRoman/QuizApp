using System.Web;
using System.Web.Optimization;

namespace QuizApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-animate.js",
                      "~/Scripts/angular-messages.js",
                      "~/Scripts/angular-aria/angular-aria.min.js",
                      "~/Scripts/angular-material/angular-material.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                      "~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                      "~/Scripts/angular-ui/datetime-picker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/common-dep").Include(
                      "~/webApp/common/ngclipboard/ngclip.min.js",
                      "~/webApp/common/ngDialog.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/managing").Include(
                      "~/webApp/quizApp.js",

                      "~/webApp/services/convertionService.js",
                      "~/webApp/services/managingService.js",

                      "~/webApp/managing/testingResultManagingController.js",
                      "~/webApp/managing/testingUrlManagingController.js",
                      "~/webApp/managing/testManagingController.js",

                      "~/webApp/managing/answer/answer.js",
                      "~/webApp/managing/question/question.js",
                      "~/webApp/managing/test/test.js",
                      "~/webApp/managing/testingResult/testingResult.js",
                      "~/webApp/managing/testingUrl/testingUrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/quiz-passing").Include(
                      "~/webApp/quizApp.js",
                      "~/webApp/common/timer.js",

                      "~/webApp/services/convertionService.js",
                      "~/webApp/services/quizPassingService.js",

                      "~/webApp/quizPassing/quizPassingController.js",
                      "~/webApp/quizPassing/questionPassing/questionPassing.js"));

            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/ngDialog.min.css",
                      "~/Content/ngDialog-theme-default.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/quiz-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-material/bootstrap-material-design.min.css",
                      "~/Content/bootstrap-material/ripples.min.css",
                      "~/Content/quizStyles.css"));

            bundles.Add(new ScriptBundle("~/bundles/material-bootstrap-js").Include(
                      "~/Content/bootstrap-material/material.min.js",
                      "~/Content/bootstrap-material/ripples.min.js"));
        }
    }
}
