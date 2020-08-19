using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConferencesProject.UITests.PageObjectModels
{
    internal class ConferencesPage : Page
    {

        protected override string PageUrl => "https://localhost:44389/";
        protected override string PageTitle => "Home - Conferences manager";


        public ConferencesPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void WaitForPartialViewAndClickSeeTalksLink()
        {
            IWebElement seeTalksLink =
                Wait.Until(d => d.FindElement(By.LinkText("See talks")));
            seeTalksLink.Click();
        }

        public void WaitForPartialViewAndClickParticipatingUsersLink()
        {
            IWebElement seeTalksLink =
                Wait.Until(d => d.FindElement(By.LinkText("Participating users")));
            seeTalksLink.Click();
        }

        public void WaitForPartialViewAndClickSignUpLink()
        {
            IWebElement signInLink =
                Wait.Until(d => d.FindElement(By.LinkText("SignUp")));
            signInLink.Click();
        }

        public void WaitForPartialViewAndClickCreateConfLink()
        {
            IWebElement createConfLink =
                Wait.Until(d => d.FindElement(By.LinkText("Create New")));
            createConfLink.Click();
        }

        public void WaitForPartialViewAndClickDeleteLink()
        {
            IWebElement deleteConfLink =
                Wait.Until(d => d.FindElement(By.LinkText("Delete")));
            deleteConfLink.Click();
        }

        public void DeleteSubmit()
        {
            IWebElement submitButton = Wait.Until(d => d.FindElement(By.XPath("/html/body/div[2]/div/form/div/input")));
            submitButton.Click();
        }

        public void SignUpSubmit()
        {
            IWebElement submitButton = Wait.Until(d => d.FindElement(By.XPath("/html/body/div[2]/form/div/input")));
            submitButton.Click();
        }

        public void ClickBackToListLink()
        {
            IWebElement submitButton = Wait.Until(d => d.FindElement(By.LinkText("Back to List")));
            submitButton.Click();
        }

        public void WaitForPartialViewAndClickDeleteLastLink()
        {
            IWebElement deleteConfLink =
                Wait.Until(d => d.FindElement(By.XPath("//tr[@class='ConferencesListItem'][last()]/td/a[2]")));
            deleteConfLink.Click();
        }

        public void ClickCarouselNextButton()
        {
            IWebElement carouselNextButton = Wait.Until(d => d.FindElement(By.CssSelector("[data-slide='next']")));
            carouselNextButton.Click();
        }

        public void SignInOnCarousel()
        {
            IWebElement signInOnCarouse = Wait.Until(d => d.FindElement(By.LinkText("SignUp")));
            signInOnCarouse.Click();
        }

        public TalksPage NavigateToAllTalks()
        {
            Driver.FindElement(By.LinkText("All talks")).Click();
            return new TalksPage(Driver, Wait);

        }

        public void FillCreateConfFormAndSubmit(string name, string eventData, string city, string street, string country)
        {
            Driver.FindElement(By.Id("Name")).SendKeys(name);
            Driver.FindElement(By.Id("EventDate")).SendKeys(eventData);
            Driver.FindElement(By.Id("Location_City")).SendKeys(city);
            Driver.FindElement(By.Id("Location_Street")).SendKeys(street);
            Driver.FindElement(By.Id("Location_Country")).SendKeys(country);
            Driver.FindElement(By.Id("Location_Country")).Submit();
        }

        public IReadOnlyCollection<string> GetConfList()
        {
            IReadOnlyCollection<IWebElement> confCollection = Wait.Until(d => d.FindElements(By.ClassName("ConferencesListItem")));

            var confCollectionList = new List<string>();

            foreach (IWebElement webElement in confCollection)
            {
                confCollectionList.Add(webElement.Text);
            }

            return confCollectionList;
        }
    }
}
