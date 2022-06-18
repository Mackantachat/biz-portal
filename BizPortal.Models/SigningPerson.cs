using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    public class SigningPerson 
    {
        public int SigningPersonID { get; set; }

        public int Order { get; set; }

        public string CitizenID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string SignatureFileName { get; set; }

        public string SignatureBase64 { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
