using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace HorusMobile.Models
{
    [Preserve(AllMembers = true)]
    public class token
    {
        public string id { get; set; }
        public string jwt { get; set; }
        public string message { get; set; }
    }
}
