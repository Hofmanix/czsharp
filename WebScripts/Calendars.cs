using System;
using Retyped;
using WebScripts.FullCalendar;

namespace WebScripts
{
    public class Calendars
    {
        public Calendars()
        {
            InitSmallCalendar();
        }
        
        private void InitSmallCalendar()
        {
            var calendar = jquery.jQuery.@select("#small-calendar");
            if (calendar.length > 0)
            {
                calendar.FullCalendar(new
                {
                    Header = false
                });
            }
            
        }
    }
}