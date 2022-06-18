using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class ArticleSearchViewModel
    {
        public ArticleSearchViewModel()
        {
            //Ordering = "Ordering";
            IsAsc = true;
            PageNo = 1;
            PageLength = 10;
            UnlistedVisible = false;
        }

        public int? CategoryId { get; set; }

        public string SearchKeyword { get; set; }

        public string SearchTag { get; set; }

        //public string Ordering { get; set; }
        
        public bool IsAsc { get; set; }

        public int PageNo { get; set; }

        public int PageLength { get; set; }

        public bool UnlistedVisible { get; set; }

        public string Language { get; set; }
    }
}
