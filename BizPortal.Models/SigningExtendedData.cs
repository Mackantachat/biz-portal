using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    public class SigningExtendedData
    {
        public int SigningExtendedDataID { get; set; }

        public SigningExtendedDataType Type { get; set; }

        public UserTypeEnum UserType { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
