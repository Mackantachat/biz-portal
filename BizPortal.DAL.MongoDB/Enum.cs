using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public enum SectionType
    {
        Form,
        ArrayOfForms
    }

    public enum ControlType
    {
        None,
        TextBox,
        Number,
        DatePicker,
        DateRangePicker,
        Email,
        CheckBox,
        Dropdown,
        MultipleDropdown,
        ChainedDropdown,
        Chained5Dropdown, // สำหรับ TSIC API เท่านั้น
        RadioGroup,
        RadioGroupWithText,
        RadioGroupWithDropdown,
        RadioGroupWithDropdownETC,
        CheckBoxWithDropdown,
        CheckBoxWithText,
        AddressForm,
        AddressFormEN,
        AjaxStaticControl,
        Heading,
        ArrayInArray,
        ConfirmSignature,
        ConfirmSignatureEDIT,
        ConfirmSignatureAnimal,
        ConfirmSignatureDangerFood,
        ConfirmSignatureFood,
        ConfirmSignatureFoodEdit,
        ConfirmSignatureFoodLicense,
        ConfirmSignatureBoard,
        ConfirmSignatureFoodRenew,
        ConfirmSignatureBuilding,
        ConfirmSignatureBuildingOwn,
        ConfirmSignatureFactoryClass2New,
        ConfirmSignatureFactoryClass2Edit,
        AnimalMedFormFooter,
        OCPBFormFooter,
        BKKBuildingFormFooter,
        BKKBuildingNoteFooter,
        BKKBuildingRenewFormFooter,
        GeoDropdown,
        Signature,

        /// <summary>
        /// Label จะดึง value จาก Resource File มาแสดงบนหน้าจอ
        /// </summary>
        Label,
        /// <summary>
        /// DataLabel จะดึง value จาก MongoDB มาแสดงบนหน้าจอ
        /// </summary>
        DataLabel,

        DataTable,
        Literal,
        ConfirmSignatureFactoryType2,
        Hidden,
        Confirm_SEC_NEW_A,
        Confirm_SEC_EDIT
    }

    public enum ValidationType
    {
        Required,
        RequiredEach,
        Regex,
        MaxLength,
        Email,
        JSExpression,
        OnlyDigitLength
    }

    public enum ProvinceType
    {
        All,
        Metro,
        Provin,
        BKK,
        NotBKK
    }
   
    public enum TextBoxTextAlignment
    {
        Left,
        Center,
        Right,
    }
  
    public enum AdvancedTextboxType
    {
        Currency,
        Percentage,
        PercentageMax100,
        Numeric,
    }
}
