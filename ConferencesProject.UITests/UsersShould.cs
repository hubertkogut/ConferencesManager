using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using ConferencesProject.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ConferencesProject.UITests
{
    public class UsersShould
    {

        [Fact]
        public void UsersList()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                var loginPage = new LoginPage(driver, wait);
                var userPage = new UserPage(driver, wait);

                loginPage.NavigateTo();
                loginPage.Logoff();
                loginPage.Login(userPage.AdminLogin, userPage.AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {userPage.AdminLogin}!", helloMessageOrRegisterLink);
                userPage.NavigateTo();

                var lista = userPage.GetUserList();

                Assert.Equal(3, lista.Count);
                Assert.Equal("jan.nowak@gmail.com", lista.ElementAt(0));
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void SimpleApprovalTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                var loginPage = new LoginPage(driver, wait);
                var userPage = new UserPage(driver, wait);

                loginPage.NavigateTo();
                loginPage.Logoff();
                loginPage.Login(userPage.AdminLogin, userPage.AdminPassword);
                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal($"Hello {userPage.AdminLogin}!", helloMessageOrRegisterLink);
                userPage.NavigateTo();

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

                Screenshot screenShot = screenShotDriver.GetScreenshot();

                screenShot.SaveAsFile("usersListPage.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("usersListPage.bmp");

                Approvals.Verify(file);
            }
        }
    }
}
