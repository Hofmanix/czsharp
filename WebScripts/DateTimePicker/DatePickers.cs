using System;
using Retyped;

namespace WebScripts.DateTimePicker
{
    public class DatePickers
    {
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