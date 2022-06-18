using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormValidationRule
    {
        [BsonRepresentation(BsonType.String)]
        public ValidationType Type { get; set; }

        public string ErrorMessage { get; set; }

        #region[FixedMessage]
        public bool FixedMessage { get; set; }
        #endregion

        #region [Regex]
        public string RegexFormat { get; set; }
        #endregion

        #region [MaxLength]
        public int MaxLength { get; set; }
        #endregion

        #region [RequiredEach]
        public string[] EachControl { get; set; }
        public bool DisableOnSpecificApps { get; set; }
        public List<string> SpecificApps { get; set; }
        public string SpecificAppErrorMessage { get; set; }

        #endregion

        public string JSExpression { get; set; }

    }
}
