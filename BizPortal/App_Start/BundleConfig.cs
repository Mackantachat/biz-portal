using System.Collections.Generic;
using System.Web.Optimization;
using BizPortal.Utils.Extensions;


namespace BizPortal
{
    class PassthruBundleOrderer : IBundleOrderer
    {
        IEnumerable<BundleFile> IBundleOrderer.OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            //file
            return files;
        }
    }

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleImagePathBundle("~/Bundles/NewBizV2/css").Include(
                "~/Content/Theme/biz-v2/vendor/photoswipe/photoswipe.css",
                "~/Content/Theme/biz-v2/vendor/photoswipe/default-skin/default-skin.css",
                "~/Content/font-awesome/css/font-awesome.min.css",
                "~/Content/SingleForm-frontis/v2/css/styles.css",
                "~/Content/SingleForm-frontis/v2/css/custom.css"
            ));

            bundles.Add(new ScriptBundle("~/Bundles/NewBizV2Bs/js").Include(
                "~/Scripts/bootstrap.js"
            ));

            bundles.Add(new StyleImagePathBundle("~/Bundles/NewBizV2Bs/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Theme/biz-v2/vendor/photoswipe/photoswipe.css",
                "~/Content/Theme/biz-v2/vendor/photoswipe/default-skin/default-skin.css",
                "~/Content/font-awesome/css/font-awesome.min.css",
                "~/Content/SingleForm-frontis/v2/css/styles.css",
                "~/Content/SingleForm-frontis/v2/css/custom.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/NewBizV2/js").Include(
                "~/Content/SingleForm-frontis/v2/scripts/main.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Theme/biz-v2/vendor/jquery.js"
                        //"~/Scripts/jquery-{version}.js")
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.custom.js",
                        "~/Scripts/expressive.annotations.validate.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Content/Theme/vendor/jquery.appear/jquery.appear.min.js",
                        "~/Content/Theme/vendor/jquery.easing/jquery.easing.min.js",
                        "~/Content/Theme/vendor/jquery-cookie/jquery-cookie.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",

                        "~/Content/Theme/vendor/common/common.min.js",
                        "~/Content/Theme/vendor/jquery.stellar/jquery.stellar.min.js",
                        "~/Content/Theme/vendor/jquery.easy-pie-chart/jquery.easy-pie-chart.min.js",
                        "~/Content/Theme/vendor/jquery.gmap/jquery.gmap.min.js",
                        "~/Content/Theme/vendor/jquery.lazyload/jquery.lazyload.min.js",
                        "~/Content/Theme/vendor/isotope/jquery.isotope.min.js",
                        "~/Content/Theme/vendor/owl.carousel/owl.carousel.min.js",
                        "~/Content/Theme/vendor/magnific-popup/jquery.magnific-popup.min.js",
                        "~/Content/Theme/vendor/vide/vide.min.js",
                        "~/Content/Theme/js/theme.js",
                        "~/Content/Theme/vendor/rs-plugin/js/jquery.themepunch.tools.min.js",
                        "~/Content/Theme/vendor/rs-plugin/js/jquery.themepunch.revolution.min.js",
                        "~/Content/Theme/vendor/circle-flip-slideshow/js/jquery.flipshow.min.js",
                        "~/Content/Theme/js/views/view.home.js",
                        "~/Scripts/datatables/jquery.dataTables.min.js",
                        //"~/Content/Theme/vendor/datatables.js/datatables.min.js",
                        "~/Content/Theme/js/custom.js",
                        "~/Content/Theme/js/theme.init.js",
                        "~/scripts/jquery.bootpag.min.js",
                        "~/Scripts/sweetalert/sweetalert.min.js",
                        "~/Scripts/toastr/toastr.min.js"));

            bundles.Add(new StyleImagePathBundle("~/Content/bootstrap-css").Include(
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleImagePathBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/flag/flags.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",

                      "~/Content/Theme/vendor/simple-line-icons/css/simple-line-icons.min.css",

                      "~/Content/Theme/vendor/owl.carousel/assets/owl.carousel.min.css",
                      "~/Content/Theme/vendor/owl.carousel/assets/owl.theme.default.min.css",
                      "~/Content/Theme/vendor/magnific-popup/magnific-popup.min.css",

                      "~/Content/Theme/vendor/select2/css/select2.min.css",
                      //"~/Content/Theme/notebook/js/select2/theme.css",

                      "~/Content/Theme/vendor/rs-plugin/css/settings.css",
                      "~/Content/Theme/vendor/rs-plugin/css/layers.css",
                      "~/Content/Theme/vendor/rs-plugin/css/navigation.css",
                      "~/Content/Theme/vendor/circle-flip-slideshow/css/component.css",

                      "~/Scripts/datatables/datatables.css",
                      "~/Scripts/ladda/css/ladda.min.css",
                      "~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",

                      "~/Content/Theme/css/theme.css",
                      "~/Content/Theme/css/theme-elements.css",
                      "~/Content/Theme/css/theme-blog.css",
                      "~/Content/Theme/css/theme-shop.css",
                      "~/Content/Theme/css/theme-animate.css",
                      "~/Content/Theme/css/skins/skin-corporate-4.css",
                      "~/Scripts/sweetalert/sweetalert.css",
                      "~/Scripts/toastr/toastr.min.css",
                      "~/Content/Theme/css/custom.css",

                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleImagePathBundle("~/Bundles/App/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/jquery-ui/jquery-ui.min.css",
                      "~/Content/flag/flags.css",
                      "~/Content/Theme/vendor/font-awesome/css/font-awesome.min.css",
                      "~/Content/Theme/vendor/simple-line-icons/css/simple-line-icons.min.css",
                      "~/Content/Theme/vendor/owl.carousel/assets/owl.carousel.min.css",
                      "~/Content/Theme/vendor/owl.carousel/assets/owl.theme.default.min.css",
                      "~/Content/Theme/vendor/magnific-popup/magnific-popup.min.css",
                      "~/Content/Theme/vendor/rs-plugin/css/settings.css",
                      "~/Content/Theme/vendor/rs-plugin/css/layers.css",
                      "~/Content/Theme/vendor/rs-plugin/css/navigation.css",
                      "~/Content/Theme/vendor/circle-flip-slideshow/css/component.css",
                      //"~/Scripts/datatables/datatables.css",
                      "~/Content/Theme/vendor/datatables.js/datatables.min.css",
                      "~/Content/Theme/vendor/select2/css/select2.min.css",
                      "~/Content/Theme/css/theme.css",
                      "~/Content/Theme/css/theme-elements.css",
                      "~/Content/Theme/css/theme-blog.css",
                      "~/Content/Theme/css/theme-shop.css",
                      "~/Content/Theme/css/theme-animate.css",
                      "~/Content/Theme/css/skins/skin-corporate-4.css",
                      "~/Content/Theme/css/custom.css",
                      "~/Content/bootstrap-datepicker-thai/datepicker.css",
                      "~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",
                      "~/Scripts/sweetalert/sweetalert.css",
                      "~/Scripts/toastr/toastr.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleImagePathBundle("~/Bundles/AppV2/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Theme/biz-v2/css/custom.css",
                "~/Content/Theme/biz-v2/css/ie.css",
                "~/Content/Theme/biz-v2/css/theme-animate.css",
                "~/Content/Theme/biz-v2/css/theme-blog.css",
                "~/Content/Theme/biz-v2/css/theme-elements.css",
                "~/Content/Theme/biz-v2/css/theme-responsive.css",
                "~/Content/Theme/biz-v2/css/theme-shop.css",
                "~/Content/Theme/biz-v2/css/theme.css",
                "~/Content/Theme/biz-v2/css/custom-merged.css",
                //"~/Content/Theme/biz-v2/css/custom-checkbox-radio.css",
                "~/Content/Theme/biz-v2/css/fonts/font-awesome/css/font-awesome.css",
                "~/Content/Theme/biz-v2/css/fonts/font-awesome/css/font-awesome.min.css",
                "~/Content/Theme/biz-v2/css/skins/blue.css",
                "~/Content/Theme/biz-v2/vendor/owl-carousel/owl.carousel.css",
                "~/Content/Theme/biz-v2/vendor/owl-carousel/owl.theme.css",
                "~/Content/Theme/biz-v2/vendor/magnific-popup/magnific-popup.css",
                "~/Content/Theme/biz-v2/vendor/SmartWizard/smart_wizard.min.css",
                "~/Content/Theme/biz-v2/vendor/SmartWizard/smart_wizard_theme_circles.min.css",
                "~/Content/Theme/biz-v2/vendor/SmartWizard/smart_wizard_theme_circles_custom.css",
                "~/Content/Theme/vendor/select2/css/select2.min.css",
                "~/Content/bootstrap-datepicker-thai/datepicker.css",
                "~/Scripts/datatables/datatables.css",
                "~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",
                "~/Scripts/sweetalert/sweetalert.css",
                "~/Scripts/toastr/toastr.min.css",
                "~/Scripts/ladda/css/ladda.min.css",
                //--//
                "~/Content/SingleForm-frontis/style.css"
                ));


            var styles = new StyleImagePathBundle("~/Bundles/Backend/Theme/Css").Include(
                "~/Content/Theme/notebook/css/bootstrap.css",
                "~/Content/Theme/notebook/css/animate.css",
                "~/Content/Theme/notebook/css/font-awesome.min.css",
                "~/Content/Theme/notebook/css/font.css",
                "~/Content/Theme/notebook/css/app.css",
                "~/Content/Theme/notebook/js/calendar/bootstrap_calendar.css",
                "~/Content/Theme/vendor/select2/css/select2.min.css",
                //"~/Content/Theme/notebook/js/select2/select2.css",
                //"~/Content/Theme/notebook/js/select2/theme.css",
                "~/Content/Theme/notebook/js/datepicker/datepicker3.css",
                "~/Content/Theme/notebook/js/timepicker/jquery.timepicker.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/Theme/notebook/js/intro/introjs.css",
                "~/Content/flag/flags.css",
                "~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",
                "~/Content/site.css",
                "~/Styles/backend.css"
                );
            styles.Orderer = new PassthruBundleOrderer(); // Fixed Order
            bundles.Add(styles);


            var baseStyles = new StyleImagePathBundle("~/Bundles/Backend/Styles").Include(
                //"~/Scripts/multi-select/css/multi-select.css",
                "~/Scripts/summernote/summernote.css",
                //"~/Styles/cropper.css",
                //"~/Styles/image.css",
                //"~/Scripts/owl-carousel/owl.theme.css",
                //"~/Scripts/owl-carousel/owl.carousel.css",
                //"~/Styles/site.css",
                //"~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",
                "~/Scripts/datatables/datatables.css",
                "~/Content/Theme/notebook/js/chosen/chosen.css",
                "~/Scripts/sweetalert/sweetalert.css",
                "~/Scripts/toastr/toastr.min.css",
                "~/Scripts/ladda/css/ladda.min.css"
               //"~/Content/Theme/vendor/select2/css/select2.min.css"
               );
            baseStyles.Orderer = new PassthruBundleOrderer(); // Fixed Order
            bundles.Add(baseStyles);

            var loginStyles = new StyleImagePathBundle("~/Bundles/Login/Css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Theme/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/Theme/css/theme.css",
                "~/Content/Theme/css/theme-elements.css",
                "~/Content/Theme/css/theme-animate.css",
                "~/Content/Theme/css/skins/skin-corporate-4.css",
                "~/Content/Theme/css/custom.css",
                "~/Scripts/sweetalert/sweetalert.css",
                "~/Content/site.css"
            );
            loginStyles.Orderer = new PassthruBundleOrderer(); // Fixed Order
            bundles.Add(loginStyles);

            var newBizUI = new StyleImagePathBundle("~/Bundles/NewBizUI/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Theme/biz-v2/css/ie.css",
                "~/Content/Theme/biz-v2/css/theme-animate.css",
                "~/Content/Theme/biz-v2/css/theme-blog.css",
                "~/Content/Theme/biz-v2/css/theme-elements.css",
                "~/Content/Theme/biz-v2/css/theme-responsive.css",
                "~/Content/Theme/biz-v2/css/theme-shop.css",
                "~/Content/Theme/biz-v2/css/theme.css",
                "~/Content/Theme/biz-v2/css/fonts/font-awesome/css/font-awesome.css",
                "~/Content/Theme/biz-v2/css/fonts/font-awesome/css/font-awesome.min.css",
                "~/Content/Theme/biz-v2/css/skins/blue.css",
                "~/Content/Theme/biz-v2/vendor/owl-carousel/owl.carousel.css",
                "~/Content/Theme/biz-v2/vendor/owl-carousel/owl.theme.css",
                "~/Content/Theme/biz-v2/vendor/magnific-popup/magnific-popup.css",
                "~/Content/Theme/biz-v2/vendor/photoswipe/photoswipe.css",
                "~/Content/Theme/biz-v2/vendor/photoswipe/default-skin/default-skin.css",
                "~/Content/Theme/vendor/select2/css/select2.min.css",
                "~/Scripts/datatables/datatables.css",
                "~/Scripts/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css",
                "~/Scripts/ladda/css/ladda.min.css",
                "~/Scripts/sweetalert/sweetalert.css",
                "~/Scripts/toastr/toastr.min.css",
                "~/Content/Theme/biz-v2/css/custom.css",
                "~/Content/SingleForm-frontis/single-form-styles.css"
                );
            newBizUI.Orderer = new PassthruBundleOrderer();
            bundles.Add(newBizUI);

            var newBizJs = new ScriptBundle("~/Bundles/NewBizUI/scripts").Include(
                //"~/Content/Theme/biz-v2/vendor/jquery.js",
                "~/Content/Theme/biz-v2/vendor/jquery.easing.js",
                "~/Content/Theme/biz-v2/vendor/jquery.appear.js",
                "~/Content/Theme/biz-v2/vendor/rs-plugin/js/jquery.themepunch.plugins.min.js",
                "~/Content/Theme/biz-v2/vendor/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/Content/Theme/biz-v2/vendor/owl-carousel/owl.carousel.js",
                "~/Content/Theme/biz-v2/vendor/circle-flip-slideshow/js/jquery.flipshow.js",
                "~/Content/Theme/biz-v2/vendor/magnific-popup/magnific-popup.js",
                "~/Content/Theme/biz-v2/vendor/photoswipe/photoswipe.js",
                "~/Content/Theme/biz-v2/vendor/photoswipe/photoswipe-ui-default.js",
                "~/Content/Theme/biz-v2/vendor/readmorejs/readmore.js",
                "~/Content/Theme/biz-v2/vendor/jquery.mobile-events/jquery.mobile-events.min.js",
                "~/Content/Theme/vendor/select2/js/select2.full.js",
                //"~/Content/Theme/biz-v2/vendor/jquery.validate.js",
                "~/Content/Theme/biz-v2/js/plugins.js",
                "~/Content/Theme/biz-v2/js/views/view.home.js",
                "~/Content/Theme/biz-v2/js/theme.js",
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Scripts/ladda/spin.js",
                "~/Scripts/ladda/ladda.js",
                "~/Scripts/ladda/ladda.jquery.js",
                "~/Scripts/handlebars-v4.0.5.js",
                "~/Scripts/sweetalert/sweetalert.min.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/plupload/js/plupload.full.min.js",
                "~/Scripts/plupload/i18n/th_TH.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/SingleForm-frontis/main.js",
                "~/Scripts/base.js"
                );
            newBizJs.Orderer = new PassthruBundleOrderer();
            bundles.Add(newBizJs);

            var corejs = new ScriptBundle("~/Bundles/Backend/Theme/Script/Core").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-check-all.js",
                "~/Scripts/jquery.form.js",
                "~/Content/Theme/notebook/js/jquery-ui-1.10.3.custom.min.js",
                "~/Content/Theme/notebook/js/bootstrap.js",
                "~/Content/Theme/notebook/js/app.js",
                "~/Content/Theme/notebook/js/slimscroll/jquery.slimscroll.min.js",
                "~/Content/Theme/notebook/js/charts/easypiechart/jquery.easy-pie-chart.js",
                "~/Content/Theme/notebook/js/app.plugin.js",
                //"~/Content/Theme/notebook/js/select2/select2.min.js",
                "~/Content/Theme/vendor/select2/js/select2.full.js",
                "~/Content/Theme/notebook/js/datepicker/bootstrap-datepicker.js",
                "~/Content/Theme/notebook/js/datepicker/plugin-datepicker-be.js",
                "~/Content/Theme/notebook/js/datepicker/locales/bootstrap-datepicker.th.js",
                "~/Content/Theme/notebook/js/timepicker/jquery.timepicker.js",
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Content/Theme/notebook/js/intro/intro.min.js",
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/plupload/js/plupload.full.min.js",
                "~/Scripts/plupload/i18n/th_TH.js",
                "~/Scripts/plupload/js/jquery.ui.plupload/jquery.ui.plupload.min.js",
                "~/Scripts/sweetalert/sweetalert.min.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/sortable.js"

            //"~/Scripts/moment-with-locales-custom.js",
            //"~/Scripts/flash.js",
            //"~/Scripts/intro.js",
            //"~/Scripts/ckeditor/ckeditor.js",
            //"~/Scripts/ckeditor/adapters/jquery.js"
            //"~/Scripts/owl-carousel/owl.carousel.js",
            //"~/Content/Theme/notebook/js/chosen/chosen.jquery.js",
            //"~/Scripts/wookmark-jQuery-master/imagesloaded.pkgd.js",
            //"~/Scripts/wookmark-jQuery-master/wookmark.js",
            //"~/Scripts/wookmark-jQuery-master/pinterest_grid.js",
            //"~/Scripts/jquery.endless-scroll.js",
            //"~/Scripts/accounting.js"
            );
            corejs.Orderer = new PassthruBundleOrderer();
            bundles.Add(corejs);

            // [SCRIPT] LT IE9
            var ie9js = new ScriptBundle("~/Bundles/Backend/Scripts/ltie9").Include(
                    "~/content/Theme/notebook/js/ie/html5shiv.js",
                    "~/content/Theme/notebook/js/ie/respond.min.js",
                    "~/content/Theme/notebook/js/ie/excanvas.js"
                );
            ie9js.Orderer = new PassthruBundleOrderer();
            bundles.Add(ie9js);


            var baseScript = new ScriptBundle("~/Bundles/Backend/Scripts/Base").Include(
                //"~/Scripts/multi-select/plugin/jquery.quicksearch.js",
                //"~/Scripts/multi-select/js/jquery.multi-select.js",
                "~/Scripts/summernote/summernote.js",
                //"~/Scripts/summernote/summernote-cleaner.js",
                "~/Scripts/ladda/spin.js",
                "~/Scripts/ladda/ladda.js",
                "~/Scripts/ladda/ladda.jquery.js",
                "~/Scripts/handlebars-v4.0.5.js",
                //"~/Scripts/cropper.js",
                "~/Scripts/toastr/toastr.min.js",
                "~/Scripts/base.js"
            );
            baseScript.Orderer = new PassthruBundleOrderer(); // Fixed Order
            bundles.Add(baseScript);

            var fontendBaseScript = new ScriptBundle("~/Bundles/Scripts/Base").Include(
                "~/Content/Theme/vendor/select2/js/select2.full.js",
                "~/Scripts/ladda/spin.js",
                "~/Scripts/ladda/ladda.js",
                "~/Scripts/ladda/ladda.jquery.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/plupload/js/plupload.full.min.js",
                "~/Scripts/plupload/i18n/th_TH.js",
                "~/Scripts/base.js"
            );
            baseScript.Orderer = new PassthruBundleOrderer(); // Fixed Order
            bundles.Add(fontendBaseScript);

            var appBaseScript = new ScriptBundle("~/Bundles/App/Script").Include(
                "~/Scripts/jquery-ui/jquery-ui.min.js",
                "~/Scripts/ladda/spin.js",
                "~/Scripts/ladda/ladda.js",
                "~/Scripts/ladda/ladda.jquery.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/plupload/js/plupload.full.min.js",
                "~/Scripts/plupload/i18n/th_TH.js",
                "~/Scripts/plupload/js/jquery.ui.plupload/jquery.ui.plupload.min.js",
                "~/Scripts/sweetalert/sweetalert.min.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/jquery.scrollTo.min.js",
                "~/Content/Theme/vendor/select2/js/select2.full.js",
                "~/Scripts/handlebars-v4.0.5.js",
                "~/Scripts/moment-with-locales.min.js",
                //"~/Scripts/arcgis-3.17.js",
                "~/Scripts/bootstrap-datepicker-thai/bootstrap-datepicker.js",
                "~/Scripts/bootstrap-datepicker-thai/bootstrap-datepicker-thai.js",
                "~/Scripts/bootstrap-datepicker-thai/locales/bootstrap-datepicker.th.js",
                //"~/Scripts/jquery.waypoints.min.js",
                "~/Scripts/cleave.min.js",
                  "~/Scripts/base.js"
            );
            appBaseScript.Orderer = new PassthruBundleOrderer();
            bundles.Add(appBaseScript);

            var appV2BaseScript = new ScriptBundle("~/Bundles/AppV2/Script").Include(
                "~/Scripts/jquery-ui/jquery-ui.min.js",
                "~/Scripts/ladda/spin.js",
                "~/Scripts/ladda/ladda.js",
                "~/Scripts/ladda/ladda.jquery.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/plupload/js/plupload.full.min.js",
                "~/Scripts/plupload/i18n/th_TH.js",
                "~/Scripts/sweetalert/sweetalert.min.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/jquery.scrollTo.min.js",
                "~/Content/Theme/vendor/select2/js/select2.full.js",
                "~/Scripts/handlebars-v4.0.5.js",
                "~/Scripts/moment-with-locales.min.js",
                //"~/Scripts/arcgis-3.17.js",
                "~/Scripts/jquery.mask.js",
                "~/Scripts/jquery.repeater.js",
                "~/Scripts/jquery.serialize-object.js",
                "~/Scripts/bootstrap-datepicker-thai/bootstrap-datepicker.js",
                "~/Scripts/bootstrap-datepicker-thai/bootstrap-datepicker-thai.js",
                "~/Scripts/bootstrap-datepicker-thai/locales/bootstrap-datepicker.th.js",
                "~/Content/Theme/biz-v2/js/plugins.js",
                "~/Content/Theme/biz-v2/js/theme.js",
                "~/Content/Theme/biz-v2/js/custom.js",
                "~/Scripts/SingleForm-frontis/main.js",
                "~/Scripts/cleave.min.js",
                "~/Scripts/base.js"
            );
            appBaseScript.Orderer = new PassthruBundleOrderer();
            bundles.Add(appV2BaseScript);

            bundles.Add(new StyleBundle("~/Content/SmartQuizCss").Include(
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/SmartQuiz/styles.css"));
            bundles.Add(new ScriptBundle("~/bundles/SmartQuiz").Include(
                        //"~/Scripts/SmartQuiz/main.js",
                        "~/Scripts/jquery.serialize-object.js",
                        "~/Scripts/SmartQuiz/SmartQuizFill.js*"
                        ));
            var permitScript = new ScriptBundle("~/Bundles/AppV2/Script/PermitSummary").Include(
                "~/Content/assets/js/permitSummary.js"
            );
            permitScript.Orderer = new PassthruBundleOrderer();
            bundles.Add(permitScript);


            bundles.Add(new ScriptBundle("~/Bundles/Scripts/sweetalert").Include(
                    "~/Scripts/sweetalert/sweetalert.min.js"
                ));
            bundles.Add(new StyleBundle("~/Bundles/Style/sweetalert").Include(
                    "~/Scripts/sweetalert/sweetalert.css"
                ));

            bundles.Add(new ScriptBundle("~/Bundles/Scripts/sweetmodal").Include(
                    "~/Scripts/sweetmodal/jquery.sweet-modal.min.js"
                ));
            bundles.Add(new StyleBundle("~/Bundles/Style/sweetmodal").Include(
                    "~/Scripts/sweetmodal/jquery.sweet-modal.min.css",
                    "~/Content/custom-sweet-modal.css"
                ));

            var dualListBox = new ScriptBundle("~/Bundles/Scripts/DualListBox").Include(
               "~/Content/bootstrap-duallistbox/jquery.bootstrap-duallistbox.js"
            );
            dualListBox.Orderer = new PassthruBundleOrderer();
            bundles.Add(dualListBox);

            bundles.Add(new StyleBundle("~/Bundles/Style/DualListBox").Include(
               "~/Content/bootstrap-duallistbox/bootstrap-duallistbox.min.css"
            ));

            var charting = new ScriptBundle("~/Bundles/Backend/Chart/Script").Include(
                "~/Scripts/node_modules/chart.js.modified/dist/Chart.js",
                "~/Scripts/select2.multi-checkboxes.js",
                "~/Scripts/select2-develop/dist/js/select2.full.min.js"
            );
            bundles.Add(charting);

            var chartingStyle = new StyleBundle("~/Bundles/Backend/Chart/Style").Include(
                "~/Content/BackOffice-Report/backoffice-report.css"
            );
            bundles.Add(chartingStyle);
        }
    }
}
