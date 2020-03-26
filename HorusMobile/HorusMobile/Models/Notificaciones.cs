using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace HorusMobile.Models
{
    [Preserve(AllMembers = true)]
    public class Notificaciones
    {
        public string id_cuerpo { get; set; }
        public string fk_cabecera { get; set; }
        public string user { get; set; }
        public string asunto { get; set; }
        public string estado { get; set; }
        public string mensaje { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}
