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
            var users = (Users)BindingContext;
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
            
            try
            {
                await Navigation.PushModalAsync(new MainPage());

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            

        }

    }
}