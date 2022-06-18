using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BizPortal.DAL.MongoDB
{
    public class FormSection : Entity, ISupportDisplayCondition, ISupportDisableCondition
    {
        public string Section { get; set; }
        public bool DisableAddRemoveItem { get; set; }

        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            if (db.AsQueryable().Count() == 0)
            {
                FormSection[] items = new FormSection[]
                {
                    #region [Additional Section]
                    //new FormSection()
                    //{
                    //    Section = "APP_BUILDING_G1_LICENSE_INFORMATION",
                    //    SectionGroup = "INFORMATION",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                    //    Ordering = 0,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_G1 },
                    //    AdditionalSection = new AdditionalSection()
                    //    {
                    //        ShowOnTrackingPage = true
                    //    }
                    //},
                    new FormSection()
                    {
                        Section = "APP_ELECTRONIC_COMMERCIAL_EXTRAS",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 0,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL },
                        AdditionalSection = new AdditionalSection()
                        {
                            ShowOnTrackingPage = true
                        }
                    },
                    #endregion

                    new FormSection()
                    {
                        Section = "CITIZEN_GENERAL_EDIT_CHECK_SECTION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 0,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                        },
                        HideSectionHeader = true
                    },

                    #region [INFORMATION]
                    new FormSection() {
                        Section = "JURISTIC_GENERAL_INFORMATION_HEADER",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 0,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All.ConcatItems(
                            AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY).ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                                AppSystemNameTextConst.APP_CLINIC_EDIT,
                                AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                            }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                            }
                            )
                            .ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW).ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_HEALTH_RENEW,
                                AppSystemNameTextConst.APP_HEALTH_EDIT,
                                AppSystemNameTextConst.APP_HEALTH_CANCEL,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                            .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                            .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT)
                    },
                    new FormSection() {
                        Section = "CITIZEN_GENERAL_INFORMATION_HEADER",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 0,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All.ConcatItems(
                            AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY).ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                            .ConcatItems(AppSystemNameTextConst.APP_BUILDING_R6)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                                AppSystemNameTextConst.APP_CLINIC_EDIT,
                                AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                            }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                            .ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_HEALTH_RENEW,
                                AppSystemNameTextConst.APP_HEALTH_EDIT,
                                AppSystemNameTextConst.APP_HEALTH_CANCEL,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),
                    },
                    new FormSection() {
                        Section = "GENERAL_INFORMATION_HEADER",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 0,
                        HideOnSpecificApps = true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                            AppSystemNameTextConst.App_Restaurant_Retail_All).ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                                AppSystemNameTextConst.APP_CLINIC_EDIT,
                                AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                            }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                            })
                            //.ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW)
                            .ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_HEALTH_RENEW,
                                AppSystemNameTextConst.APP_HEALTH_EDIT,
                                AppSystemNameTextConst.APP_HEALTH_CANCEL,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                            .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                            .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),
                    },
                    new FormSection() {
                        Section = "GENERAL_INFORMATION",
                        SectionGroup = "INFORMATION",
                        HideSectionHeader = true,
                        Type = SectionType.Form,
                        Ordering = 1
                    },
                    new FormSection() {
                        Section = "JURISTIC_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        HideSectionHeader = false,
                        HideOnSpecificApps = true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                            AppSystemNameTextConst.App_Restaurant_Retail_All).ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                            .ConcatItems(AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL).ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                                AppSystemNameTextConst.APP_CLINIC_EDIT,
                                AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,

                            })
                            .ConcatItems(new string[] {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                            }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                                AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_HEALTH_RENEW,
                                AppSystemNameTextConst.APP_HEALTH_EDIT,
                                AppSystemNameTextConst.APP_HEALTH_CANCEL,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                            .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                            .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT),
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 2
                    },
                    new FormSection() { Section = "JURISTIC_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        HideSectionHeader = true,
                        HideOnSpecificApps = true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY
                        .ConcatItems(AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL),
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All.ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                            .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL
                            })
                            .ConcatItems(new string[] {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,

                                AppSystemNameTextConst.APP_CLINIC_EDIT,
                                AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                            }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2).ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                            .ConcatItems(new string[]
                            {
                                AppSystemNameTextConst.APP_HEALTH_RENEW,
                                AppSystemNameTextConst.APP_HEALTH_EDIT,
                                AppSystemNameTextConst.APP_HEALTH_CANCEL,
                            })
                            .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                            .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                            .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                            .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT)
                            .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT),
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 2
                    },
                    new FormSection() {
                        Section = "CITIZEN_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        Ordering = 2,
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Utilities.ConcatItems(AppSystemNameTextConst.App_Restaurant_Retail_All)
                        .ConcatItems(AppSystemNameTextConst.APP_BUILDING_G1)
                        .ConcatItems(AppSystemNameTextConst.APP_BUILDING_R6)
                        .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                        .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                        .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                        .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                        .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),

                        HideOnSpecificApps = true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                            AppSystemNameTextConst.APP_ANIMAL_BUILD,
                            AppSystemNameTextConst.APP_LAW_OFFICE,
                            AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                            AppSystemNameTextConst.APP_ANIMAL_BUILD_RENEW,
                            AppSystemNameTextConst.APP_TOURISM_BUSINESS
                            ),
                    },
                    new FormSection() {
                        Section = "CURRENT_ADDRESS",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 2,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_HOSPITAL,
                            AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                            AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                            AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                            AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_CANCEL,
                            AppSystemNameTextConst.APP_CLINIC,
                            AppSystemNameTextConst.APP_CLINIC_RENEW,
                            AppSystemNameTextConst.APP_CLINIC_CANCEL,
                            AppSystemNameTextConst.APP_CLINIC_BUSINESS,
                            AppSystemNameTextConst.APP_CLINIC_OPERATION,
                            AppSystemNameTextConst.APP_HOSPITAL_BUSINESS,
                            AppSystemNameTextConst.APP_HOSPITAL_OPERATION,
                        },
                        HideOnSpecificApps = true,
                        HideAppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                            AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                        }
                    },
                    new FormSection() {
                        Section = "CURRENT_ADDRESS",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        Ordering = 2,
                        ShowOnSpecificApps = true,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR,
                            //AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                            AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT,
                            AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                            AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW
                        }

                    },
                    //new FormSection() { Section = "BRANCH0_ADDRESS_INFORMATION", SectionGroup = "INFORMATION", Type = SectionType.ArrayOfForms,
                    //    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 3,
                    //    EmptyDataMessage = "BRANCH0_ADDRESS_INFORMATION_EMPTY", AddButtonText = "BRANCH0_ADDRESS_INFORMATION_BTN_ADD",
                    //    SubmitButtonText = "BRANCH0_ADDRESS_INFORMATION_BTN_SUBMIT", ArrayRequiredAtLeast = true, NumberRequiredAtLeast = 1 ,
                    //},
                    new FormSection()
                    {
                        Section = "BRANCH0_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 3,
                        EmptyDataMessage = "BRANCH0_ADDRESS_INFORMATION_EMPTY",
                        AddButtonText = "BRANCH0_ADDRESS_INFORMATION_BTN_ADD",
                        SubmitButtonText = "BRANCH0_ADDRESS_INFORMATION_BTN_SUBMIT",
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1 ,
                        ArrayOfFormNotice = "BRANCH0_ADDRESS_INFORMATION_NOTICE",
                        DisableAddRemoveItem = true,
                        ShowOnSpecificApps = true,
                        GetArrayData = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_VAT },
                        IsPartialView = true
                    },
                    new FormSection()
                    {
                        Section = "BRANCH_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 4,
                        EmptyDataMessage = "BRANCH_ADDRESS_INFORMATION_EMPTY",
                        AddButtonText = "BRANCH_ADDRESS_INFORMATION_BTN_ADD",
                        SubmitButtonText = "BRANCH_ADDRESS_INFORMATION_BTN_SUBMIT",
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_VAT },
                        IsPartialView = true
                    },
                    new FormSection() {
                        Section = "ABOVE_COMMITTEE_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 4,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                            AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                            AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2,
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT,
                            AppSystemNameTextConst.APP_HEALTH_EDIT,
                        },
                        HideSectionHeader = true,
                    },
                    new FormSection()
                    {
                        Section = "COMMITTEE_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 4,
                        EmptyDataMessage = "COMMITTEE_INFORMATION_EMPTY",
                        AddButtonText = "COMMITTEE_INFORMATION_BTN_ADD",
                        SubmitButtonText = "COMMITTEE_INFORMATION_BTN_SUBMIT",
                        ArrayRequiredAtLeast = true, NumberRequiredAtLeast = 1 ,
                        ArrayOfFormNotice = "COMMITTEE_INFORMATION_NOTICE",
                        DisableAddRemoveItem = true,
                        ShowOnSpecificApps = true,
                        GetArrayData = true,
                        AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All.ConcatItems(
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                            AppSystemNameTextConst.APP_VAT,
                            AppSystemNameTextConst.APP_SSO
                        )
                        .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                        .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                        .ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE).ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                        .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                        .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                        .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                        .ConcatItems(new string[]
                        {
                            AppSystemNameTextConst.APP_HEALTH_RENEW,
                            AppSystemNameTextConst.APP_HEALTH_EDIT,
                            AppSystemNameTextConst.APP_HEALTH_CANCEL,
                        })
                        .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                        .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                        .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                        .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                        .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),
                        HideOnSpecificApps= true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY
                    },
                    new FormSection
                    {
                        Section = "COMMITTEE_INFORMATION_LAWYER",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 5,
                        HideSectionHeader = true,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_LAW_OFFICE,
                            AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                        },
                    },
                    new FormSection
                    {
                        Section = "COMMITTEE_INFORMATION_SELL_ANIMAL_FOOD",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 5,
                        HideSectionHeader = true,
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                            AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD),
                    },
                    new FormSection()
                    {
                        Section = "CONTACT_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Utilities
                    },
                    new FormSection()
                    {
                        Section = "UTILITY_ADDRESS_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 7,
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.App_Utilities
                    },
                    #endregion

                    #region [APP_BUILDING_G1]
                    new FormSection()
                    {
                        Section = "APPLICANT_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_G1 }
                    },
                    new FormSection()
                    {
                        Section = "CONTACT_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_G1 }
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_INFORMATION",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 1
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 2
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_BUILDING_OWNER",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 3,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_R6_BUILDING_OWNER_EMPTY",
                        AddButtonText = "APP_BUILDING_R6_BUILDING_OWNER_BTN_ADD",
                        SubmitButtonText = "ยืนยัน",
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_BUILDING_INFORMATION",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 4,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_BUILDING_INFORMATION_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_BUILDING_INFORMATION_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_BUILDING_INFORMATION_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_TITLE_DEED",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 5,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_TITLE_DEED_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_TITLE_DEED_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_TITLE_DEED_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_DESIGN_ARCHITECT",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 6,
                        ArrayRequiredAtLeast = false,
                        //NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_DESIGN_ARCHITECT_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_DESIGN_ENGINEER",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 7,
                        ArrayRequiredAtLeast = false,
                        //NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_DESIGN_ENGINEER_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ENGINEER_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ENGINEER_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 8,
                        EmptyDataMessage = "APP_BUILDING_G1_SUPERVISE_ARCHITECT_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_SUPERVISE_ENGINEER",
                        SectionGroup = "APP_BUILDING_G1",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 9,
                        EmptyDataMessage = "APP_BUILDING_G1_SUPERVISE_ENGINEER_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ENGINEER_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ENGINEER_BTN_SUBMIT",
                    },
                    #endregion

                    #region [APP_BUILDING_R6]
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_SEARCH_AREA_SECTION",
                        SectionGroup = "APP_BUILDING_R6_SEARCH",
                        Type = SectionType.Form,
                        Ordering = 1,
                        IsPartialView = true,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_SEARCH_SECTION",
                        SectionGroup = "APP_BUILDING_R6_SEARCH",
                        Type = SectionType.Form,
                        Ordering = 2,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }
                    },

                    new FormSection()
                    {
                        Section = "APPLICANT_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }
                    },
                    new FormSection()
                    {
                        Section = "CONTACT_INFORMATION",
                        SectionGroup = "INFORMATION",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_INFORMATION",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 1
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 2
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_TITLE_DEED",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 3,
                        //ArrayRequiredAtLeast = true,
                        //NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_R6_TITLE_DEED_EMPTY",
                        AddButtonText = "APP_BUILDING_R6_TITLE_DEED_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_R6_TITLE_DEED_BTN_SUBMIT",
                        DisableAddRemoveItem = true,
                        UneditableData = true
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_CONSTRUCT_COMPLETEDATE",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 4
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_REQUESTOR_INFORMATION_HEADER",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen, UserTypeEnum.Juristic },
                        Ordering = 5,
                        HideSectionHeader = false,
                        ShowOnSpecificApps = true, 
                        AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_BUILDING_OWNER",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 5,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_R6_BUILDING_OWNER_EMPTY",
                        AddButtonText = "APP_BUILDING_R6_BUILDING_OWNER_BTN_ADD",
                        SubmitButtonText = "ยืนยัน",
                        HideSectionHeader = true,
                        DisplayCondition = new FormControlDisableCondition
                        {
                            Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                           {
                                new FormControlDisplayCondition.ControlWithAnswer
                                {
                                    ControlName = "APP_BUILDING_R6_REQUESTOR_INFORMATION_REQUEST_TYPE",
                                    ControlAnswer = "BUILDING_OWNER"
                                },
                           },
                        }
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_BUILDING_POSSESSORS",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 6,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_R6_BUILDING_POSSESSORS_EMPTY",
                        AddButtonText = "APP_BUILDING_R6_BUILDING_POSSESSORS_BTN_ADD",
                        SubmitButtonText = "ยืนยัน",
                        HideSectionHeader = true,
                        DisplayCondition = new FormControlDisableCondition
                        {
                            Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                           {
                                new FormControlDisplayCondition.ControlWithAnswer
                                {
                                    ControlName = "APP_BUILDING_R6_REQUESTOR_INFORMATION_REQUEST_TYPE",
                                    ControlAnswer = "BUILDING_POSSESSOR"
                                },
                           },
                        }
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_R6_BUILDING_INFORMATION",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 7,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_R6_BUILDING_INFORMATION_EMPTY",
                        //AddButtonText = "APP_BUILDING_G1_BUILDING_INFORMATION_BTN_ADD",
                        SubmitButtonText = "ยืนยัน",
                        DisableAddRemoveItem = true,
                    },
                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 8,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_SUPERVISE_ARCHITECT_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ARCHITECT_BTN_SUBMIT",
                    },

                    new FormSection()
                    {
                        Section = "APP_BUILDING_G1_SUPERVISE_ENGINEER",
                        SectionGroup = "APP_BUILDING_R6",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen },
                        Ordering = 9,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        EmptyDataMessage = "APP_BUILDING_G1_SUPERVISE_ENGINEER_EMPTY",
                        AddButtonText = "APP_BUILDING_G1_ENGINEER_BTN_ADD",
                        SubmitButtonText = "APP_BUILDING_G1_ENGINEER_BTN_SUBMIT",
                    },
                    #endregion

                    #region [APP_SSO]
                    // APP_SSO
                    new FormSection() { Section = "SSO_EMPLOYEE_INFORMATION", SectionGroup = "APP_SSO", Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 1,
                        EmptyDataMessage = "SSO_EMPLOYEE_INFORMATION_EMPTY", AddButtonText = "SSO_EMPLOYEE_INFORMATION_BTN_ADD",
                        SubmitButtonText = "EMPLOYEE_INFORMATION_BTN_SUBMIT", IsPartialView = true },
                    new FormSection() { Section = "CONTRACTOR_INFORMATION", SectionGroup = "APP_SSO", Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 2,
                        EmptyDataMessage = "CONTRACTOR_INFORMATION_EMPTY", AddButtonText = "CONTRACTOR_INFORMATION_BTN_ADD",
                        SubmitButtonText = "CONTRACTOR_INFORMATION_BTN_SUBMIT" },
                    new FormSection() { Section = "SSO_ETC", SectionGroup = "APP_SSO", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 3, IsPartialView = true },
                    new FormSection() { Section = "SSO_ETC2", SectionGroup = "APP_SSO", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 4, NoHeader = true, HideSectionHeader = true },
                    #endregion
                    #region [APP_VAT]
                    // APP_VAT_AGREEMENT
                    new FormSection() { Section = "VAT_EXPLANATION", SectionGroup = "APP_VAT_AGREEMENT", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 1 , HideOnConfirmationPage = true },
                    new FormSection() { Section = "VAT_AGREEMENT", SectionGroup = "APP_VAT_AGREEMENT", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 2, IsPartialView = true, HideOnConfirmationPage = true },

                    // APP_VAT
                    new FormSection() { Section = "VAT_INFORMATION", SectionGroup = "APP_VAT", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 1, IsPartialView = true, NoHeader = true },

                    // APP_VAT_CONFIRMATION
                    new FormSection() { Section = "VAT_CONFIRMATION", SectionGroup = "APP_VAT_CONFIRMATION", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 1, IsPartialView = true, HideOnConfirmationPage = true },
                    new FormSection() { Section = "VAT_EXAMPLE", SectionGroup = "APP_VAT_CONFIRMATION", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 2, IsPartialView = true },
                    new FormSection() { Section = "VAT_ONLINE_REGIS", SectionGroup = "APP_VAT_CONFIRMATION", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 3, IsPartialView = true, HideOnConfirmationPage = true },
                    #endregion
                    #region [UTILITY APPS]
                    // APP_MEA
                    new FormSection() { Section = "MEA_INFORMATION", SectionGroup = "APP_MEA", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen }, Ordering = 1 },

                    //APP_MWA
                    new FormSection() { Section = "MWA_INFORMATION", SectionGroup = "APP_MWA", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen }, Ordering = 1 },

                    //APP_TOT
                    new FormSection() { Section = "TOT_INFORMATION", SectionGroup = "APP_TOT", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen }, Ordering = 1 },

                    //APP_PEA
                    new FormSection() { Section = "PEA_INFORMATION", SectionGroup = "APP_PEA", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen }, Ordering = 1 },

                    //APP_PWA
                    new FormSection() { Section = "PWA_INFORMATION", SectionGroup = "APP_PWA", Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic, UserTypeEnum.Citizen }, Ordering = 1 },
                    #endregion

                    #region APP SOFTWARE HOUSE
                    //NEW
                    //new FormSection()
                    //{
                    //    Section = "COMMITTEE_VAT_CHECK",
                    //    SectionGroup = "APP_CHECK_COMMITEE",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 1,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = AppSystemNameTextConst.APP_SOFTWARE_HOUSE,
                    //    IsPartialView = true,
                    //    HideOnConfirmationPage = true,
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_DETAIL",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 1,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    }
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CHECK",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 1,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 1,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                        //DisplayCondition = new FormControlDisplayCondition()
                        //{
                        //    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        //    {
                        //        new FormControlDisplayCondition.ControlWithAnswer()
                        //        {
                        //            ControlName = "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CHECKBOX__EDIT_SOFTWARE_HOUSE_DETAIL",
                        //            ControlAnswer = "true"
                        //        }
                        //    }
                        //},
                        //HideSectionHeader = true
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_TYPE",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.ArrayOfForms,
                    //    TemplateName = "SOFTWARE_HOUSE_BUSINESS_TYPE",
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 2,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    },
                    //    EmptyDataMessage = "SOFTWARE_HOUSE_BUSINESS_TYPE_EMPTY",
                    //    AddButtonText = "SOFTWARE_HOUSE_BUSINESS_TYPE_BTN_ADD",
                    //    SubmitButtonText = "SOFTWARE_HOUSE_BUSINESS_TYPE_SUBMIT",
                    //    ArrayRequiredAtLeast = true,
                    //    NumberRequiredAtLeast = 1,
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_TYPE_EDIT",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.ArrayOfForms,
                    //    TemplateName = "SOFTWARE_HOUSE_BUSINESS_TYPE_EDIT",
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 2,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //    EmptyDataMessage = "SOFTWARE_HOUSE_BUSINESS_TYPE_EMPTY",
                    //    AddButtonText = "SOFTWARE_HOUSE_BUSINESS_TYPE_BTN_ADD",
                    //    SubmitButtonText = "SOFTWARE_HOUSE_BUSINESS_TYPE_SUBMIT",
                        //DisplayCondition = new FormControlDisplayCondition()
                        //{
                        //    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        //    {
                        //        new FormControlDisplayCondition.ControlWithAnswer()
                        //        {
                        //            ControlName = "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CHECKBOX__EDIT_SOFTWARE_HOUSE_DETAIL",
                        //            ControlAnswer = "true"
                        //        }
                        //    }
                        //}
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_DETAIL_CON",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 3,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    },
                    //    HideSectionHeader = true,
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_DETAIL_CON_EDIT",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 3,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //    HideSectionHeader = true,
                        //DisplayCondition = new FormControlDisplayCondition()
                        //{
                        //    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        //    {
                        //        new FormControlDisplayCondition.ControlWithAnswer()
                        //        {
                        //            ControlName = "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CHECKBOX__EDIT_SOFTWARE_HOUSE_DETAIL",
                        //            ControlAnswer = "true"
                        //        }
                        //    }
                        //}
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_EDIT_CHECK",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 4,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 4,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    },
                    //    HideSectionHeader = true,
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_EDIT",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 4,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //    HideSectionHeader = true,
                        //DisplayCondition = new FormControlDisplayCondition()
                        //{
                        //    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        //    {
                        //        new FormControlDisplayCondition.ControlWithAnswer()
                        //        {
                        //            ControlName = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_EDIT_CHECKBOX__EDIT_SOFTWARE_DETAIL",
                        //            ControlAnswer = "true"
                        //        }
                        //    }
                        //}
                    //},

                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.ArrayOfForms,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 2,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    },
                    //    HideSectionHeader = true,
                    //    EmptyDataMessage = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EMPTYMSG",
                    //    AddButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_ADDMSG",
                    //    SubmitButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_SUMITMSG",
                    //    ArrayRequiredAtLeast = true,
                    //    NumberRequiredAtLeast = 1,
                    //},

                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_CONFIRM",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 6,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                    //    },
                    //    //AppSystemNames = AppSystemNameTextConst.APP_SOFTWARE_HOUSE.Where(x => x != AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW).ToArray(),
                    //    HideSectionHeader = true,
                    //},
                    //new FormSection()
                    //{
                    //    Section = "SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT",
                    //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                    //    Type = SectionType.Form,
                    //    IdentityTypes = new UserTypeEnum[]
                    //    {
                    //        UserTypeEnum.Juristic,
                    //        UserTypeEnum.Citizen
                    //    },
                    //    Ordering = 5,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                    //    },
                    //    //AppSystemNames = AppSystemNameTextConst.APP_SOFTWARE_HOUSE.Where(x => x != AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW).ToArray(),
                    //    HideSectionHeader = true,
                    //},

                    // RENEW
                    new FormSection()
                    {
                        Section = "SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW",
                        SectionGroup = "APP_SOFTWARE_HOUSE_RENEW",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        Ordering = 6,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW
                        },
                    },
                    new FormSection()
                    {
                        Section = "SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW_ARR",
                        SectionGroup = "APP_SOFTWARE_HOUSE_RENEW",
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        Ordering = 7,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW
                        },
                        EmptyDataMessage = "SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW_ARR_EMPTY",
                        AddButtonText = "SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW_ARR_ADD",
                        SubmitButtonText = "SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW_ARR_SUBMIT",
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                    },
                    new FormSection()
                    {
                        Section = "SOFTWARE_HOUSE_BUSSINESS_CONFIRM_RENEW",
                        SectionGroup = "APP_SOFTWARE_HOUSE_RENEW",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        Ordering = 8,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW
                        },
                        HideSectionHeader = true,
                    }
                    #endregion

                };

                db.InsertMany(items);
                InitForConsent(db);
                InitForRestaurant(db);
                InitForBusinessBadHealth(db);
                InitForSellProductInPublic(db);
                InitForRetail(db);
                InitForVeterinaryHospital(db);
            }
        }

        private static void InitForConsent(IMongoCollection<FormSection> db)
        {
            FormSection[] items = new FormSection[]
            {
                #region Consent
                    new FormSection()
                    {
                        Section = "APP_SOFTWARE_HOUSE_CONSENT_DETAIL",
                        SectionGroup = "CONSENT",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        Ordering = 1,
                        ShowOnSpecificApps = true,
                        AppSystemNames = AppSystemNameTextConst.APP_SOFTWARE_HOUSE,
                        IsPartialView = true,
                        HideOnConfirmationPage = true,
                        HideSectionHeader = true,
                    },
                    #endregion
            };

            db.InsertMany(items);
        }

        private static void InitForRetail(IMongoCollection<FormSection> db)
        {

            FormSection[] items = new FormSection[]
            {
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_FOOD_OPERATOR_HEADER",
                    SectionGroup = "APP_SELL_ANIMAL_FOOD",
                    Type = SectionType.Form,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                    Ordering = -1
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_FOOD_OPERATOR_INFO",
                    SectionGroup = "APP_SELL_ANIMAL_FOOD",
                    Type = SectionType.Form,
                    Ordering = 0,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                    //EmptyDataMessage = "APP_SELL_ANIMAL_FOOD_OPERATOR_INFO_EMPTY", AddButtonText = "APP_SELL_ANIMAL_FOOD_OPERATOR_INFO_BTN_ADD",
                    //SubmitButtonText = "APP_SELL_ANIMAL_FOOD_OPERATOR_INFO_BTN_SUBMIT",
                    //ArrayRequiredAtLeast = true,
                    //NumberRequiredAtLeast = 1,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_FOOD",
                    SectionGroup = "APP_SELL_ANIMAL_FOOD",
                    Type = SectionType.Form,
                    Ordering = 1
                },

                new FormSection() {
                    Section = "APP_SELL_ANIMAL_FOOD_INFO",
                    SectionGroup = "APP_SELL_ANIMAL_FOOD",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "APP_SELL_ANIMAL_FOOD_INFO_EMPTY",
                    AddButtonText = "APP_SELL_ANIMAL_FOOD_INFO_BTN_ADD",
                    SubmitButtonText = "APP_SELL_ANIMAL_FOOD_INFO_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_SELL_CARCASS_HEADER",
                    SectionGroup = "APP_SELL_CARCASS",
                    Type = SectionType.Form,
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_SELL_CARCASS_INFO2",
                    SectionGroup = "APP_SELL_CARCASS",
                    Type = SectionType.Form,
                    Ordering = 2,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_SELL_CARCASS_INFO",
                    SectionGroup = "APP_SELL_CARCASS",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "APP_SELL_CARCASS_INFO_EMPTY",
                    AddButtonText = "APP_SELL_CARCASS_INFO_BTN_ADD",
                    SubmitButtonText = "APP_SELL_CARCASS_INFO_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,

                    // Not show this section anymore
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[0],
                },
                new FormSection() {
                    Section = "APP_SELL_CARCASS_FOOTER",
                    SectionGroup = "APP_SELL_CARCASS",
                    HideSectionHeader = true,
                    Type = SectionType.Form,
                    Ordering = 3
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_HEADER",
                    SectionGroup = "APP_SELL_ANIMAL",
                    Type = SectionType.Form,
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_INFO2",
                    SectionGroup = "APP_SELL_ANIMAL",
                    Type = SectionType.Form,
                    Ordering = 2,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_INFO",
                    SectionGroup = "APP_SELL_ANIMAL",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "APP_SELL_ANIMAL_INFO_EMPTY",
                    AddButtonText = "APP_SELL_ANIMAL_INFO_BTN_ADD",
                    SubmitButtonText = "APP_SELL_ANIMAL_INFO_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,

                    // Not show this section anymore
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[0],
                },
                new FormSection() {
                    Section = "APP_SELL_ANIMAL_FOOTER",
                    SectionGroup = "APP_SELL_ANIMAL",
                    HideSectionHeader = true,
                    Type = SectionType.Form,
                    Ordering = 3
                },
                new FormSection() {
                    Section = "APP_DIRECT_MARKETING_SECTION",
                    SectionGroup = "APP_DIRECT_MARKETING",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_DIRECT_MARKETING_EDIT_SECTION",
                    SectionGroup = "APP_DIRECT_MARKETING_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_EDIT,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_DIRECT_MARKETING_CANCEL_SECTION",
                    SectionGroup = "APP_DIRECT_MARKETING_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_DIRECT_SELL_SECTION",
                    SectionGroup = "APP_DIRECT_SELL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_SELL,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_DIRECT_SELL_EDIT_SECTION",
                    SectionGroup = "APP_DIRECT_SELL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_SELL_EDIT,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_DIRECT_SELL_CANCEL_SECTION",
                    SectionGroup = "APP_DIRECT_SELL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_SELL_CARD_SECTION",
                    SectionGroup = "APP_SELL_CARD",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_CARD,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_BUILDING_BUILDING_SECTION",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.Form,
                    Ordering = 1,
                },
                new FormSection() {
                    Section = "APP_BUILDING_BUILDING_SECTION_PART",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "APP_BUILDING_BUILDING_SECTION_EMPTY",
                    AddButtonText = "APP_BUILDING_BUILDING_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_BUILDING_BUILDING_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_BUILDING_OWNED_BUILDING_SECTION",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 3,
                    EmptyDataMessage = "APP_BUILDING_OWNED_BUILDING_SECTION_EMPTY",
                    AddButtonText = "APP_BUILDING_OWNED_BUILDING_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_BUILDING_OWNED_BUILDING_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    HideOnSpecificApps = true,
                    HideAppSystemNames = new string[]
                    {
                      AppSystemNameTextConst.APP_BUILDING_RENEW,
                      AppSystemNameTextConst.APP_BUILDING_DPW_RENEW,
                      AppSystemNameTextConst.APP_BUILDING_DISTRICT_RENEW,
                      AppSystemNameTextConst.APP_BUILDING_OTHER_RENEW
                    },
                },
                 new FormSection() {
                    Section = "APP_BUILDING_OWNED_AREA_SECTION",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 4,
                    EmptyDataMessage = "APP_BUILDING_OWNED_AREA_SECTION_EMPTY",
                    AddButtonText = "APP_BUILDING_OWNED_AREA_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_BUILDING_OWNED_AREA_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                  new FormSection() {
                    Section = "APP_BUILDING_SUPERVISOR_SECTION",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 5,
                    EmptyDataMessage = "APP_BUILDING_SUPERVISOR_SECTION_EMPTY",
                    AddButtonText = "APP_BUILDING_SUPERVISOR_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_BUILDING_SUPERVISOR_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                  new FormSection() {
                    Section = "APP_BUILDING_DESIGN_ENGINEER_SECTION",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 6,
                    EmptyDataMessage = "APP_BUILDING_DESIGN_ENGINEER_SECTION_EMPTY",
                    AddButtonText = "APP_BUILDING_DESIGN_ENGINEER_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_BUILDING_DESIGN_ENGINEER_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_BUILDING_STATIC",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.Form,
                    HideSectionHeader = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_BUILDING,
                        AppSystemNameTextConst.APP_BUILDING_DPW,
                        AppSystemNameTextConst.APP_BUILDING_DISTRICT,
                        AppSystemNameTextConst.APP_BUILDING_OTHER

                    },
                    Ordering = 7,
                },
                 new FormSection() {
                    Section = "APP_BUILDING_RENEW_STATIC",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.Form,
                    HideSectionHeader = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_BUILDING_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_DPW_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_DISTRICT_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_OTHER_RENEW
                    },
                    Ordering = 8,
                },
                 new FormSection() {
                    Section = "APP_BUILDING_RENEW_INFO",
                    SectionGroup = "APP_BUILDING",
                    Type = SectionType.Form,
                    HideSectionHeader = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_BUILDING_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_DPW_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_DISTRICT_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_OTHER_RENEW
                    },
                    Ordering = 9,
                },

                  new FormSection() {
                    Section = "APP_HOTEL_SECTION",
                    SectionGroup = "APP_HOTEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOTEL,
                    },
                    Ordering = 1
                },
                  new FormSection() {
                    Section = "APP_HOTEL_RENEW_SECTION",
                    SectionGroup = "APP_HOTEL_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOTEL_RENEW,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_REGISTRATION_SECTION",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 1,
                    EmptyDataMessage = "APP_COMMERCIAL_REGISTRATION_SECTION_EMPTY",
                    AddButtonText = "APP_COMMERCIAL_REGISTRATION_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_COMMERCIAL_REGISTRATION_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_REGISTRATION_SECTION_PART",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 2,
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_DIRECTOR_SECTION",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 3,
                    Info = "APP_COMMERCIAL_DIRECTOR_SECTION_INFO",
                    EmptyDataMessage = "APP_COMMERCIAL_DIRECTOR_SECTION_EMPTY",
                    AddButtonText = "APP_COMMERCIAL_DIRECTOR_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_COMMERCIAL_DIRECTOR_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_PARTNER_SECTION",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 4,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                    DisplayCondition = APP_COMMERCIAL_DISPLAY_CONDITION.COMMERCIAL_TYPE_PARTNERSHIP,
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_PARTNER_SECTION_PART",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 5,
                    DisplayCondition = APP_COMMERCIAL_DISPLAY_CONDITION.COMMERCIAL_TYPE_PARTNERSHIP,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                    EmptyDataMessage = "APP_COMMERCIAL_PARTNER_SECTION_EMPTY",
                    AddButtonText = "APP_COMMERCIAL_PARTNER_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_COMMERCIAL_PARTNER_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    HideSectionHeader = true,

                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_COMPANY_SECTION",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 6,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_COMPANY_SECTION_PART",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 7,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                    EmptyDataMessage = "APP_COMMERCIAL_COMPANY_SECTION_EMPTY",
                    AddButtonText = "APP_COMMERCIAL_COMPANY_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_COMMERCIAL_COMPANY_SECTION_BTN_SUBMIT",
                    HideSectionHeader = true,
                },
                new FormSection() {
                    Section = "APP_COMMERCIAL_OTHER_SECTION",
                    SectionGroup = "APP_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    },
                    Ordering = 8,
                },
                new FormSection() {
                    Section = "APP_TAX_BOARD_SECTION",
                    SectionGroup = "APP_TAX",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TAX,
                        AppSystemNameTextConst.APP_TAX_RENEW,
                    },
                    Ordering = 2,
                    EmptyDataMessage = "APP_TAX_BOARD_INFO_SECTION_EMPTY",
                    AddButtonText = "APP_TAX_BOARD_INFO_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_TAX_BOARD_INFO_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    HideSectionHeader = true,
                },
                   new FormSection() {
                    Section = "APP_TAX_ALL_AMOUNT",
                    SectionGroup = "APP_TAX",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TAX,
                        AppSystemNameTextConst.APP_TAX_RENEW,
                    },
                    Ordering = 1
                },
                   new FormSection() {
                    Section = "APP_TAX_ALL_BOARD",
                    SectionGroup = "APP_TAX",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    HideSectionHeader = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TAX,
                        AppSystemNameTextConst.APP_TAX_RENEW,
                    },
                    Ordering = 3
                },
                new FormSection() {
                    Section = "APP_RADIO_SECTION",
                    SectionGroup = "APP_RADIO",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_RADIO,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_RADIO_RENEW_SECTION",
                    SectionGroup = "APP_RADIO_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_RADIO_RENEW,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_ANIMAL_MED_SECTION",
                    SectionGroup = "APP_ANIMAL_MED",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_MED,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_ANIMAL_MED_OWNER_INFO",
                    SectionGroup = "APP_ANIMAL_MED",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_MED,
                    },
                    Ordering = 2
                },
                //new FormSection() {
                //    Section = "APP_ANIMAL_MED_DR_INFO",
                //    SectionGroup = "APP_ANIMAL_MED",
                //    Type = SectionType.ArrayOfForms,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_ANIMAL_MED,
                //    },
                //    Ordering = 3,
                //    EmptyDataMessage = "APP_ANIMAL_MED_DR_INFO_EMPTY",
                //    AddButtonText = "APP_ANIMAL_MED_DR_INFO_BTN_ADD",
                //    SubmitButtonText = "APP_ANIMAL_MED_DR_INFO_BTN_SUBMIT",
                //    //ArrayRequiredAtLeast = true,
                //    NumberRequiredAtLeast = 1,
                //},
                new FormSection() {
                    Section = "APP_ANIMAL_MED_DR_CONFIRM",
                    SectionGroup = "APP_ANIMAL_MED",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_MED,
                    },
                    Ordering = 4,
                    HideSectionHeader = true,

                },
                new FormSection() {
                    Section = "APP_ANIMAL_MED_RENEW_SECTION",
                    SectionGroup = "APP_ANIMAL_MED_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_MED_RENEW,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_ANIMAL_LICENSE_SECTION",
                    SectionGroup = "APP_ANIMAL_LICENSE",
                    Type = SectionType.Form,
                    Info = "APP_ANIMAL_LICENSE_SECTION_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_ANIMAL_LICENSE_INFO",
                    SectionGroup = "APP_ANIMAL_LICENSE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    },
                    Ordering = 2
                },
                new FormSection() {
                    Section = "APP_ANIMAL_LICENSE_WORK_TIME",
                    SectionGroup = "APP_ANIMAL_LICENSE",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    },
                    Ordering = 2,
                    EmptyDataMessage = "APP_ANIMAL_LICENSE_WORK_TIME_EMPTY",
                    AddButtonText = "APP_ANIMAL_LICENSE_WORK_TIME_BTN_ADD",
                    SubmitButtonText = "APP_ANIMAL_LICENSE_WORK_TIME_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_ANIMAL_LICENSE_STATUS",
                    SectionGroup = "APP_ANIMAL_LICENSE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    },
                    Ordering = 3
                },
                new FormSection() {
                    Section = "APP_ANIMAL_LICENSE_RENEW_SECTION",
                    SectionGroup = "APP_ANIMAL_LICENSE_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE_RENEW,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_HEALTH_SECTION",
                    SectionGroup = "APP_HEALTH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HEALTH,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_HEALTH_CARE_MANAGER",    //ประเภทสถานประกอบการที่ต้องมีผู้ดำเนินการ
                    SectionGroup = "APP_HEALTH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HEALTH,
                    },
                    Ordering = 2
                },
                new FormSection() {
                    Section = "APP_HEALTH_CARE_MANAGER_PART",   //ชื่อผู้ดำเนินการ
                    SectionGroup = "APP_HEALTH",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HEALTH,
                    },
                    Ordering = 2,
                    HideSectionHeader = true,
                    EmptyDataMessage = "APP_HEALTH_CARE_MANAGER_PART_SECTION_EMPTY",
                    AddButtonText = "APP_HEALTH_CARE_MANAGER_PART_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_HEALTH_CARE_MANAGER_PART_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_HEALTH_CARE_SERVICE",   //ข้อมูลผู้ให้บริการ
                    SectionGroup = "APP_HEALTH",
                    Type = SectionType.ArrayOfForms,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HEALTH,
                    },
                    Ordering = 3,
                    EmptyDataMessage = "APP_HEALTH_CARE_SERVICE_SECTION_EMPTY",
                    AddButtonText = "APP_HEALTH_CARE_SERVICE_SECTION_BTN_ADD",
                    SubmitButtonText = "APP_HEALTH_CARE_SERVICE_SECTION_BTN_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                },
                //-----------------------------------------------------------
                //new FormSection() {
                //    Section = "SELL_TOBACCO_INFO",
                //    SectionGroup = "APP_SELL_TOBACCO",
                //    Type = SectionType.Form,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_SELL_TOBACCO,
                //    },
                //    Ordering = 1
                //},
                 new FormSection() {
                    Section = "APP_MOVIE_RESPOND_SECTION",
                    SectionGroup = "APP_MOVIE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_MOVIE,
                    },
                    Ordering = 1
                },
                new FormSection() {
                    Section = "APP_KARAOKE_RESPOND_SECTION",
                    SectionGroup = "APP_KARAOKE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_KARAOKE,
                    },
                    Ordering = 1
                },

                 new FormSection() {
                    Section = "APP_MOVIE_INFO",
                    SectionGroup = "APP_MOVIE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_MOVIE,
                    },
                    Ordering = 2
                 },
                 new FormSection() {
                    Section = "APP_KARAOKE_INFO",
                    SectionGroup = "APP_KARAOKE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_KARAOKE,
                    },
                    Ordering = 2
                 },
                 new FormSection() {
                    Section = "APP_SELL_FOOD_EDIT_SECTION",
                    SectionGroup = "APP_SELL_FOOD_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT,
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT,
                    },
                    Ordering = 1
                 },
                 new FormSection()
                 {
                    Section = "SELL_FOOD_FOOD_WORKER_CONFIRM",
                    SectionGroup = "APP_SELL_FOOD_EDIT",
                    Type = SectionType.Form,
                    HideSectionHeader = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT,
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT,
                    },
                    Ordering = 3
                 },

#region FRT001
               //FRT Section New
                // ข้อมูลกิจการ
                new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION1",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    Ordering = 1,
                 },

                 //ข้อมูลบุคคล
                 new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION2",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    Ordering = 2,
                },

                 //ข้อมูลผู้จัดการ Temlate
                new FormSection()
                {
                    Section = "F01_BOX_MNG_INFO_SECTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "F01_BOX_MNG_INFO",    //ข้อมูลผู้จัดการ
                    Ordering = 4,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    EmptyDataMessage = "MANAGER_EMPTY",
                    AddButtonText = "MANAGER_BTN_ADD",
                    SubmitButtonText = "MANAGER_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                    HideOnSpecificApps = true,
                    HideAppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_RENEW_001,
                        AppSystemNameTextConst.APP_FRT_EDIT_001,
                        AppSystemNameTextConst.APP_FRT_CANCEL_001,
                    }
                },
                 //วัตถุดิบ
                new FormSection()
                {
                    Section = "F01_BOX_MATERIAL_SECTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "F01_BOX_MATERIAL",    //วัตถุดิบ
                    Ordering = 4,
                    EmptyDataMessage = "MATERIAL_EMPTY",
                    AddButtonText = "MATERIAL_BTN_ADD",
                    SubmitButtonText = "MATERIAL_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                },
                 //ผลิตภัณฑ์
                 new FormSection()
                {
                    Section = "F01_BOX_PRODUCT_SECTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "F01_BOX_PRODUCT",   //ผลิตภัณฑ์
                    Ordering = 5,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    EmptyDataMessage = "PRODUCT_EMPTY",
                    AddButtonText = "PRODUCT_BTN_ADD",
                    SubmitButtonText = "PRODUCT_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                    HideOnSpecificApps = true,
                    HideAppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_RENEW_001,
                        AppSystemNameTextConst.APP_FRT_EDIT_001,
                        AppSystemNameTextConst.APP_FRT_CANCEL_001,
                    }
                },
                 //วัตถุพลอยได้
                 new FormSection()
                {
                    Section = "F01_BOX_OTHER_OUTPUT_SECTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "F01_BOX_OTHER_OUTPUT",    //วัตถุพลอยได้
                    Ordering = 6,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    EmptyDataMessage = "OTHER_OUTPUT_EMPTY",
                    AddButtonText = "OTHER_OUTPUT_BTN_ADD",
                    SubmitButtonText = "OTHER_OUTPUT_BTN_SUBMIT",
                },
                 //อุปกรณ์ เครื่องมือ เครื่องจักร ที่ใช้ในการประกอบการ
                 new FormSection()
                {
                    Section = "F01_BOX_MACHINE_SECTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "F01_BOX_MACHINE",   ////อุปกรณ์ เครื่องมือ เครื่องจักร ที่ใช้ในการประกอบการ
                    Ordering = 7,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    EmptyDataMessage = "MACHINE_EMPTY",
                    AddButtonText = "MACHINE_BTN_ADD",
                    SubmitButtonText = "MACHINE_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                },
                 //การควบคุมมลพิษ
                 new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION_POLLUTION",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_NEW_001,
                    },
                    Ordering = 9,
                },

                 //ข้าพเจ้าขอรับรองว่า
                 new FormSection()
                {
                    Section = "APP_FRT_NEW_001_SECTION_INFO_TAIL",
                    SectionGroup = "APP_FRT_NEW_001",
                    Type = SectionType.Form,
                    Ordering = 10,
                    HideSectionHeader = true,
                },

                 //FRT-RENEW 1.2
                 // ข้อมูลกิจการ
                   new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION1",
                    SectionGroup = "APP_FRT_RENEW_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                         AppSystemNameTextConst.APP_FRT_RENEW_001,
                    },
                    Ordering = 1,
                 },
                   new FormSection() {
                        Section = "APP_FRT_NEW_001_SECTION2",   //ข้อมูลบุคคล
                        SectionGroup = "APP_FRT_RENEW_001",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_FRT_RENEW_001,
                        },
                        Ordering = 2,
                   },
                   new FormSection() {
                        Section = "APP_FRT_NEW_001_SECTION3",   //ข้อมูลการเปลี่ยนแปลง ขยาย....เลขที่ เล่มที่
                        SectionGroup = "APP_FRT_RENEW_001",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_FRT_RENEW_001,
                        },
                        Ordering = 2,
                        HideSectionHeader = true,
                   },
                    new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION_INFO_TAIL",   //ข้าพเจ้าขอรับรองว่า...
                    SectionGroup = "APP_FRT_RENEW_001",
                    Type = SectionType.Form,
                    Ordering = 10,
                    HideSectionHeader = true,
                },


                 //FRT-EDIT 1.2
                 // ข้อมูลกิจการ
                  new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION1",
                    SectionGroup = "APP_FRT_EDIT_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                         AppSystemNameTextConst.APP_FRT_EDIT_001,
                    },
                    Ordering = 1,
                  },
                  // ข้อมูลบุคลากร
                 new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION2",   //ข้อมูลบุคคล
                    SectionGroup = "APP_FRT_EDIT_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_FRT_EDIT_001,
                    },
                    Ordering = 2,
                },
                 //ข้อมูลการเปลี่ยนแปลง ขยาย....เล่มที่ เลขที่ ปี  วัน เดิอน ปี ที่ขอใบอนุญาต
                 new FormSection() {
                        Section = "APP_FRT_NEW_001_SECTION3",   //ข้อมูลการเปลี่ยนแปลง ขยาย....เล่มที่ เลขที่ ปี  วัน เดิอน ปี ที่ขอใบอนุญาต
                        SectionGroup = "APP_FRT_EDIT_001",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_FRT_EDIT_001,
                        },
                        Ordering = 2,
                   },
                 //ข้าพเจ้าขอรับรองว่า...
                  new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION_INFO_TAIL",   //ข้าพเจ้าขอรับรองว่า...
                    SectionGroup = "APP_FRT_EDIT_001",
                    Type = SectionType.Form,
                    Ordering = 10,
                    HideSectionHeader = true,

                },
                 //ข้อมูลการเปลียนแปลง ขยาย ....
                 //new FormSection()
                 //{
                 //   Section = "APP_FRT_EDIT_001_SECTION3",
                 //   SectionGroup = "APP_FRT_EDIT_001",
                 //   Type = SectionType.Form,
                 //   TemplateName = "APP_Danger_Food_EDIT",  // ข้อมูลบุคลากร
                 //   Ordering = 8,
                 //},

                 //FRT-CANCEL
                 // ข้อมูลกิจการ
                  new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION1",
                    SectionGroup = "APP_FRT_CANCEL_001",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                         AppSystemNameTextConst.APP_FRT_CANCEL_001,
                    },
                    Ordering = 1,
                  },
                  // ข้อมูลบุคลากร
                // new FormSection() {
                //    Section = "APP_FRT_NEW_001_SECTION2",   //ข้อมูลบุคคล
                //    SectionGroup = "APP_FRT_CANCEL_001",
                //    Type = SectionType.Form,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_FRT_CANCEL_001,
                //    },
                //    Ordering = 2,
                //},
                 //ข้อมูลการเปลี่ยนแปลง ขยาย....เล่มที่ เลขที่ ปี  วัน เดิอน ปี ที่ขอใบอนุญาต
                 new FormSection() {
                        Section = "APP_FRT_NEW_001_SECTION3",   //ข้อมูลการเปลี่ยนแปลง ขยาย....เล่มที่ เลขที่ ปี  วัน เดิอน ปี ที่ขอใบอนุญาต
                        SectionGroup = "APP_FRT_CANCEL_001",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_FRT_CANCEL_001,
                        },
                        Ordering = 2,
                        HideSectionHeader = true,
                   },
                 //ข้อมูลการบอกเลิกการดำเนินกิจการ  
                  new FormSection() {
                        Section = "APP_FRT_CANCEL_001",   //ข้อมูลการเปลี่ยนแปลง ขยาย....เล่มที่ เลขที่ ปี  วัน เดิอน ปี ที่ขอใบอนุญาต
                        SectionGroup = "APP_FRT_CANCEL_001",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_FRT_CANCEL_001,
                        },
                        Ordering = 7,
                   },
                 //ข้าพเจ้าขอรับรองว่า...
                  new FormSection() {
                    Section = "APP_FRT_NEW_001_SECTION_INFO_TAIL",   //ข้าพเจ้าขอรับรองว่า...
                    SectionGroup = "APP_FRT_CANCEL_001",
                    Type = SectionType.Form,
                    Ordering = 10,
                    HideSectionHeader = true,
                },

                 
#endregion FRT001
                //Factory 2
                //new FormSection() {
                //  Section = "APP_FACTORY_CLASS_2_NEW_SECTION_SEARCH",
                //  SectionGroup = "APP_FACTORY_CLASS_2_NEW_SEARCH",
                //  Type = SectionType.Form,
                //  ShowOnSpecificApps = true,
                //  AppSystemNames = new string[] {
                //       AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
                //  },
                //  Ordering = 1,
                //},

                //Factory class 2 new search to array of form
                //new FormSection()
                //    {
                //        Section = "FACTORY_CLASS_2_SEARCH",
                //        SectionGroup = "APP_FACTORY_CLASS_2_NEW_SEARCH",
                //        Type = SectionType.Form,
                //        IdentityTypes = new UserTypeEnum[]
                //        {
                //            UserTypeEnum.Juristic,
                //            UserTypeEnum.Citizen
                //        },
                //        Ordering = 1,
                //        ShowOnSpecificApps = true,
                //        AppSystemNames = new string [] { AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW },
                //        IsPartialView = true,
                //        HideOnConfirmationPage = false,
                //    },
                //-------------------------------------------------------
                 new FormSection()
                    {
                        Section = "FACTORY_CLASS_2_SEARCH",
                        SectionGroup = "APP_FACTORY_CLASS_2_NEW_SEARCH",
                        Type = SectionType.Form,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        Ordering = 1,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string [] { AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW },
                        HideOnConfirmationPage = false,
                    },
                  //-----------------Factory New-------------------------
                  new FormSection() {
                    Section = "APP_FACTORY_CLASS_2_SECTION_INFO",
                    SectionGroup = "APP_FACTORY_CLASS_2_NEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                         AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
                    },
                    Ordering = 1,
                  },
                  new FormSection() {
                    Section = "APP_FACTORY_CLASS_2_SECTION_CONFIRM",
                    SectionGroup = "APP_FACTORY_CLASS_2_NEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                         AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
                    },
                    HideSectionHeader = true,
                    Ordering = 2,
                  },

                  //----------------Factory class 2 Edit------------------------
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_SECTION_PLANT_LOCATION_INFO",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 0,
                  },
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 1,
                  },
                  // Condition 1
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_OPT_1",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 1,
                        HideSectionHeader = true,
                        DisplayCondition = APP_FACTORY_CLASS_2_EDIT.Opt1,
                  },
                  // Condition 2
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_OPT_2",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 1,
                        HideSectionHeader = true,
                        DisplayCondition = APP_FACTORY_CLASS_2_EDIT.Opt2,
                  },
                  // Condition 3
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_OPT_3",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 1,
                        HideSectionHeader = true,
                        DisplayCondition = APP_FACTORY_CLASS_2_EDIT.Opt3,
                  },
                  // Condition 4
                  new FormSection()
                  {
                        Section = "APP_FACTORY_CLASS_2_OPT_4",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        Ordering = 1,
                        HideSectionHeader = true,
                        DisplayCondition = APP_FACTORY_CLASS_2_EDIT.Opt4,
                  },
                  new FormSection() {
                        Section = "APP_FACTORY_CLASS_2_SECTION_CONFIRM",
                        SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                        },
                        HideSectionHeader = true,
                        Ordering = 2,
                  },
                  //Factory class 2 Cancel
                  new FormSection() {
                        Section = "APP_FACTORY_CLASS_2_SECTION_LOCATION_CANCEL",
                        SectionGroup = "APP_FACTORY_CLASS_2_CANCEL",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL,
                        },
                        Ordering = 1,
                  },
                  new FormSection() {
                        Section = "APP_FACTORY_CLASS_2_SECTION_CANCEL_REQUEST",
                        SectionGroup = "APP_FACTORY_CLASS_2_CANCEL",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL,
                        },
                        Ordering = 2,
                  },
                  new FormSection() {
                        Section = "APP_FACTORY_CLASS_2_SECTION_CONFIRM",
                        SectionGroup = "APP_FACTORY_CLASS_2_CANCEL",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                             AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL,
                        },
                        HideSectionHeader = true,
                        Ordering = 3,
                  },

                  // APP_FACTORY_TYPE2
                  new FormSection()
                  {
                        Section = "APP_FACTORY_TYPE2_INFO", // This line is will find word equal 'ข้อมูลกิจการ'
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2
                        },
                        Ordering = 1,
                  },

                   new FormSection()
                   {
                        Section = "APP_FACTORY_TYPE2_RANKS_INFO", // This line is will find word equal 'ข้อมูลกิจการ'
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2
                        },
                        Info = "APP_FACTORY_TYPE2_RANKS_INFO_DETAIL",
                        DefaultShowInfo = true,
                        Ordering = 2,
                   },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO", // This line is will find word equal 'ข้อมูลกิจการ'
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2
                        },
                        Ordering = 3,
                    },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO_CHOICE", // This line is will find word equal 'ข้อมูลกิจการ'
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2
                        },
                        DisplayCondition = APP_FACTORY_TYPE2_CONDITIO.A,
                        DisableCondition = APP_FACTORY_TYPE2_CONDITIO.B,
                        Ordering = 3,
                        HideSectionHeader = true,
                    },

                    //new FormSection()
                    //{
                    //    Section = "APP_FACTORY_TYPE2_LOCATION_INFORMATION_SECTION",
                    //    SectionGroup = "APP_FACTORY_TYPE2_NEW",
                    //    Type = SectionType.ArrayOfForms,
                    //    ShowOnSpecificApps = true,
                    //    HideSectionHeader = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_FACTORY_TYPE2,
                    //    },
                    //    Ordering = 5,
                    //    DisplayCondition = APP_FACTORY_TYPE2_CONDITIO.FACTORY_TYPE2_PROPERTY,
                    //    EmptyDataMessage = "APP_FACTORY_TYPE2_LOCATION_EMPTY_MSG",
                    //    AddButtonText = "APP_FACTORY_TYPE2_LOCATION_ADD_MSG",
                    //    SubmitButtonText = "APP_FACTORY_TYPE2_LOCATION_SUBMIT_MSG",
                    //    ArrayRequiredAtLeast = true,
                    //    NumberRequiredAtLeast = 1,
                    //},

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_MATERIAL_INFORMATION_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        //HideSectionHeader = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2,
                        },
                        Ordering = 5,
                        EmptyDataMessage = "APP_FACTORY_TYPE2_MATERIAL_EMPTY_MSG",
                        AddButtonText = "APP_FACTORY_TYPE2_MATERIAL_ADD_MSG",
                        SubmitButtonText = "APP_FACTORY_TYPE2_MATERIAL_SUBMIT_MSG",
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                    },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_PRODUCT_INFORMATION_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        //HideSectionHeader = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2,
                        },
                        Ordering = 5,
                        EmptyDataMessage = "APP_FACTORY_TYPE2_PRODUCT_EMPTY_MSG",
                        AddButtonText = "APP_FACTORY_TYPE2_PRODUCT_ADD_MSG",
                        SubmitButtonText = "APP_FACTORY_TYPE2_PRODUCT_SUBMIT_MSG",
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                    },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_BY_PRODUCT_INFORMATION_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        //HideSectionHeader = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2,
                        },
                        Ordering = 5,
                        EmptyDataMessage = "APP_FACTORY_TYPE2_BY_PRODUCT_EMPTY_MSG",
                        AddButtonText = "APP_FACTORY_TYPE2_BY_PRODUCT_ADD_MSG",
                        SubmitButtonText = "APP_FACTORY_TYPE2_BY_PRODUCT_SUBMIT_MSG",
                        ArrayRequiredAtLeast = true,
                    },

                   new FormSection()
                   {
                        Section = "APP_FACTORY_TYPE2_ENGINE_INFORMATION_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        //HideSectionHeader = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2,
                        },
                        Ordering = 5,
                        EmptyDataMessage = "APP_FACTORY_TYPE2_ENGINE_EMPTY_MSG",
                        AddButtonText = "APP_FACTORY_TYPE2_ENGINE_ADD_MSG",
                        SubmitButtonText = "APP_FACTORY_TYPE2_ENGINE_SUBMIT_MSG",
                        ArrayRequiredAtLeast = true,
                   },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_SIGNATURE_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        HideSectionHeader = false,
                        HideOnSpecificApps = true,
                        HideAppSystemNames = new string[]
                        {
                            AppSystemNameTextConst .APP_FACTORY_TYPE2,
                        },
                        Ordering = 6
                    },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_SIGNATURE_SECTION",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW",
                        Type = SectionType.Form,
                        HideSectionHeader = true,
                        HideOnSpecificApps = true,
                        HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst .APP_FACTORY_TYPE2,
                        },
                        Ordering = 6
                    },

                    new FormSection()
                    {
                        Section = "APP_FACTORY_TYPE2_INFO_13",
                        SectionGroup = "APP_FACTORY_TYPE2_NEW_13",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_FACTORY_TYPE2
                        },
                        Ordering = 1,
                    },
                  // END APP_FACTORY_TYPE2

                  // APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC
                  //new FormSection()
                  //{
                  //      Section= "APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION",//
                  //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                  //      Type = SectionType.Form,
                  //      ShowOnSpecificApps = true,
                  //      AppSystemNames = new string[]
                  //      {
                  //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                  //      },
                  //},

                  //new FormSection()
                  //{
                  //      Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION",//
                  //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                  //      Type = SectionType.ArrayOfForms,
                  //      ShowOnSpecificApps = true,
                  //      HideSectionHeader = true,
                  //      AppSystemNames = new string[]
                  //      {
                  //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                  //      },
                  //      DisplayCondition = APP_ORGANIC_NEW.ALONE_PRODUCTION,
                  //      EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_EMPTY_MSG",
                  //      AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_ADD_MSG",
                  //      SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_SUBMIT_MSG",
                  //},

                  //new FormSection()
                  //{
                  //      Section= "APP_ORGANIC_PLANT_PRODUCTION_CONFIRM_LANGUAGE",//
                  //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                  //      Type = SectionType.Form,
                  //      ShowOnSpecificApps = true,
                  //      AppSystemNames = new string[]
                  //      {
                  //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                  //      },
                  //      DisplayCondition = APP_ORGANIC_NEW.ALONE_PRODUCTION,
                  //      HideSectionHeader = true,
                  //},
                
                  //new FormSection()
                  //{
                  //      Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_THAI",//
                  //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                  //      Type = SectionType.Form,
                  //      ShowOnSpecificApps = true,
                  //      AppSystemNames = new string[] {
                  //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                  //      },
                  //      DisplayCondition = APP_ORGANIC_NEW.THAI_NAME
                  // },

                  //new FormSection()
                  //{
                  //      Section= "APP_ORGANIC_PLANT_PRODUCTION_ENG_ADDRESS_SECTION",
                  //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                  //      Type = SectionType.Form,
                  //      ShowOnSpecificApps = true,
                  //      AppSystemNames = new string[]
                  //      {
                  //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                  //      },
                  //      DisplayCondition = APP_ORGANIC_NEW.ENG_NAME,
                  //},

                   //new FormSection()
                   //{
                   //     Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION",
                   //     SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                   //     Type = SectionType.Form,
                   //     ShowOnSpecificApps = true,
                   //     AppSystemNames = new string[] 
                   //     {
                   //         AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                   //     },
                   //     DisplayCondition = APP_ORGANIC_NEW.ALONE_PRODUCTION,
                   //},

                    //new FormSection()
                    //{
                    //    Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_ALONE_SECTION",
                    //    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                    //    Type = SectionType.Form,
                    //    ShowOnSpecificApps = true,
                    //    HideSectionHeader = true,
                    //    AppSystemNames = new string[] {
                    //        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                    //    },
                    //    DisplayCondition = APP_ORGANIC_NEW.ALONE_PRODUCTION,
                    //},

                    //new FormSection()
                    //{
                    //    Section= "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION",
                    //    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                    //    Type = SectionType.Form,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                    //    },
                    //    DisplayCondition = APP_ORGANIC_NEW.HEAD_OF_GROUP,
                    //},

                    //new FormSection()
                    //{
                    //    Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION",
                    //    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                    //    Type = SectionType.ArrayOfForms,
                    //    ShowOnSpecificApps = true,
                    //    AppSystemNames = new string[]
                    //    {
                    //        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                    //    },
                    //    DisplayCondition = APP_ORGANIC_NEW.GROUP_PRODUCTION,
                    //    EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_EMPTY_MSG",
                    //    AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_ADD_MSG",
                    //    SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_SUBMIT_MSG",
                    //},

                     //new FormSection()
                     //{
                     //   Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION",
                     //   SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                     //   Type = SectionType.Form,
                     //   ShowOnSpecificApps = true,
                     //   HideSectionHeader = true,
                     //   AppSystemNames = new string[] {
                     //       AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                     //   },
                     //   DisplayCondition =  APP_ORGANIC_NEW.GROUP_PRODUCTION,
                     //},

                      //new FormSection()
                      //{
                      //      Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION",
                      //      SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                      //      Type = SectionType.Form,
                      //      ShowOnSpecificApps = true,
                      //      AppSystemNames = new string[] {
                      //          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                      //      },
                      //      DisplayCondition = APP_ORGANIC_NEW.GROUP_PRODUCTION,
                      //},

                   //new FormSection()
                   //{
                   //     Section = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION",
                   //     SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                   //     Type = SectionType.Form,
                   //     ShowOnSpecificApps = true,
                   //     AppSystemNames = new string[] {
                   //         AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                   //     },
                   //     DisplayCondition = APP_ORGANIC_NEW.GROUP_PRODUCTION,
                   //},
                 
                  // END APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC

                  // APP_HEALTH_SPECIFICALLY
                  
                   new FormSection()
                   {
                        Section = "APP_HEALTH_CANCEL_SPECIFICALLY_DETAIL",
                        SectionGroup = "APP_HEALTH_CANCEL_SPECIFICALLY",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_HEALTH_CANCEL,
                        },
                   },

                   new FormSection()
                   {
                        Section = "APP_HEALTH_RENEW_PLACE_DETIAL",
                        SectionGroup = "APP_HEALTH_RENEW_SPECIFICALLY",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_HEALTH_RENEW,
                        },
                   },

                   new FormSection()
                   {
                        Section = "APP_HEALTH_EDIT_PLACE_DETIAL",
                        SectionGroup = "APP_HEALTH_EDIT_SPECIFICALLY",
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] {
                            AppSystemNameTextConst.APP_HEALTH_EDIT,
                        },
                   },
                   //new FormSection()
                   //{
                   //     Section = "APP_HEALTH_RENEW_OPERATION_SECTION",
                   //     SectionGroup = "APP_HEALTH_RENEW_SPECIFICALLY",
                   //     Type = SectionType.Form,
                   //     ShowOnSpecificApps = true,
                   //     AppSystemNames = new string[] {
                   //         AppSystemNameTextConst.APP_HEALTH_RENEW,
                   //     },
                   //},
                  // END APP_HEALTH_SPECIFICALLY

                 // ข้อมูลกิจการ
                 //new FormSection()
                 //{
                 //   Section = "APP_FRT_CANCEL_001_SECTION1",
                 //   SectionGroup = "APP_FRT_CANCEL_001",
                 //   Type = SectionType.Form,
                 //   TemplateName = "APP_Danger_Food_INFO",   // ข้อมูลกิจการ
                 //   Ordering = 1,
                 //},
                 ////ข้อมูลการบอกเลิกการดำเนินกิจการที่เป็นอันตรายต่อสุขภาพ
                 //new FormSection()
                 //{
                 //   Section = "APP_FRT_CANCEL_001_SECTION2",
                 //   SectionGroup = "APP_FRT_CANCEL_001",
                 //   Type = SectionType.Form,
                 //   TemplateName = "APP_Danger_Food_CANCEL",
                 //   Ordering = 11,
                 //},

            //,new FormSection() //ข้อมูลกิจการ หัวข้อสีฟ้า
            //{
            //   Section = "APP_FRT_NEW_001_SECTION",
            //   SectionGroup = "APP_FRT_NEW_001",
            //   Type = SectionType.Form,
            //   HideSectionHeader = true,
            //   ShowOnSpecificApps = true,
            //   AppSystemNames = new string[] {
            //       AppSystemNameTextConst.APP_FRT_NEW_001
            //   },
            //   Ordering = 3
            //}

        };
            db.InsertMany(items);
        }

        private static void InitForRestaurant(IMongoCollection<FormSection> db)
        {
            FormSection[] items = new FormSection[]
            {
                // แยกไฟล์เฉพาะใบ
                //new FormSection() {
                //    Section = "SELL_ALCOHOL_INFO",
                //    SectionGroup = "APP_SELL_ALCOHOL",
                //    Type = SectionType.Form,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_SELL_ALCOHOL,
                //    },
                //    Ordering = 1
                //},
                new FormSection() {
                    Section = "INFORMATION_STORE",
                    SectionGroup = "INFORMATION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                    .ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_EDIT,
                        AppSystemNameTextConst.APP_HEALTH_RENEW,
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                        AppSystemNameTextConst.APP_HEALTH_CANCEL
                    })
                    .ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_PERMISSION)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL,
                        AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL,
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS,
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_CANCEL,
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL,
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_CANCEL),
                    Ordering = 10,
                },
                
                new FormSection() {
                    Section = "REQUESTOR_INFORMATION__HEADER",
                    SectionGroup = "INFORMATION",
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                    .ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE).ConcatItems(AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                    .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                    .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                        AppSystemNameTextConst.APP_CLINIC_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                    }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    })
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    })
                    .ConcatItems(AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW)
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_RENEW,
                        AppSystemNameTextConst.APP_HEALTH_EDIT,
                        AppSystemNameTextConst.APP_HEALTH_CANCEL,
                    })
                    .ConcatItems(AppSystemNameTextConst.APP_SEC_EDIT)
                    .ConcatItems(AppSystemNameTextConst.ALL_APP_SEC_CANCEL)
                    .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW),
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(new string[] 
                    {
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_RENEW,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW
                    }),
                    Ordering = 5,
                },
                new FormSection() {
                    Section = "REQUESTOR_INFORMATION__HEADER",
                    SectionGroup = "INFORMATION",
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                 //  HideSectionHeader = true,
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.App_Restaurant_Retail_All
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_NEW_001).ConcatItems(AppSystemNameTextConst.APP_FRT_RENEW_001)
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                    .ConcatItems(AppSystemNameTextConst.APP_SOFTWARE_HOUSE)
                    .ConcatItems(AppSystemNameTextConst.APP_FRT_EDIT_001).ConcatItems(AppSystemNameTextConst.APP_FRT_CANCEL_001)
                    .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW).ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                    .ConcatItems(AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL).ConcatItems(new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                        AppSystemNameTextConst.APP_CLINIC_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                    }).ConcatItems(AppSystemNameTextConst.APP_FACTORY_TYPE2)
                    .ConcatItems(new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW
                    })
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_RENEW,
                        AppSystemNameTextConst.APP_HEALTH_EDIT,
                        AppSystemNameTextConst.APP_HEALTH_CANCEL,
                    })
                    .ConcatItems(AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_BUSINESS)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_OPERATION)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_SPLIT_RENEW)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_SPLIT_RENEW)
                    .ConcatItems(AppSystemNameTextConst.APP_HOSPITAL_EDIT_SPLIT)
                    .ConcatItems(AppSystemNameTextConst.APP_CLINIC_EDIT_SPLIT),
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                        AppSystemNameTextConst.APP_DIRECT_SELL_EDIT,
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL,
                        AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_RENEW,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW),
                    Ordering = 5
                },
                new FormSection() { Section = "REQUESTOR_INFORMATION", SectionGroup = "INFORMATION", Type = SectionType.ArrayOfForms,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic }, Ordering = 5,
                    EmptyDataMessage = "REQUESTOR_INFORMATION_EMPTY", AddButtonText = "REQUESTOR_INFORMATION_BTN_ADD",
                    SubmitButtonText = "REQUESTOR_INFORMATION_BTN_SUBMIT", ArrayRequiredAtLeast = true, NumberRequiredAtLeast = 1 ,
                    ShowOnSpecificApps = true,
                    AppSystemNames = (new List<string>(AppSystemNameTextConst.App_Restaurant_All))
                        .Where(x=>
                            x != AppSystemNameTextConst.APP_SELL_ALCOHOL &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_GE_200 &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_LT_200 &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_LT_200_CANCEL &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_GE_200_RENEW &&
                            x != AppSystemNameTextConst.APP_SELL_FOOD_GE_200_CANCEL &&
                            !AppSystemNameTextConst.APP_DANGER_ALL.Contains(x)
                        )
                        .ToArray()
                        .ConcatItems(
                            //AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD,
                            //AppSystemNameTextConst.APP_SELL_ANIMAL,
                            //AppSystemNameTextConst.APP_SELL_CARCASS
                        ),
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(
                        AppSystemNameTextConst.APP_DIRECT_SELL_EDIT,
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_EDIT,
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL,
                        AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL),
                    HideSectionHeader = true,
                },
                //APP_SELL_FOOD
                new FormSection() { Section = "SELL_FOOD_INFORMATION", SectionGroup = "APP_SELL_FOOD",
                    Type = SectionType.Form,
                    Ordering = 1 },

                new FormSection() {
                    Section = "SELL_FOOD_FOOD_WORKER_INFO",
                    SectionGroup = "APP_SELL_FOOD",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "SELL_FOOD_FOOD_WORKER_INFO_EMPTY",
                    AddButtonText = "SELL_FOOD_FOOD_WORKER_INFO_ADD",
                    SubmitButtonText = "SELL_FOOD_FOOD_WORKER_INFO_SUBMIT",
                    //ArrayRequiredAtLeast = true,
                    //NumberRequiredAtLeast = 1,
                },
                new FormSection() {
                    Section = "APP_SELL_FOOD_RENEW_INFO",
                    SectionGroup = "APP_SELL_FOOD_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_RENEW,
                    },
                    Ordering = 1
                },
                 new FormSection() {
                    Section = "SELL_FOOD_FOOD_WORKER_INFO",
                    SectionGroup = "APP_SELL_FOOD_RENEW",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "SELL_FOOD_FOOD_WORKER_INFO_EMPTY",
                    AddButtonText = "SELL_FOOD_FOOD_WORKER_INFO_ADD",
                    SubmitButtonText = "SELL_FOOD_FOOD_WORKER_INFO_SUBMIT",
                    //ArrayRequiredAtLeast = true,
                    //NumberRequiredAtLeast = 1,
                },
                 new FormSection() {
                    Section = "SELL_FOOD_FOOD_WORKER_INFO",
                    SectionGroup = "APP_SELL_FOOD_RENEW_LT",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 3,
                    EmptyDataMessage = "SELL_FOOD_FOOD_WORKER_INFO_EMPTY",
                    AddButtonText = "SELL_FOOD_FOOD_WORKER_INFO_ADD",
                    SubmitButtonText = "SELL_FOOD_FOOD_WORKER_INFO_SUBMIT",
                    //ArrayRequiredAtLeast = true,
                    //NumberRequiredAtLeast = 1,
                },
                 new FormSection() {
                    Section = "SELL_FOOD_FOOD_WORKER_INFO",
                    SectionGroup = "APP_SELL_FOOD_EDIT",
                    Type = SectionType.ArrayOfForms,
                    Ordering = 2,
                    EmptyDataMessage = "SELL_FOOD_FOOD_WORKER_INFO_EMPTY",
                    AddButtonText = "SELL_FOOD_FOOD_WORKER_INFO_ADD",
                    SubmitButtonText = "SELL_FOOD_FOOD_WORKER_INFO_SUBMIT",
                    //ArrayRequiredAtLeast = true,
                    //NumberRequiredAtLeast = 1,
                },
                 new FormSection() {
                    Section = "APP_SELL_FOOD_RENEW_CONFIRM_SECTION",
                    SectionGroup = "APP_SELL_FOOD_RENEW",
                    Type = SectionType.Form,
                    HideSectionHeader = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_RENEW,
                    },
                    Ordering = 4
                },
                  new FormSection() {
                    Section = "APP_SELL_FOOD_CANCEL_INFO",
                    SectionGroup = "APP_SELL_FOOD_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_CANCEL,
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_CANCEL,
                    },
                    Ordering = 1
                },


                new FormSection() {
                    Section = "SELL_FOOD_FOOD_MANAGER_INFO",
                    SectionGroup = "APP_SELL_FOOD",
                    Type = SectionType.Form,
                    Ordering = 2,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
                    },
                },
                new FormSection() {
                    Section = "APP_SELL_FOOD_RENEW_LT_FEE",
                    SectionGroup = "APP_SELL_FOOD_RENEW_LT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW,
                    },
                    Ordering = 1
                 },
                new FormSection() {
                    Section = "APP_SELL_FOOD_RENEW_LT_SECTION",
                    SectionGroup = "APP_SELL_FOOD_RENEW_LT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW,
                    },
                    Ordering = 2
                 },
                new FormSection() {
                    Section = "APP_SELL_FOOD_RENEW_LT_MANAGER_INFO",
                    SectionGroup = "APP_SELL_FOOD_RENEW_LT",
                    Type = SectionType.Form,
                    Ordering = 3,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW,
                    },
                },

                #region INFORMATION

                new FormSection()
                {
                    Section = "INFORMATION_STORE_HAS_PROVINCE_ONLY",
                    SectionGroup = "INFORMATION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.ConcatItems(new string[]
                    {
                        //AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                        //AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_CANCEL

                    })
                    .ConcatItems(new string[]
                    {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL,
                    }),
                    HideOnSpecificApps = true,
                    HideAppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TAX_EDIT,
                        AppSystemNameTextConst.APP_TAX_CANCEL,
                    },
                    Ordering = 6,
                },


                #endregion

                new FormSection()
                {
                    Section = "INFORMATION_STORE_TAX_PROVINCE_ONLY",
                    SectionGroup = "INFORMATION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TAX_EDIT,
                        AppSystemNameTextConst.APP_TAX_CANCEL,
                    },
                    Ordering = 1,
                }
            };
            db.InsertMany(items);
        }

        private static void InitForBusinessBadHealth(IMongoCollection<FormSection> db)
        {
            #region FORM SECTION DANGER GENERAL

            FormSection FORM_SECTION_BAD_HEALTH_MNG_INFO(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_MNG_INFO",
                    SectionGroup = foodSysName,
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "APP_DANGER_SECTION_MNG_INFO",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL),
                    Ordering = 4,
                    EmptyDataMessage = "MANAGER_EMPTY",
                    AddButtonText = "MANAGER_BTN_ADD",
                    SubmitButtonText = "MANAGER_BTN_SUBMIT",
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_GENERAL(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_GENERAL",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_DANGER_SECTION_GENERAL",
                    Ordering = 1,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_WORKER(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_WORKER",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_DANGER_SECTION_WORKER",
                    Ordering = 2,
                };
            }

            #endregion

            #region FORM SECTION DANGER FOOD

            FormSection FORM_SECTION_BAD_HEALTH_INFO(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_INFO",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_Danger_Food_INFO",
                    Ordering = 1,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_MATERIAL(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_MATERIAL",
                    SectionGroup = foodSysName,
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "APP_Danger_Food_MATERIAL",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL),
                    Ordering = 4,
                    EmptyDataMessage = "MATERIAL_EMPTY",
                    AddButtonText = "MATERIAL_BTN_ADD",
                    SubmitButtonText = "MATERIAL_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_PRODUCT(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_PRODUCT",
                    SectionGroup = foodSysName,
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "APP_Danger_Food_PRODUCT",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL),
                    Ordering = 5,
                    EmptyDataMessage = "PRODUCT_EMPTY",
                    AddButtonText = "PRODUCT_BTN_ADD",
                    SubmitButtonText = "PRODUCT_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_OTHER_OUTPUT",
                    SectionGroup = foodSysName,
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "APP_Danger_Food_OTHER_OUTPUT",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL),
                    Ordering = 6,
                    EmptyDataMessage = "OTHER_OUTPUT_EMPTY",
                    AddButtonText = "OTHER_OUTPUT_BTN_ADD",
                    SubmitButtonText = "OTHER_OUTPUT_BTN_SUBMIT",
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_MACHINE(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_MACHINE",
                    SectionGroup = foodSysName,
                    Type = SectionType.ArrayOfForms,
                    TemplateName = "APP_Danger_Food_MACHINE",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL),
                    Ordering = 7,
                    EmptyDataMessage = "MACHINE_EMPTY",
                    AddButtonText = "MACHINE_BTN_ADD",
                    SubmitButtonText = "MACHINE_BTN_SUBMIT",
                    NumberRequiredAtLeast = 1,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_EDIT(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_EDIT",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_Danger_Food_EDIT",
                    HideOnSpecificApps = true,
                    HideAppSystemNames = AppSystemNameTextConst.APP_Danger_Food.ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL).ConcatItems(
                        AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL).ConcatItems(
                        AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL).ConcatItems(
                        AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW).ConcatItems(
                        AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL),
                    Ordering = 8,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_POLLUTION(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_POLLUTION",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_Danger_Food_POLLUTION",
                    Ordering = 9,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_INFO_TAIL(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_INFO_TAIL",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    Ordering = 10,
                    HideSectionHeader = true,
                };
            }

            FormSection FORM_SECTION_BAD_HEALTH_CANCEL(string food = "", string foodSysName = "")
            {
                return new FormSection()
                {
                    Section = foodSysName + "_CANCEL",
                    SectionGroup = foodSysName,
                    Type = SectionType.Form,
                    TemplateName = "APP_Danger_Food_CANCEL",
                    Ordering = 11,
                };
            }

            #endregion

            var items = new List<FormSection>
            {

            };

            for (int i = 0; i < AppSystemNameTextConst.APP_Danger_Food.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_Danger_Food[i];
                var foodSysName = AppSystemNameTextConst.APP_Danger_Food_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            //for loop danger
            //for (int i = 0; i < 1; i++)
            //{
            //    var food = AppSystemNameTextConst.APP_FRT_NEW_001;
            //    var foodSysName = "APP_FRT_NEW_001";
            //    items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
            //    items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            //}


            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_EDIT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_CANCEL(food, foodSysName));
            }

            #region SERVICES

            #region RESTAURANT

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_CANCEL(food, foodSysName));
            }

            #endregion

            #region HOTEL

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_CANCEL(food, foodSysName));
            }

            #endregion

            #region FITNESS

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL_SYSTEM_NAMES[i];
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_CANCEL(food, foodSysName));
            }

            #endregion

            #endregion

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_MNG_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }
            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                //items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }
            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_MNG_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }
            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                //items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_MNG_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }
            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                //items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            #region CARCARE

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_MNG_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW_SystemNames[i];
                items.Add(FORM_SECTION_BAD_HEALTH_GENERAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_WORKER(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MATERIAL(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_PRODUCT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_OTHER_OUTPUT(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_MACHINE(food, foodSysName));
                //items.Add(FORM_SECTION_BAD_HEALTH_POLLUTION(food, foodSysName));
                items.Add(FORM_SECTION_BAD_HEALTH_INFO_TAIL(food, foodSysName));
            }

            #endregion

            db.InsertMany(items);
        }

        private static void InitForSellProductInPublic(IMongoCollection<FormSection> db)
        {
            FormSection[] items = new FormSection[]
            {
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC,
                    Type = SectionType.Form,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA
                    },
                    Ordering = 1
                }
                ,new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC,
                    Type = SectionType.ArrayOfForms,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA,
                    },
                    EmptyDataMessage = string.Format("{0}_EMPTY",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    AddButtonText = string.Format("{0}_BTN_ADD",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    SubmitButtonText = string.Format("{0}_BTN_SUBMIT",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    Ordering = 2
                },
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION_2,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC,
                    HideSectionHeader = true,
                    Type = SectionType.Form,
                    Ordering = 3,
                },
                // สณ.5
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION_RENEW,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_RENEW,
                    Type = SectionType.Form,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW
                    },
                    //TemplateName = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION,
                    Ordering = 4
                },
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_RENEW,
                    Type = SectionType.ArrayOfForms,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW,
                    },
                    EmptyDataMessage = string.Format("{0}_EMPTY",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    AddButtonText = string.Format("{0}_BTN_ADD",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    SubmitButtonText = string.Format("{0}_BTN_SUBMIT",FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION),
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    //TemplateName = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION,
                    Ordering = 5
                },
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION_2_RENEW,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_RENEW,
                    HideSectionHeader = true,
                    Type = SectionType.Form,
                    //TemplateName = FormSectionTextConstant.SELL_PRODUCT_ASSISTANT_INFORMATION_2,
                    Ordering = 6,
                },
                // สณ.9
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION_EDIT,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_EDIT,
                    Type = SectionType.Form,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT
                    },
                    TemplateName = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION,
                    Ordering = 7
                },
                // สณ.8
                new FormSection() {
                    Section = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION_CANCEL,
                    SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_CANCEL,
                    Type = SectionType.Form,
                    IdentityTypes = new UserTypeEnum[] {UserTypeEnum.Juristic, UserTypeEnum.Citizen},
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL
                    },
                    TemplateName = FormSectionTextConstant.SELL_PRODUCT_IN_PUBLIC_INFORMATION,
                    Ordering = 10
                }
            };
            db.InsertMany(items);
        }

        private static void InitForVeterinaryHospital(IMongoCollection<FormSection> db)
        {
            FormSection[] items = new FormSection[]
            {
               new FormSection() {
                    Section = "APP_ANIMAL_BUILD_SECTION",
                    SectionGroup = "APP_ANIMAL_BUILD",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    },
                    Ordering = 1,
                },
                new FormSection() {
                    Section = "APP_ANIMAL_BUILD_OWNER_INFO",
                    SectionGroup = "APP_ANIMAL_BUILD",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    },
                    Ordering = 2,
                },
                new FormSection() {
                    Section = "APP_ANIMAL_BUILD_SERVICE",
                    SectionGroup = "APP_ANIMAL_BUILD",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    },
                    Ordering = 3,
                },
                new FormSection() {
                    Section = "APP_ANIMAL_BUILD_LOCATION",
                    SectionGroup = "APP_ANIMAL_BUILD",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    },
                    Ordering = 4,
                },
                new FormSection() {
                    Section = "APP_ANIMAL_BUILD_RENEW_SECTION",
                    SectionGroup = "APP_ANIMAL_BUILD_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD_RENEW,
                    },
                    Ordering = 1,
                },
                new FormSection() {
                    Section = "APP_LAW_OFFICE_GENERAL_OWNER",
                    SectionGroup = "INFORMATION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE,
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
                    Ordering = 12,
                },
                new FormSection() {
                    Section = "APP_LAW_OFFICE_GENERAL_MANAGER",
                    SectionGroup = "INFORMATION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE,
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
                    Ordering = 13,
                },
                //new FormSection() {
                //    Section = "APP_LAW_OFFICE_GENERAL_LOCATION",
                //    SectionGroup = "INFORMATION",
                //    Info = "APP_LAW_OFFICE_GENERAL_LOCATION_INFO",
                //    Type = SectionType.Form,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_LAW_OFFICE,
                //    },
                //    Ordering = 13,
                //},
                new FormSection() {
                    Section = "APP_SECURITIES_BUSINESS_EDIT_SECTION",
                    SectionGroup = "APP_SECURITIES_BUSINESS_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_EDIT,
                    },
                    Ordering = 1,
                },
                new FormSection() {
                    Section = "APP_SECURITIES_BUSINESS_CANCEL_SECTION",
                    SectionGroup = "APP_SECURITIES_BUSINESS_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_CANCEL,
                    },
                    Ordering = 1,
                },
            };

            db.InsertMany(items);
        }

        public static FormSection[] GetSections(string sectionGroup, string[] appSystemNames, UserTypeEnum? idenType = null)
        {
            var db = MongoFactory.GetFormSectionCollection();
            FormSection[] sections = null;

            var query = db.AsQueryable().Where(o => o.SectionGroup == sectionGroup).OrderBy(o => o.Ordering).AsQueryable();
            //var query = db.AsQueryable().Where(o => o.SectionGroup == sectionGroup && (!o.ShowOnSpecificApps || (o.ShowOnSpecificApps && o.AppSystemNames.Any(p => appSystemNames.Contains(p))))).OrderBy(o => o.Ordering).AsQueryable();

            if (idenType != null)
            {
                sections = query.Where(o => o.IdentityTypes == null || o.IdentityTypes.Count() == 0 || o.IdentityTypes.Contains(idenType.Value))
                    .ToArray();
            }
            else
            {
                sections = query.ToArray();
            }

            if (appSystemNames != null)
            {
                foreach (var sec in sections)
                {
                    sections = sections.Where(o => (!o.HideOnSpecificApps || (o.HideOnSpecificApps && appSystemNames.Any(p => !o.HideAppSystemNames.Contains(p)))) &&
                    (!o.ShowOnSpecificApps || (o.ShowOnSpecificApps && o.AppSystemNames.Any(p => appSystemNames.Contains(p))))).ToArray();
                }
            }

            return sections;
        }

        #region Support Form Revision
        public int RevisionCode { get; set; } = 0;
        public string RevisionName { get; set; } = "Before Config Revision";

        public static FormSection[] GetSectionsByRevision(string sectionGroup, int revisionCode, string[] appSystemNames, UserTypeEnum? idenType = null)
        {
            // Use same logic as GetSections excepts that we look in FormSectionRevision instead.
            var db = MongoFactory.GetFormSectionRevisionCollection();
            FormSection[] sections = null;

            var query = db.AsQueryable().Where(o => o.RevisionCode == revisionCode && o.SectionGroup == sectionGroup).OrderBy(o => o.Ordering).AsQueryable();
            if (idenType != null)
            {
                sections = query.Where(o => o.IdentityTypes == null || o.IdentityTypes.Count() == 0 || o.IdentityTypes.Contains(idenType.Value))
                    .ToArray();
            }
            else
            {
                sections = query.ToArray();
            }

            if (appSystemNames != null)
            {
                foreach (var sec in sections)
                {
                    sections = sections.Where(o => (!o.HideOnSpecificApps || (o.HideOnSpecificApps && appSystemNames.Any(p => !o.HideAppSystemNames.Contains(p)))) &&
                    (!o.ShowOnSpecificApps || (o.ShowOnSpecificApps && o.AppSystemNames.Any(p => appSystemNames.Contains(p))))).ToArray();
                }
            }

            return sections;
        }

        #endregion

        /// <summary>
        /// For reference with PageID in excel sheet
        /// </summary>
        public string PageID { get; set; }

        public string SectionGroup { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SectionType Type { get; set; }

        [BsonRepresentation(BsonType.String)]
        public UserTypeEnum[] IdentityTypes { get; set; }

        public string Description { get; set; }

        public int Ordering { get; set; }

        public bool ShowOnSpecificApps { get; set; }

        public string[] AppSystemNames { get; set; }

        public bool NoHeader { get; set; }

        public bool HideOnSpecificApps { get; set; }

        public string[] HideAppSystemNames { get; set; }

        public bool HideOnConfirmationPage { get; set; }

        public string Info { get; set; }

        public bool DefaultShowInfo { get; set; }

        public string ResourceName { get; set; }

        /// <summary>
        /// Section level validation rules. Currently support only JSExpression validation.
        /// </summary>
        public FormValidationRule[] ValidationRules { get; set; }

        #region For ArrayOfForms type
        public string EmptyDataMessage { get; set; }

        public string AddButtonText { get; set; }

        public string SubmitButtonText { get; set; }

        public bool ArrayRequiredAtLeast { get; set; }

        public int NumberRequiredAtLeast { get; set; }

        public string TemplateName { get; set; }

        public string ArrayOfFormNotice { get; set; }
        #endregion

        #region [Partial View]
        public bool IsPartialView { get; set; }
        public bool PartialReadonly { get; set; }
        public string[] PartialApps { get; set; }
        #endregion

        #region [Additional Section]
        public AdditionalSection AdditionalSection { get; set; }
        #endregion

        #region Display options

        public bool HideSectionHeader { get; set; }
        public FormControlDisplayCondition DisplayCondition { get; set; }


        public bool GetArrayData { get; set; }

        public bool UneditableData { get; set; }
        #endregion

        #region Disable options
        public FormControlDisableCondition DisableCondition { get; set; }
        #endregion

        public bool isCompatibleWithDataKey(string key, UserTypeEnum identityType)
        {
            if (Type == SectionType.ArrayOfForms)
            {
                var keyWithoutIndex = RemoveIndexFromDataKey(key);
                return (keyWithoutIndex == Section + "_TOTAL") ||
                    (keyWithoutIndex == "ARR_ITEM_ID") ||
                    (keyWithoutIndex == "ARR_IDX") ||
                    (keyWithoutIndex == "CUSREQ") ||
                    (keyWithoutIndex == "IS_EDIT");

            }
            return false;
        }

        static Regex reg = new Regex("^(.*)_[0-9]+$");
        public string RemoveIndexFromDataKey(string key)
        {
            if (Type == SectionType.Form)
                return key;
            if (reg.IsMatch(key))
            {
                return reg.Match(key).Groups[1].Value;
            }
            else
            {
                return key;
            }
        }
    }

    public class AdditionalSection
    {
        public bool ShowOnTrackingPage { get; set; }
    }
}
