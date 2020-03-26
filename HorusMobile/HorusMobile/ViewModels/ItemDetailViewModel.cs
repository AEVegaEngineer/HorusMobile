using System;

using HorusMobile.Models;
using Xamarin.Forms.Internals;

namespace HorusMobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
