using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public enum ResultDataType
    {
        Error = 0,
        Success = 1,
        Info = 2,
        Warning = 3,
        SuccessWithErrors = 4
    }

    public class ResponseData<T>
    {
        private Dictionary<string, object> _validationErrors;

        public ResultDataType Type { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public Dictionary<string, object> ValidationErrors
        {
            get
            {
                if (this._validationErrors == null)
                    this._validationErrors = new Dictionary<string, object>();
                return this._validationErrors;
            }
            set
            {
                this._validationErrors = value;
            }
        }

        public Dictionary<string, object> ApplicationRequestData { get; set; }

        public void SetFormValidationErrors(IEnumerable<DbValidationError> errors)
        {
            foreach (var error in errors)
            {
                ValidationErrors.Add(error.PropertyName, error.ErrorMessage);
            }
        }
    }

    public class ResponseData
    {
        public ResultDataType Type { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
