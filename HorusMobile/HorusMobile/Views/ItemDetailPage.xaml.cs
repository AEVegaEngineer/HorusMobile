using System.ComponentModel;
using Xamarin.Forms;

using HorusMobile.Models;
using HorusMobile.ViewModels;

using Xamarin.Essentials;
using System;

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
            //http://colegiomedico.i-tic.com/horus
            await Browser.OpenAsync("http://192.168.50.98/intermedio/login_simple.html", BrowserLaunchMode.SystemPreferred);
        }
        
    }
}