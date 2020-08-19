using System;
using ConferencesProject.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ConferencesProject.UITests
{
    public class LoginShould
    {
        [Fact]
        public void LoginLogoff()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                LoginPage loginPage = new LoginPage(driver, wait);

                loginPage.NavigateTo();
                // Helper.Pause(1);
                loginPage.Logoff();
                loginPage.Login(loginPage.AdminLogin, loginPage.AdminPassword);

                //Helper.Pause(1);

                var helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();

                Assert.Equal($"Hello {loginPage.AdminLogin}!", helloMessageOrRegisterLink);
                loginPage.Logoff();
                helloMessageOrRegisterLink = loginPage.HelloMessageOrRegisterLink();
                Assert.Equal("Register", helloMessageOrRegisterLink);
                //Helper.Pause(1);

            }
        }

        [Fact]
        public void LoginValidation()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                LoginPage loginPage = new LoginPage(driver, wait);

                const string emptyPassword = "";
                const string emptyLogin = "";
                const string wrongEmail = "test";
                const string thereIsNoSuchUser = "test@gmail.com";

                loginPage.NavigateTo();
                loginPage.Logoff(); //to make sure that test run as unauthorized user

                loginPage.Login(emptyLogin, emptyPassword);
                //Helper.Pause();

                Assert.Equal(2, loginPage.ValidationErrorMessages.Count);
                Assert.Contains("Pole Email jest wymagane.", loginPage.ValidationErrorMessages[0]);
                Assert.Contains("Pole Password jest wymagane.", loginPage.ValidationErrorMessages[1]);

                loginPage.ClearForm();
                loginPage.Login(wrongEmail, emptyPassword);
                //Helper.Pause();
                Assert.Contains("Wartość w polu Email nie jest prawidłowym adresem e-mail.",
                    loginPage.ValidationErrorMessages[0]);
                Helper.Pause();

                loginPage.ClearForm();
                loginPage.Login(thereIsNoSuchUser, wrongEmail);
                //Helper.Pause();
                Assert.Equal("Invalid login attempt.", loginPage.AnotherValidationErrorMessage);


            }
        }
    }
}
