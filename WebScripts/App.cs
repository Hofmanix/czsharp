using Bridge;

[assembly:Convention(Notation.LowerCamelCase)]
namespace WebScripts
{
    public class App
    {
        public static void Main()
        {
            var calendars = new Calendars();
            var forms = new Forms();
            var editors = new Editors();
        }
       
    }
}
