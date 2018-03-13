using System;
using System.Collections.Generic;
using Retyped;

namespace WebScripts
{
    public class AceEditor
    {
        private static string aceEditorSelector = "div[data-editor='ace']";
        
        /// <summary>
        /// Creates ace editors from all divs with data-editor=ace
        /// </summary>
        public AceEditor()
        {
            try
            {
                var editors = jquery.jQuery.@select(aceEditorSelector);
                
                for (var i = 0; i < editors.length; i++)
                {
                    var aceEditor = ace.ace2.edit(editors.get(i));
                    if (aceEditor != null)
                    {
                        var jEditor = jquery.jQuery.@select(editors[i]);
                        
                        aceEditor.setTheme("ace/theme/chrome");
                        aceEditor.session.setMode("ace/mode/csharp");
                        aceEditor.setReadOnly(jEditor.attr("data-readonly") == "true");

                        jEditor.closest("form").submit(evt => FormSubmitted(aceEditor, evt));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Ace not found, continue
            }
        }

        private object FormSubmitted(ace.AceAjax.Editor editor, jquery.BaseJQueryEventObject evt)
        {
            var form = jquery.jQuery.@select(evt.target);
            var editorDiv = form.find(aceEditorSelector);
            var textarea = form.find($"[name='{editorDiv.attr("data-for")}']");
            
            textarea.val(editor.session.getValue());
            return null;
        }
    }
}