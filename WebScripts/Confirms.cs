using System;
using Bridge.Html5;
using Retyped;

namespace WebScripts
{
    public class Confirms
    {
        /// <summary>
        /// Creates confirms for all elements with data-confirm - value is message
        /// </summary>
        public Confirms()
        {
            jquery.jQuery.@select("[data-confirm]").click(ObjectWithConfirmClicked);
        }
        
        /// <summary>
        /// Shows confirm on element click
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
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