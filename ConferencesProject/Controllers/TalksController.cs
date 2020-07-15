using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConferencesProject.Data;
using ConferencesProject.Models;

namespace ConferencesProject.Controllers
{
    public class TalksController : Controller
    {
        private readonly IRepository _repository;

        public TalksController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var model = await _repository.GetAllTalksAsync();
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
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
        public async Task<ActionResult> Delete(int id, FormCollection form)
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

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                var model = await _repository.GetAllConferecesAsync();
                List<SelectListItem> selectList = new List<SelectListItem>();
                foreach (var element in model)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = element.Id.ToString(),
                        Text = element.Name
                    });
                }
                ViewBag.SelectList = selectList;
                return View();
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Create(Talk talk)
        {
            try
            {
                var conf = await _repository.GetConferenceAsync(talk.Conference.Id);
                if (conf == null)
                {
                    return View("NotFound");
                }
                talk.Conference = conf;
                _repository.AddTalk(talk);
                _repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ErrorInfo");
            }
            
        }
    }
}