using AgendaService;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace AgendaService.Service.Utils
{
    public class HttpRestClient
    {
        public static async Task<T> GetAsync<T>(string url) where T : class
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false))
                    {
                        var retorno = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        if (httpResponseMessage.IsSuccessStatusCode)
                            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result) as T;
                        return default(T);
                    }
                }
                finally
                {
                    if (httpClient != null)
                        httpClient.Dispose();
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        
        public static async Task<T> PostAsync<T>(string url, object dto) where T : class
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string content = string.Empty;
                    if (dto != null)
                        content = JsonConvert.SerializeObject(dto);
                    using (HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, (HttpContent)new StringContent(content, Encoding.UTF8, "application/json")).ConfigureAwait(false))
                    {
                        string str = await httpResponseMessage.Content.ReadAsStringAsync();
                        if (httpResponseMessage.IsSuccessStatusCode)
                            return (T)JsonConvert.DeserializeObject<T>(str);
                        return default(T);
                    }
                }
                finally
                {
                    if (httpClient != null)
                        httpClient.Dispose();
                }
            }
            catch (Exception)
            {            
                return default(T);
            }
        }
    }
}