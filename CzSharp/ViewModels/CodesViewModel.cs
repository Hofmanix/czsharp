using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    public class CodesViewModel
    {
        public IQueryable<Code> Codes { get; set; }
        public int CodeCount { get; set; }
        public int ActivePage { get; set; }
    }
}