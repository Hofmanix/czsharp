using System;
using Retyped;

namespace WebScripts
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
                tinymce.init(new tinymce.Settings
                {
                    selector = "textarea"
                });
            }
            catch (Exception)
            {
                // Tinymce doesn't exists
            }
        }
    }
    
}