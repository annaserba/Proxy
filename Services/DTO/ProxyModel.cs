using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class ProxyModel
    {
        public Uri UriOriginal { get; set; }
        public Uri UriProxy { get; set; }
        public string Query { get; set; }
    }
}
