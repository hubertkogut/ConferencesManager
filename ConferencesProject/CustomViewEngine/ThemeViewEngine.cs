using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConferencesProject.CustomViewEngine
{
    public class ThemeViewEngine : RazorViewEngine
    {
        public ThemeViewEngine(string activeThemeName )
        {
            ViewLocationFormats = new[]
            {
                "~/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            AreaViewLocationFormats = new[]
            {
                "~Areas/{2}/ViewsThemes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~Areas/{2}/ViewsThemes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
        }
    }
}
