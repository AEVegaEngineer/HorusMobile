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


namespace HorusMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {            
            InitializeComponent();
        }

        /*
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient client = new RestClient();
                var getUserLogin = await client.Get<getUserLogin>("http://192.168.50.98/intermedio/api/usuarios/login.php");
                if (getUserLogin != null)
                {
                    LabelChange.Text = getUserLogin.data.id;
                    Debug.WriteLine("\n\nejecutado correctamente: " + getUserLogin.data.id);
                }
                else
                {
                    Debug.WriteLine("\n\nMAINPAGE NULL ERROR\n\n");
                }
            });

            
        }
        */

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            
            var user = username.Text;
            var pass = password.Text;
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient client = new RestClient();
                var getUserLogin = await client.Get<getUserLogin>("http://192.168.50.98/intermedio/api/usuarios/login.php");
                if (getUserLogin != null)
                {
                    LabelChange.Text = getUserLogin.data.id;
                    Debug.WriteLine("\n\nlogin ejecutado correctamente: "+getUserLogin.data.id);
                }
                else
                {
                    Debug.WriteLine("\n\nMAINPAGE NULL ERROR\n\n");
                }
            });
            /*
            if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
            {              
                if (user == "10" & pass == "10")
                {
                    Debug.WriteLine("logeado");
                    try
                    {
                        await Navigation.PushModalAsync(new MainPage());

                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                } else {
                    await DisplayAlert("Login", "Usuario o pass incorrecto", "OK");
                    Debug.WriteLine("Usuario o pass incorrecto"); 
                }
            }
            else
            {
                await DisplayAlert("Login", "Debe escribir un usuario y una contraseña", "OK");
                Debug.WriteLine("Debe escribir un usuario y una contraseña");
            }     
            */

        }

    }
}