using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace BizPortal.Extensions
{
    public class CustomValidationException : BizPortalException
    {
        public Dictionary<string, string> ValidationErrors { get; set; }

        public CustomValidationException()
            : this("Validation failed for one or more entities.")
        {

        }

        public CustomValidationException(string message)
            : base(message)
        {

        }

        public CustomValidationException(Dictionary<string, string> errors)
            : this("Validation failed for one or more entities.")
        {
            ValidationErrors = errors;
            Data.Add("ValidationErrors", ValidationErrors);
        }

        public CustomValidationException(ModelStateDictionary modelState)
            : this("Validation failed for one or more entities.")
        {
            ValidationErrors = new Dictionary<string, string>();

            foreach (var key in modelState.Keys)
            {
                ModelState state = null;
                if (modelState.TryGetValue(key, out state))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in state.Errors)
                    {
                        sb.AppendFormat("{0} / ", error.ErrorMessage);
                    }
                    if (sb.Length > 0)
                    {
                        sb.Length -= 3;

                        string errorKey = key;
                        int idx = errorKey.IndexOf(".");
                        if (idx >= 0)
                            errorKey = errorKey.Substring(idx + 1);

                        ValidationErrors.Add(errorKey, sb.ToString());
                    }
                }
            }

            Data.Add("ValidationErrors", ValidationErrors);
        }
    }
}
