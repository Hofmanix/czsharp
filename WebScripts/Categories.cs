using System;
using Bridge.Html5;
using Retyped;

namespace WebScripts
{
    public class Categories
    {
        public Categories()
        {
            jquery.jQuery.@select("#create-category").submit(CreateCategorySubmitted);
        }

        /// <summary>
        /// Creates new blog category and puts it in the select
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        private object CreateCategorySubmitted(jquery.JQueryEventObject @event)
        {
            @event.preventDefault();
            var data = jquery.jQuery.@select("#create-category").serialize();
            jquery.jQuery.post("/blog/createcategory", data, (resData, status, xhr) =>
            {
                Console.WriteLine(resData);
                string id = ((dynamic) resData).id;
                string title = ((dynamic) resData).title;
                var option = jquery.jQuery.Self($"<option value='{id}'>{title}</option>");
                jquery.jQuery.@select("#Article_Category").append(option);
                jquery.jQuery.@select("#new-category-modal").bootstrap().modal("hide");
                return null;
            }).then((value, values) =>
            {
            }, reasons =>
            {
                Window.Alert(((dynamic)reasons).responseText);
                return null;
            });

            return null;
        }
    }
}