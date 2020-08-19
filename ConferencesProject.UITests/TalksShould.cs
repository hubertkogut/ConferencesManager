using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ConferencesProject.UITests
{
    public class TalksShould
    {
        private const string CreateTitle = "Create - Conferences manager";
        private const string DeleteTitle = "Delete - Conferences manager";
        private const string DeleteSuccessTitle = "DeleteSuccess - Conferences manager";

        private const string AdminLogin = "hubkog@gmail.com";
        private const string AdminPassword = "123456";

        [Fact]
        [Trait("Category", "SmokeAndRedirection")]
        public void UnauthorizedUserRedirection()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                var talksPage = new TalksPage(driver, wait);
                var loginPage = new LoginPage(driver, wait);

                talksPage.NavigateTo();
                loginPage.Logoff();
                talksPage.NavigateTo();

                talksPage.ClickCreateTalkLink();
                loginPage.EnsurePageLoaded();
                driver.Navigate().Back();

                talksPage.ClickDeleteFirstTalkLink();
                loginPage.EnsurePageLoaded();
            }
        }

        [Fact]
        [Trait("Category", "SmokeAndRedirection")]
        public void AuthorizedUserNavigation()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                var talksPage = new TalksPage(driver, wait);
                var loginPage = new LoginPage(driver, wait);

                talksPage.NavigateTo();
                loginPage.Logoff();
                loginPage.NavigateTo();
                loginPage.Login(AdminLogin, AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {AdminLogin}!", helloMessageOrRegisterLink);
                talksPage.NavigateTo();

                talksPage.ClickCreateTalkLink();
                Assert.Equal(CreateTitle, driver.Title);
                driver.Navigate().Back();

                talksPage.ClickDeleteFirstTalkLink();
                Assert.Equal(DeleteTitle, driver.Title);
            }
        }

        [Fact]
        public void CreateAndDeleteConference()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                var talksPage = new TalksPage(driver, wait);
                var loginPage = new LoginPage(driver, wait);

                talksPage.NavigateTo();
                loginPage.Logoff();
                loginPage.NavigateTo();
                loginPage.Login(AdminLogin, AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {AdminLogin}!", helloMessageOrRegisterLink);
                talksPage.NavigateTo();

                var talksListBeforeCreateNewTalk = talksPage.GetTalksList();

                talksPage.ClickCreateTalkLink();
                talksPage.FillCreateTalkFormAndSubmit("TitleTest", "AbstractTest");
                talksPage.EnsurePageLoaded();

                var talksListAfterCreateNewTalk = talksPage.GetTalksList();

                Assert.Equal(talksListBeforeCreateNewTalk.Count, talksListAfterCreateNewTalk.Count - 1);
                Assert.Contains("TitleTest", talksListAfterCreateNewTalk.Last());

                talksPage.ClickDeleteTalkLastLink();
                Assert.Equal(DeleteTitle, driver.Title);

                talksPage.DeleteSubmit();
                Assert.Equal(DeleteSuccessTitle, driver.Title);

                talksPage.ClickBackToListLink();
                talksPage.EnsurePageLoaded();
                var talksListAfterDeleteNewTalk = talksPage.GetTalksList();
                Assert.Equal(talksListBeforeCreateNewTalk.Count, talksListAfterDeleteNewTalk.Count);
                Assert.DoesNotContain("TitleTest", talksListAfterDeleteNewTalk.Last());
            }
        }
    }
}
