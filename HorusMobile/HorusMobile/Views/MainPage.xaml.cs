using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

using HorusMobile.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace HorusMobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {

            InitializeComponent(); 

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
            
            var usr_id = Application.Current.Properties["_user_id"];
            
        }

        //public async Task NavigateFromMenu(int id)
        public async void NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    /*
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                        */
                    case (int)MenuItemType.Logout:
                        HttpClient client = new HttpClient();
                        Users usuario = new Users();
                        usuario.deviceId = App.Current.getCurrentDeviceId();
                        usuario.id = Application.Current.Properties["_user_id"].ToString();
                        var myContent = JsonConvert.SerializeObject(usuario);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var result = await client.PostAsync("http://colegiomedico.i-tic.com/horus/apirest/usuarios/logout.php", byteContent);
                        App.Current.Logout();
                        break;
                }
            }
            /*
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
            */
        }
    }
}