using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConferencesProject.Data;

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
    }
}