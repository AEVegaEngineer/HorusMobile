using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HorusMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

namespace HorusMobile.Services
{
    [Preserve(AllMembers = true)]
    public class NotifDataStore : INotification<Item>
    {
        readonly List<Item> items;

        public NotifDataStore()
        {
            items = new List<Item>() { };            
        }
        
        public async Task<bool> AddNotifAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateNotifAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteNotifAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }        

        public async Task<Item> GetNotifAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Item>> GetNotifsAsync(bool forceRefresh = false)
        {
            //RestClient client = new RestClient();
            HttpClient client = new HttpClient();

            //serializo el objeto a json
            var token = new token { jwt = App.Current.Properties["_json_token"].ToString() };
            var myContent = JsonConvert.SerializeObject(token);

            //construyo un objeto contenido para mandar la data, uso un objeto ByteArrayContent
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            //establezco el tipo de contenido a JSON para que la api la reconozca
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //http://192.168.50.98/intermedio
            //envío el request por POST
            var result = client.PostAsync("http://colegiomedico.i-tic.com/horus/apirest/notifications/get.php", byteContent).Result;

            if (result != null)
            {
                var contents = await result.Content.ReadAsStringAsync();

                //reviso si se ha hecho una conexión correcta con el servidor
                if (!this.IsValidJson(contents))
                {
                    await DisplayAlert("Error", "No se ha obtenido respuesta del servidor, revise su conexión a internet.", "OK");
                    //Debug.WriteLine("\n\nERROR No se ha obtenido respuesta del servidor, revise su conexión a internet.\n\n");
                    App.Current.Logout();
                }

                //Deserializo el JSON resultante para obtener los datos del usuario

                var j = JArray.Parse(contents);
                List<Item> itemsRetornados = new List<Item>() { };
                foreach (var notif in j)
                {
                    var Notif = JsonConvert.DeserializeObject<Notificaciones>(notif.ToString());
                    if (Notif.id_cuerpo == null)
                    {
                        await DisplayAlert("Error", "ERROR " + Notif.message + "\n" + Notif.error, "OK");                        
                    }
                    else
                    {
                        Item objeto = new Item
                        {
                            Text = Notif.asunto,
                            Description = Notif.mensaje,
                            id_cuerpo = Notif.id_cuerpo,
                            estado = int.Parse(Notif.estado)
                        };
                        itemsRetornados.Add(objeto);
                    }
                }
                //Debug.WriteLine("\n\n**************RETORNA ITEMS*************\n\n");
                return await Task.FromResult(itemsRetornados);

            }
            else
            {
                List<Item> itemsRetornados = new List<Item>() { };
                await DisplayAlert("Error", "RESULT NULL ERROR AT NOTIFDATASTORE.GETNOTIFASYNC", "OK");
                return await Task.FromResult(itemsRetornados);
            }
        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}