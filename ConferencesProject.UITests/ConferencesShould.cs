using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace ConferencesProject.UITests
{
    public class ConferencesShould          
    {
        private const string SeeTalksTitle = "Talks list - Conferences manager";
        private const string CreateTitle = "Create - Conferences manager";
        private const string DeleteTitle = "Delete - Conferences manager";
        private const string DeleteSuccessTitle = "DeleteSuccess - Conferences manager";
        private const string SignUpTitle = "SignUp - Conferences manager";
        private const string ParticipatingUsersTitle = "Participating users - Conferences manager";
        private const string AlreadySignedUp = "AlreadySignedUp - Conferences manager";

        private readonly ITestOutputHelper _output;
        public ConferencesShould(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        [Trait("Category", "SmokeAndRedirection")]
        public void UnauthorizedUserNavigation()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

                var conferencesPage = new ConferencesPage(driver, wait);

                conferencesPage.NavigateTo();

                conferencesPage.WaitForPartialViewAndClickSeeTalksLink();

                Assert.Equal(SeeTalksTitle, driver.Title);

                TalksPage talksPage = conferencesPage.NavigateToAllTalks();
                talksPage.EnsurePageLoaded();
            }
        }


        [Fact]
        [Trait("Category", "SmokeAndRedirection")]
        public void UnauthorizedUserRedirection()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                var conferencesPage = new ConferencesPage(driver, wait);
                conferencesPage.NavigateTo();
                var loginPage = new LoginPage(driver, wait);
                loginPage.Logoff();

                conferencesPage.WaitForPartialViewAndClickParticipatingUsersLink();
                loginPage.EnsurePageLoaded();
                driver.Navigate().Back();

                conferencesPage.WaitForPartialViewAndClickCreateConfLink();
                loginPage.EnsurePageLoaded();
                driver.Navigate().Back();

                conferencesPage.WaitForPartialViewAndClickDeleteLink();
                loginPage.EnsurePageLoaded();
                driver.Navigate().Back();

                conferencesPage.ClickCarouselNextButton();
                conferencesPage.SignInOnCarousel();
                loginPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void AuthorizedUserNavigation()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                var loginPage = new LoginPage(driver, wait);
                var conferencesPage = new ConferencesPage(driver, wait);

                loginPage.NavigateTo();
                loginPage.Logoff();
                loginPage.Login(conferencesPage.AdminLogin, conferencesPage.AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {conferencesPage.AdminLogin}!", helloMessageOrRegisterLink);
                conferencesPage.EnsurePageLoaded();

                conferencesPage.WaitForPartialViewAndClickParticipatingUsersLink();
                Assert.Equal(ParticipatingUsersTitle, driver.Title);
                driver.Navigate().Back();

                conferencesPage.WaitForPartialViewAndClickCreateConfLink();
                Assert.Equal(CreateTitle, driver.Title);
                driver.Navigate().Back();

                conferencesPage.WaitForPartialViewAndClickDeleteLink();
                Assert.Equal(DeleteTitle, driver.Title);
                driver.Navigate().Back();

                conferencesPage.ClickCarouselNextButton();
                conferencesPage.SignInOnCarousel();
                Assert.Equal(SignUpTitle, driver.Title);
            }
        }

        [Fact]
        public void AlreadySignedIn()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                var loginPage = new LoginPage(driver, wait);
                var conferencesPage = new ConferencesPage(driver, wait);

                loginPage.NavigateTo();
                loginPage.Logoff();
                loginPage.Login(conferencesPage.AdminLogin, conferencesPage.AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {conferencesPage.AdminLogin}!", helloMessageOrRegisterLink);
                conferencesPage.EnsurePageLoaded();

                conferencesPage.WaitForPartialViewAndClickSignUpLink();
                conferencesPage.SignUpSubmit();

                Assert.Equal(AlreadySignedUp, driver.Title);
            }
        }

        [Fact]
        public void CreateAndDeleteConference()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                var loginPage = new LoginPage(driver, wait);
                var conferencesPage = new ConferencesPage(driver, wait);

                loginPage.NavigateTo();
                loginPage.Logoff();
                loginPage.Login(conferencesPage.AdminLogin, conferencesPage.AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {conferencesPage.AdminLogin}!", helloMessageOrRegisterLink);
                conferencesPage.EnsurePageLoaded();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                var confListBeforeCreateNewConf = conferencesPage.GetConfList();
                _output.WriteLine($"{confListBeforeCreateNewConf.Count} before");

                conferencesPage.WaitForPartialViewAndClickCreateConfLink();
                conferencesPage.FillCreateConfFormAndSubmit("NameTest", "2020-11-01", "CityTest",
                    "StreetTest", "CountyTest");
                conferencesPage.EnsurePageLoaded();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                var confListAfterCreateNewConf = conferencesPage.GetConfList();
                _output.WriteLine($"{ confListAfterCreateNewConf.Count} after");

                Assert.Equal(confListBeforeCreateNewConf.Count, confListAfterCreateNewConf.Count-1);
                Assert.Contains("NameTest", confListAfterCreateNewConf.Last());

                conferencesPage.WaitForPartialViewAndClickDeleteLastLink();
                Assert.Equal(DeleteTitle, driver.Title);
                
                conferencesPage.DeleteSubmit();
                Assert.Equal(DeleteSuccessTitle, driver.Title);

                conferencesPage.ClickBackToListLink();
                conferencesPage.EnsurePageLoaded();
                var confListAfterDeleteNewConf = conferencesPage.GetConfList();
                Assert.Equal(confListBeforeCreateNewConf.Count, confListAfterDeleteNewConf.Count);
                Assert.DoesNotContain("NameTest", confListAfterDeleteNewConf.Last());
            }
        }
    }
}
