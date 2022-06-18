using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class ArticleDataTables : DataTables
    {
        public int? SectionID { get; set; }

        public int? CategoryID { get; set; }
    }
}
