using System;
using Retyped;

namespace WebScripts.DateTimePicker
{
    public class DatePickers
    {
        /// <summary>
        /// Creates datetimepickers at all elements with data-editor=bootstrap-datepicker
        /// </summary>
        public DatePickers()
        {
            var pickers = jquery.jQuery.@select("[data-editor='bootstrap-datepicker']");
            Console.WriteLine(pickers);
            if (pickers.length > 0)
            {
                pickers.Datetimepicker(new
                {
                    AllowInputToggle = true
                });
            }
        }
    }
}