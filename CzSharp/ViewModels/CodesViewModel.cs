using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for codes with pagination
    /// </summary>
    public class CodesViewModel
    {
        public IQueryable<Code> Codes { get; set; }
        public int CodeCount { get; set; }
        public int ActivePage { get; set; }
    }
}