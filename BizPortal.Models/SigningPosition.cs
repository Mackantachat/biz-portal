using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    public class SigningPosition
    {
        public int SigningPositionID { get; set; }

        public int Order { get; set; }

        public string Left { get; set; }

        public string Bottom { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
