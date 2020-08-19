using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConferencesProject.UITests.PageObjectModels
{
    internal class TalksPage : Page
    {
        protected override string PageUrl => "https://localhost:44389/Talks";
        protected override string PageTitle => "Talks list - Conferences manager";

        public TalksPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void ClickCreateTalkLink()
        {
            IWebElement createLink = Driver.FindElement(By.LinkText("Create New"));
            createLink.Click();
        }

        public void ClickDeleteFirstTalkLink()
        {
            IWebElement deleteLink = Driver.FindElement(By.LinkText("Delete"));
            deleteLink.Click();
        }

        public IReadOnlyCollection<string> GetTalksList()
        {
            IReadOnlyCollection<IWebElement> talksCollection = Driver.FindElements(By.ClassName("TalksListItem"));

            var talksCollectionList = new List<string>();

            foreach (IWebElement webElement in talksCollection)
            {
                talksCollectionList.Add(webElement.Text);
            }

            return talksCollectionList;
        }

        public void FillCreateTalkFormAndSubmit(string title, string talkAbstract)
        {
            Driver.FindElement(By.Id("Title")).SendKeys(title);
            Driver.FindElement(By.Id("Abstract")).SendKeys(talkAbstract);
            Driver.FindElement(By.Id("Abstract")).Submit();
        }

        public void ClickDeleteTalkLastLink()
        {
            IWebElement deleteTalkLink =
                Driver.FindElement(By.XPath("//tr[@class='TalksListItem'][last()]/td/a"));
            deleteTalkLink.Click();
        }
        public void DeleteSubmit()
        {
            IWebElement submitButton = Driver.FindElement(By.XPath("/html/body/div[2]/div/form/div/input"));
            submitButton.Click();
        }

        public void ClickBackToListLink()
        {
            IWebElement submitButton = Wait.Until(d => d.FindElement(By.LinkText("Back to List")));
            submitButton.Click();
        }
    }
}
