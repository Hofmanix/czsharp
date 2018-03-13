using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Entities.Forum;
using CzSharp.Model.Repositories.Forum;
using CzSharp.Services;
using CzSharp.Utils.Extensions;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CzSharp.Controllers
{
    public class ForumController : Controller
    {
        private ITopicGroupsRepository topicGroupsRepository;
        private ITopicsRepository topicsRepository;
        private IDiscussionsRepository discussionsRepository;
        private IContributionsRepository contributionsRepository;

        private UserManager<User> userManager;
        private ITagsService tagsService;

        public ForumController(ITopicGroupsRepository topicGroupsRepository,
            ITopicsRepository topicsRepository,
            IDiscussionsRepository discussionsRepository,
            IContributionsRepository contributionsRepository,
            UserManager<User> userManager,
            ITagsService tagsService)
        {
            this.topicGroupsRepository = topicGroupsRepository;
            this.topicsRepository = topicsRepository;
            this.discussionsRepository = discussionsRepository;
            this.contributionsRepository = contributionsRepository;
            this.userManager = userManager;
            this.tagsService = tagsService;
        }
        
        /// <summary>
        /// Returns page with base forum topic groups and topics
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new ForumIndexViewModel
            {
                TopicGroups = topicGroupsRepository.FindAll()
            });
        }

        /// <summary>
        /// Returns topic specified by id
        /// </summary>
        /// <param name="id">topic id</param>
        /// <returns></returns>
        public async Task<IActionResult> Topic(int id)
        {
            var topic = await topicsRepository.FindByIdAsync(id);
            return View(new TopicViewModel
            {
                Topic = topic,
                TopicId = topic.Id
            });
        }

        /// <summary>
        /// Bad page access - redirect to index
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Policy = Polices.Moderators)]
        public IActionResult CreateTopicGroup()
        {
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Creates new topic group - only for moderators
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = Polices.Moderators)]
        public async Task<IActionResult> CreateTopicGroup(ForumIndexViewModel model)
        {
            model.TopicGroups = topicGroupsRepository.FindAll();
            
            if (string.IsNullOrWhiteSpace(model.NewTopicGroup.Title))
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.NewTopicGroup.Title, "Zadejte název skupiny témat.");
                return View("Index", model);
            }

            try
            {
                model.NewTopicGroup.Created = DateTime.Now;
                model.NewTopicGroup.User = await userManager.GetUserAsync(User);
                await topicGroupsRepository.CreateAsync(model.NewTopicGroup);
            }
            catch (Exception)
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.NewTopicGroup.Title, "Skupina s tímto názvem již existuje");
                return View("Index", model);
            }
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Creates new topic - only for moderators
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = Polices.Moderators)]
        public async Task<IActionResult> CreateTopic(ForumIndexViewModel model)
        {
            model.TopicGroups = topicGroupsRepository.FindAll();

            if (string.IsNullOrWhiteSpace(model.NewTopic.Title))
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.NewTopic.Title, "Zadejte název tématu.");
                return View("Index", model);
            }

            if (string.IsNullOrWhiteSpace(model.NewTopic.Info))
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.NewTopic.Info, "Zadejte informace o novém tématu.");
                return View("Index", model);
            }

            if (string.IsNullOrWhiteSpace(model.SelectedTopicGroup))
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.SelectedTopicGroup, "Vyberte skupinu témat.");
                return View("Index", model);
            }

            int id = Convert.ToInt32(model.SelectedTopicGroup);

            var topicGroup = await topicGroupsRepository.FindByIdAsync(id);
            if (topicGroup == null)
            {
                Console.WriteLine("GroupNull");
                TempData.AddErrorMessage("Do této skupiny nemůžete přidat nové téma.");
                return View("Index", model);
            }

            if (topicGroup.Topics.Any(t => t.Title.Trim() == model.NewTopic.Title.Trim()))
            {
                ModelState.AddModelError<ForumIndexViewModel>(m => m.NewTopic.Title, "Téma s tímto názvem již v této skupině existuje.");
                return View("Index", model);
            }
            Console.WriteLine("Creating");

            model.NewTopic.Created = DateTime.Now;
            model.NewTopic.User = await userManager.GetUserAsync(User);
            model.NewTopic.TopicGroup = topicGroup;

            await topicsRepository.CreateAsync(model.NewTopic);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Bad page access, redirect to index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateDiscussion()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Creates new discussion specified by parameters
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateDiscussion(TopicViewModel model)
        {
            var tags = new List<Tag>();
            if (!string.IsNullOrEmpty(model.SelectedTags))
            {
                tags = (await tagsService.ParseTags(model.SelectedTags)).ToList();
            }
            
            model.Topic = await topicsRepository.FindByIdAsync(model.TopicId);
            
            if (string.IsNullOrWhiteSpace(model.Discussion.Title))
            {
                ModelState.AddModelError<TopicViewModel>(m => m.Discussion.Title, "Zadejte název diskuse.");
                return View("Topic", model);
            }

            if (string.IsNullOrWhiteSpace(model.Contribution.Content))
            {
            }

            var user = await userManager.GetUserAsync(User);

            model.Discussion.Created = DateTime.Now;
            model.Discussion.User = user;
            model.Discussion.Topic = model.Topic;
            model.Discussion.DiscussionTags = tags.Select(t => new DiscussionTag
            {
                Tag = t
            }).ToList();

            await discussionsRepository.CreateAsync(model.Discussion);

            model.Contribution.Created = DateTime.Now;
            model.Contribution.User = user;
            model.Contribution.Discussion = model.Discussion;

            await contributionsRepository.CreateAsync(model.Contribution);

            return RedirectToAction("Discussion", new {id = model.Discussion.Id});
        }

        /// <summary>
        /// Returns discussion specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Discussion(int id)
        {
            var discussion = await discussionsRepository.FindByIdAsync(id);
            return View(new DiscussionViewModel
            {
                Discussion = discussion,
                DiscussionId = discussion.Id
            });
        }

        /// <summary>
        /// Creates new contribution to the discussion, everyting specified in the model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateContribution(DiscussionViewModel model)
        {
            model.Discussion = await discussionsRepository.FindByIdAsync(model.DiscussionId);
            if (string.IsNullOrWhiteSpace(model.Contribution.Content))
            {
                ModelState.AddModelError<TopicViewModel>(m => m.Contribution.Content, "Zadejte příspěvek.");
                return View("Discussion", model);
            }

            model.Contribution.Discussion = model.Discussion;
            model.Contribution.Created = DateTime.Now;
            model.Contribution.User = await userManager.GetUserAsync(User);

            await contributionsRepository.CreateAsync(model.Contribution);

            return RedirectToAction("Discussion", new {id = model.DiscussionId});
        }
    }
}