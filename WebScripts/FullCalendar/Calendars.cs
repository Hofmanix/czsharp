using System;
using Bridge.Html5;
using Retyped;
using WebScripts.FullCalendar;

namespace WebScripts.FullCalendar
{
    public class Calendars
    {
        public Calendars()
        {
            InitSmallCalendar();
            InitCalendar();
        }
        
        /// <summary>
        /// Inits small calendar at the side
        /// </summary>
        private void InitSmallCalendar()
        {
            var calendar = jquery.jQuery.@select("#small-calendar");
            if (calendar.length > 0)
            {
                calendar.FullCalendar(new
                {
                    Header = false,
                    ThemeSystem = "bootstrap4",
                    AspectRatio = 1.15,
                    Events = "/events/range/",
                    EventClick = new Action<FullCalendarEvent>(EventClicked)
                });
            }
        }

        /// <summary>
        /// Inits big calendar at events page
        /// </summary>
        private void InitCalendar()
        {
            var calendar = jquery.jQuery.@select("#calendar");
            if (calendar.length > 0)
            {
                calendar.FullCalendar(new
                {
                    Header = new
                    {
                        Left = "title",
                        Center = "",
                        Right = "today prev,next"
                    },
                    Locale = "cs",
                    ThemeSystem = "bootstrap4",
                    AspectRatio = 1.15,
                    Events = "/events/range/",
                    EventClick = new Action<FullCalendarEvent>(EventClicked)
                });
            }
        }

        private void EventClicked(FullCalendarEvent fcEvent)
        {
            Window.Location.Href = $"/events/detail/{fcEvent.Id}";
        }
    }
}