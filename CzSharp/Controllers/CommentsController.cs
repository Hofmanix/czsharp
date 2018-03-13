using System;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class CommentsController: BaseController
    {
        private ICommentsRepository commentsRepository;

        private UserManager<User> userManager;

        public CommentsController(ICommentsRepository commentsRepository, UserManager<User> userManager)
        {
            this.commentsRepository = commentsRepository;
            this.userManager = userManager;
        }
        
        /// <summary>
        /// Creates comment, only for authorized users and returns that comment
        /// </summary>
        /// <param name="model">Model containing new comment</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateComment(CommentsViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.NewComment.Content))
            {
                return BadRequest("Zadejte obsah komentáře.");
            }
            
            model.NewComment.Created = DateTime.Now;
            model.NewComment.User = await userManager.GetUserAsync(User);

            await commentsRepository.CreateAsync(model.NewComment);

            return Json(model.NewComment);
        }
    }
}