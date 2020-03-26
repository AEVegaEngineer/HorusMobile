using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace HorusMobile.Models
{
    [Preserve(AllMembers = true)]
    public class Users
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }        
        public string estado { get; set; }
        public string tipo { get; set; }
        public string fk_personal { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public string deviceId { get; set; }

    }
    /*
    [Preserve(AllMembers = true)]
    public class Usuario
    {

        public string username;
        public string password;
        public string deviceId;

        public Usuario(string login, string pass, string deviceId)
        {
            this.username = login;
            this.password = pass;
            this.deviceId = deviceId;
        }
    }
    */
}
