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