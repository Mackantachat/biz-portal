using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class OrderJsonModel
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("order")]
        public int order { get; set; }
    }

    public class OrderRequest
    {
        public string Orders { get; set; }
    }
}
