using Conta.Application.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Conta.WebAPP.Util
{
    public static class HttpUtil
    {        

        public static USD Get(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseGet = client.GetAsync(url).Result;

                if (responseGet.IsSuccessStatusCode)
                {
                   var dataObject = responseGet.Content.ReadAsAsync<CotacaoUSD>().Result;

                    return dataObject.Usd;
                }
                else
                {
                    var message = responseGet.Content.ReadAsStringAsync();

                    throw new ApplicationException(message.Result);
                }
            }
        }      
    }
}
