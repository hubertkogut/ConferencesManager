using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ConferencesProject.Helpers
{
    public static class MyHelpers
    {
        public enum AjaxMethods
        {
            GET,
            POST
        }
        public static MvcHtmlString AsyncPartial(this HtmlHelper helper, string controller, string action, string previewPartialName = null, AjaxMethods method = AjaxMethods.GET, TempDataDictionary TempData = null)
        {
            string xo = $"x_{controller}_{action}";
            string js = $"function get_{controller}_{action}()" +
                            "{" +
                                    $"var d= document.getElementById('div-{controller}-{action}'); " +
                                    $"var ds= document.getElementById('div-{controller}-{action}-sh'); " +

                                    "var " + xo + " = new XMLHttpRequest();" +
                                    xo + ".onreadystatechange = function() {" +
                                        "  if (" + xo + ".readyState == XMLHttpRequest.DONE ) {" +
                                            "  if (" + xo + ".status == 200) {" +
                                                 $"d.innerHTML = " + xo + ".responseText.replace(/data-partial-refresh/ig, 'data-partial-refresh onclick=\"get_{controller}_{action}()\"');" +
                                                  $"ds.style.display='none';" +
                                                  $"d.style.display='block';" +
                                            "  }" +
                                            "else if (" + xo + ".status == 400) {" +
                                             "      alert('Error 400');" +
                                             "    }" +
                                             "else{" +
                                             "      alert('Generic error');" +
                                             "    }" +
                                         "  }" +
                                    "};" +
                                      $"ds.style.display='block';" +
                                      $"d.style.display='none';" +
                                    xo + $".open('{method}', '/{controller}/{action}', true);" +
                                    xo + ".send();" +
                             "};" +
                             $"get_{controller}_{action}();";

            MvcHtmlString StringPartial = PartialExtensions.Partial(helper, previewPartialName == null ? action + "_preview" : previewPartialName);

            if (TempData != null)
            {
                TempData["script"] += js;
                return MvcHtmlString.Create($"<div  id='div-{controller}-{action}'>" + StringPartial.ToString() + "</div>" +
                                       $"<div  id='div-{controller}-{action}-sh' style='display:none'>" + StringPartial.ToString() + "</div>"
                                       );
            }
            else
            {
                return MvcHtmlString.Create($"<div  id='div-{controller}-{action}'>" + StringPartial.ToString() + "</div>" +
                                      $"<div  id='div-{controller}-{action}-sh' style='display:none'>" + StringPartial.ToString() + "</div>" +
                                      "<script>" + js + "</script>");
            }

        }
    }
}
