using HorusMobile.Models;
using HorusMobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
/*
using System.ComponentModel;
using HorusMobile.Views;
using HorusMobile.ViewModels;
*/
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
/*
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HorusMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ILoginManager iml = null;
        public LoginPage(ILoginManager ilm)
        {
            InitializeComponent();
            iml = ilm;
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
            IndicadorActividad.IsRunning = true;
            IndicadorActividad.IsEnabled = true;
            IndicadorActividad.IsVisible = true;

            //Muestra el activity indicator para el login
            PBIndicator = !PBIndicator;
            IsBusy = true;

            var user = username.Text;
            var pass = password.Text;

            if (string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Login", "Debe escribir un usuario y una contraseña", "OK");
                Debug.WriteLine("Debe escribir un usuario y una contraseña");
                return;
            }

            

            //RestClient client = new RestClient();
            HttpClient client = new HttpClient();

            //var getUserLogin = await client.Get<getUserLogin>("http://192.168.50.98/intermedio/api/usuarios/login.php");
            Users usuario = new Users();
            //Usuario usuario = new Usuario(user,pass, App.Current.getCurrentDeviceId());
            usuario.password = pass;
            usuario.username = user;
            usuario.deviceId = App.Current.getCurrentDeviceId();
            Application.Current.Properties["_user_login"] = user;
            Application.Current.Properties["_user_pass"] = pass;
            Application.Current.Properties["_device_id"] = App.Current.getCurrentDeviceId();

            //serializo el objeto a json
            var myContent = JsonConvert.SerializeObject(usuario);
            //construyo un objeto contenido para mandar la data, uso un objeto ByteArrayContent
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            //establezco el tipo de contenido a JSON para que la api la reconozca
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //http://192.168.50.98/intermedio
            var result = (HttpResponseMessage)null;
            try
            {
                //envío el request por POST
                result = client.PostAsync("http://colegiomedico.i-tic.com/horus/apirest/usuarios/login.php", byteContent).Result;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
            }
            if (result != null)
            {
                var contents = await result.Content.ReadAsStringAsync();
                //reviso si se ha hecho una conexión correcta con el servidor
                if (!IsValidJson(contents))
                {
                    await DisplayAlert("Error", "No se ha obtenido respuesta del servidor, revise su conexión a internet.", "OK");
                    return;
                }

                //Deserializo el JSON resultante para obtener los datos del token de sesión
                token tk = JsonConvert.DeserializeObject<token>(contents);
                //Token tk = new Token(contents["id"], contents["jwt"], contents["message"]);


                //Quita el activity indicator para el login
                PBIndicator = !PBIndicator;
                IsBusy = false;

                if (tk.message == "FAIL")
                {
                    await DisplayAlert("Login", "Usuario o pass incorrecto", "OK");
                }
                else if (tk.message == null)
                {
                    ErrorEnConexion(130);
                }
                else
                {
                    try
                    {
                        //seteo el id del usuario en la app, para postearla al appcenter
                        var usr_id = tk.id;
                        //seteo el token de la  app para persistirlo
                        Application.Current.Properties["_user_id"] = tk.id;
                        Application.Current.Properties["_json_token"] = tk.jwt;

                        //Muestro la página principal
                        iml.ShowMainPage();
                        /*
                        await Navigation.PushModalAsync(new MainPage());
                        await Navigation.PopAsync();
                        */
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        await DisplayAlert("Error en Login", ex.ToString(), "OK");
                    }
                }

            }
            else
            {
                ErrorEnConexion(159);
            }

            IndicadorActividad.IsRunning = false;
            IndicadorActividad.IsEnabled = false;
            IndicadorActividad.IsVisible = false;
        }
        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async void ErrorEnConexion(int line)
        {
            Debug.WriteLine("\n\nRESULT NULL ERROR IN LINE "+line+"\n\n");
            await DisplayAlert("Error de red", "No se ha logrado hacer conexión con http://colegiomedico.i-tic.com, revise su conexión o hable con el administrador de la red.", "OK");
        }
    }
}