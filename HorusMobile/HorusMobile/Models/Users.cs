using System;
using System.Collections.Generic;
using System.Text;

namespace HorusMobile.Models
{
    public class Users
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }        
        public string estado { get; set; }
        public string tipo { get; set; }
        public string fk_personal { get; set; }
    }
}
