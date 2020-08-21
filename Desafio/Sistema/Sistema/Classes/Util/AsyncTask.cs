using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Classes.Util
{
    class AsyncTask
    {
        public Delegate callback;
        public AsyncTask(Delegate callback) 
        { 
            this.callback = callback;
        }

        public async Task run(String url)
        {
            using (var client = new HttpClient())
            {
                String webmethod = "";
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var myContent = JsonConvert.SerializeObject(webmethod);
                String parametro = WebUtility.UrlEncode(myContent);

                HttpResponseMessage response;
                response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    callback.DynamicInvoke(json);
                }
            }
        }
    }
}
