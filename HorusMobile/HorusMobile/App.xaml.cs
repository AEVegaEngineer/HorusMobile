using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HorusMobile.Services;
using HorusMobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
namespace HorusMobile
{
    public partial class App : Application, ILoginManager
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
            var isLoggedIn = Properties.ContainsKey("_json_token") ? true : false;
            if(isLoggedIn)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
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
            MainPage = new LoginPage();
        }
    }
}
