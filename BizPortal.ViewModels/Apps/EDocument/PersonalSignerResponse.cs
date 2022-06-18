using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class PersonalSignerResponse
    {
        public PersonalSignerResponse()
        {
            ActiveDocuments = new List<PersonalSignedDocument>();
            CompletedDocuments = new List<PersonalSignedDocument>();
        }

        public List<PersonalSignedDocument> ActiveDocuments { get; set; }
        public List<PersonalSignedDocument> CompletedDocuments { get; set; }
    }
}
