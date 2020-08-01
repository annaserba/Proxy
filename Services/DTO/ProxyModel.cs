using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class ProxyModel
    {
        public Uri FullOriginal { get; set; }
        public SortedList<string, string> ReplacePatterns { get; } = new SortedList<string, string>();
        
    }
}
