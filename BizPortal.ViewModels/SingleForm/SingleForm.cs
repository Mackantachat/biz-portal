using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleForm
    {
        public SingleForm()
        {
            SingleFormApps = new List<SingleFormAppsViewModel>();
        }
        public string BusinessID { get; set; }
        public IList<SingleFormAppsViewModel> SingleFormApps { get; set; }

    }

    public class ConsentModel
    {
        public Guid TransID { get; set; }

        public int? ConsentResult { get; set; }
    }
}
