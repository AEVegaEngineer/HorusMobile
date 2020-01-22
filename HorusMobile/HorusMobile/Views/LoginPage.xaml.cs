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

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            
            var user = username.Text;
            var pass = password.Text;
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
            /*
            if (string.IsNullOrWhiteSpace(users.username))
            {
                // redirigir y mostrar mensaje de error en login
            }
            else
            {
                // Redirige al menú
                
                //await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            */
            //Debug.WriteLine("calling the door");

                      

        }

    }
}