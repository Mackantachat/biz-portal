using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION
    {
        private static FormRadioGroup RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC()
        {
            return new FormRadioGroup()
            {
                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                RadioButtons = new FormRadioButton[]
                {
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.OWNED },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                },
            };
        }

        private static FormRadioGroup RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN()
        {
            return new FormRadioGroup()
            {
                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                RadioButtons = new FormRadioButton[]
                {
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.OWNED_CITIZEN },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                },
            };
        }

        private static FormRadioGroup RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE()
        {
            return new FormRadioGroup()
            {
                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE_OPTION",
                RadioButtons = new FormRadioButton[]
                {
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.JURISTIC },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.CITIZEN, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.CITIZEN },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Government, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Government },
                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Royal, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Royal },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE()
        {
            return new FormControlDisplayCondition
            {

                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT,
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT,
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                    },
                },
            };
        }
    }
}
