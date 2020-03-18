using System;
using System.ComponentModel;
using Xamarin.Forms;

using HorusMobile.Models;
using HorusMobile.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HorusMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();            
            BindingContext = viewModel = new ItemsViewModel();
            /*
            Resources["ListNotifTextStyle"] = Resources["labelTextBlack"];
            Resources["ListNotifDetailTextStyle"] = Resources["labelDescriptionBlack"];
            */
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            //cambio de estilo
            /*
            Resources["ListNotifTextStyle"] = Resources["labelTextGray"];
            Resources["ListNotifDetailTextStyle"] = Resources["labelDescriptionGray"];
            */
            

            //Debug.WriteLine("ITEM CUERPO: " + item.id_cuerpo);
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Deselecciona el item manualmente.
            ItemsListView.SelectedItem = null;

            // Marcando como leída la notificación
            HttpClient client = new HttpClient();
            var token = new token { jwt = App.Current.Properties["_json_token"].ToString(), id = item.id_cuerpo };
            var myContent = JsonConvert.SerializeObject(token);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //http://192.168.50.98/intermedio
            var result = client.PostAsync("http://colegiomedico.i-tic.com/horus/apirest/notifications/mark.php", byteContent).Result;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}