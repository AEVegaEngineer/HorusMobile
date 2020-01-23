using System;
using System.Collections.Generic;
using System.Text;

namespace HorusMobile
{
    public class Data
    {
        public string id { get; set; }
        public string login { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string fk_personal { get; set; }
    }

    public class getUserLogin
    {
        public Data data { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
    }
}
