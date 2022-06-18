using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class ApplicationArticleViewModel
    {
        public string[] Categories { get; set; }
        public string[] OrganizationNames { get; set; }
        public ArticleViewModel[] Articles { get; set; }
    }
    
}
