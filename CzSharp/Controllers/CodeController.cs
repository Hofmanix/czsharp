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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CodeViewModel
            {
                SelectedTags = string.Empty
            });
        }

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
                return RedirectToAction("Index");
            }
            Console.WriteLine(ModelState.ErrorCount);
            
            ModelState.AddModelError("", "Vyplňte všechna povinná pole prosím.");

            return View(model);
        }

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