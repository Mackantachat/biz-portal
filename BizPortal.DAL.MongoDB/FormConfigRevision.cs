using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormConfigRevision : Entity
    {
        public int RevisionCode { get; set; } = 0;
        public string RevisionName { get; set; } = "Before Config Revision";
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? LastDeployedDate { get; set; }
        public DateTime? ArchivedDate { get; set; }
    }
}
