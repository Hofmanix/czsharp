using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class TagsController: BaseController
    {
        private readonly ITagsRepository tagsRepository;
        
        public TagsController(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Json(tagsRepository.FindAll());
        }

        [HttpGet]
        public IActionResult Find(string titlePart)
        {
            return Json(tagsRepository.FindByTitlePart(titlePart));
        }

        [HttpGet]
        public async Task<IActionResult> Tag(string title)
        {
            var tag = await tagsRepository.FindByTitleAsync(title);
            if (tag == null)
            {
                tag = new Tag
                {
                    Title = title
                };
                await tagsRepository.CreateAsync(tag);
            }

            return Ok(tag.Title);
        }
    }
}