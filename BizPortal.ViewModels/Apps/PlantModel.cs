using Newtonsoft.Json;
using System.Collections.Generic;

namespace BizPortal.ViewModels.Apps
{

    public class Result
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("plantgroup_id")]
        public string plantgroup_id { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("result")]
        public List<Result> result { get; set; }
    }
}
