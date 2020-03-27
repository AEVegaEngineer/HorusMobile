using HorusMobile.Models;
using HorusMobile.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HorusMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Título de notificación 1",
                Description = "Ejemplo de cuerpo de notificación."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void VerMasInfoHorusTapped(object sender, EventArgs e)
        {
            string user = App.Current.Properties["_user_login"].ToString();
            string pass = App.Current.Properties["_user_pass"].ToString();
            //http://192.168.50.98/intermedio/funciones/
            await Browser.OpenAsync("http://colegiomedico.i-tic.com/horus/funciones/login_app.php?usuario=" + user + "&pass=" + pass, BrowserLaunchMode.SystemPreferred);
        }
    }
}