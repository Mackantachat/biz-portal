using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Select2
{
    public class Select2GroupOpt
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("children")]
        public IEnumerable<Select2Opt> Children { get; set; }
    }
}
