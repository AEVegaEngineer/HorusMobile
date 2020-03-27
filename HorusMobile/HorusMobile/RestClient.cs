using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HorusMobile
{
    public class RestClient
    {
        public async Task<T> Get<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nERROR EN RESTCLIENT!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return default(T);
        }
    }
}
