using System;
using Bridge;
using Retyped;

namespace WebScripts.TinyMce
{
    public class Editors
    {
        public Editors()
        {
            InitEditor();
        }
        
        private void InitEditor()
        {
            try
            {
                var editors = jquery.jQuery.@select("textarea[data-editor='tinymce']");
                for (var i = 0; i < editors.length; i++)
                {
                    var editor = editors.get(i);
                    var height = jquery.jQuery.@select(editor).attr("data-height");
                    
                    tinymce.init(new TinyMceSettings
                    {
                        selector = $"#{editor.id}",
                        menubar = false,
                        branding = false,
                        height = Convert.ToInt32(height),
                        plugins = new [] {"lists", "link", "codesample", "image", "imagetools", "wordcount"},
                        toolbar = "undo redo | bold italic underline | alignleft aligncenter alignright | bullist numlist | outdent indent | link image  | codesample"
                    });
                }
               
            }
            catch (Exception)
            {
                // Tinymce doesn't exists
            }
        }
        
    }
    
}