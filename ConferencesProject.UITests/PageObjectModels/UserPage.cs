using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConferencesProject.UITests.PageObjectModels
{
    internal class UserPage : Page
    {
        protected override string PageUrl => "https://localhost:44389/Users";
        protected override string PageTitle => "Users list - Conferences manager";


        public UserPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public IReadOnlyCollection<string> GetUserList()
        {
             var lista = Driver.FindElements(
                    By.ClassName("UsersListItem"))
                .Select(x => x.Text)
                .ToList()
                .AsReadOnly();
             return lista;
        }
    }
}
