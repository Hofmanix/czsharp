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

        /// <summary>
        /// Returns all tags as json
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Json(tagsRepository.FindAll());
        }

        /// <summary>
        /// Returns tags specified by part of its title as json
        /// </summary>
        /// <param name="titlePart"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Find(string titlePart)
        {
            return Json(tagsRepository.FindByTitlePart(titlePart));
        }

        /// <summary>
        /// Returns one specific tag specified by full title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
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