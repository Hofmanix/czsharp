using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model;
using CzSharp.Model.Entities;
using CzSharp.Model.Entities.Blog;
using CzSharp.Model.Repositories;
using CzSharp.Model.Repositories.Blog;
using CzSharp.Services;
using CzSharp.Utils.Extensions;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CzSharp.Controllers
{
    public class BlogController: BaseController
    {
        private AppDbContext dbContext;

        private IArticlesRepository articlesRepository;
        private ICategoriesRepository categoriesRepository;
        private ITagsRepository tagsRepository;

        private UserManager<User> userManager;
        private ITagsService tagsService;
        
        public BlogController(IArticlesRepository articlesRepository, 
            ICategoriesRepository categoriesRepository,
            ITagsRepository tagsRepository,
            ITagsService tagsService, 
            AppDbContext dbContext,
            UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            
            this.articlesRepository = articlesRepository;
            this.categoriesRepository = categoriesRepository;
            this.tagsRepository = tagsRepository;

            this.tagsService = tagsService;
            this.userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            return View(new ArticlesViewModel
            {
                Articles = articlesRepository.FindPage(page),
                ArticlesCount = articlesRepository.FindAll().Count(),
                ActivePage = page
            });
        }

        [HttpGet, Authorize(Policy = Polices.Bloggers)]
        public async Task<IActionResult> Create()
        {
            return View(new ArticleViewModel
            {
                Categories = categoriesRepository.FindAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }),
                Tags = tagsRepository.FindAll().Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Title
                })
            });
        }

        [HttpPost, Authorize(Policy = Polices.Bloggers)]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tags = new List<Tag>();
                if (!string.IsNullOrEmpty(model.SelectedTags))
                {
                    tags = (await tagsService.ParseTags(model.SelectedTags)).ToList();
                }

                model.Article.User = await userManager.GetUserAsync(HttpContext.User);
                model.Article.Created = DateTime.Now;
                await articlesRepository.CreateWithTagsAsync(model.Article, tags);

                return RedirectToAction("Detail", new {id = model.Article.Id});
            }
            
            ModelState.AddModelError("", "Vyplňte všechna povinná pole prosím.");

            return View(new ArticleViewModel
            {
                Categories = categoriesRepository.FindAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }),
                Tags = tagsRepository.FindAll().Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Title
                }),
                Article = model.Article,
                SelectedTags = model.SelectedTags
            });
        }

        [HttpGet, Authorize(Policy = Polices.Bloggers)]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var article = await articlesRepository.FindByIdAsync(id);
                var user = await userManager.GetUserAsync(User);

                if ((article.User != null && article.User.Id == user.Id) || User.IsInRole(UserRole.Administrator))
                {
                    return View("Create", new ArticleViewModel
                    {
                        Article = article,
                        Categories = categoriesRepository.FindAll().Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Title
                        }),
                        Tags = tagsRepository.FindAll().Select(t => new SelectListItem
                        {
                            Value = t.Id.ToString(),
                            Text = t.Title
                        }),
                        SelectedTags = string.Join(", ", article.ArticleTags.Select(t => t.Tag.Title))
                    });
                }
            }
            
            TempData.AddErrorMessage("Nemáte dostatečná oprávnění pro úpravu tohoto článku.");
            return RedirectToAction("Detail", new {id});
        }

        [HttpPost, Authorize(Policy = Polices.Bloggers)]
        public async Task<IActionResult> Edit(ArticleViewModel model)
        {
            var tags = new List<Tag>();
            if (!string.IsNullOrEmpty(model.SelectedTags))
            {
                tags = (await tagsService.ParseTags(model.SelectedTags)).ToList();
            }

            model.Article.ArticleTags = tags.Select(t => new ArticleTag
            {
                ArticleId = model.Article.Id,
                TagId = t.Id
            }).ToList();

            await articlesRepository.UpdateAsync(model.Article);
            return RedirectToAction("Detail", new {id = model.Article.Id});
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await articlesRepository.FindByIdAsync(id);
            
            Console.WriteLine(article.ArticleTags.Count);
            if (article != null)
            {
                return View(article);
            }

            return NotFound();
        }

        [HttpGet, Authorize(Policy = Polices.Bloggers)]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await articlesRepository.FindByIdAsync(id);
            var user = await userManager.GetUserAsync(User);

            if ((article.User != null && article.User.Id == user.Id) || User.IsInRole(UserRole.Administrator))
            {
                await articlesRepository.DeleteAsync(article);
            
                TempData.AddSuccessMessage("Článek byl odstraněn");
                return RedirectToAction("Index");
            }
            
            TempData.AddErrorMessage("Nemáte dostatečná oprávnění pro odstranění tohoto článku.");
            return RedirectToAction("Detail", new {id});
        }

        [HttpPost, Authorize(Policy = Polices.SeniorBloggers)]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Title))
            {
                return BadRequest("Zadejte název kategorie.");
            }

            if (await categoriesRepository.FindByTitleAsync(category.Title) != null)
            {
                return BadRequest("Kategorie s tímto názvem již existuje.");
            }

            category.Created = DateTime.Now;
            category.User = await userManager.GetUserAsync(User);

            await categoriesRepository.CreateAsync(category);
            return Json(category);
        }
    }
}