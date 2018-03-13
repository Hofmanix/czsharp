using Bridge;
using WebScripts.DateTimePicker;
using WebScripts.Tags;
using WebScripts.TinyMce;
using WebScripts.FullCalendar;

[assembly:Convention(Notation.LowerCamelCase)]
namespace WebScripts
{
    public class App
    {
        /// <summary>
        /// Method running at script start, creates instances of all stripts for page
        /// </summary>
        public static void Main()
        {
            var calendars = new Calendars();
            var forms = new Forms();
            var editors = new Editors();
            var tooltips = new Tooltips();
            var confirms = new Confirms();
            var ace = new AceEditor();
            var tags = new TagsInput();
            var datePickers = new DatePickers();
            var categories = new Categories();
            var comments = new Comments();
        }
       
    }
}
