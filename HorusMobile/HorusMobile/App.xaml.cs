using Xamarin.Forms;
using HorusMobile.Services;
using HorusMobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using HorusMobile.Models;
using Com.OneSignal;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace HorusMobile
{
    public partial class App : Application, ILoginManager
    {
        public Item Item { get; set; }
        public static App Current;
        public static int val;
        private string playerId;
        public App()
        {
            InitializeComponent();
            Current = this;
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<NotifDataStore>();
            //MainPage = new MainPage();
            var isLoggedIn = Properties.ContainsKey("_json_token") ? true : false;
            if(isLoggedIn)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage(this);
            OneSignal.Current.StartInit("5bd931bc-a426-44ec-85a5-bfd47a771213")
                  .EndInit();
            ShowPlayerIdHandler();
        }
        protected override void OnStart()
        {
            //AppCenter.Start("66dd0120-23ef-484f-a21c-e9d4f19a2e36", typeof(Push)); 
                                 
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
        private void ShowPlayerIdHandler()
        {
            OneSignal.Current.IdsAvailable(new Com.OneSignal.Abstractions.IdsAvailableCallback((playerID, pushToken) =>
            {
                this.playerId = playerID;
            }));
        }
        public string getCurrentDeviceId()
        {
            return this.playerId;
        }
    }
}
