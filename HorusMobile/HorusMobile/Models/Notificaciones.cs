using System;
using System.Collections.Generic;
using System.Text;

namespace HorusMobile.Models
{
    public class Notificaciones
    {
        public string id_notif_cuerpo { get; set; }
        public string fk_cabecera { get; set; }
        public string user { get; set; }
        public string asunto { get; set; }
        public string estado { get; set; }
        public string mensaje { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}
