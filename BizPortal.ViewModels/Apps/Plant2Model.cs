using Newtonsoft.Json;
using System.Collections.Generic;

namespace BizPortal.ViewModels.Apps
{

    public class Result2
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string NAME { get; set; }
    }

    public class RootObject2
    {
        [JsonProperty("status")]
        public bool STATUS { get; set; }
        [JsonProperty("message")]
        public string MESSAGE { get; set; }
        [JsonProperty("result")]
        public List<Result2> RESULT { get; set; }
    }

}
