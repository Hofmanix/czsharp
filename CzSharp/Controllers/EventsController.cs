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
        /// <summary>
        /// Returns page with calendar, events are downloaded asynchronously by javascript
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Shows page for creating new event, only for event creators
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Policy = Polices.EventCreators)]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Returns event defined by the dates range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns event page specified by the detail
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates new event specified by the model, only for event creators
        /// </summary>
        /// <param name="model">Model with new event params</param>
        /// <returns></returns>
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