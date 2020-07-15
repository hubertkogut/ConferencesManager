﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConferencesProject.Data;
using ConferencesProject.Models;

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
                var model = await _repository.GetAllConferecesAsync();
                return View(model);
            }
            catch
            {
                return View("ErrorInfo");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
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
        public ActionResult CreateTalk()
        {
            return View();
        }
        [HttpPost]
        [Route("{id:int}")]
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

    }
}