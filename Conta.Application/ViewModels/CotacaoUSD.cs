using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conta.Application.ViewModels
{
   public class CotacaoUSD
    {
        [JsonProperty("USD")]
        public USD Usd { get; set; }
    }
}
