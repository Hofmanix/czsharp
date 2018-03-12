using Retyped;

namespace WebScripts
{
    public class Tooltips
    {
        public Tooltips()
        {
            jquery.jQuery.@select("[data-toggle='tooltip']").bootstrap().tooltip();
        }
    }
}