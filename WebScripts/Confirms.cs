using System;
using Bridge.Html5;
using Retyped;

namespace WebScripts
{
    public class Confirms
    {
        public Confirms()
        {
            jquery.jQuery.@select("[data-confirm]").click(ObjectWithConfirmClicked);
        }
        
        private object ObjectWithConfirmClicked(jquery.JQueryEventObject evt)
        {
            Console.WriteLine("Confirm clicked");
            if (!Window.Confirm(jquery.jQuery.@select(evt.target).attr("data-confirm")))
            {
                evt.preventDefault();
            }

            return null;
        }
    }
}