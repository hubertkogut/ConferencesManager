using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConferencesProject.UITests.PageObjectModels
{
    internal class LoginPage : Page
    {
        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        protected override string PageUrl => "https://localhost:44389/Account/Login";
        protected override string PageTitle => "Log in - Conferences manager";

        public ReadOnlyCollection<string> ValidationErrorMessages
        {
            get
            {
                return Driver.FindElements(
                    By.CssSelector(".field-validation-error > span"))
                        .Select(x => x.Text)
                        .ToList()
                        .AsReadOnly();
            }
        }

        public string AnotherValidationErrorMessage =>
            Driver.FindElement(By.CssSelector(".validation-summary-errors > ul > li")).Text;


        public void Login(string email, string password)
        {
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Password")).Submit();
        }

        public void ClearForm()
        {
            Driver.FindElement(By.Id("Email")).Clear();
            Driver.FindElement(By.Id("Password")).Clear();
        }

        public void Logoff()
        {
            IWebElement logInfo = Wait.Until(d => d.FindElement(By.XPath("//a[text()[contains(.,'Log')]]")));//relative Xpath
            if (logInfo.Text == "Log off")
            {
                logInfo.Click();
            }
        }

        public string HelloMessageOrRegisterLink() //to confirm if user is logged in or not
        {
            IWebElement helloMessageOrRegisterLink = Wait.Until(d => d.FindElement(By
                .XPath("//a[text()[contains(.,'Hello') or contains(.,'Register')]]")));
            return helloMessageOrRegisterLink.Text;
        }

        public override void EnsurePageLoaded() //cuz of rederiction 
        {
            bool pageHasLoaded = (Driver.Url.Contains("https://localhost:44389/Account/Login")) &&
                                 (Driver.Title == PageTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Current URL = '{Driver.Url}");
            }
        }
    }
}
