using System;
using Bridge;

namespace WebScripts.Tags
{
    public class TypeAheadOptions
    {
        public Union<string[], Action<string>> Source { get; set; }
    }
}