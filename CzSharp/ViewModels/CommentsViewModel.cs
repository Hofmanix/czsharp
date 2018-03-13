using System.Collections.Generic;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for comments show and creation
    /// </summary>
    public class CommentsViewModel
    {
        public ICollection<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
    }
}