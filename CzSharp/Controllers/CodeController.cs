using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;
using CzSharp.Services;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    /// <summary>
    /// Controller for codes
    /// </summary>
    public class CodeController: BaseController
    {
        private ITagsService tagsService;
        private ICodeRepository codeRepository;
        private UserManager<User> userManager;
        
        public CodeController(ITagsService tagsService, ICodeRepository codeRepository, UserManager<User> userManager)
        {
            this.tagsService = tagsService;
            this.codeRepository = codeRepository;
            this.userManager = userManager;
        }
        
        /// <summary>
        /// Returns page with codes ordered by id desc, selected by the page attribute
        /// </summary>
        /// <param name="page">attribute for codes range</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            return View(new CodesViewModel
            {
                Codes = codeRepository.FindPage(page),
                CodeCount = codeRepository.FindAll().Count(),
                ActivePage = page
            });
        }

        /// <summary>
        /// Returns page for creating new code, only for Administrators and Coders
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Policy = Polices.Coders)]
        public IActionResult Create()
        {
            return View(new CodeViewModel
            {
                SelectedTags = string.Empty
            });
        }

        /// <summary>
        /// Creates new code, saves it to db and returns code detail page
        /// </summary>
        /// <param name="model">Model with code data</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = Polices.Coders)]
        public async Task<IActionResult> Create(CodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var code = model.Code;
                var tags = new List<Tag>();
                if (!string.IsNullOrEmpty(model.SelectedTags))
                {
                    tags = (await tagsService.ParseTags(model.SelectedTags)).ToList();
                }

                code.CodeTags = tags.Select(t => new CodeTag
                {
                    TagId = t.Id
                }).ToList();
                code.User = await userManager.GetUserAsync(User);
                code.Created = DateTime.Now;

                await codeRepository.CreateAsync(code);
                return RedirectToAction("Detail", new {id = code.Id});
            }
            Console.WriteLine(ModelState.ErrorCount);
            
            ModelState.AddModelError("", "Vyplňte všechna povinná pole prosím.");

            return View(model);
        }

        /// <summary>
        /// Shows code detail selected by id
        /// </summary>
        /// <param name="id">id of detail to show</param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(int id)
        {
            var code = await codeRepository.FindByIdAsync(id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }
    }
}