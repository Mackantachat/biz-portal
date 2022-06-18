using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.Select2;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormControl : ISupportDisplayCondition, ISupportAutoFillCondition, IFormControlDateTimeCondition
    {
        public enum FormControlFactoryType
        {
            Time,
        }
        public FormControl() {
            AddressForm_ShowSearchControl = false;
            AddressForm_ShowAddressIdControl = false;
            AddressForm_ShowBranchControl = false;
            AddressForm_ShowAddressControl = true;
            AddressForm_ShowMooControl = true;
            AddressForm_ShowVillageControl = true;
            AddressForm_ShowSoiControl = true;
            AddressForm_ShowRoadControl = true;
            AddressForm_ShowAmphurControl = true;
            AddressForm_ShowTumbolControl = true;
            AddressForm_ShowProvinceControl = true;
            AddressForm_ShowMobileControl = false;
            AddressForm_ShowEmailControl = false;
            AddressForm_IgnoreAddressControl = false;
            AddressForm_ShowNbr = true;
            AddressForm_ShowYakControl = false;
            AddressFormEN_ShowBuildingControl = true;
            AddressFormEN_ShowVillageControl = false;
        }

        public FormControl(FormControlFactoryType factoryType) : this()
        {
            if (factoryType == FormControlFactoryType.Time)
            {
                DisplayMaskInput = true;
                MaskInputPattern = "a0cb0";
                MaskInputPatternTranslation = new Dictionary<string, string>
                {
                    { "a", "{ pattern: /[0-2]/ }" },
                    { "b", "{ pattern: /[0-5]/ }" },
                    { "c", "{ pattern: /[\\:]/ }" },
                };
            }
        }

        /// <summary>
        /// For refernce with Field ID in excel sheet
        /// </summary>
        public string FieldID { get; set; }

        public string Control { get; set; }

        public bool IsNotDisplayControl { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ControlType Type { get; set; }

        [BsonRepresentation(BsonType.String)]
        public UserTypeEnum[] IdentityTypes { get; set; }

        public bool PreFill { get; set; }

        public bool DisplayStaticIfHasData { get; set; }

        public string DataKey { get; set; }

        public string DataFormat { get; set; }

        public FormValidationRule[] Rules { get; set; }

        public int ColFixed { get; set; }

        public bool ShowOnSpecificApps { get; set; }

        public string[] AppSystemNames { get; set; }

        public class CheckboxConfig
        {
            public bool CheckMin { get; set; } = false;
            public int Min { get; set; } = 0;
            public bool CheckMax { get; set; } = false;
            public int Max { get; set; } = int.MaxValue;
        }
        public CheckboxConfig CheckboxConfigs { get; set; }


        /// <summary>
        /// Section Name
        /// </summary>
        public string SectionName { get; set; }

        public bool HideOnSpecificApps { get; set; }

        public string[] HideAppSystemNames { get; set; }

        public bool DisplayReadonly { get; set; }

        public bool DisplayDisabled { get; set; }

        public string ResourceName { get; set; }

        #region [AddressForm Data]
        public bool AddressForm_ShowSearchControl { get; set; }

        public bool AddressForm_ShowAddressIdControl { get; set; }

        public bool AddressForm_ShowBranchIdControl { get; set; }

        public bool AddressForm_ShowBranchControl { get; set; }

        public bool AddressForm_ShowAddressControl { get; set; }

        public bool AddressForm_ShowMooControl { get; set; }

        public bool AddressForm_ShowVillageControl { get; set; }

        public bool AddressForm_ShowSoiControl { get; set; }

        public bool AddressForm_ShowBuildingControl { get; set; }

        public bool AddressForm_ShowYakControl { get; set; }

        public bool AddressForm_ShowRoadControl { get; set; }

        public bool AddressForm_ShowAmphurControl { get; set; }

        public bool AddressForm_ShowTumbolControl { get; set; }

        public bool AddressForm_ShowProvinceControl { get; set; }

        public bool AddressForm_ShowPostCodeControl { get; set; }

        public bool AddressForm_ShowTelephoneControl { get; set; }

        public bool AddressForm_ShowMobileControl { get; set; }
        public bool AddressForm_ShowEmailControl { get; set; }

        public bool AddressForm_ShowFaxControl { get; set; }

        public bool AddressForm_ShowMapControl { get; set; }

        public bool AddressForm_ShowOnlyProvinceControl { get; set; }
        public bool AddressForm_HideTumbol { get; set; }

        public bool AddressForm_English { get; set; }
        public bool AddressForm_ShowDeed { get; set; }
        public bool AddressForm_IgnoreAddressControl { get; set; }

        public ProvinceType AddressForm_ProvinceType { get; set; }

        public bool AddressForm_ShowNbr { get; set; } // Add By Charun Sa. 20190911

        public bool AddressFormEN_ShowBuildingControl { get; set; }

        public bool AddressFormEN_ShowVillageControl { get; set; }

        #endregion

        #region [Ignore Data]
        public string ClientID { get; set; }
        [BsonIgnore]
        public object Data { get; set; }
        [BsonIgnore]
        public bool ValidateNeeded { get; set; }
        #endregion

        #region [Display Only Label] or [Hide Label]
        public bool DisplayOnlyLabel { get; set; }
        public bool HideLabel { get; set; }
        #endregion

        #region [Maskinput Data]
        /// <summary>
        /// https://igorescobar.github.io/jQuery-Mask-Plugin/
        /// </summary>
        public bool DisplayMaskInput { get; set; }
        public string MaskInputPattern { get; set; }
        public Dictionary<string, string> MaskInputPatternTranslation { get; set; }
        public bool MaskInputReverse { get; set; }

        public class NumberSettings
        {
            public string Min { get; set; }
            public string Max { get; set; }
            public string Step { get; set; }
        }
        public NumberSettings TextboxNumberSettings { get; set; }
        #endregion

        #region [Repeat Data]
        /// <summary>
        /// https://github.com/DubFriend/jquery.repeater
        /// http://briandetering.net/repeater
        /// </summary>
        public bool DisplayRepeater { get; set; }
        #endregion

        #region [CheckboxWithDropdown]
        public FormCheckboxWithDropdown[] CheckboxWithDropdown { get; set; }
        #endregion

        #region [CheckboxWithText]
        public FormCheckboxWithText[] CheckboxWithText { get; set; }
        #endregion

        #region [RadioGroup]
        public bool DisplayRadioButtonInline { get; set; }
        public FormRadioGroup RadioGroup { get; set; }
        #endregion

        #region [RadioGroupWithText]
        public FormRadioGroupWithText RadioGroupWithText { get; set; }
        #endregion

        #region [RadioGroupWithDropdown]
        public FormRadioGroupWithDropdown RadioGroupWithDropdown { get; set; }
        #endregion

        #region [Checkbox Group]
        public bool DisplayCheckboxInline { get; set; }
        public string[] CheckboxName { get; set; }
        public bool CheckboxTextBold { get; set; }
        #endregion

        #region [DatePicker Option]
        public bool IsShowAge { get; set; } = false;
        public bool AllowFutureDate { get; set; } = true;
        public class DatePickerProperties {
            /// <summary>
            /// กรณีนับจากปัจจุบัน [ตัวเลข(สามารถเป็นจำนวนลบได้)][d = วัน, m = เดือน, y = ปี] เช่น 1m เริ่มหลังจากปัจจุบัน 1 เดือน, -2d เริ่มก่อนปัจจุบัน 2 วัน"
            /// กรณีระบุวันที่ [dd/mm/yyyy] ปีเป็น พ.ศ. เช่น 27/08/2562"
            /// </summary>
            public string StartDate { get; set; }
            /// <summary>
            /// กรณีนับจากปัจจุบัน [ตัวเลข(สามารถเป็นจำนวนลบได้)][d = วัน, m = เดือน, y = ปี] เช่น 1m สิ้นสุดหลังจากปัจจุบัน 1 เดือน, -2d สิ้นสุดก่อนปัจจุบัน 2 วัน"
            /// กรณีระบุวันที่ [dd/mm/yyyy] ปีเป็น พ.ศ. เช่น 27/08/2562"
            /// </summary>
            public string EndDate { get; set; }
            /// <summary>
            /// กำหนดวันแรกของสัปดาห์ [0-6] Default = 0 (วันอาทิตย์)
            /// </summary>
            public int WeekDayStart { get; set; } = 0;
            /// <summary>
            /// แสดงลำดับเลขของสัปดาห์ในปี Default = false
            /// </summary>
            public bool ShowCalendarWeeksNumbers { get; set; } = false;
            /// <summary>
            /// Disable วันในสัปดาห์ [0-6] หากมีหลายวันให้คั้นด้วย , (Comma) เช่น 0,6 = disable วันเสาร์-อาทิตย์
            /// </summary>
            public string DaysOfWeekDisabled { get; set; }
        }
        public DatePickerProperties DatePickerPropertiesConfig { get; set; }
        public class DateRangePickerProperties
        {
            /// <summary>
            /// ระยะห่างระหว่าง Date From เป็นจำนวน X วัน, Default = null
            /// </summary>
            public int? MinDateTo { get; set; }
            /// <summary>
            /// วันที่สูงสุดของ Date To, Default = null
            /// </summary>
            public int? MaxDateTo { get; set; }
        }
        public DateRangePickerProperties DateRangePickerPropertiesConfig { get; set; }
        public FormControlDateTimeCondition DateTimeCondition { get; set; }
        #endregion

        #region [Dropdown Option]
        public Select2Opt[] Select2Opts { get; set; }
        public string PlaceholderText { get; set; }
        public bool IsInModal { get; set; }
        public string ModalSectionName { get; set; }
        #endregion

        #region [Multiple Dropdown Option]
        public FormDropdown[] MultipleDropdown { get; set; }
        #endregion

        #region [ChainedDropdown]
        public string ChainedLabel { get; set; }
        public FormChainedDropdown[] ChainedDropdownOpts { get; set; }
        public string ChainedPlaceholderText { get; set; }

        #endregion

        #region [AJAX Request]
        public string AjaxUrl { get; set; }
        public string AjaxQueryString { get; set; }
        #endregion

        #region [AJAX Dropdown]
        public bool IsAjaxDropdown { get; set; }
        #endregion

        #region [UtilData]
        public bool IsUtilDataNeeded { get; set; }
        #endregion

        #region [Geo Dropdown]
        public GeoDropdown GeoDropdown { get; set; }
        #endregion

        #region [Add HTML Class]
        public bool IsCustomClass { get; set; }
        public string CustomClassName { get; set; }
        #endregion

        #region [Textbox Multiline]
        public int Textbox_Rows { get; set; }
        #endregion

        #region [Info]
        public string Info { get; set; }
        public bool DefaultShowInfo { get; set; }
        #endregion

        #region Trigger Show/Hide of other UI

        public bool WillTriggerDisplayOfOtherUI { get; set; }

        #endregion

        #region Conditional display from other FormControl

        public FormControlDisplayCondition DisplayCondition { get; set; }

        #endregion

        #region Auto-fill from other FormControl
        public FormControlAutoFillCondition[] AutoFillConditions { get; set; }

        #endregion

        #region [Additional Data]
        public AdditionalData AdditionalData { get; set; }
        #endregion

        #region [DataTable]
        public DataTableColumn[] DataTableColumns { get; set; }

        /// <summary>
        /// Specify the of column names that combination of their values must be unique.
        /// </summary>
        public string[] DataTableRowKeys { get; set; }
        #endregion

        #region [Literal]
        public string LiteralContent { get; set; }
        #endregion

        #region [Advanced Text Box Type]
        public AdvancedTextboxType? AdvancedTextboxType { get; set; }
        #endregion

        public int MaxLength { get; set; } = 0;
        public TextBoxTextAlignment TextAlignment { get; set; } = TextBoxTextAlignment.Left;

        // Currently work with AjaxQueryString for DropDownList only
        public ControlVariable[] ControlVariables { get; set; }

        public bool isCompatibleWithDataKey(string keyO, UserTypeEnum identityType)
        {
            var key = keyO.ToUpper();
            var dataKeyUpperCased = this.DataKey.ToUpper();
            var sectionUpperCased = this.SectionName != null ? this.SectionName.ToUpper() : "";

            if (IdentityTypes != null && !IdentityTypes.Contains(identityType))
            {
                return false;
            }
            if (key == sectionUpperCased + "_TOTAL" ||
                key.StartsWith("ARR_IDX_") ||
                key.StartsWith("IS_EDIT_") ||
                key.StartsWith("CUSREQ_"))
                return true; // Reserved keyword

            switch (Type)
            {
                case ControlType.AddressForm:
                    if (key.StartsWith("ADDRESS_") && (key.EndsWith(dataKeyUpperCased) || key.EndsWith(dataKeyUpperCased + "_TEXT"))) {
                        return true;
                    }
                    return false;
                case ControlType.Dropdown:
                    return (key == "DROPDOWN_" + dataKeyUpperCased + "_TEXT") || (key == "DROPDOWN_" + dataKeyUpperCased);
                case ControlType.CheckBox:
                    if (key.StartsWith(dataKeyUpperCased + "_"))
                    {
                        return CheckboxName.Contains(key.Substring(dataKeyUpperCased.Length + 1));
                    }
                    return false;
                case ControlType.RadioGroup:
                    return key == RadioGroup.RadioGroupName;
                case ControlType.TextBox:
                    return key == dataKeyUpperCased;
                case ControlType.DatePicker:
                    return key == dataKeyUpperCased;
                default:
                    // Other type
                    return false;
            }
        }
    }

    public class GeoDropdown
    {
        public string AjaxUrl { get; set; }
        public string AjaxQueryString { get; set; }
        public string FromControlName { get; set; }
    }

    public class AdditionalData
    {
        public bool ShowOnTrackingPage { get; set; }
    }

    public class DataTableColumn
    {
        public bool IsIndexColumn { get; set; } = false;
        public bool IsReadOnly { get; set; } = false;
        public string Title { get; set; }
        public string Name { get; set; }
        public int CustomColFixed { get; set; } = 0;
        public FormControl Control { get; set; }
        public FormControl ExtraControl { get; set; }
        public string[] AnswersForExtraControl { get; set; }

        public DataTableColumn()
            : this(ControlType.TextBox)
        {
        }

        public DataTableColumn(ControlType ctrlType)
            : this(ctrlType, null)
        {
        }

        public DataTableColumn(ControlType ctrlType, FormValidationRule[] rules)
        {
            Control = new FormControl();
            Control.Type = ctrlType;
            Control.Rules = rules;
        }
    }

    public class ControlVariable
    {
        public enum VariableSource
        {
            FormControl
        }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public VariableSource Source { get; set; } = VariableSource.FormControl;

        public string ControlSelector { get; set; }
        public bool ListenOnChange { get; set; } = true;
        public string DefaultIfEmpty { get; set; } = "";
    }
}
