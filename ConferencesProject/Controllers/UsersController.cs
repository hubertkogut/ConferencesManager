using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using ConferencesProject.CustomActionResult;
using ConferencesProject.Data;
using ConferencesProject.Models;

namespace ConferencesProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository _repository;

        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var model = await _repository.GetAllUsersAsync();

                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsersXML()
        {

            var model = await _repository.GetAllUsersAsync();
            var userList = new List<SimpleUserInfo>();
            foreach (var user1 in model)
            {
                SimpleUserInfo user = new SimpleUserInfo();
                user.Email = user1.Email;
                userList.Add(user);
            }

            return new XMLResult(userList);
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DownloadUsersInCSV()
        {

            var model = await _repository.GetAllUsersAsync();
            var userList = new List<SimpleUserInfo>();
            foreach (var user1 in model)
            {
                SimpleUserInfo user = new SimpleUserInfo();
                user.Email = user1.Email;
                userList.Add(user);
            }

            return new CSVResult(userList, "allUsers.csv");
        }
    }
}