using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models.Base
{
    public abstract class TranslationModel
    {
        public int LanguageID { get; set; }

        public virtual Language Language { get; set; }
    }
}
