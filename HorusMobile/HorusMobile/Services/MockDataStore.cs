using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorusMobile.Models;

namespace HorusMobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>() { };
            /*
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                
                new Item { Id = Guid.NewGuid().ToString(), Text = "Notificación Ejemplo", Description="Este es el cuerpo de una notificación de ejemplo. Nuevas notificaciones deberán aparecer de este modo." }
            };
            */
        }
        /*
        public async Task<List<Item>> GetNotificationAsync()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
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

                //envío el request por POST
                var result = client.PostAsync("http://192.168.50.98/intermedio/apirest/notifications/get.php", byteContent).Result;

                if (result != null)
                {
                    var contents = await result.Content.ReadAsStringAsync();

                    //reviso si se ha hecho una conexión correcta con el servidor
                    if (!LoginPage.IsValidJson(contents))
                    {
                        //await DisplayAlert("Error", "No se ha obtenido respuesta del servidor, revise su conexión a internet.", "OK");
                        Debug.WriteLine("\n\nERROR No se ha obtenido respuesta del servidor, revise su conexión a internet.\n\n");
                        App.Current.Logout();
                    }

                    //Deserializo el JSON resultante para obtener los datos del usuario

                    var j = JArray.Parse(contents);
                    List<Item> lista = new List<Item> { };
                    foreach (var notif in j)
                    {
                        var Notif = JsonConvert.DeserializeObject<Notificaciones>(notif.ToString());
                        if (Notif.id_notif_cuerpo == null)
                        {
                            Debug.WriteLine("\n\nERROR " + Notif.message + "\n" + Notif.error + "\n\n");
                            return;
                        }
                        else
                        {
                            Item = new Item
                            {
                                Text = Notif.asunto,
                                Description = Notif.mensaje
                            };
                            lista.Add(Item);
                        }
                    }
                    return lista;
                }
                else
                {
                    Debug.WriteLine("\n\nRESULT NULL ERROR AT APP.GETNOTIFICATIONS\n\n");
                }
            });
        }
        */
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}