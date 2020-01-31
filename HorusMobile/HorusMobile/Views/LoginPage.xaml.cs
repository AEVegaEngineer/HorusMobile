using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.ComponentModel;
using HorusMobile.Models;
using HorusMobile.Views;
using HorusMobile.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace HorusMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {            
            InitializeComponent();
            // revisar si el token esta set, si lo está, redireccionar.
        }
        private bool _pbIndicator;
        public bool PBIndicator
        {
            get { return _pbIndicator; }
            set
            {
                _pbIndicator = value;
                OnPropertyChanged();
            }
        }
        /*
        void UpdateUiState()
        {
            Debug.WriteLine("\n\nUPDATEUISTATE\n\n");
            lblStatus.Text = statusSesion ? "Iniciando Sesión..." : "";
            IndicadorActividad.IsRunning = statusSesion;
            IndicadorActividad.IsVisible = statusSesion;
            IndicadorActividad.IsEnabled = statusSesion;
        }
        */
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PBIndicator = true;
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            
            var user = username.Text;
            var pass = password.Text;            

            if (string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Login", "Debe escribir un usuario y una contraseña", "OK");
                Debug.WriteLine("Debe escribir un usuario y una contraseña");
                return;
            }

            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                //Muestra el activity indicator para el login
                PBIndicator = !PBIndicator;

                //RestClient client = new RestClient();
                HttpClient client = new HttpClient();

                //var getUserLogin = await client.Get<getUserLogin>("http://192.168.50.98/intermedio/api/usuarios/login.php");
                Users usuario = new Users();
                usuario.password = pass;
                usuario.username = user;

                //serializo el objeto a json
                var myContent = JsonConvert.SerializeObject(usuario);

                //construyo un objeto contenido para mandar la data, uso un objeto ByteArrayContent
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                //establezco el tipo de contenido a JSON para que la api la reconozca
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //envío el request por POST
                var result = client.PostAsync("http://192.168.50.98/intermedio/api/usuarios/login.php", byteContent).Result;

                if (result != null)
                {
                    var contents = await result.Content.ReadAsStringAsync();                    
                    Debug.WriteLine("\n\nlogin ejecutando:"+ contents);

                    //Deserializo el JSON resultante para obtener los datos del usuario
                    Users u = JsonConvert.DeserializeObject<Users>(contents);

                    //Quita el activity indicator para el login
                    PBIndicator = !PBIndicator;

                    if (u.id == null)
                    {
                        await DisplayAlert("Login", "Usuario o pass incorrecto", "OK");
                        Debug.WriteLine("\n\nUsuario o pass incorrecto\n\n");
                    }
                    else
                    {
                        try
                        {
                            //seteo el id del usuario en la app, para postearla al appcenter
                            AppCenter.SetUserId(u.id);
                            //seteo el token de la app para persistirlo
                            //Application.Current.Properties["_app_token"] = oLoginService.AppToken;

                            //Muestro la página principal
                            await Navigation.PushModalAsync(new MainPage());

                            await Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }                   

                }
                else
                {
                    Debug.WriteLine("\n\nRESULT NULL ERROR\n\n");
                }

            });            

        }

    }
}