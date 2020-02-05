using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HorusMobile.Services;
using HorusMobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using HorusMobile.Models;
using System.Collections.Generic;

namespace HorusMobile
{
    public partial class App : Application, ILoginManager, INotification
    {
        public Item Item { get; set; }
        public static App Current;
        public static int val;
        public App()
        {
            InitializeComponent();
            Current = this;
            DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
            var isLoggedIn = Properties.ContainsKey("_json_token") ? true : false;
            if(isLoggedIn)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage(this);
        }
        protected override void OnStart()
        {
            AppCenter.Start("66dd0120-23ef-484f-a21c-e9d4f19a2e36", typeof(Push));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void ShowMainPage()
        {
            MainPage = new MainPage();
        }

        public void Logout()
        {
            Properties["_json_token"] = false;
            MainPage = new LoginPage(this);
        }

        public void getNotifications()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                //RestClient client = new RestClient();
                HttpClient client = new HttpClient();

                //serializo el objeto a json
                var token = new token { jwt = Application.Current.Properties["_json_token"].ToString() };
                var myContent = JsonConvert.SerializeObject(token);

                //construyo un objeto contenido para mandar la data, uso un objeto ByteArrayContent
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                //establezco el tipo de contenido a JSON para que la api la reconozca
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //envío el request por POST
                var result = client.PostAsync("http://192.168.50.98/intermedio/apirest/notifications/get.php", byteContent).Result;

                if (result != null)
                {
                    var contents = await result.Content.ReadAsStringAsync();

                    //reviso si se ha hecho una conexión correcta con el servidor
                    if (!LoginPage.IsValidJson(contents))
                    {
                        //await DisplayAlert("Error", "No se ha obtenido respuesta del servidor, revise su conexión a internet.", "OK");
                        Debug.WriteLine("\n\nERROR No se ha obtenido respuesta del servidor, revise su conexión a internet.\n\n");
                        App.Current.Logout();
                    }

                    //Deserializo el JSON resultante para obtener los datos del usuario

                    var j = JArray.Parse(contents);
                    foreach (var notif in j)
                    {
                        var Notif = JsonConvert.DeserializeObject<Notificaciones>(notif.ToString());
                        if (Notif.id_notif_cuerpo == null)
                        {
                            Debug.WriteLine("\n\nERROR " + Notif.message + "\n" + Notif.error + "\n\n");                           
                            return;
                        }
                        else
                        {
                            Item = new Item
                            {
                                Text = Notif.asunto,
                                Description = Notif.mensaje
                            };
                            MessagingCenter.Send(this, "AddItem", Item);
                            Debug.WriteLine("\n\n**************EJECUTANDO SEND*************\n\n");
                        }
                    }                 
                }
                else
                {
                    Debug.WriteLine("\n\nRESULT NULL ERROR AT APP.GETNOTIFICATIONS\n\n");
                }
            });
        }
    }
}
