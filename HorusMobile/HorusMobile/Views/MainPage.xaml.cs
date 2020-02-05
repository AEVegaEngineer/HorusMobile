using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HorusMobile.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace HorusMobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            //validarToken();

            InitializeComponent(); 

            MasterBehavior = MasterBehavior.Popover;            

            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        //public async Task NavigateFromMenu(int id)
        public void NavigateFromMenu(int id)
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
        private void validarToken()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                //RestClient client = new RestClient();
                HttpClient client = new HttpClient();
                
                //serializo el objeto a json
                var myContent = JsonConvert.SerializeObject("{'jwt':'" + Application.Current.Properties["_json_token"] + "'}");

                //construyo un objeto contenido para mandar la data, uso un objeto ByteArrayContent
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                //establezco el tipo de contenido a JSON para que la api la reconozca
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //envío el request por POST
                var result = client.PostAsync("http://192.168.50.98/intermedio/apirest/usuarios/validate.php", byteContent).Result;

                if (result != null)
                {
                    var contents = await result.Content.ReadAsStringAsync();                    

                    //reviso si se ha hecho una conexión correcta con el servidor
                    if (!LoginPage.IsValidJson(contents))
                    {
                        await DisplayAlert("Error", "No se ha obtenido respuesta del servidor, revise su conexión a internet.", "OK");

                        //Muestro la página principal
                        await Navigation.PushModalAsync(new MainPage());
                        await Navigation.PopAsync();

                        return;
                    }

                    //Deserializo el JSON resultante para obtener los datos del usuario
                    Users u = JsonConvert.DeserializeObject<Users>(contents);
                    
                    if (u.id == null)
                    {
                        // Si el usuario no está autorizado para ver esta página
                        // se redirige al login
                        await DisplayAlert(u.message, u.error, "OK");
                        //Muestro la página principal
                        //MainPage = new LoginPage();

                        return;
                    }

                }
                else
                {
                    Debug.WriteLine("\n\nRESULT NULL ERROR AT MAINPAGE\n\n");
                }

            });
        }
    }
}