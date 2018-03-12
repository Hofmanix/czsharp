using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace CzSharp.Utils.Extensions
{
    public static class HtmlHelperExtensions
    {
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

        public static IHtmlContent TinyMceFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, int height = 400)
        {
            return html.TextAreaFor(expression, new { @data_editor = "tinymce", @data_height= height });
        }
        
        public static IHtmlContent AceEditorFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, bool readOnly = false)
        {
            var builder = new TagBuilder("div");
            
            var editor = new TagBuilder("div");
            editor.Attributes.Add("data-editor", "ace");
            editor.Attributes.Add("data-for", ExpressionHelper.GetExpressionText(expression));
            editor.Attributes.Add("data-readonly", readOnly.ToString().ToLower());
            editor.InnerHtml.Append(expression.Compile()(html.ViewData.Model).ToString());

            builder.InnerHtml.AppendHtml(editor);
            builder.InnerHtml.AppendHtml(html.TextAreaFor(expression, new {@style="display: none;"}));
            
            return builder;
        }

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