using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class FormControlAutoFillSource
    {
        /// <summary>
        /// Section name
        /// </summary>
        public string Section { get; set; }
        /// <summary>
        /// Control name
        /// </summary>
        public string Control { get; set; }
        /// <summary>
        /// Get Data from draft
        /// </summary>
        public bool SourceFromDraft { get; set; }
        public bool IsAddressControl { get; set; }
    }
    public class FormControlAutoFillCondition
    {
        /// <summary>
        /// Name of the Control
        /// </summary>
        public string TriggerAutoFillOnControl { get; set; }
        /// <summary>
        /// Name of the Radio or Checkbox
        /// </summary>
        public string TriggerAutoFillOnSpecificValue { get; set; }
        /// <summary>
        /// Trigger control is a Checkbox or not
        /// </summary>
        public bool TriggerAutoFillOnCheckboxControl { get; set; }
        /// <summary>
        /// In case the control is Checkbox. It will be triggered only when checked or unchecked
        /// </summary>
        public bool TriggerAutoFillOnChecked { get; set; }
        /// <summary>
        /// Auto fill with value from...
        /// </summary>
        public FormControlAutoFillSource AutoFillValueFrom { get; set; }
    }

    public interface ISupportAutoFillCondition
    {
        FormControlAutoFillCondition[] AutoFillConditions { get; set; }
    }
    public class AutoFillConditionAttribute
    {
        public string Class;
        public string Data;
        public string DataSourceFromDraft;
        public string DataSourceIsAddressControl;
        public string DataSourceSection;
        public string DataSourceControl;
    }

    public class AutoFillConditionHelper
    {
        public static AutoFillConditionAttribute GetAttributes(ISupportAutoFillCondition ctrl)
        {
            var displayConditionClass = "";
            var displayConditionData = new Dictionary<string, List<string>>();
            var dataSourceFromDraft = new Dictionary<string, string>();
            var dataSourceIsAddressControl = new Dictionary<string, string>();
            var dataSourceSection = new Dictionary<string, string>();
            var dataSourceControl = new Dictionary<string, string>();

            if (ctrl.AutoFillConditions != null)
            {
                //List<string> addedControlNames = new List<string>();
                foreach (var c in ctrl.AutoFillConditions)
                {
                    if (!displayConditionData.ContainsKey(c.TriggerAutoFillOnControl))
                    {
                        displayConditionClass += "autofill-on-" + c.TriggerAutoFillOnControl + " ";
                        displayConditionData.Add(c.TriggerAutoFillOnControl, new List<string>());
                    }
                    var triggerValue = c.TriggerAutoFillOnSpecificValue;
                    if (c.TriggerAutoFillOnCheckboxControl)
                    {
                        triggerValue = c.TriggerAutoFillOnChecked ? (triggerValue + "__true") : (triggerValue + "__false");
                    }
                    if (!displayConditionData[c.TriggerAutoFillOnControl].Contains(triggerValue))
                    {
                        displayConditionData[c.TriggerAutoFillOnControl].Add(triggerValue);
                        var sourceKey = c.TriggerAutoFillOnControl + "::" + triggerValue;
                        dataSourceFromDraft.Add(sourceKey, c.AutoFillValueFrom.SourceFromDraft.ToString().ToLower());
                        dataSourceIsAddressControl.Add(sourceKey, c.AutoFillValueFrom.IsAddressControl.ToString().ToLower());
                        dataSourceSection.Add(sourceKey, c.AutoFillValueFrom.Section);
                        dataSourceControl.Add(sourceKey, c.AutoFillValueFrom.Control);
                    }

                }
            }

            return new AutoFillConditionAttribute
            {
                Class = string.IsNullOrEmpty(displayConditionClass) ? "" : (displayConditionClass + "autofill-on"),
                Data = "data-autofill-conditions=" + JsonConvert.SerializeObject(displayConditionData),
                DataSourceFromDraft = "data-autofill-source-from-draft=" + JsonConvert.SerializeObject(dataSourceFromDraft),
                DataSourceIsAddressControl = "data-autofill-source-is-address-control=" + JsonConvert.SerializeObject(dataSourceIsAddressControl),
                DataSourceSection = "data-autofill-source-section=" + JsonConvert.SerializeObject(dataSourceSection),
                DataSourceControl = "data-autofill-source-control=" + JsonConvert.SerializeObject(dataSourceControl),
            };
        }
    }
}
