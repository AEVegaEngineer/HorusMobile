using HorusMobile.Models;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace HorusMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    //[Preserve(AllMembers = true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                /*
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Buscar" },
                new HomeMenuItem {Id = MenuItemType.About, Title="Sobre Nosotros" }
                */
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Cerrar Sesión"}
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (id == (int)MenuItemType.Logout)
                    RootPage.NavigateFromMenu(id);
            };
        }
    }
}