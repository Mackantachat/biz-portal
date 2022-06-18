using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleFormSectionDataViewModel : ISectionData
    {
        public SingleFormSectionDataViewModel()
        {
            FormData = new Dictionary<string, object>();
            ArrayData = new List<Dictionary<string, object>>();
        }

        public string SectionName { get; set; }

        public string Type { get; set; }

        public Dictionary<string, object> FormData { get; set; }

        public List<Dictionary<string, object>> ArrayData { get; set; }
    }

    public interface ISectionData
    {
        string SectionName { get; set; }

        Dictionary<string, object> FormData { get; set; }

        List<Dictionary<string, object>> ArrayData { get; set; }
    }
}
