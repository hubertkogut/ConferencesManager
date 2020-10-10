using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConferencesProject.CustomActionResult;
using ConferencesProject.Data;
using ConferencesProject.Models;
using Microsoft.AspNet.Identity;

namespace ConferencesProject.Controllers
{
    public class ConferencesController : Controller
    {

        private readonly IRepository _repository;

        public ConferencesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var model = await _repository.GetAllConferecesSortedByPopularityAsync();
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
        }

        [HttpGet]
        public async Task<ActionResult> _IndexAllConfList()
        {
            try
            {
                System.Threading.Thread.Sleep(1200); //Simulation of a time-consuming operation, to show cool spinning wheels :)
                var model = await _repository.GetAllConferecesAsync();
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Conference model)
        {
            try
            {
                _repository.AddConference(model);
                _repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ErrorInfo");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete (int id)
        {
            try
            {
                var model = await _repository.GetConferenceAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }

                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, FormCollection form)
        {
            try
            {
                var model = await _repository.GetConferenceAsync(id);
                _repository.DeleteConference(model);
                _repository.Save();
                return View("DeleteSucces");
            }
            catch
            {
                return View("ErrorInfo");
            }
        }



        [HttpGet]
        public async Task<ActionResult> SeeTalks(int id)
        {
            try
            {
                var model = await _repository.GetTalksByConfIdAsync(id);
                ViewBag.ConfId = id;
                if (!model.Any())
                {
                    var conf = await _repository.GetConferenceAsync(id);
                    if (conf == null)
                    {
                        return View("NotFound");
                    }
                    return View(model);
                }
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateTalk()
        {
            return View();
        }
        [HttpPost]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateTalk(Talk talk, int id)
        {
            try
            {
                var conf = await _repository.GetConferenceAsync(id);
                if (conf == null)
                {
                    return View("NotFound");
                }
                talk.Conference = conf;
                _repository.AddTalk(talk);
                _repository.Save();

                return RedirectToAction("SeeTalks", new { id });
            }
            catch
            {
                return View("ErrorInfo");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTalk(int id)
        {
            try
            {
                var model = await _repository.GetTalkByTalkIdAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTalk(int id, FormCollection form)
        {
            try
            {
                var model = await _repository.GetTalkByTalkIdAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }
                _repository.DeleteTalk(model);
                _repository.Save();
                return View("DeleteSucces");
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        [Authorize]
        public async Task<ActionResult> SeeUsers(int id)
        {
            try
            {
                ViewBag.CurrentConf = id; 
                var model = await _repository.GetUsersByConfIdAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }

                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
        }


        [Authorize]
        public async Task<ActionResult> DownloadUsersInCSV(int id)
        {
            try
            {
                var model = await _repository.GetUsersByConfIdAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }

                var userList = new List<SimpleUserInfo>();
                foreach (var user1 in model)
                {
                    SimpleUserInfo user = new SimpleUserInfo();
                    user.Email = user1.Email;
                    userList.Add(user);
                }

                return new CSVResult(userList, "UserList.csv");
            }
            catch
            {
                return View("ErrorInfo");
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> SignUp(int id)
        {
            try
            {
                var model = await _repository.GetConferenceAsync(id);
                if (model == null)
                {
                    return View("NotFound");
                }
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SignUp(int id, FormCollection form)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var participatingUser = await _repository.GetUsersByConfIdAsync(id);

                if (participatingUser.Any(x => x.IdInAspNetUsers == userId))
                {
                    return View("SignedUp");
                }
                else
                {
                    await _repository.AddUserToConfAsync(id, userId);
                    _repository.Save();
                    return View("SignUpSucces");
                }
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        public ActionResult Error()
        {
            var test = int.Parse("test");
            return View();
        }

    }
}