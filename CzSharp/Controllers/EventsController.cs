using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;
using CzSharp.Services;
using CzSharp.Utils.Extensions;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class EventsController: BaseController
    {
        private IEventsRepository eventsRepository;

        private ITagsService tagsService;
        private UserManager<User> userManager;
        
        public EventsController(IEventsRepository eventsRepository, ITagsService tagsService, UserManager<User> userManager)
        {
            this.eventsRepository = eventsRepository;
            this.tagsService = tagsService;
            this.userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize(Policy = Polices.EventCreators)]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Range(DateTime start, DateTime end)
        {
            return Json(eventsRepository.GetRange(start, end)
                .Select(e => new FullCalendarEvent
                {
                    AllDay = false,
                    End = e.To,
                    Start = e.From,
                    Id = e.Id,
                    Title = e.Title
                }));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var evt = await eventsRepository.FindByIdAsync(id);

            if (evt != null)
            {
                return View(evt);
            }

            return NotFound();
        }

        [HttpPost, Authorize(Policy = Polices.EventCreators)]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tags = new List<Tag>();
                if (!string.IsNullOrEmpty(model.SelectedTags))
                {
                    tags = (await tagsService.ParseTags(model.SelectedTags)).ToList();
                }
                
                model.Event.User = await userManager.GetUserAsync(User);
                model.Event.EventTags = tags.Select(t => new EventTag
                {
                    Tag = t
                }).ToList();
                model.Event.Created = DateTime.Now;

                await eventsRepository.CreateAsync(model.Event);
                
                return RedirectToAction("Detail", new {id = model.Event.Id});
            }
            
            TempData.AddErrorMessage("Vyplňte všechna pole.");
            return View("Create", model);
        }
    }
}