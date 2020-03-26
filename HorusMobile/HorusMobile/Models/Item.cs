using System;
using Xamarin.Forms.Internals;

namespace HorusMobile.Models
{
    [Preserve(AllMembers = true)]
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string id_cuerpo { get; set; }
        public int estado { get; set; }

    }
}