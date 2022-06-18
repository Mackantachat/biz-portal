using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public interface IFormControlDateTimeCondition
    {
        FormControlDateTimeCondition DateTimeCondition { get; set; }
    }

    public enum FormControlDateTimeMethodEnum
    {
        [StringValue("setStartDate")]
        SetStartDate,
        [StringValue("setEndDate")]
        SetEndDate
    }

    public class SingleFormDateTimeHelper
    {
        public static FormControlDateTimeAttribute GetAttributes(IFormControlDateTimeCondition ctrl)
        {
            var isTrigger = false;
            var dataMethod = "null";
            var dataSourceFormat = "null";
            var dataSourceCulture = "null";
            var dataDestinationControl = "null";

            if (ctrl.DateTimeCondition != null && ctrl.DateTimeCondition.IsTriggerDateTimeHelper)
            {
                isTrigger = ctrl.DateTimeCondition.IsTriggerDateTimeHelper;
                dataMethod = ctrl.DateTimeCondition.Method.GetEnumStringValue();
                dataSourceFormat = ctrl.DateTimeCondition.SourceFormat;
                dataSourceCulture = ctrl.DateTimeCondition.SourceCulture;
                dataDestinationControl = ctrl.DateTimeCondition.DestinationControl;
            }

            return new FormControlDateTimeAttribute
            {
                IsTrigger = $"data-datetimetrigger-istrigger={isTrigger.ToString().ToLower()}",
                DataMethod = $"data-datetimetrigger-method={dataMethod}",
                DataSourceFormat = $"data-datetimetrigger-sourceformat={dataSourceFormat}",
                DataSourceCulture = $"data-datetimetrigger-sourceculture={dataSourceCulture}",
                DataDestinationControl = $"data-datetimetrigger-destinationcontrol={dataDestinationControl}"
            };
        }
    }

    public class FormControlDateTimeCondition
    {
        public bool IsTriggerDateTimeHelper { get; set; }
        public FormControlDateTimeMethodEnum Method { get; set; }
        public string SourceFormat { get; set; }
        public string SourceCulture { get; set; }
        public string DestinationControl { get; set; }
    }

    public class FormControlDateTimeAttribute
    {
        public string IsTrigger { get; set; }
        public string DataMethod { get; set; }
        public string DataSourceFormat { get; set; }
        public string DataSourceCulture { get; set; }
        public string DataDestinationControl { get; set; }
    }
    
}
