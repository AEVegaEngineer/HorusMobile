using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace HorusMobile.Models
{
    [Preserve(AllMembers = true)]
    public enum MenuItemType
    {
        /*
        Browse,
        About,
        */
        Logout
    }
    [Preserve(AllMembers = true)]
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
