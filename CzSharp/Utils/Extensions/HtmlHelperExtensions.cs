using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace CzSharp.Utils.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Checks if specified url is active
        /// </summary>
        /// <param name="html">Current html object</param>
        /// <param name="action">Path action</param>
        /// <param name="controller">Path controller</param>
        /// <param name="area">Path area</param>
        /// <returns></returns>
        public static string IsActive(this IHtmlHelper<dynamic> html, string action = null, string controller = null, string area = null)
        {
            var routeData = html.ViewContext.RouteData;
            var routeController = routeData.Values["controller"].ToString();
            var routeAction = action != null ? routeData.Values["action"].ToString() : null;
            var routeArea = area != null && routeData.Values["area"] != null ? routeData.Values["area"].ToString() : null;

            var isActive = (controller == null || controller.Equals(routeController, StringComparison.CurrentCultureIgnoreCase)) && 
                           (action == null || action.Equals(routeAction, StringComparison.CurrentCultureIgnoreCase)) && 
                           (area == null || area.Equals(routeArea, StringComparison.CurrentCultureIgnoreCase));
            return isActive ? "active" : "";
        }

        /// <summary>
        /// Creates tinymce editor, working with Tinymce in WebScripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="height"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IHtmlContent TinyMceFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, int height = 400)
        {
            return html.TextAreaFor(expression, new { @data_editor = "tinymce", @data_height= height });
        }
        
        /// <summary>
        /// Creates ace editor - working with Ace in WebScripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="readOnly"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IHtmlContent AceEditorFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, bool readOnly = false)
        {
            var builder = new TagBuilder("div");
            
            var editor = new TagBuilder("div");
            editor.Attributes.Add("data-editor", "ace");
            editor.Attributes.Add("data-for", ExpressionHelper.GetExpressionText(expression));
            editor.Attributes.Add("data-readonly", readOnly.ToString().ToLower());

            try
            {
                editor.InnerHtml.Append(expression.Compile()(html.ViewData.Model).ToString());
            }
            catch (NullReferenceException)
            {
                // Nothing in data, not filling
            }

            builder.InnerHtml.AppendHtml(editor);
            builder.InnerHtml.AppendHtml(html.TextAreaFor(expression, new {@style="display: none;"}));
            
            return builder;
        }

        /// <summary>
        /// Creates bootstrap datepicker - working with WebScripts DateTimePicker
        /// </summary>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IHtmlContent DatePickerFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression)
        {
            var builder = new TagBuilder("div");
            builder.AddCssClass("input-group date");
            builder.Attributes.Add("data-editor", "bootstrap-datepicker");
            builder.InnerHtml.AppendHtml(html.EditorFor(expression, new { htmlAttributes = new {@class = "form-control", type = "text"}}));
            
            var inputAppend = new TagBuilder("div");
            inputAppend.Attributes.Add("class", "input-group-append");
            
            var iconGroup = new TagBuilder("span");
            iconGroup.Attributes.Add("class", "input-group-text");
            
            var icon = new TagBuilder("i");
            icon.AddCssClass("far fa-calendar-alt");

            iconGroup.InnerHtml.AppendHtml(icon);
            inputAppend.InnerHtml.AppendHtml(iconGroup);
            builder.InnerHtml.AppendHtml(inputAppend);;

            return builder;
        }
    }
}