using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConferencesProject.UITests.PageObjectModels
{
    internal class Page
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public string AdminLogin = "hubkog@gmail.com";
        public string AdminPassword = "123456";

        protected virtual string PageUrl { get; }
        protected virtual string PageTitle { get; }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public virtual void EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == PageUrl) &&
                                 (Driver.Title == PageTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Current URL = '{Driver.Url}");
            }
        }
    }
}
