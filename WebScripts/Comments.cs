using System;
using Bridge.Html5;
using Retyped;

namespace WebScripts
{
    public class Comments
    {
        public Comments()
        {
            jquery.jQuery.@select("#new-comment").submit(CommentsFormSubmitted);
        }

        /// <summary>
        /// Creates new comments and puts it in comment
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        private object CommentsFormSubmitted(jquery.JQueryEventObject @event)
        {
            @event.preventDefault();
            
            var data = jquery.jQuery.@select("#new-comment").serialize();
            jquery.jQuery.post("/comments/createcomment", data).then((value, values) =>
            {
                AddComment(value);
            }, reason =>
            {
                Window.Alert(((dynamic)reason).responseText);
                return null;
            });

            return null;
        }

        /// <summary>
        /// Appends new comment to comments div
        /// </summary>
        /// <param name="comment"></param>
        private void AddComment(dynamic comment)
        {
            string content = "<div class='card'>" +
                                 "<div class='card-body p-2'>" +
                                     comment.user.userName +
                                     "<br>" +
                                     $"<small>{comment.created}</small>" +
                                     "<hr class='mt-1 mb-1'/>" +
                                     comment.content +
                                 "</div>" +
                             "</div>";

            var card = jquery.jQuery.Self(content);
            jquery.jQuery.@select("#comments").append(card);
            jquery.jQuery.@select("#NewComment_Content").val("");
        }
    }
}