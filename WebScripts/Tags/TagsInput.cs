using Retyped;

namespace WebScripts.Tags
{
    public class TagsInput
    {
        /// <summary>
        /// Creates tags inputs on all elements with data-editor=tagsinput
        /// </summary>
        public TagsInput()
        {
            var editors = jquery.jQuery.@select("[data-editor='tagsinput']");
            if (editors.length > 0)
            {
                editors.Tagsinput(new TagsInputOptions
                {
                    Typeahead = new TypeAheadOptions
                    {
                        Source = new [] {"First", "Second", "Third"}
                    }
                });
            }
        }
    }
}