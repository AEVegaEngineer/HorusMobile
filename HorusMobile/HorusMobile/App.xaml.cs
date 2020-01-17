using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HorusMobile.Services;
using HorusMobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
namespace HorusMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
            //LoginPage = new LoginPage();
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
    }
}
