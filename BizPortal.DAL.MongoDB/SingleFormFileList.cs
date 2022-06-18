using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormFileList : Entity
    {
        public SingleFormFileList(bool required = true)
        {
            this.Required = required;
        }

        private static SingleFormFileConfig.ConditionConfig[] GetProductProducerPermitConditions
        {
            get
            {
                var conditions = new SingleFormFileConfig.ConditionConfig[]
                {
                    new SingleFormFileConfig.ConditionConfig
                    {
                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                        {
                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_DIRECT_SELL),
                            new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_SECTION",
                                                DataName = "DIRECT_SELL_SELECT_PRODUCT_DIRECT_SELL_PRODUCT_FOOD",
                                            },
                                            ExpectedValue = "true",
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_SECTION",
                                                DataName = "DIRECT_SELL_SELECT_PRODUCT_DIRECT_SELL_PRODUCT_COSMETIC",
                                            },
                                            ExpectedValue = "true",
                                        },
                                    },
                                }
                            }
                        },
                    },
                    new SingleFormFileConfig.ConditionConfig
                    {
                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                        {
                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_DIRECT_MARKETING),
                            new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_SECTION",
                                                DataName = "DIRECT_MARKETING_SELECT_PRODUCT_DIRECT_SELL_PRODUCT_FOOD",
                                            },
                                            ExpectedValue = "true",
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_SECTION",
                                                DataName = "DIRECT_MARKETING_SELECT_PRODUCT_DIRECT_SELL_PRODUCT_COSMETIC",
                                            },
                                            ExpectedValue = "true",
                                        },
                                    },
                                }
                            }
                        },
                    },
                };
                return conditions;
            }

        }
        private static SingleFormFileConfig.ConditionConfig ShowIfJuristic
        {
            get
            {
                var showIfJuristic = new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        CheckIfSectionExist = true,
                        Data = new SingleFormDataItem
                        {
                            SectionName = "COMMITTEE_INFORMATION",
                        },

                    }
                };
                return showIfJuristic;
            }
        }
        private static SingleFormFileConfig.ConditionConfig ShowIfCitizen
        {
            get
            {
                var showIfCitizen = new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        CheckIfSectionExist = true,
                        Data = new SingleFormDataItem
                        {
                            SectionName = "CITIZEN_GENERAL_INFORMATION_HEADER",
                        },

                    }
                };
                return showIfCitizen;
            }
        }
        private static SingleFormFileConfig.ConditionConfig ShowIfEngineer
        {
            get
            {
                var showIfEngineer = new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        CheckIfSectionExist = true,
                        Data = new SingleFormDataItem
                        {
                            SectionName = "APP_BUILDING_G1_SUPERVISE_ENGINEER",
                        },

                    }
                };
                return showIfEngineer;
            }
        }
        private static SingleFormFileConfig.ConditionConfig ShowIfElectronicCommercial
        {
            get
            {
                return new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        Data = new SingleFormDataItem
                        {
                            SectionName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION",
                            DataName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE_OPTION",
                        },
                        ExpectedValue = "ELECTRONIC",
                    }
                };
            }
        }
        private static SingleFormFileConfig GetAuthorizeCommitteeCondition(bool isOptional = false)
        {
            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                DisplayCondition = ShowIfJuristic,
                IsOptional = isOptional,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                {
                    BindToSection = true,
                    SectionName = "COMMITTEE_INFORMATION",
                    DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                    FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION" },
                    FilterDataItemText = "yes",
                }
            };
            return Config;
        }
        private static SingleFormFileConfig GetAllCommitteeCondition(bool isOptional = false)
        {
            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                DisplayCondition = ShowIfJuristic,
                IsOptional = isOptional,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                {
                    BindToSection = true,
                    SectionName = "COMMITTEE_INFORMATION",
                    DataItems = new SingleFormDataItem[]
                    {
                        new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                        new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                        new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                        new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION", BindDataValueToResourceText = true },
                    },
                }
            };

            return Config;
        }
        private static SingleFormFileConfig.ConditionConfig GetBuildingCondition(string buildingType)
        {
            var ShowOnBuilding_Juristic = new SingleFormFileConfig.ConditionConfig
            {

                Condition = new SingleFormFileConfig.ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionName = "INFORMATION_STORE",
                        DataName = "INFORMATION_STORE_BUILDING_TYPE_OPTION",
                    },
                    ExpectedValue = buildingType,
                }
            };
            var ShowOnBuilding_Citizen = new SingleFormFileConfig.ConditionConfig
            {

                Condition = new SingleFormFileConfig.ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionName = "INFORMATION_STORE",
                        DataName = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION",
                    },
                    ExpectedValue = buildingType,
                }
            };
            return new SingleFormFileConfig.ConditionConfig
            {
                IsOr = true,
                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                {
                        ShowOnBuilding_Citizen,
                        ShowOnBuilding_Juristic
                }
            };
        }

        private static SingleFormFileConfig.ConditionConfig GetBuildingConditionCustom(string sectionName, string dataNameJuristuc, string dataNameCitizen, string buildingType)
        {
            var ShowOnBuilding_Juristic = new SingleFormFileConfig.ConditionConfig
            {

                Condition = new SingleFormFileConfig.ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionName = sectionName,
                        DataName = dataNameJuristuc,
                    },
                    ExpectedValue = buildingType,
                }
            };
            var ShowOnBuilding_Citizen = new SingleFormFileConfig.ConditionConfig
            {

                Condition = new SingleFormFileConfig.ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionName = sectionName,
                        DataName = dataNameCitizen,
                    },
                    ExpectedValue = buildingType,
                }
            };
            return new SingleFormFileConfig.ConditionConfig
            {
                IsOr = true,
                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                {
                        ShowOnBuilding_Citizen,
                        ShowOnBuilding_Juristic
                }
            };
        }

        private static SingleFormFileConfig G1DesEnConfig()
        {
            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                IsOptional = false,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                {
                    BindToSection = true,
                    SectionName = "APP_BUILDING_G1_DESIGN_ENGINEER",
                    DataItems = new SingleFormDataItem[]
                    {
                        new SingleFormDataItem(){ DataName = "DROPDOWN_APP_BUILDING_G1_DESIGN_ENGINEER_TITLE_TEXT" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ENGINEER_FIRSTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ENGINEER_LASTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ENGINEER_LICENSE_ID" }
                    }
                }
            };
            return Config;
        }
        private static SingleFormFileConfig G1DesArchConfig()
        {
            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                IsOptional = false,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                {
                    BindToSection = true,
                    SectionName = "APP_BUILDING_G1_DESIGN_ARCHITECT",
                    DataItems = new SingleFormDataItem[]
                    {
                        new SingleFormDataItem(){ DataName = "DROPDOWN_APP_BUILDING_G1_DESIGN_ARCHITECT_TITLE_TEXT" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ARCHITECT_FIRSTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ARCHITECT_LASTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_DESIGN_ARCHITECT_LICENSE_ID" }
                    }
                }
            };
            return Config;
        }
        private static SingleFormFileConfig G1SupEnConfig()
        {
            var ShowOnSupervise_Engineer = new SingleFormFileConfig.ConditionConfig
            {
                Condition = new SingleFormFileConfig.ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionType = SingleFormDataItem.SectionTypeEnum.ArraySection,
                        SectionName = "APP_BUILDING_G1_SUPERVISE_ENGINEER",
                        DataName = "APP_BUILDING_G1_SUPERVISE_ENGINEER_KIND_OF_PERSON_OPTION",
                    },
                    ExpectedValue = "1",
                },
            };

            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                IsOptional = false,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                {
                    BindToSection = true,
                    SectionName = "APP_BUILDING_G1_SUPERVISE_ENGINEER",
                    DataItems = new SingleFormDataItem[]
                    {
                        new SingleFormDataItem() { DataName = "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT" },
                        new SingleFormDataItem() { DataName = "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_FIRSTNAME" },
                        new SingleFormDataItem() { DataName = "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LASTNAME" },
                        new SingleFormDataItem() { DataName = "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LICENSE_ID" }
                    }
                },
                DisplayCondition = ShowOnSupervise_Engineer
            };
            return Config;
        }
        private static SingleFormFileConfig G1SupArchConfig()
        {
            SingleFormFileConfig Config = new SingleFormFileConfig
            {
                IsOptional = false,
                ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                {
                    BindToSection = true,
                    SectionName = "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                    DataItems = new SingleFormDataItem[]
                    {
                        new SingleFormDataItem(){ DataName = "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE_TEXT" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_SUPERVISE_ARCHITECT_FIRSTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_SUPERVISE_ARCHITECT_LASTNAME" },
                        new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_SUPERVISE_ARCHITECT_LICENSE_ID" }
                    }
                }
            };
            return Config;
        }

        public static void Init()
        {

            var db = MongoFactory.GetSingleFormFileListCollection();
            if (db.AsQueryable().Count() == 0)
            {
                SingleFormFileList[] items = new SingleFormFileList[]
                {
                     // ACCOUNT_SERVICE
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_DOC_RENEW_UPLOAD_1",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/acc/สวช5_2.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_DOC_RENEW_UPLOAD_2",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/acc/สวช5_3.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_DOC_CANCEL_UPLOAD_1",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/acc/สวบช.5_1_ยกเลิก.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_DOC_EDIT_UPLOAD_1",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/acc/สวบช5_1.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_DOC_EDIT_UPLOAD_2",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/acc/สวบช5_4.pdf",
                    },
                    // END ACCOUNT_SERVICE

                    #region First Doc Clinic Renew
                    //new SingleFormFileList()
                    //{
                    //    FileName = "APP_CLINIC_RENEW_BUSSINESS_REQ_DOC", FileGroup = "FORM_SECTION",
                    //    Required = false,
                    //    DocFormUrl = "http://mrd-hss.moph.go.th/sp/sp11.pdf",
                    //    Note = "กรณีขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล"
                    //},
                    //new SingleFormFileList()
                    //{
                    //    FileName = "APP_CLINIC_RENEW_PROCESS_REQ_DOC", FileGroup = "FORM_SECTION",
                    //    Required = false,
                    //    DocFormUrl = "http://mrd-hss.moph.go.th/sp/sp20.pdf",
                    //    Note = "กรณีต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล"
                    //},
                    #endregion

                    #region First Doc Clinic Edit
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_EDIT_BUSSINESS_REQ_DOC", FileGroup = "FORM_SECTION",
                        Required = false,
                        DocFormUrl = "http://mrd-hss.moph.go.th/sp/sp10.pdf",
                        Note = "กรณีขอเปลี่ยนแปลงรายการใบอนุญาตให้ประกอบกิจการสถานพยาบาล"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_EDIT_PROCESS_REQ_DOC", FileGroup = "FORM_SECTION",
                        Required = false,
                        DocFormUrl = "http://mrd-hss.moph.go.th/sp/sp18.pdf",
                        Note = "กรณีขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล"
                    },
                    #endregion

                    #region First Doc Clinic Cancel
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_CANCEL_BUSSINESS_REQ_DOC", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://mrd-hss.moph.go.th/sp/sp17.pdf",
                    },
                    #endregion

                    #region First Doc Energy Production
                    new SingleFormFileList()
                    {
                        FileName = "APP_ENERGY_PRODUCTION_REQ_DOC", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "https://www.dede.go.th/article_attach/pk1_100816.pdf",
                    },
                    #endregion

                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_LICENSE_CANCEL_DOC", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://www.nbtc.go.th/getattachment/Business/commu/radio/licensing/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B9%8C%E0%B8%A1%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD/%E0%B8%AB%E0%B8%99%E0%B8%87%E0%B8%AA%E0%B8%AD%E0%B8%A2%E0%B8%81%E0%B9%80%E0%B8%A5%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB-%E0%B8%84%E0%B8%B2-%E0%B9%81%E0%B8%A5%E0%B8%B0%E0%B8%84%E0%B8%B2%E0%B9%80%E0%B8%9E%E0%B8%AD%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8B%E0%B8%AD%E0%B8%A1%E0%B9%81%E0%B8%8B%E0%B8%A1/%E0%B9%81%E0%B8%9A%E0%B8%9A-%E0%B8%84%E0%B8%97-29.pdf.aspx",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "BUILDING_CONTROL_CANCEL_DOC_EDIT", FileGroup = "FORM_SECTION",
                        Required = true,
                        Note = "เปลี่ยนผู้คุมงานใหม่",
                        DocFormUrl = "http://www.bangkok.go.th/upload/user/00000098/Form/pulic%20work/16.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_NORMAL_REQUEST_1_3",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/dopa/แบบคำขอแจ้งความประสงค์ ร.ร.1-3.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/bkk/อภ4.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/bkk/อภ9.pdf",
                    },
                    new SingleFormFileList(false)
                    {
                        FileName = "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE_OPTIONAL",
                        FileGroup = "FORM_SECTION",
                        DocFormUrl = "~/Uploads/apps/bkk/อภ9.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FORM_COMMERCIAL_REGISTRATION", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://www.dbd.go.th/download/downloads/01_tp/form_tp.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FORM_COMMERCIAL", FileGroup = "FORM_SECTION",
                        Note = "เฉพาะกรณีเปลี่ยนคำนำหน้านาม แขวง เขต การเปลี่ยนแปลงที่เกิดจากภาครัฐ ",
                        DocFormUrl = "http://www.dbd.go.th/download/downloads/01_tp/form_tp_request.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TAX_DETAIL", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://www.bangkok.go.th/upload/user/00000090/download/revenue/sign_list.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TAX_CANCEL", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://www.bangkok.go.th/upload/user/00000089/pdf/form/5_raidai/2.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FORM_CHANGE_PERMISSION", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://dcontrol.dld.go.th/dcontrol/images/%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20%E0%B8%9E%E0%B8%A8.2561%E0%B8%A5%E0%B8%B2%E0%B8%AA%E0%B8%94/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B8%A1/%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%A2%E0%B8%99%E0%B9%81%E0%B8%9B%E0%B8%A5%E0%B8%87%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B9%84%E0%B8%94%E0%B8%A3%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%8D%E0%B8%B2%E0%B8%95%20%E0%B8%AA%E0%B8%AA%2010-1.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FORM_CANCEL_ANIMAL_MED", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://dcontrol.dld.go.th/images/stories/document/Animal_Hospital/81-4.doc",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_LICENSE_CHANGE_OWNER", FileGroup = "FORM_SECTION",
                        Note = "กรณีแก้ไขผู้ดำเนินการ",
                        DocFormUrl = "http://dcontrol.dld.go.th/dcontrol/images/%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20%E0%B8%9E%E0%B8%A8.2561%E0%B8%A5%E0%B8%B2%E0%B8%AA%E0%B8%94/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B8%A1/%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%A2%E0%B8%99%E0%B8%9C%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%20%E0%B8%AA%E0%B8%AA%2010.pdf",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_LICENSE_EDIT_DOC", FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://nbtc.go.th/getattachment/Business/commu/radio/licensing/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B9%8C%E0%B8%A1%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD/%E0%B8%84%E0%B8%97-1_%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95-%E0%B8%84%E0%B9%89%E0%B8%B2/%E0%B9%81%E0%B8%9A%E0%B8%9A-%E0%B8%84%E0%B8%97-1.pdf.aspx",
                    },
                    //ORGANIC
                     new SingleFormFileList()
                    {
                        FileName = "ORGANIC_PLANT_PRODUCTION_REQUEST_FORM_DOC",   //แบบคำขอใบรับรองแหล่งผลิตพืชอินทรีย์ (สำหรับแปลงเดียว/ รายเดียว) (F-51.1) 
                        FileGroup = "FORM_SECTION",
                        Required = false,
                        DocFormUrl = "http://gap.doa.go.th/web_manual/doc/F/F51.1.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_PLANT_PRODUCTION_REQUEST_CER_DOC",   //แบบคำขอใบรับรองแหล่งผลิตพืชอินทรีย์ (สำหรับกลุ่ม) (F-53)
                        FileGroup = "FORM_SECTION",
                        Required = false,
                        DocFormUrl = "http://gap.doa.go.th/web_manual/doc/F/F53.pdf",
                    },
                     new SingleFormFileList()
                    {
                        FileName = "ORGANIC_PLANT_PRODUCTION_FORM_CANCEL",   //แบบคำขอยกเลิกการรับรอง (F-6)
                        FileGroup = "FORM_SECTION",
                        Required = true,
                        DocFormUrl = "http://gap.doa.go.th/web_manual/doc/F/F53.pdf",
                    },
                    //new SingleFormFileList()
                    //{
                    //    FileName = "APP_RADIO_LICENSE_CANCEL_DOC", FileGroup = "FORM_SECTION",
                    //    Required = true,
                    //    DocFormUrl = "http://www.nbtc.go.th/getattachment/Business/commu/radio/licensing/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B9%8C%E0%B8%A1%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD/%E0%B8%AB%E0%B8%99%E0%B8%87%E0%B8%AA%E0%B8%AD%E0%B8%A2%E0%B8%81%E0%B9%80%E0%B8%A5%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB-%E0%B8%84%E0%B8%B2-%E0%B9%81%E0%B8%A5%E0%B8%B0%E0%B8%84%E0%B8%B2%E0%B9%80%E0%B8%9E%E0%B8%AD%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8B%E0%B8%AD%E0%B8%A1%E0%B9%81%E0%B8%8B%E0%B8%A1/%E0%B9%81%E0%B8%9A%E0%B8%9A-%E0%B8%84%E0%B8%97-29.pdf.aspx",
                    //},
                    new SingleFormFileList() {
                        FileName = "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", FileGroup = "JURISTIC_INFORMATION",
                        PreDoc = true,
                        PreDocFileName = "หนังสือรับรองนิติบุคคล.pdf",
                        PreDocType = "application/pdf",
                        PreDocOrg = "กรมพัฒนาธุรกิจ",
                        //Note = "1.มีอายุไม่เกิน 3 เดือนนับถึงวันที่ยื่นคำขอ",
                        //Note_2 = "2.อัปโหลดอัตโนมัติและได้รับการยืนยันความถูกต้องแล้ว จากกรมพัฒนาธุรกิจ (ไม่ต้องอัปโหลดซ้ำ)",
                        //Note = "1.ต้องมีอายุไม่เกิน 6 เดือน",
                        //Note_2 = "2.ลงนามรับรองสำเนาถูกต้อง โดยกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคลทุกหน้า",
                        //Note_3 = "3.ต้องประทับตราบริษัททุกหน้า",
                        Config = new SingleFormFileConfig

                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "ID_CARD_COPY", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },                   
                    new SingleFormFileList() {
                        FileName = "ID_CARD_COPY_NON_PASSPORT", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "ID_CARD_COPY_RADIO_CANCEL", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList(false) {
                        FileName = "EMPLOYER_TEST",
                        FileGroup = "SSO_EMPLOYER"
                    },

                    new SingleFormFileList(false) {
                        FileName = "EMPLOYEE_TEST",
                        FileGroup = "SSO_EMPLOYEE",
                        Note = "สามารถอัปโหลดเป็น .zip ได้"
                    },
                   


                    #region Upload No condition

                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_ID_CARD_OPIONAL_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_COMMITTEE_FINGERPRINT_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note = "กรณีแก้ไขเปลี่ยนผู้แทนนิติบุคคล",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ ลงนามสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON_HOTEL",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_TAX_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ ลงนามสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้มอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "AUTHORIZATION_AUTHORIZEE_ID_CARD_TAX_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ ลงนามสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_ENERGY_NOT_PERMIT",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                        DocFormUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/%E0%B9%81%E0%B8%99%E0%B8%9A2.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_MENDATORY",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้มอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_ENERGY_NOT_PERMIT",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                        DocFormUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/%E0%B9%81%E0%B8%99%E0%B8%9A2.pdf",
                    },
                #endregion
                    
                    #region Direct sell edit Juristic
                    //new SingleFormFileList
                    //{
                    //    FileName = "DIRECT_SELL_EDIT_CERTIFICATION", FileGroup = "JURISTIC_INFORMATION", //หนังสือรับรองการจดทะเบียนนิติบุคคล
                    //    Required = true,
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            IsOr = true,
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }

                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }

                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //            }
                    //        }
                    //    }
                    //},
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CERTIFICATION_OLD", FileGroup = "JURISTIC_INFORMATION", //หนังสือรับรองการจดทะเบียนนิติบุคคล
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COMMITTEE_LIST", FileGroup = "JURISTIC_INFORMATION", //ตารางเปรียบเทียบรายชื่อกรรมการเก่า-ใหม่ (ถ้ามี)
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },

                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CRIME", FileGroup = "JURISTIC_INFORMATION", //หนังสือตรวจสอบประวัติอาชญากรรม ตามมาตรา 38/2
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CER_BOOK", FileGroup = "JURISTIC_INFORMATION", //หนังสือตรวจสอบประวัติอาชญากรรม ตามมาตรา 38/2
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                }
                            }
                        }
                    },
                    #endregion

                    #region Direct Sell Committee
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COMMITTEE_ID", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล
                        Required = true,
                        Note = "ลงนามสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },

                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                    BindToSection = true,
                                    SectionName = "COMMITTEE_INFORMATION",
                                    DataItems = new SingleFormDataItem[]
                                                {
                                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                                },
                                    FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION" },
                                    FilterDataItemText = "yes",
                            }
                        }
                    },

                    //new SingleFormFileList
                    //{
                    //    FileName = "DIRECT_SELL_EDIT_COMMITTEE_ID_OPTIONAL", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                    //    Required = false,
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            IsOr = true,
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //            }
                    //        }
                    //    }
                    //},
                    #endregion


                    #region HOTEL_FILE_SECTION
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_EDIT_AGREEMENT",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีเปลี่ยนแปลงประเภทโรงแรมให้มีสถานบริการในโรงแรม",
                        Required = false,

                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_AGREEMENT",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีมีสถานบริการในโรงแรม",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_HOTEL_SECTION",
                                                DataName = "DROPDOWN_APP_HOTEL_TYPE",
                                            },
                                            ExpectedValue = "03"
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_HOTEL_SECTION",
                                                DataName = "DROPDOWN_APP_HOTEL_TYPE",
                                            },
                                            ExpectedValue = "04"
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_ENVIRONMENT",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                        Note = "กรณีโรงแรมมีห้องพักตั้งแต่ 80 ห้องขึ้นไปหรือมีพื้นที่ใช้สอยตั้งแต่ 4,000 ตารางเมตรขึ้นไป",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_JURISTIC",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                        Note = "กรณีในหนังสือรับรองนิติบุคคลไม่ได้ระบุชื่อกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคลให้ชัดเจน",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_CHANGE_BUILDING",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note ="1.กรณีอาคารที่มาขออนุญาตเป็นอาคารที่ถูกเปลี่ยนให้เป็นโรงแรม ให้แนบเอกสารใบอ.5",
                        Note_2 = "2.กรณีอาคารที่มาขออนุญาตเป็นอาคารที่ถูกก่อสร้างมาเพื่อเป็นโรงแรม ให้แนบเอกสารใบอ.6",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_CERTIFICATE",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                        Note ="กรณีโรงแรมที่มีจำนวนห้องพักตั้งแต่ 80 ห้องขึ้นไป หรือโรงแรมที่เป็นอาคารสูงตั้งแต่ 23 เมตรขึ้นไป หรือโรงแรมที่เป็นอาคารขนาดใหญ่พิเศษที่มีพื้นที่ตั้งแต่ 10,000 ตารางเมตรขึ้นไป แล้วแต่กรณีตามกฎหมายว่าด้วยการควบคุมอาคาร ",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_LAW",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOTEL_SECTION",
                                        DataName = "APP_HOTEL_LOCATION_OPTION",
                                    },
                                    ExpectedValue = "2"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_DIAGRAM",
                        FileGroup = "HOTEL_FILE_SECTION",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_CERTIFICATE_LICENSE",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                        Note = "กรุณาอัปโหลดใบอนุญาตทั้งด้านหน้าและด้านหลัง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_BUILDING_MODIFY_CERTIFICATE",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีมีการดัดแปลงอาคารตามกฎหมายว่าด้วยควบคุมอาคาร (ดัดแปลงที่ส่งผลกระทบต่อโครงสร้างโรงแรม)",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_FLOOR_PLAN_BEFORE_AND_AFTER_CHANGE_TYPE",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "สำหรับกรณีเปลี่ยนประเภทโรงแรมที่กระทบโครงสร้างโรงแรม จะต้องเป็นแผนผังทั้งก่อนและหลังเปลี่ยนประเภทโรงแรม",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_PLAN",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณีแก้ไขเพิ่มหรือลดจำนวนห้องพักโรงแรมที่กระทบโครงสร้างโรงแรม จะต้องเป็นแผนผังทั้งก่อนและหลังเพิ่มหรือลดจำนวนห้องพัก",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_AGREEMENT_OTHER_OWNER_HOTEL_SAME_NAME",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีชื่อโรงแรมซ้ำหรือพ้องกับชื่อโรงแรมอื่น ที่ได้รับอนุญาตแล้ว",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_OTHER_OWNER_ID_CARD",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีเปลี่ยนชื่อและชื่อโรงแรมซ้ำหรือพ้องกับชื่อโรงแรมอื่น ที่ได้รับอนุญาตแล้ว",
                        Required = false,
                    },

                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_REASON_FOR_OUT_OF_BUSINESS",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_HOTEL_FLOOR_PLAN_BEFORE_AND_AFTER_CHANGE_AMOUNT_ROOM",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีที่เพิ่มหรือลดจำนวนห้องพักที่กระทบโครงสร้างโรงแรม จะต้องเป็นแผนผังทั้งก่อนและหลังเพิ่มหรือลดจำนวนห้องพัก",
                        Required = false,
                    },

                    #endregion

                    //new SingleFormFileList() {
                    //    FileName = "JURISTIC_SHARE_HOLDER_LIST",
                    //    FileGroup = "JURISTIC_INFORMATION",
                    //    PreDoc = true,
                    //    PreDocFileName = "บัญชีรายชื่อผู้ถือหุ้น.pdf",
                    //    PreDocType = "application/pdf",
                    //    PreDocOrg = "กรมพัฒนาธุรกิจ",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = ShowIfJuristic,
                    //    }
                    //},

                    //new SingleFormFileList() {
                    //    FileName = "JURISTIC_MEMORANDUM", FileGroup = "JURISTIC_INFORMATION",
                    //    PreDoc = true,
                    //    PreDocFileName = "หนังสือบริคณห์สนธิ.pdf",
                    //    PreDocType = "application/pdf",
                    //    PreDocOrg = "กรมพัฒนาธุรกิจ",
                    //    Note = "1.ลงนามรับรองสำเนาถูกต้อง โดยกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคลทุกหน้า",
                    //    Note_2 = "2.ต้องประทับตราบริษัททุกหน้า",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = ShowIfJuristic,
                    //    }
                    //},

                    new SingleFormFileList() {
                        FileName = "JURISTIC_OBJECTIVE", FileGroup = "JURISTIC_INFORMATION",

                        Note = "1.ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Note_2 = "2.ต้องประทับตราบริษัท",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },


                    new SingleFormFileList() {
                        FileName = "JURISTIC_SHARE_HOLDER_LIST", FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },

                    new SingleFormFileList() {
                        FileName = "JURISTIC_MEMORANDUM", FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },

                    new SingleFormFileList(false) {
                        FileName = "CITIZEN_RENAME_MARRIAGE_DOC", FileGroup = "CITIZEN_INFORMATION",
                        Note = "CITIZEN_RENAME_MARRIAGE_DOC_NOTE",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            DisplayCondition = ShowIfCitizen,
                        }
                    },


                    
                    /*
                     บัตรประชาชน แบบ Predoc
                    new SingleFormFileList() {
                        FileName = "ID_CARD_COPY", FileGroup = "CITIZEN_INFORMATION",
                        PreDoc = true,
                        PreDocFileName = "ข้อมูลทะเบียนราษฎร์.pdf",
                        PreDocType = "application/pdf",
                        PreDocOrg = "กรมการปกครอง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    /**/
                    new SingleFormFileList() {
                        FileName = "CITIZEN_HOUSEHOLD_REGISTRATION_COPY", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_HOUSEHOLD_REGISTRATION_COPY_RADIO_CANCEL", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "DIRECT_MARKETING_CITIZEN_HOUSEHOLD", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfCitizen,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_OTHER",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_MEDICAL_CERT", FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "HOTEL_CITIZEN_MEDICAL_CERT", FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Required = false
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_BKK_FOOD_HEALTH_CERT", FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                            IsOptional = true,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_PHOTO", FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    //new SingleFormFileList() {
                    //    FileName = "VAT_REGISTRATION", FileGroup = "JURISTIC_INFORMATION",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = ShowIfJuristic,
                    //    }
                    //},
                    new SingleFormFileList() {
                        FileName = "VAT_REGISTRATION_FRONTIS", FileGroup = "OTHER_DOC",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Not = true,
                                                IsOr = true,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD),
                                                    SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_ANIMAL),
                                                    SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_CARCASS),
                                                }
                                            },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_BUILDING_TYPE_OPTION",
                                                            },
                                                            ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_ANIMAL_FOOD_COMPANY_CERT_STORE_ADDRESS_OPTION",
                                                            },
                                                            ExpectedValue = "no",
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_ANIMAL_FOOD_WHICH_DOC_SHOW_YOUR_STORE_ADDRESS_OPTION",
                                                            },
                                                            ExpectedValue = "vat_registration",
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                        }
                    },
                    new SingleFormFileList(false) {
                        FileName = "HOUSEHOLD_REGISTRATION_COPY", FileGroup = "ADDRESS_INFORMATION"
                    },
                    // SSO
                    new SingleFormFileList(false) { FileName = "COMPANY_PHOTOS", FileGroup = "SSO_EMPLOYER", Note = "สามารถอัปโหลดเป็น .zip ได้", Required = true },
                    new SingleFormFileList(false) { FileName = "COMMITTEE_IDENTITY", FileGroup = "SSO_EMPLOYER", Note = "สามารถอัปโหลดเป็น .zip ได้", Required = true },
                    new SingleFormFileList(false) { FileName = "DISABILITY_EMPLOYEE_IDENTITY", FileGroup = "SSO_EMPLOYEE", Note = "เฉพาะกรณีลูกจ้างที่พิการ / สามารถอัปโหลดเป็น .zip ได้" },
                    new SingleFormFileList(false) { FileName = "FOREIGNER_EMPLOYEE_IDENTITY", FileGroup = "SSO_EMPLOYEE", Note = "เฉพาะกรณีลูกจ้างที่เป็นคนต่างด้าว / สามารถอัปโหลดเป็น .zip ได้" },

                    new SingleFormFileList() {
                        FileName = "VAT_REGISTRATION",
                        FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "VAT_REGISTRATION_CARD_REQUIRED",
                        FileGroup = "OTHER_DOC",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_SELL_CARD_SECTION",
                                        DataName = "SELL_CARD_AREA_OPTION",
                                    },
                                    ExpectedValue = "SELL_CARD_AREA_OPTION_HASVAT",
                                }
                            },
                        },
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "VAT_REGISTRATION_CARD_OPTIONAL",
                        FileGroup = "OTHER_DOC",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Not = true,
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_SELL_CARD_SECTION",
                                        DataName = "SELL_CARD_AREA_OPTION",
                                    },
                                    ExpectedValue = "SELL_CARD_AREA_OPTION_HASVAT",
                                }
                            },
                        },
                        Required = false,
                    },
                    #region [APP_BUILDING_R6]
                    
                    new SingleFormFileList()
                    {
                        FileName = "SUPERVISE_ENGINEER_CONFIRM_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",

                    },
                    #endregion

                    #region [APP_BUILDING_G1 + APP_BUILDING_R6]
                    new SingleFormFileList()
                    {
                        FileName = "APPLICANT_ID_CARD_COPY",
                        FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CONSTRUCTION_BLUEPRINT",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        FileSize = new FileSizeConfig()
                        {
                            MaxFileSize = "10mb"
                        },
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50, 
                            },
                        },
                        FileIsEnableUrl = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CALCULATION_PLAN",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Required = false,
                        FileSize = new FileSizeConfig()
                        {
                            MaxFileSize = "10mb"
                        },
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    //หนังสือแสดงความเป็นตัวแทนเจ้าของอาคาร R1
                    new SingleFormFileList()
                    {
                        FileName = "BUILDING_OWNER_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION", 
                        Required = false,
                        //Config = new SingleFormFileConfig
                        //{
                        //    ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                        //    {
                        //        AllowMultipleEqualToSectionItem = true,
                        //        AllowMultipleEqualToSectionItemAdjust = 50,
                        //    },
                        //},
                    },
                    // หนังสือแสดงความยินยอมเจ้าของอาคาร
                    new SingleFormFileList()
                    {
                        FileName = "BUILDING_OWNERSHIP_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                //AddItemBtnText = "เพิ่มไฟล์"
                            },
                        },
                    },
                    //หนังสือแสดงความเป็นตัวแทนเจ้าของอาคาร R6
                    new SingleFormFileList() // R6
                    {
                        FileName = "BUILDING_OWNER_CONSENT_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Required = false
                    },
                    new SingleFormFileList() //R6
                    {
                        FileName = "BUILDING_A1LICENSE_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        PreDocApphook = true,  
                        Note = "กรุณาตรวจสอบข้อมูลในไฟล์ หากไม่ใช่ข้อมูลล่าสุด ให้ท่านแนบไฟล์ล่าสุดเข้าสู่ระบบ"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_DELEGATOR_DOC",
                        FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DESIGN_ENGINEER_CONSENT_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1DesEnConfig()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DESIGN_ENGINEER_PROFESSIONAL_LICENSE",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1DesEnConfig()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SUPERVISE_ENGINEER_CONSENT_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1SupEnConfig(),

                    },
                    new SingleFormFileList()
                    {
                        FileName = "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1SupEnConfig()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DESIGN_ARCHITECT_CONSENT_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1DesArchConfig()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DESIGN_ARCHITECT_PROFESSIONAL_LICENSE",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1DesArchConfig()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SUPERVISE_ARCHITECT_CONSENT_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1SupArchConfig(), 
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = G1SupArchConfig()
                    },
                    new SingleFormFileList() //R6
                    {
                        FileName = "SUPERVISE_ENGINEER_CONFIRM_DOC",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",

                    },
                    new SingleFormFileList()
                    {
                        FileName = "TITLE_DEED_COPY",
                        FileGroup = "CONSTRUCTION_SITE_INFORMATION",
                        Config = new SingleFormFileConfig()
                        {
                            IsOptional = false,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                            {
                                BindToSection = true,
                                SectionName = "APP_BUILDING_G1_TITLE_DEED",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem(){ DataName = "DROPDOWN_APP_BUILDING_G1_TITLE_DEED_ID_TYPE_TEXT" },
                                    new SingleFormDataItem(){ DataName = "APP_BUILDING_G1_TITLE_DEED_ID" },
                                }
                            }
                        }
                    },
                    #endregion

                    new SingleFormFileList() {
                        FileName = "ID_CARD",
                        FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Required = true
                    },
                    new SingleFormFileList() {
                        FileName = "HOUSE_REGISTARTION",
                        FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Note = "ลงนามสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList() {
                        FileName = "TRAVEL_ID",
                        FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Note = "กรณีเป็นชาวต่างชาติ",
                        Required = false
                    },

                    new SingleFormFileList() {
                        FileName = "MEDICINE_SMART_CARD_ID",
                        FileGroup = "CITIZEN_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Required = false,
                        Note = "กรณีผู้ดำเนินการเป็นแพทย์ ลงนามสำเนาถูกต้อง",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_BUSINESS_EDIT_DOCA",
                        FileGroup = "APP_CLINIC_BUSINESS_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_BUSINESS_EDIT_DOCB",
                        FileGroup = "APP_CLINIC_BUSINESS_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_BUSINESS_EDIT_DOCC",
                        FileGroup = "APP_CLINIC_BUSINESS_EDIT",
                        Note ="กรณีที่มีการเปลี่ยนชื่อนามสกุล/ชื่อนิติบุคคลของผู้รับอนุญาต",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_BUSINESS_EDIT_DOCD",
                        FileGroup = "APP_CLINIC_BUSINESS_EDIT",
                        Note ="กรณีที่มีการเปลียนเลขที่ตั้ง ชื่อถนน ตำบล หรือแขวง อำเภอหรือเขต จังหวัด",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_BUSINESS_EDIT_DOCE",
                        FileGroup = "APP_CLINIC_BUSINESS_EDIT",
                        Note ="กรณีที่มีการเปลียนชื่อสถานพยาบาล",
                        Required = false
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCA",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCB",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCC",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCD",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCE",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCF",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = false,
                        Note = "กรณีเป็นคลินิกเฉพาะทาง"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_DOCG",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT",
                        Required = false,
                        Note = "กรณีเป็นชาวต่างชาติ"
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCA",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCB",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = false,
                        Note= "กรณีที่มีการเปลี่ยนชื่อสถานพยาบาล"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCC",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCD",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อเลขที่ตั้ง ชื่อถนน ตำบล หรือแขวง อำเภอหรือเขต จังหวัด"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCE",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อนามสกุลของผู้รับอนุญาต"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_EDIT_B_DOCF",
                        FileGroup = "APP_CLINIC_OPERATION_EDIT_B",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_EDIT_DOCA",
                        FileGroup = "APP_HOSPITAL_BUSINESS_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_EDIT_DOCB",
                        FileGroup = "APP_HOSPITAL_BUSINESS_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_EDIT_DOCC",
                        FileGroup = "APP_HOSPITAL_BUSINESS_EDIT",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อนามสกุล/ชื่อนิติบุคคลของผู้รับอนุญาต"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_EDIT_DOCD",
                        FileGroup = "APP_HOSPITAL_BUSINESS_EDIT",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อเลขที่ตั้ง ชื่อถนน ตำบล หรือแขวง อำเภอหรือเขต จังหวัด"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_EDIT_DOCE",
                        FileGroup = "APP_HOSPITAL_BUSINESS_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCA",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCB",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCC",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCD",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCE",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_DOCF",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_B_DOCA",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_B_DOCB",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_B_DOCC",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT_B",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อนามสกุลของผู้รับอนุญาต"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_B_DOCD",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT_B",
                        Required = false,
                        Note = "กรณีที่มีการเปลี่ยนชื่อเลขที่ตั้ง ชื่อถนน ตำบล หรือแขวง อำเภอหรือเขต จังหวัด"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_OPERATION_EDIT_B_DOCE",
                        FileGroup = "APP_HOSPITAL_OPERATION_EDIT_B",
                        Required = true,
                    },
                };

                db.InsertMany(items);
                var ShowOnRequestor_NomineeJuristic = new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        Data = new SingleFormDataItem
                        {
                            SectionName = "REQUESTOR_INFORMATION__HEADER",
                            DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                        },
                        ExpectedValue = "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE",
                    },
                };
                var ShowOnRequestor_NomineeCitizen = new SingleFormFileConfig.ConditionConfig
                {
                    Condition = new SingleFormFileConfig.ConditionItem
                    {
                        Data = new SingleFormDataItem
                        {
                            SectionName = "REQUESTOR_INFORMATION__HEADER",
                            DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                        },
                        ExpectedValue = "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE",
                    },
                };
                var ShowOnRequestor_Nominee = new SingleFormFileConfig.ConditionConfig
                {
                    IsOr = true,
                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    {
                        ShowOnRequestor_NomineeJuristic,
                        ShowOnRequestor_NomineeCitizen,
                    }
                };

                var restaurantItems = new SingleFormFileList[] {
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_ID_CARD",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        //Note = "1.ลงนามรับรองสำเนาถูกต้อง",
                        //Note_2 = "2.ประทับตราบริษัท (ถ้ามี)",
                        Config = GetAuthorizeCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_ALL_COMMITTEE_ID_CARD",
                        FileGroup = "JURISTIC_ALL_COMMITTEE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        //Note = "1.ลงนามรับรองสำเนาถูกต้อง",
                        //Note_2 = "2.ประทับตราบริษัท (ถ้ามี)",
                        Config = GetAllCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Config = GetAuthorizeCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_ALL_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "JURISTIC_ALL_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Config = GetAllCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_MEDICAL_CERT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = GetAuthorizeCommitteeCondition(),
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOTEL_JURISTIC_COMMITTEE_MEDICAL_CERT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = GetAuthorizeCommitteeCondition(),
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_JURISTIC_CERT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note = "ออกให้ไม่เกิน 6 เดือนนับถึงวันที่ยื่นคำขอใบอนุญาต",
                        Note_2 = "ต้องมีตราประทับจากหน่วยงาน/คลินิกที่รับรอง",
                        Config = GetAuthorizeCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_ALL_COMMITTEE_MEDICAL_CERT",
                        FileGroup = "JURISTIC_ALL_COMMITTEE_FILE_SECTION",
                        Note = "ออกให้ไม่เกิน 6 เดือนนับถึงวันที่ยื่นคำขอใบอนุญาต",
                        Note_2 = "ต้องมีตราประทับจากหน่วยงาน/คลินิกที่รับรอง",
                        Config = GetAllCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_ALL_COMMITTEE_PROPERTY_17",
                        FileGroup = "JURISTIC_ALL_COMMITTEE_FILE_SECTION",
                        Config = GetAllCommitteeCondition()
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_HOTEL_EDIT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note = "ลงนามสำเนาถูกต้อง",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "COMMITTEE_WORK_PERMIT_NO_CON",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note = "ลงนามสำเนาถูกต้อง",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_WORKPERMIT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION" },
                                FilterDataItemText = "foreigner",
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "GENERAL_INFORMATION",
                                                DataName = "JURISTIC_MAJOR_SHARE_HOLDER_NATIONALITY_OPTION",
                                            },
                                            ExpectedValue = "foreigner",
                                        }
                                    },
                                    ShowIfJuristic,
                                }
                            },
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION" },
                                FilterDataItemText = "foreigner",
                            },
                            //DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            //{
                            //    IsOr = true,
                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                            //    {
                            //        new SingleFormFileConfig.ConditionConfig
                            //        {
                            //            Condition = new SingleFormFileConfig.ConditionItem
                            //            {
                            //                Data = new SingleFormDataItem
                            //                {
                            //                    SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                            //                    DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                            //                },
                            //                ExpectedValue = "true",
                            //            }
                            //        },
                            //    }
                            //}
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_COMMITTEE_FINGERPRINT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        //Note = "ระบุชื่อกรรมการผู้มีอำนาจลงนาม",
                        Config = GetAuthorizeCommitteeCondition(),
                    },

                    new SingleFormFileList()
                    {
                        FileName = "HOTEL_DOC_COMMITTEE_FINGERPRINT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        //Note = "ระบุชื่อกรรมการผู้มีอำนาจลงนาม",
                        Config = GetAuthorizeCommitteeCondition(),
                        Required = false
                    },


                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                },
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้มอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfJuristic,
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfJuristic,
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig()
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้มอบอำนาจ",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "ต้องระบุอำนาจหน้าที่ของผู้รับมอบอำนาจอย่างชัดเจน",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeCitizen,
                        }
                    },

                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeCitizen,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            },
                        }
                    },

                    new SingleFormFileList()
                    {
                        FileName = "CANCEL_CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfCitizen,
                                }
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfJuristic,
                                }
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Required = false,
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowOnRequestor_Nominee,
                                    ShowIfCitizen,
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Required = false,
                        Note =   "กรณีรับมอบอำนาจ พร้อมลงนามรับรองสำเนาถูกต้องโดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_MENDATORY", FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                }
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        FileGroup = "JURISTIC_AUTHORIZATION_FILE_SECTION",
                        Note = "ต้องระบุอำนาจหน้าที่ของผู้รับมอบอำนาจอย่างชัดเจน",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowOnRequestor_NomineeJuristic,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                                AllowMultipleMinItem = 1
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "SELL_ANIMAL_FOOD_OPERATOR_WORKPERMIT", FileGroup = "SELL_ANIMAL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_SELL_ANIMAL_FOOD_OPERATOR_INFO",
                                        DataName = "APP_SELL_ANIMAL_FOOD_OPERATOR_NATIONALITY_OPTION",
                                    },
                                    ExpectedValue = "foreigner",
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_ANIMAL_FOOD_OPERATOR_ID_CARD_COPY", FileGroup = "SELL_ANIMAL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_SELL_ANIMAL_FOOD_OPERATOR_HEADER",
                                        DataName = "AUTHORIZE_COMMITTEE_OPERATE_BY_HIMSELF_OPTION",
                                    },
                                    ExpectedValue = "no",
                                }
                            },
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_ANIMAL_FOOD", FileGroup = "SELL_ANIMAL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_SELL_ANIMAL_FOOD_OPERATOR_HEADER",
                                                DataName = "AUTHORIZE_COMMITTEE_OPERATE_BY_HIMSELF_OPTION",
                                            },
                                            ExpectedValue = "no",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "COMMITTEE_INFORMATION_SELL_ANIMAL_FOOD",
                                                DataName = "MORE_THAN_ONE_AUTHORIZE_COMMITTEE_OPTION",
                                            },
                                            ExpectedValue = "more_than_one",
                                        }
                                    },
                                }
                            }
                        },
                        DocFormUrl = "http://afvc.dld.go.th/index.php/2016-04-12-04-46-53/func-startdown/20/",
                    },


                    new SingleFormFileList()
                    {
                        FileName = "JURISTIC_REGISTRATION_TYPE_PARTNERSHIP",
                        FileGroup = "JURISTIC_INFORMATION",
                        Required = false,
                        Note =   "กรณีเป็นห้างหุ้นส่วนสามัญจดทะเบียนหรือห้างหุ้นส่วนจำกัด",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList() {
                        FileName = "LIST_OF_SHAREHOLDERS_5_JURISTIC_OPTIONAL",  //บัญชีรายชื่อผู้ถือหุ้น (บอจ.5) 
                        FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                        Required = false
                    },
                    new SingleFormFileList() {
                        FileName = "MEMORANDUM_2_JURISTIC_OPTIONAL",  //หนังสือบริคณห์สนธิ (บอจ.2) 
                        FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                        Required = false
                    },
                    new SingleFormFileList() {
                        FileName = "REGISTRATION_LIST_3_JURISTIC_OPTIONAL",  //รายการจดทะเบียนจัดตั้ง (บอจ.3)
                        FileGroup = "JURISTIC_INFORMATION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                        Required = false
                    },

                    //new SingleFormFileList
                    //{

                    //    FileName = "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", FileGroup = "JURISTIC_INFORMATION",
                    //    Note = "1.ต้องมีอายุไม่เกิน 6 เดือน",
                    //    Note_2 = "2.ลงนามรับรองสำเนาถูกต้อง โดยกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคลทุกหน้า",
                    //    Note_3 = "3.ต้องประทับตราบริษัททุกหน้า",

                    //},
                     //DIRECT_SELL_REGISTER_REFERENCE หนังสือยินยอมให้เช่าอาคารสถานที่
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_REGISTER_REFERENCE", FileGroup = "INFORMATION_STORE_FILE_SECTION",

                        Config = new SingleFormFileConfig
                        {

                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OTHER),
                                    new SingleFormFileConfig.ConditionConfig
                                {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                    {
                                        Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                    },
                                        ExpectedValue = "true"
                                    },
                                },
                                    new SingleFormFileConfig.ConditionConfig
                                {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                    {
                                        Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                    },
                                        ExpectedValue = "true"
                                    },
                                }


                                },

                            },
                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    Condition = new SingleFormFileConfig.ConditionItem
                                            //    {
                                            //        Data = new SingleFormDataItem
                                            //        {
                                            //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                            //            DataName = "DIRECT_SELL_CHANGE_TYPE",
                                            //        },
                                            //        ExpectedValue = "DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            //    },
                                            //},
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DIRECT_MARKETING_PRODUCT_RECEIVE_PERMIT",
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DIRECT_MARKETING_PERMIT_BEHAVIOR",
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                    },


                     new SingleFormFileList()
                    {   //เอกสารเกี่ยวกับสินค้า จำแนกตามประเภทของสินค้า
                        FileName = "DIRECT_MARKETING_PRODUCT_TYPE", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Note = "เช่น  กรณี สินค้าประเภทอาหาร ให้แนบใบอนุญาตนำ/สั่งอาหารเข้ามาในราชอาณาจักร ใบสำคัญการขึ้นทะเบียนตำรับอาหาร กรณีสินค้าประเภทเครื่องสำอาง ให้แนบ แบบแจ้งการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม เป็นต้น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                     // ขต2/1 เปลี่ยนแปลงเอกสารหลักฐาน ตลาดแบบตรง
                     
                    new SingleFormFileList()
                    {   //ตารางสรุปเปรียบเทียบแผนการจ่ายผลตอบแทนฉบับเดิม/ฉบับใหม่
                        FileName = "DIRECT_MARKETING_PRODUCT_DOC_CHENGE", FileGroup = "DIRECT_MARKETING_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                      new SingleFormFileList()
                    {   //ตารางสรุปเปรียบเทียบแผนการจ่ายผลตอบแทนฉบับเดิม/ฉบับใหม่
                        FileName = "DIRECT_MARKETING_PRODUCT__DOC_CHENGE_TYPE", FileGroup = "DIRECT_MARKETING_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },

                      //DIRECT_MARKETING_PRODUCT_DOC_CHENGE_SELL_TYPE เปลี่ยนแปลงวิธีการขายสินค้าบริการ ตลาดแบบตรง
                      new SingleFormFileList()
                    {   //ตารางสรุปเปรียบเทียบแผนการจ่ายผลตอบแทนฉบับเดิม/ฉบับใหม่
                        FileName = "DIRECT_MARKETING_PRODUCT_DOC_CHENGE_SELL_TYPE", FileGroup = "DIRECT_MARKETING_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },

                        new SingleFormFileList()
                    {   //ตารางสรุปเปรียบเทียบแผนการจ่ายผลตอบแทนฉบับเดิม/ฉบับใหม่
                        FileName = "DIRECT_MARKETING_PRODUCT_TABLE_BENEFIT", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },

                        new SingleFormFileList()
                        {   //แผนการจ่ายผลตอบแทน/คู่มือ (ฉบับเดิมและฉบับใหม่)
                        FileName = "DIRECT_MARKETING_PRODUCT_TABLE_BENEFIT_BOOK", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },


                        new SingleFormFileList()
                    {   //ขต1/1 เปลี่ยนแปลงเอกสารหลักฐาน ขายตรง
                        FileName = "DIRECT_SELL_PRODUCT_DOC_CHENGE", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },


                        new SingleFormFileList()
                    {
                        FileName = "DIRECT_SELL_PRODUCT__DOC_CHENGE_TYPE", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },


                        new SingleFormFileList()
                    {   //ขต1/1 เปลี่ยนแปลงแผนการจ่ายผลตอบแทน
                        FileName = "DIRECT_SELL_PRODUCT__DOC_CHENGE_BENEFIT", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        
                         // แยกไฟล์เฉพาะใบ
                        //new SingleFormFileList()
                        //{
                        //    FileName = "ALCOHOL_STORE", FileGroup = "ALCOHOL_FILE_SECTION",
                        //    Required = true,
                        //},

                    //การขออนุญาตก่อสร้างอาคาร
                        new SingleFormFileList()
                        {
                            FileName = "INFORMATION_STORE_PLAN_DOC",
                            FileGroup = "INFORMATION_STORE_FILE_SECTION",
                            Note = "กรณีแก้ไขเพิ่มหรือลดจำนวนห้องพักโรงแรมที่กระทบโครงสร้างโรงแรม จะต้องเป็นแผนผังทั้งก่อนและหลังเพิ่มหรือลดจำนวนห้องพัก",
                        },


                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CAL_PLAN", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                            Note = "รายการคำนวณโครงสร้างไฟฟ้า/ประปา/อากาศ/ระบายน้ำทิ้ง/อัคคีภัย เฉพาะอาคารสูงใหญ่พิเศษ",
                            Note_2 = " รายการคำนวณบำบัดน้ำเสีย อิง ตามประกาศกฎกระทรวง ฉบับที่ 51 (พ.ศ.2541) ตาม พรบ. ควบคุมอาคาร 2522",
                        },

                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_DESIGNER_DOC", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                            Note = "กรณีที่อาคารมีลักษณะขนาดอยู่ในประเภทเป็นวิชาชีพวิศวกรรมควบคุมหรือวิชาชีพสถาปัตยกรรมควบคุม แล้วแต่กรณี",

                        },

                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_DOC", FileGroup = "BUILDING_FILE_SECTION",
                            Required = false,
                            DocFormUrl = "http://www.tungpheesao.go.th/fileupload/download/tb_download_1_1.pdf",
                            Note = "กรณีหาผู้คุมงานได้แล้ว และกรณีที่เป็นอาคารมีลักษณะขนาดอยู่ในประเภทเป็นวิชาชีพวิศวกรรมควบคุมหรือวิชาชีพสถาปัตยกรรมควบคุมแล้วแต่กรณี และผู้ขออนุญาตระบุชื่อมาในคำขออนุญาตต้วย",
                        },

                         new SingleFormFileList()
                    {
                        FileName = "BUILDING_CONTROL_PERMIT_DOC", FileGroup = "BUILDING_FILE_SECTION",
                        Required = false,
                        Note = "กรณีที่เป็นอาคารมีลักษณะ ขนาด อยู่ในประเภทเป็นวิชาชีพวิศวกรรมควบคุมหรือวิชาชีพสถาปัตยกรรมควบคุม แล้วแต่กรณี และผู้ขออนุญาตระบุชื่อมาในคำขออนุญาตต้วย",
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },

                         new SingleFormFileList()
                    {
                        FileName = "BUILDING_AUTHORIZATION_ID_CARD", FileGroup = "BUILDING_FILE_SECTION",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },

                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_USING_AREA", FileGroup = "BUILDING_FILE_SECTION",
                            Required = false,

                        },

                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_DOC_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = false,
                            Note = "กรณีหาผู้คุมงานได้แล้ว และ",
                            Note_2 = "กรณีที่เป็นอาคารมีลักษณะ ขนาด อยู่ในประเภทเป็นวิชาชีพวิศวกรรมควบคุมหรือวิชาชีพสถาปัตยกรรมควบคุม แล้วแต่กรณี และผู้ขออนุญาตระบุชื่อมาในคำขออนุญาตต้วย",
                            DocFormUrl = "http://www.tungpheesao.go.th/fileupload/download/tb_download_1_1.pdf",
                        },
                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_PERMIT_DOC_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = false,
                            Note = "กรณีที่เป็นอาคารมีลักษณะ ขนาด อยู่ในประเภทเป็นวิชาชีพวิศวกรรมควบคุมหรือวิชาชีพสถาปัตยกรรมควบคุม แล้วแต่กรณี และผู้ขออนุญาตระบุชื่อมาในคำขออนุญาตต้วย",
                        },
                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_CHANGE_TYPE_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                        },
                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_CANCEL_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                            DocFormUrl = "http://www.bangkok.go.th/upload/user/00000076/AboutUs/yota/yota12.pdf",
                        },
                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_CANCEL_MANAGER_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                            DocFormUrl = "http://www.bangkok.go.th/upload/user/00000076/AboutUs/yota/yota10.pdf",
                        },
                        new SingleFormFileList()
                        {
                            FileName = "BUILDING_CONTROL_CANCEL_NEW_MANGER_EDIT", FileGroup = "BUILDING_FILE_SECTION",
                            Required = true,
                            DocFormUrl = "http://www.ieat.go.th/assets/uploads/cms/file/20180511163116191321206.pdf",
                        },


                    new SingleFormFileList()
                    {
                        FileName = "DOC_FINGERPRINT", FileGroup = "CITIZEN_INFORMATION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOTEL_DOC_FINGERPRINT", FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                        new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_DOCUMENT_CHANGE", FileGroup = "HOTEL_FILE_SECTION",
                        Note = "กรณีที่หลักฐาน หรือเอกสารที่ใช้ยื่นตอนขอใบอนุญาตประกอบธุรกิจโรงแรมมีการแก้ไขหรือเปลี่ยนแปลง ",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DOC_HOTEL_JURISTIC_RENEW",
                        FileGroup = "HOTEL_FILE_SECTION",
                        Required = false,
                        Note = "กรณีในหนังสือรับรองนิติบุคคลไม่ได้ระบุชื่อกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคลให้ชัดเจน",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = ShowIfJuristic,
                        },
                    },

                        new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_HOTEL", FileGroup = "INFORMATION_STORE_FILE_SECTION",

                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {

                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Not = true,
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD),
                                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_ANIMAL),
                                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_SELL_CARCASS),
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    ShowIfCitizen,
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION",
                                                            },
                                                            ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_ANIMAL_FOOD_COMMERCE_REGISTERED_OPTION",
                                                            },
                                                            ExpectedValue = "no",
                                                        }
                                                    }
                                                }
                                            },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    ShowIfJuristic,
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_BUILDING_TYPE_OPTION",
                                                            },
                                                            ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_ANIMAL_FOOD_COMPANY_CERT_STORE_ADDRESS_OPTION",
                                                            },
                                                            ExpectedValue = "no",
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "INFORMATION_STORE",
                                                                DataName = "INFORMATION_STORE_ANIMAL_FOOD_WHICH_DOC_SHOW_YOUR_STORE_ADDRESS_OPTION",
                                                            },
                                                            ExpectedValue = "other",
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_HOTEL",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณีเช่าอาคาร สถานที่ผู้อื่น โดยในสัญญาเช่าไม่ได้มีระบุว่าสามารถให้ใช้ดำเนินการประกอบธุรกิจโรงแรมได้",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),

                                },
                            },
                        },
                    },


                    new SingleFormFileList()
                    {
                        FileName = "DOC_COMMERCIAL_WEBSITE",
                        FileGroup = "COMMERCIAL_FILE_SECTION",
                        Note ="กรณีมีรายละเอียดเกี่ยวกับเว็บไซต์",
                        DocFormUrl = "http://www.dbd.go.th/download/downloads/01_tp/form_tp_website01.pdf",
                        Required = false,
                    },
                         new SingleFormFileList()
                    {
                        FileName = "DOC_COMMERCIAL_ALLOW", FileGroup = "COMMERCIAL_FILE_SECTION",
                        Note ="1.กรณีประกอบพาณิชยกิจการขาย หรือให้เช่า แผ่นซีดี แถบบันทึก วีดิทัศน์ แผ่นวีดิทัศน์ ดีวีดี หรือแผ่นวีดิทัศน์ระบบดิจิทัลเฉพาะที่เกี่ยวกับการบันเทิง",
                        //Note_2 = "2.พร้อมลงนามรับรองเอกสารถูกต้อง",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                         new SingleFormFileList()
                    {
                        FileName = "DOC_COMMERCIAL_BUDGET", FileGroup = "COMMERCIAL_FILE_SECTION",
                        Note ="กรณีประกอบพาณิชยกิจการค้าอัญมณีหรือเครื่องประดับซึ่งประดับด้วยอัญมณี",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                        {
                            FileName = "DOC_COMMERCIAL_STATEMENT", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="กรณีประกอบพาณิชยกิจการค้าอัญมณีหรือเครื่องประดับซึ่งประดับด้วยอัญมณี",
                            Note_2 = "Statement ย้อนหลัง 6 เดือน",
                            Required = false,
                                Config = new SingleFormFileConfig
                                {
                                    DisplayCondition = ShowIfJuristic,
                                },
                        },
                        new SingleFormFileList()
                        {
                            FileName = "DOC_COMMERCIAL_EDIT_STATEMENT", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="กรณีประกอบพาณิชยกิจการค้าอัญมณีหรือเครื่องประดับซึ่งประดับด้วยอัญมณี",
                            Note_2 = "Statement ย้อนหลัง 6 เดือน",
                            Required = false,
                        },
                         new SingleFormFileList()
                    {
                        FileName = "DOC_COMMERCIAL_AREA_PICTURE", FileGroup = "COMMERCIAL_FILE_SECTION",
                        Note ="กรณีที่ใช้สถานที่เดียวกันกับที่ที่เคยขอจดทะเบียนมาก่อนแล้ว",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                         new SingleFormFileList()
                    {
                        FileName = "DOC_COMMERCIAL_CHILD", FileGroup = "COMMERCIAL_FILE_SECTION",
                        Note ="กรณีเด็กยังไม่บรรลุนิติภาวะยื่นขอจดใบทะเบียนพาณิชย์",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                        {
                            FileName = "DOC_COMMERCIAL_DOC_TRADE", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="กรณีผู้ขอจดทะเบียนเป็นห้างหุ้นส่วนสามัญ คณะบุคคล และกิจการร่วมค้า",
                            Required = false,
                        },
                        new SingleFormFileList()
                        {
                            FileName = "COMMERCE_REGISTRATION_DOC_NO_CON", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfCitizen,
                            }
                        },
                        new SingleFormFileList()
                        {
                            FileName = "COMMERCE_REGISTRATION_CANCEL_DOC_NO_CON", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Required = true,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfCitizen,
                            }
                        },
                        new SingleFormFileList()
                        {
                            FileName = "COMMERCE_REGISTRATION_DOC_JURISTIC_NO_CON", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfJuristic,
                            }
                        },
                         new SingleFormFileList()
                        {
                            FileName = "COMMERCE_REGISTRATION_DOC_JURISTIC_CANCEL_NO_CON", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Required = true,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfJuristic,
                            }
                        },
                         new SingleFormFileList()
                         {
                            FileName = "COMMERCIAL_BOOK_NO_CON", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note = "1.กรณีผู้ขอจดทะเบียนเป็นห้างหุ้นส่วนสามัญ คณะบุคคล และกิจการร่วมค้า",
                            Note_2 = "2.พร้อมหุ้นส่วนทุกคนลงนามรับรองรับทราบ",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfCitizen,
                            }
                         },
                         new SingleFormFileList()
                        {
                            FileName = "COMMERCIAL_OWNER_DEATH_DOC", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="กรณีถึงแก่กรรม โดยให้ทายาทที่ยื่นคำขอเป็นผู้ลงนามรับรองสำเนาถูกต้อง",
                            Required = false,
                        },
                         new SingleFormFileList()
                        {
                            FileName = "COMMERCIAL_OWNER_DOC", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="ลงนามรับรองสำเนาถูกต้อง",
                            Required = false,
                        },
                         new SingleFormFileList()
                        {
                            FileName = "COMMERCIAL_CANCEL_DOC", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="ลงนามรับรองสำเนาถูกต้อง",
                            Required = true,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfJuristic,
                            }
                        },
                         new SingleFormFileList()
                        {
                            FileName = "COMMERCIAL_BOOK_CANCEL_DOC", FileGroup = "COMMERCIAL_FILE_SECTION",
                            Note ="กรณีผู้ขอจดทะเบียนเป็นห้างหุ้นส่วนสามัญ คณะบุคคล และกิจการร่วมค้า โดยให้หุ้นส่วนทุกคนลงนามรับรองรับทราบ",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = ShowIfCitizen,
                            }
                        },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TAX_BOARD_PICTURE", FileGroup = "TAX_FILE_SECTION",
                        Required = true,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TAX_BILL", FileGroup = "TAX_FILE_SECTION",
                        Required = false,
                    },
                          new SingleFormFileList()
                    {
                        FileName = "APP_TAX_ALLOW_DOC", FileGroup = "TAX_FILE_SECTION",
                        Note = "พร้อมบอกขนาดของป้ายด้วย",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_TAX_PICTURE", FileGroup = "TAX_FILE_SECTION",
                        Required = false,
                    },


                    new SingleFormFileList()
                    {
                        FileName = "APP_SELL_FOOD_EDIT_DOC",
                        FileGroup = "SELL_FOOD_FILE_SECTION",
                        Required = true,
                        Note =   "กรณีที่มีการแก้ไขเอกสารจากครั้งล่าสุดที่ได้ขออนุญาต เช่น แผนที่สังเขป แสดงสถานที่ตั้งของร้าน/สถานประกอบ สัญญาเช่า ใบรับรองแพทย์การตรวจโรคติดต่อ 9 โรคของผู้สัมผัสอาหาร แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกสถานประกอบการ แสดงให้เห็นถึงกระบวนการผลิต เป็นต้น",

                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_PERMIT", FileGroup = "ANIMAL_MED_SECTION",
                        Required = false,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_DOC", FileGroup = "ANIMAL_MED_SECTION",
                        Required = true,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_LICENSE_CON", FileGroup = "ANIMAL_MED_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    CheckIfSectionExist = true,
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ANIMAL_MED_DR_INFO",
                                    },
                                }
                            },
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_ANIMAL_MED_DR_INFO",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_TITLE" },
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                                },
                            }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_LICENSE_RENEW", FileGroup = "ANIMAL_MED_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50 // Unlimit items
                            }
                        }
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_PICTURE", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="หน้าตรง แต่งกายสุภาพ ไม่สวมหมวกหรือแว่นตาดำ ซึ่งถ่ายไว้ไม่เกิน 6 เดือน",
                        Required = true,
                        FileFilter = "jpg,png"
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },



                         new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_WORK_TIME", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="สามารถรวมเอกสารสแกนส่งแนบรวมได้",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/dld/รายชื่อเวลาทำการของผู้ประกอบวิชาชีพการสัตวแพทย์ทุกคน.pdf",
                        //DocFormUrl = "https://files.info.go.th/OPDC/Uploads/cguide_ex_form/B2FB19FE374529D3658197DA0657AB0C/1434105317/แผนที่แสดงที่ตั้งสถานพยาบาลสัตว์.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_ALLOW_DOC", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="สามารถรวมเอกสารสแกนส่งแนบรวมได้",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/dld/หนังสือแสดงความจำนงเป็นผู้ประกอบวิชาชีพการสัตวแพทย์ในสถานพยาบาลสัตว์ทุกคน.pdf",
                        //DocFormUrl = "https://files.info.go.th/OPDC/Uploads/cguide_ex_form/B2FB19FE374529D3658197DA0657AB0C/1434105317/แผนที่แสดงที่ตั้งสถานพยาบาลสัตว์.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_ALLOW_DOC_CON", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="สามารถรวมเอกสารสแกนส่งแนบรวมได้",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/dld/หนังสือแสดงความจำนงเป็นผู้ประกอบวิชาชีพการสัตวแพทย์ในสถานพยาบาลสัตว์ทุกคน.pdf",
                        //DocFormUrl = "https://files.info.go.th/OPDC/Uploads/cguide_ex_form/B2FB19FE374529D3658197DA0657AB0C/1434105317/แผนที่แสดงที่ตั้งสถานพยาบาลสัตว์.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    CheckIfSectionExist = true,
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ANIMAL_MED_DR_INFO",
                                    },
                                }
                            },
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_ANIMAL_MED_DR_INFO",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_TITLE" },
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                                },
                            }
                        },
                    },
                         new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_COPY_ID", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="กรณีสาขาเฉพาะทาง",
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                         new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_TRASH", FileGroup = "ANIMAL_MED_SECTION",
                        Note ="(ถ้ามี)",
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                         new SingleFormFileList() {
                        FileName = "MED_ANIMAL_CITIZEN_CERT", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ออกให้ไม่เกิน 6 เดือนนับถึงวันที่ยื่นคำขอใบอนุญาต",
                        Note_2 = "ต้องมีตราประทับจากหน่วยงาน/คลินิกที่รับรอง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Required = false,
                    },
                         new SingleFormFileList() {
                        FileName = "MED_ANIMAL_CITIZEN_CERT_SPA_ONLY", FileGroup = "CITIZEN_INFORMATION",
                        Note = "ใบรับรองแพทย์ระบุไม่เป็นโรคที่รัฐมนตรีประกาศกำหนดในราชกิจจานุเบกษาและอายุใบรับรองแพทย์ไม่เกิน 30 วันนับถึงวันที่ยื่น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_LICENSE_DOC", FileGroup = "ANIMAL_LICENSE_SECTION",
                        Note = "จะต้องไม่หมดอายุ พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                        new SingleFormFileList()
                    {
                        FileName = "ANIMAL_PICTURE", FileGroup = "ANIMAL_LICENSE_SECTION",
                        Note ="หน้าตรง แต่งกายสุภาพ ไม่สวมหมวกหรือแว่นตาดำ ซึ่งถ่ายไว้ไม่เกิน 6 เดือน",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "ANIMAL_DATE", FileGroup = "ANIMAL_LICENSE_SECTION",
                         DocFormUrl = "~/Uploads/apps/dld/date_time_form_animal_hospital.doc",
                        Required = true,
                        
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_COPY_DOC", FileGroup = "ANIMAL_LICENSE_SECTION",
                        Note ="กรณีสาขาเฉพาะทาง",
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_RENAME_DOC", FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList() {
                        FileName = "CITIZEN_RENAME_DOC_REAL", FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Note = "ลงนามสำเนาถูกต้อง"
                    },
                    new SingleFormFileList() {
                        FileName = "MEDICINE_CERTIFICATE", FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                        Note = "ลงนามสำเนาถูกต้อง"
                    },

                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_RENEW_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_EDIT_DOC", FileGroup = "ANIMAL_MED_EDIT_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_RENEW_LICENSE_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_EDIT_LICENSE_DOC", FileGroup = "ANIMAL_MED_EDIT_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_RENEW_BOOK_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = false,
                        Note = "MED_ANIMAL_RENEW_BOOK_DOC_NOTE",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_RENEW_EDITED_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = false,
                        Note = "MED_ANIMAL_RENEW_EDITED_DOC_NOTE",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_EDIT_BOOK_DOC", FileGroup = "ANIMAL_MED_EDIT_SECTION",
                        Required = true,
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_CANCEL_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "MED_ANIMAL_CANCEL_BOOK_DOC", FileGroup = "ANIMAL_MED_RENEW_SECTION",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },

                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_PERMIT_OPEN_BUILD", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_STORE_MAP", FileGroup = "ANIMAL_BUILD_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/dld/1-1_form_animal_hospital.doc",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_STORE_PLAN_AREA", FileGroup = "ANIMAL_BUILD_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/dld/1-2_form_animal_hospital.doc",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_STORE_BUILDING_PLAN", FileGroup = "ANIMAL_BUILD_SECTION",
                        Note ="ถ้ามี",
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_STORE_NATURAL_DOC", FileGroup = "ANIMAL_BUILD_SECTION",
                        Required = false,
                        Note = "ถ้ามี",
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_BILL", FileGroup = "ANIMAL_BUILD_SECTION",
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ANIMAL_BUILD_XRAY_MACHINE", FileGroup = "ANIMAL_BUILD_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ANIMAL_BUILD_SERVICE",
                                        DataName = "APP_ANIMAL_BUILD_SERVICE_TYPE_APP_ANIMAL_BUILD_SERVICE_TYPE_SIX",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },


                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_SERVICE_PROVIDER_LIST", FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Note = "กรณีที่หลักฐาน หรือเอกสารที่ใช้ยื่นตอนขอใบอนุญาตประกอบธุรกิจโรงแรมมีการแก้ไขหรือเปลี่ยนแปลง",
                        DocFormUrl = "http://www.thaispa.go.th/spa2013/web/web_new/fileupload/2560-203.pdf",
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_SERVICE_PROVIDER_CERTIFICATE",
                        FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HEALTH_CARE_SERVICE",
                                DataItems = new SingleFormDataItem[] {
                                    new SingleFormDataItem { DataName = "APP_HEALTH_CARE_SERVICE_PROVIDER_ID" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_SERVICE_PROVIDER_ID_CARD",
                        FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HEALTH_CARE_SERVICE",
                                DataItems = new SingleFormDataItem[] {
                                    new SingleFormDataItem { DataName = "APP_HEALTH_CARE_SERVICE_PROVIDER_ID" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_MANAGER_BOOK",
                        FileGroup = "HEALTH_CARE_FILE_SECTION",
                        DocFormUrl = "http://www.thaispa.go.th/spa2013/web/web_new/fileupload/2560-204.pdf",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HEALTH_CARE_MANAGER_PART",
                                DataItems = new SingleFormDataItem[] {
                                    new SingleFormDataItem { DataName = "APP_HEALTH_CARE_MANAGER_ID" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_MANAGER_LICENSE",
                        FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HEALTH_CARE_MANAGER_PART",
                                DataItems = new SingleFormDataItem[] {
                                    new SingleFormDataItem { DataName = "APP_HEALTH_CARE_MANAGER_ID" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_MANAGE_ID_CARD",
                        FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HEALTH_CARE_MANAGER_PART",
                                DataItems = new SingleFormDataItem[] {
                                    new SingleFormDataItem { DataName = "APP_HEALTH_CARE_MANAGER_ID" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HEALTH_CARE_SHOP_DIAGRAM", FileGroup = "HEALTH_CARE_FILE_SECTION",
                        Note ="ต้องระบุรายละเอียดสัดส่วน กว้าง ยาว ให้ชัดเจน เพื่อใช้ในการคำนวณพื้นที่การให้บริการ",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                        
                    },

                        new SingleFormFileList()
                    {
                        FileName = "CITIZEN_COMMITTEE_PICTURE", FileGroup = "MOVIE_FILE_SECTION",
                        Note ="ถ่ายมาแล้วไม่เกิน 6 เดือน",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 50 // Unlimit items
                            }
                        },
                        Required = false,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MOIVE_STORE_PICTURE", FileGroup = "MOVIE_FILE_SECTION",
                        Note ="ถ่ายมาแล้วไม่เกิน 6 เดือน",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MOIVE_STORE_PICTURE_LONG_RANGE", FileGroup = "MOVIE_FILE_SECTION",
                        Note ="ถ่ายมาแล้วไม่เกิน 6 เดือน",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "MOIVE_STORE_PICTURE_SHORT_RANGE", FileGroup = "MOVIE_FILE_SECTION",
                        Note ="ถ่ายมาแล้วไม่เกิน 6 เดือน",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },
                        new SingleFormFileList()
                    {
                        FileName = "KARAOKE_STORE_PLAN", FileGroup = "MOVIE_FILE_SECTION",
                        Required = true,
                        //Config = GetAuthorizeCommitteeCondition(true)
                    },







                    new SingleFormFileList
                    {   
                        //เอกสารแสดงที่มาของ สินค้าหรือบริการ
                        FileName = "DIRECT_MARKETING_PRODUCT_PRODUCER_PERMIT", FileGroup = "DIRECT_SELL_FILE_SECTION",
                        Note = "เช่น ใบอนุญาตนำ/สั่งเข้ามาในราชอาณาจักร ใบแจ้งเครื่องสำอางควบคุม/ควบคุมพิเศษ ใบอนุญาตผลิตสินค้า",
                        Config = new SingleFormFileConfig
                        {

                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OTHER),
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                    //        },
                                    //    ExpectedValue = "true"
                                    //    },
                                    //},
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                    //        },
                                    //    ExpectedValue = "true"
                                    //    },
                                    //},
                                },
                            },
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_CANCEL_LOCAL_NEWS", FileGroup = "MARKETING_CANCEL_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_CANCEL_SECTION",
                                        DataName = "DIRECT_MARKETING_CANCEL_TYPE_DIRECT_MARKETING_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_CANCEL_REASON", FileGroup = "MARKETING_CANCEL_SECTION",
                        Config = new SingleFormFileConfig
                        {

                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_CANCEL_SECTION",
                                        DataName = "DIRECT_MARKETING_CANCEL_TYPE_DIRECT_MARKETING_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_CANCEL_RESPOND", FileGroup = "MARKETING_CANCEL_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_CANCEL_SECTION",
                                        DataName = "DIRECT_MARKETING_CANCEL_TYPE_DIRECT_MARKETING_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_CANCEL_LOCAL_NEWS", FileGroup = "DIRECT_SELL_CANCEL",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_CANCEL_SECTION",
                                        DataName = "DIRECT_SELL_CANCEL_TYPE_DIRECT_SELL_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_CANCEL_REASON", FileGroup = "DIRECT_SELL_CANCEL",
                        Config = new SingleFormFileConfig
                        {

                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_CANCEL_SECTION",
                                        DataName = "DIRECT_SELL_CANCEL_TYPE_DIRECT_SELL_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_CANCEL_RESPOND", FileGroup = "DIRECT_SELL_CANCEL",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_SELL_CANCEL_SECTION",
                                        DataName = "DIRECT_SELL_CANCEL_TYPE_DIRECT_SELL_CANCEL_TYPE__CANCEL",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },







                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_OWENED_AREA_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "1.เช่น โฉนดที่ดิน น.ส.3ก/น.ส.3 ส.ค.1",
                        Note_2 = "2.ขนาดเท่าเอกสารต้นฉบับ ถ่ายทุกหน้า และต้องครบถ้วนตามแผนผังบริเวณ หรือที่ระบุไว้ในคำขออนุญาต",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_RENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_RENT_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย"
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT_OPTIONAL",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_BUILDING_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณีเช่าอาคาร สถานที่ผู้อื่น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "INFORMATION_STORE_BUILDING_DOC_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                    },


                    #region INFORMATION STORE
                    #region สณ.5 (18.2)
                    new SingleFormFileList(false)
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | ทะเบียนบ้าน: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_HOUSEHOLD_REGISTRATION_COPY_RENEW", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList(false)
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | บัตรประชาชน:: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_ID_CARD_RENEW", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_MEDICAL_CERT_RENEW", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_BKK_FOOD_HEALTH_CERT_RENEW", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | รูปถ่ายหน้าตรงครึ่งตัวไม่สวมหมวก: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_PHOTO_RENEW", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    #endregion
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_LICENSE", //ใบอนุญาตเป็นผู้จำหน่ายสินค้าในที่หรือทางสาธารณะ
                        FileGroup = "SELL_FOOD_ON_PUBLIC_AREA", //การจำหน่ายสินค้าในที่หรือทางสาธารณะ
                    },
                    new SingleFormFileList //*** แบบเหน้า html มีปุ่มให้กดเพิ่มจำนวนได้
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_OTHER_EVIDENCE_EDIT_PRODUCT_OR_SELL_METHOD_OR_LOCATION",
                        FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            //DisplayCondition = GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OWNED),
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OWNED),
                                    ShowIfJuristic
                                },
                            }
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีไม่มีเอกสารดังกล่าว ให้อัปโหลดเอกสารแสดงกรรมสิทธิในอาคาร/สถานที่ดังกล่าว ที่เอกสารเกี่ยวกับการแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบ เช่น สัญญาเช่า หนังสือยินยอมให้ใช้อาคาร/สถานที่ เป็นต้น",
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_DOC_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเป็นเจ้าของอาคารเอง"
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_ID_CARD_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required =false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย"
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_RENTAL_CONTRACT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_RENTAL_CONTRACT_SPA_ONLY",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_RENTAL_CONTRACT_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น"
                    },

                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย"
                    },

                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "INFORMATION_STORE",
                                                DataName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                            },
                                            ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_OPTIONAL",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานของนิติบุคคล"
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HQ_MAP",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "ลงลายมือชื่อรับรองเอกสาร",
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_MAP",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                    },

                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_MAP_REQUIRED",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                    },

                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณีเช่าอาคาร สถานที่ผู้อื่น",
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BOOK_NO_CON",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณีเช่าอาคาร สถานที่ผู้อื่น",
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_DETAIL_DATE_BUILDING",
                        Note = "เช่น ใบอนุญาตก่อสร้างอาคาร (แบบ อ.1), ทะเบียนบ้าน, สัญญาซื้อขายที่กรมที่ดิน หรือหนังสือรับรองการก่อสร้างจากเขต",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                    },
                    #endregion

                    #region OTHER
                    
                    new SingleFormFileList
                    {
                        FileName = "COMMERCE_REGISTRATION_DOC_CITIZEN",
                        FileGroup = "OTHER_DOC",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "INFORMATION_STORE",
                                                DataName = "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION",
                                            },
                                            ExpectedValue = CommercialRegistrationValueConst.YES_REGISTRATION,
                                        }
                                    },
                                }
                            },
                        },
                    },

                    #endregion

                    #region RADIO

                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_LICENSE",
                        FileGroup = "RADIO_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_RADIO_RENEW_SECTION",
                                        DataName = "APP_RADIO_SECTION_RENEW_TYPE_OPTION",
                                    },
                                    ExpectedValue = "1",
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_REPAIR_LICENSE",
                        FileGroup = "RADIO_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_RADIO_RENEW_SECTION",
                                        DataName = "APP_RADIO_SECTION_RENEW_TYPE_OPTION",
                                    },
                                    ExpectedValue = "2",
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_OTHER",
                        FileGroup = "RADIO_FILE_SECTION",
                        Note = "กรณีหลักฐาน หรือเอกสารของผู้ได้รับอนุญาตมีการแก้ไขเปลี่ยนแปลงจากตอนที่ยืนคำขอในครั้งล่าสุด",
                        Required = false,

                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_RADIO_LICENSE_EDIT", FileGroup = "RADIO_FILE_SECTION",
                        Required = true,
                    },
                    
                    

                    #endregion

                    #region ANIMAL BUILD

                    new SingleFormFileList
                    {
                        FileName = "APP_ANIMAL_BUILD_LICENSE",
                        FileGroup = "APP_ANIMAL_BUILD_RENEW_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "APP_ANIMAL_BUILD_PHOTO_STORE",
                        FileGroup = "APP_ANIMAL_BUILD_RENEW_SECTION",
                    },

                    #endregion


                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_CONTROL_DOC", FileGroup = "BUILDING_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Note = "กรณี ทะเบียนบ้าน: อาคารที่ใช้เป็นที่ตั้งร้าน ไม่ระบุเจ้าบ้าน ให้แนบเอกสารแสดงความเป็นเจ้าของอาคารหรือสถานที่ เช่น โฉนดที่ดิน ใบอนุญาตก่อสร้างอาคาร (อ.1) ในช่องเอกสารเพิ่มเติม",
                        Required = true,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_CONTROL_DOC_A6", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "INFORMATION_STORE",
                                        DataName = "INFORMATION_STORE_AREA",
                                    },
                                    ExpectedValue = ">=300", // Hardcode: ถ้าเปลี่ยนค่านี้ต้องไปแก้ code ใน SingleFormController.evaluateCondition ด้วย
                                }
                            },
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_PRODUCTION_FLOW_CHART", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_WASTE_CONTROL_CHART", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_STORE_LAYOUT", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_FOOD_MEDICAL_CERTIFICATE", FileGroup = "SELL_FOOD_FILE_SECTION",
                        DocFormUrl = "http://www.bangkok.go.th/upload/user/00000056/service/environment/16.70247842.pdf",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_FOOD_FOOD_WORKER_INFO",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_SELL_FOOD_FOOD_WORKER_INFO__TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "SELL_FOOD_FOOD_WORKER_INFO__NAME" },
                                    new SingleFormDataItem { DataName = "SELL_FOOD_FOOD_WORKER_INFO__LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_PRODUCTION_PROCESS_CHART",
                        FileGroup = "SELL_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_POLUTION_CONTROL_CHART",
                        FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_HEALTH_CONTROL_CHART", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_SEFTY_CONTROL_CHART", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },

                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_STORE_PHOTO", FileGroup = "SELL_FOOD_FILE_SECTION",
                    },
                     new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_RENEW_DOC", FileGroup = "SELL_FOOD_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList(false)
                    {
                        FileName = "SELL_FOOD_FOOD_WORKER_CERTIFICATE", FileGroup = "SELL_FOOD_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_FOOD_FOOD_WORKER_INFO",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_SELL_FOOD_FOOD_WORKER_INFO__TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "SELL_FOOD_FOOD_WORKER_INFO__NAME" },
                                    new SingleFormDataItem { DataName = "SELL_FOOD_FOOD_WORKER_INFO__LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_FOOD_LICENSE_LASTYEAR",
                        FileGroup = "SELL_FOOD_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList
                    {
                        FileName = "INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT)
                                },
                            }
                        }
                    },

                    new SingleFormFileList(false)
                    {
                        // กรรมการผู้มีอำนาจลงนามฯ | ใบสำคัญการเปลี่ยนชื่อ
                        FileName = "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            IsOptional = true,
                        },
                    },

                    new SingleFormFileList(false)
                    {
                        // กรรมการผู้มีอำนาจลงนามฯ | ทะเบียนสมรส
                        FileName = "JURISTIC_COMMITTEE_MARRIAGE_DOCUMENT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                            IsOptional = true,
                        },
                    },
                    new SingleFormFileList()
                    {
                        // กรรมการผู้มีอำนาจลงนามฯ | รูปถ่ายหน้าตรงครึ่งตัวไม่สวมหมวก
                        FileName = "JURISTIC_COMMITTEE_PHOTO", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Config = GetAuthorizeCommitteeCondition()
                    },



                    new SingleFormFileList(false)
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | ทะเบียนบ้าน: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_HOUSEHOLD_REGISTRATION_COPY", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList(false)
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | บัตรประชาชน:: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_ID_CARD", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_MEDICAL_CERT", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_BKK_FOOD_HEALTH_CERT", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        // การจำหน่ายสินค้าในที่หรือทางสาธารณะ | รูปถ่ายหน้าตรงครึ่งตัวไม่สวมหมวก: ผู้ช่วยจำหน่ายสินค้าในที่หรือทางสาธารณะ (ชื่อ-นามสกุล)
                        FileName = "SELL_FOOD_IN_PUBLIC_AREA_HELPER_PHOTO", FileGroup = "SELL_FOOD_ON_PUBLIC_AREA",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "SELL_PRODUCT_ASSISTANT_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_ASSISTANT_TITLE_TEXT" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_FIRSTNAME" },
                                    new SingleFormDataItem { DataName = "ASSISTANT_LASTNAME" },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_TOBACCO_STORE_MAP",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileName = "SELL_TOBACCO_STORE_LAYOUT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PERMIT",
                        DocFormUrl = "~/Uploads/apps/ocpb/เอกสารรับรองรายการเอกสาร.pdf",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT__OPTIONAL",
                        Note = "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT_NOTE",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Not = true,
                                IsOr = true,
                                InnerConditions = GetProductProducerPermitConditions,
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT",
                        Note = "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT_NOTE",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = GetProductProducerPermitConditions,
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleMinItem = 1,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_MARKETING_PERMIT",
                        DocFormUrl = "~/Uploads/apps/ocpb/เอกสารรับรองรายการเอกสาร.pdf",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_MARKETING_PRODUCT_PERMIT",
                        DocFormUrl = "~/Uploads/apps/DirectSell/doc_1.pdf",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PRODUCT_PERMIT",
                        Note = "DIRECT_SELL_PRODUCT_PERMIT_NOTE",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_DIRECT_SELL),
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_SECTION",
                                                        DataName = "DIRECT_SELL_PRODUCE_BY_YOURSELF_OPTION",
                                                    },
                                                    ExpectedValue = "No",
                                                },
                                            },
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            SingleFormFileConfig.RequestForAppConfig(AppSystemNameTextConst.APP_DIRECT_MARKETING),
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_SECTION",
                                                        DataName = "DIRECT_MARKETING_PRODUCE_BY_YOURSELF_OPTION",
                                                    },
                                                    ExpectedValue = "No",
                                                },
                                            },
                                        }

                                    }
                                },

                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                AllowMultipleMinItem = 0,
                                AddItemBtnText = "DIRECT_SELL_PRODUCT_PERMIT_ADD_BTN",
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_BENEFIT_PLAN",
                        Note = "DIRECT_SELL_BENEFIT_PLAN_NOTE",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_BUSINESS_PLAN",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PRODUCT_LABEL",
                        Note = "DIRECT_SELL_PRODUCT_LABEL_NOTE",
                    },

                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_EXAMPLE_AGENT_DOC",
                        Note = "DIRECT_SELL_EXAMPLE_AGENT_DOC_NOTE",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_EXAMPLE_BUYER_DOC",
                        Note = "DIRECT_SELL_EXAMPLE_BUYER_DOC_NOTE",
                    },

                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_INVOICE_EXAMPLE",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_MEMBER_CARD_EXAMPLE",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_FREE_AGENT_CARD_EXAMPLE",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_FREE_AGENT_CONTRACT_EXAMPLE",
                        Note = "DIRECT_SELL_FREE_AGENT_CONTRACT_EXAMPLE_NOTE",
                        DocFormUrl = "~/Uploads/apps/ocpb/ตัวอย่างสัญญา.pdf",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PRODUCT_TABLE",
                        DocFormUrl = "~/Uploads/apps/ocpb/ตารางรายการสินค้า (ตลาดแบบตรง).xlsx",
                    },
                    new SingleFormFileList
                    {// ตารางรายการสินค้าที่แสดงรายระเอียดสิค้า DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT
                        FileGroup = "DIRECT_SELL_FILE_SECTION", FileName = "DIRECT_MARKETING_PRODUCT_TABLE",
                         Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                        DocFormUrl = "~/Uploads/apps/ocpb/ตารางรายการสินค้า (ขายตรง).xlsx",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_HQ_PHOTO",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_PERMIT_BEHAVIOR",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_BOOK_DOC",
                    },
                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_SELL_EXAMPLE_DOC",
                    },
                    //new SingleFormFileList
                    //{
                    //    FileName = "COMMERCE_REGISTRATION_DOC", FileGroup = "OTHER_DOC",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = ShowIfJuristic,
                    //    }
                    //},
                    new SingleFormFileList
                    {
                        FileName = "COMMERCE_REGISTRATION_DOC", FileGroup = "OTHER_DOC",
                        Required = false
                    },
                    new SingleFormFileList
                    {
                        FileName = "RADIO_COMMERCE_REGISTRATION_NO_CON", FileGroup = "RADIO_FILE_SECTION",
                        Required = true,
                        Note = "ต้องระบุพาณิชย์กิจเกี่ยวกับเครื่องโทรคมนาคม เครื่องรับส่งวิทยุ หรือเครื่องมือสื่อสาร หรืออย่างใดอย่างหนึ่งที่เกี่ยวข้องกับกิจการโทรคมนาคม",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "RADIO_STORE_COMMERCE_REGISTRATION_NO_CON", FileGroup = "RADIO_FILE_SECTION",
                        Required = false,
                        Note = "กรณีร้านค้า ขออนุญาต",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "ECOMMERCE_REGISTRATION_DOC", FileGroup = "OTHER_DOC",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_DIRECT_MARKETING_SECTION",
                                        DataName = "DIRECT_MARKETING_SELECT_CHANNEL_DIRECT_MARKETING_SELECT_CHANNEL_MEDIA",
                                    },
                                    ExpectedValue = "true",
                                }
                            }
                        }
                    },

                    #region DIRECT_MARKETING_EDIT
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PRODUCT_PICTURE", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        Note = "โดยรูปถ่ายจะต้องประกอบไปด้วย รูปสินค้าและรูปฉลากที่แสดงให้เห็นข้อความบนฉลากทุกด้าน เช่นหากขายขวดน้ำ ต้องส่งรูปขวดน้ำ พร้อมรูปฉลากที่เห็นข้อความบนฉลากทุกด้าน",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PRODUCT_PRODUCER_PERMIT_OPTIONAL", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหารเช่น กรณีเป็นผู้นำเข้าอาหาร ให้แนบใบอนุญาตนำ/สั่งอาหารเข้ามาในราชอาณาจักร (แบบ อ.7) กรณีเป็นผู้ผลิตอาหารให้แนบใบอนุญาตผลิตอาหาร (แบบ อ.2) เป็นต้น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_CERTIFICATE_FOOD", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_REGIS_FOOD_LIST", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PERMIT_LABEL", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PERMIT_PUBLISH", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_COSMETICS", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทเครื่องสำอาง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_IMPORT_RECEIPT", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีมีสินค้าประเภทเครื่องสำอาง, เครื่องไฟฟ้า หรืออื่นๆ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_CER_BOOK_C1", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงสินค้าประเภทที่ต้องมีมาตรฐานผลิตภัณฑ์อุตสาหกรรม (สมอ.)(อก.)",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PRODUCT_RECEIVE_PERMIT", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงสินค้าประเภทอื่นๆ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_CERTIFICATION_OLD", FileGroup = "JURISTIC_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_OTHER",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETTING_EDIT_PRODUCT_TABLE", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        DocFormUrl = "~/Uploads/apps/ocpb/ตารางรายการสินค้า (ตลาดแบบตรง).xlsx",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_CRIME", FileGroup = "JURISTIC_INFORMATION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_CER_BOOK", FileGroup = "JURISTIC_INFORMATION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_ELECTRIC_COMMER", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_SELL_DETAIL", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_TEXT_PAYMENT", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_TEXT_TRANSPORT", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_PRODUCT_TABLE", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/ocpb/ตารางรายการสินค้า (ตลาดแบบตรง).xlsx",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_COMPANY_NAME_COMPARE", FileGroup = "DIRECT_MARKETING_EDIT",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_COMMITTEE_LIST", FileGroup = "JURISTIC_INFORMATION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    #endregion
                    new SingleFormFileList()
                    {
                        FileName = "DIRECT_MARKETING_EDIT_COMMITTEE2",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Note =   "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    ShowIfJuristic,
                                }

                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION" },
                                FilterDataItemText = "yes",
                            }
                        }
                    },

                    new SingleFormFileList()
                    {
                        FileName = "DIRECT_MARKETING_EDIT_COMMITTEE_OPTIONAL",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_USAGE_AGREEMENT", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                   {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                                   },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_RENTAL", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                   {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                                   },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_BUILDING_OWNED", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OWNED),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_OWNED_ID_CARD", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },
                    //new SingleFormFileList
                    //{
                    //    FileName = "DIRECT_MARKETING_EDIT_STORE_HOUSEHOLD_OWNED", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                    //    Required = false,
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                    //                            DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //            }
                    //        }
                    //    }
                    //},
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_CERTIFICATE_OWNED", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            Condition = new SingleFormFileConfig.ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                    SectionName = "INFORMATION_STORE",
                                                    DataName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                                },
                                                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            },
                                        },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                        DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_STORE_MAP", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเพิ่มสำนักงานแห่งใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_LOCATION_MAP", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเพิ่มสาขา",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_LOCATION_PICTURE", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },

                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_MARKETING_EDIT_STORE_LOCATION_COMPARE", FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }

                                    },

                                }
                            }
                        }
                    },

                    #region DIRECT SELL EDIT แก้ไขเปลี่ยนแปลงสถานที่ 
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                   {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                                   },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_RENTAL_CONTACT",
                        FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_BUILDING_OWNED",
                        FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OWNED),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_OWNED_ID_CARD",
                        FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },



                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_CERTIFICATE_OWNED", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Required = true,
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            Condition = new SingleFormFileConfig.ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                    SectionName = "INFORMATION_STORE",
                                                    DataName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                                },
                                                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            },
                                        },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_MAP", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Required = false,
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_LOCATION_PICTURE", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Required = true,
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    //new SingleFormFileList
                    //{
                    //    FileName = "DIRECT_SELL_EDIT_STORE_OFFICE_PICTURE", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                    //    Required = false,
                    //    Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            IsOr = true,
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //            }
                    //        }
                    //    }
                    //},
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_LOCATION_MAP", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Required = true,
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_STORE_LOCATION_COMPARE", FileGroup = "INFORMATION_STORE_CHANGE_OFFICE_FILE_SECTION",
                        Required = false,
                        Note = "กรณี แก้ไขเปลี่ยนแปลง สถานที่ติดต่อ ที่ตั้ง สำนักงานใหญ่",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    #endregion


                    #region DIRECT SELL EDIT แก้ไขเปลี่ยนแปลงสถานที่ 
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_USAGE_AGREEMENT", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                   {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                                   },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_RENTAL_CONTACT", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_BUILDING_OWNED", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.OWNED),

                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",
                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_OWNED_ID_CARD", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_CERTIFICATE_OWNED", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingCondition(StoreInformationBuildingTypeOptionValueConst.RENT),

                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            Condition = new SingleFormFileConfig.ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                    SectionName = "INFORMATION_STORE",
                                                    DataName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                                },
                                                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            },
                                        },
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                   Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                        DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                                    },
                                                    ExpectedValue = "true",

                                                },
                                            },
                                        },
                                    },

                                },
                            },
                        },
                    },
                    //new SingleFormFileList
                    //{
                    //    FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_MAP", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                    //    Required = false,
                    //    Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            IsOr = true,
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                new SingleFormFileConfig.ConditionConfig
                    //                {
                    //                    Condition = new SingleFormFileConfig.ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                    //                            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                    //                        },
                    //                        ExpectedValue = "true",
                    //                    }
                    //                },
                    //            }
                    //        }
                    //    }
                    //},
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_PICTURE", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_OFFICE_PICTURE", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = false,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_MAP", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = true,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_COMPARE", FileGroup = "INFORMATION_STORE_ADD_OR_CANCEL_FILE_SECTION",
                        Required = false,
                        Note = "กรณี ยกเลิก หรือเพิ่มเติม สำนักงานสาขา ของผู้ประกอบธุรกิจขายตรง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_ADD_OR_CANCEL_HEAD_OFFICE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    #endregion

                    #region แก้ไขเปลี่ยนแปลงประเภทหรือชนิดของสินค้าหรือบริการ

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_LIST", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CERTIFICATE_FOOD", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_PICTURE", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Note = "โดยรูปถ่ายจะต้องประกอบไปด้วย รูปสินค้าและรูปฉลากที่แสดงให้เห็นข้อความบนฉลากทุกด้าน เช่นหากขายขวดน้ำ ต้องส่งรูปขวดน้ำ พร้อมรูปฉลากที่เห็นข้อความบนฉลากทุกด้าน",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_PRODUCER_PERMIT_OPTIONAL", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหารเช่น กรณีเป็นผู้นำเข้าอาหาร ให้แนบใบอนุญาตนำ/สั่งอาหารเข้ามาในราชอาณาจักร (แบบ อ.7) กรณีเป็นผู้ผลิตอาหารให้แนบใบอนุญาตผลิตอาหาร (แบบ อ.2) เป็นต้น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CERTIFICATE_IMPORT_FOOD", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_REGIS_FOOD_LIST", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PERMIT_LABEL", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PERMIT_PUBLISH", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทอาหาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_PRODUCT_LABEL", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_SOURCE", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COSMETICS", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงรายการเกี่ยวกับสินค้าประเภทเครื่องสำอาง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_IMPORT_RECEIPT", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีมีสินค้าประเภทเครื่องสำอาง, เครื่องไฟฟ้า หรืออื่นๆ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_CER_BOOK_C1", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงสินค้าประเภทที่ต้องมีมาตรฐานผลิตภัณฑ์อุตสาหกรรม (สมอ.)(อก.)",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_RECEIVE_PERMIT", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแก้ไขเปลี่ยนแปลงสินค้าประเภทอื่นๆ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_PRODUCT_TABLE", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/ocpb/ตารางรายการสินค้า (ตลาดแบบตรง).xlsx",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    #endregion
                    #region แก้ไขเปลี่ยนแปลงการจ่ายผลตอบแทน
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_BENEFIT_LIST", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_PRODUCT",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_BENEFIT_BOOK", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_BENEFIT_LIST", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_BENEFIT_COMPARE_LIST", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    //new SingleFormFileConfig.ConditionConfig
                                    //{
                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                    //    {
                                    //        Data = new SingleFormDataItem
                                    //        {
                                    //            SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                    //            DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                    //        },
                                    //        ExpectedValue = "true",
                                    //    }
                                    //},
                                }
                            }
                        }
                    },
                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COMPANY_NAME_COMPARE", FileGroup = "DIRECT_SELL_EDIT_FILE_GROUP",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COMMITTEE_WORK_PERMIT", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION" },
                                FilterDataItemText = "foreigner",
                            },
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                }
                            }
                        }
                    },

                    new SingleFormFileList
                    {
                        FileName = "DIRECT_SELL_EDIT_COMMITTEE_FOREIGNER", FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_TITLE" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NAME" },
                                    new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_LASTNAME" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION" },
                                FilterDataItemText = "foreigner",
                            },
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_INVEESTMENT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_SELL_EDIT_SECTION",
                                                DataName = "DIRECT_SELL_CHANGE_TYPE_DIRECT_SELL_CHANGE_COMMITTEE",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_ADDRESS",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_METHOD",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_OTHER",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_DIRECT_MARKETING_EDIT_SECTION",
                                                DataName = "DIRECT_MARKETING_CHANGE_TYPE_DIRECT_MARKETING_CHANGE_PRODUCT",
                                            },
                                            ExpectedValue = "true",
                                        }
                                    }
                                }
                            }
                        }
                    },

                    #endregion

                    new SingleFormFileList
                    {
                        FileGroup = "DIRECT_SELL_FILE_SECTION",
                        FileName = "DIRECT_MARKETING_SELL_METHOD_DETAIL",
                        Note = "DIRECT_MARKETING_SELL_METHOD_DETAIL_NOTE",
                    },

                    new SingleFormFileList
                    {
                        FileName = "SELL_ANIMAL_FOOD_STORE_PHOTO", FileGroup = "SELL_ANIMAL_FILE_SECTION",
                    },
                     #region APP DANGER

                    new SingleFormFileList()
                    {
                        FileName = "DOC_APP_DANGER_CURRENT_LICENSE",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_OTHER_DOC",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Note = "กรณีไม่มีเอกสารดังกล่าว ให้อัปโหลดเอกสารเกี่ยวกับการยินยอม หรือเอกสารด้านสภาพแวดล้อม",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_MACHINE_LOCATION",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_STORE_PHOTO",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_POLUTION_CONTROL_CHART",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_HEALTH_CONTROL_CHART",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_SEFTY_CONTROL_CHART",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_ACTIVITY_PERMIT",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_ENV_QUALITY",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Note = "DANGER_FOOD_ENV_QUALITY_NOTE",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_EIA_HIA",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Note = "DANGER_FOOD_EIA_HIA_NOTE",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_FOOD_MEDICAL_CERT",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Note = "DANGER_FOOD_MEDICAL_CERT_NOTE",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "BUILDING_USING_AREA_CONFIRM_AGENT",
                        FileGroup = "DANGER_FOOD_FILE_SECTION",
                        Required = false,

                    },
                    #endregion
                    new SingleFormFileList()
                    {
                        FileName = "DANGER_ALL_RENTAL_CONTACT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = false,
                    },

                    //----------------------FRT_DOC--------------------------------------------------------------------------------------------------------------
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_01",  //หนังสือแจ้งการใช้ประโยชน์ที่ดินหรือเปลี่ยนแปลงการใช้ประโยชน์ที่ดินในเขตกรุงเทพ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_02",  //แผนที่แสดงพื้นที่ประกอบการ และที่ตั้งของเครื่องจักร (หากมีหลายชั้นให้แสดงทุกชั้น) รวมทั้งที่พักของผู้ปฏิบ้ติงานในสถานประกอบการ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_03",  //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงกระบวนการผลิต
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_04",  //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงการป้องกันมลพิษ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_05",  //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงสุขลักษณะภายในสถานประกอบการ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_06",  //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงระบบความปลอดภัยในการทำงาน
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()  //New,Renew,Edit
                    {
                        FileName = "FRT_001_DOC_07",  //ใบอนุญาต หนังสือรับรองการแจ้ง หรือเอกสารหลักฐานจากหน่วยงานที่เกี่ยวข้องให้ประกอบกิจการนั้นได้ เช่น ใบรับจดแจ้งเครื่องสำอาง ใบอนุญาตประกอบกิจการโรงงาน เป็นต้น
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_08",  //ผังภาพรวมกระบวนการผลิต
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_09",  //หนังสือแจ้งการใช้ประโยชน์ที่ดินหรือเปลี่ยนแปลงการใช้ประโยชน์ที่ดินในเขตกรุงเทพ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },


                    //FRT_001 Edit
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_10",  //ใบอนุญาตประกอบกิจการที่เป้นอันตรายต่อสุขภาพ
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_11",  //เอกสารที่มีการเปลี่ยนแปลง
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },

                    //FRT_002 CANCEL
                     new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_12",  //ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ (แบบ อภ.2) 
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_13",  //ใบอนุญาตให้เปลี่ยนแปลง ขยาย หรือลดการประกอบกิจการ สถานที่หรือเครื่องจักรของกิจการที่เป็นอันตรายต่อสุขภาพ (แบบ อภ.5)
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = false,
                    },
                     new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_14",  //ใบแทนใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ (แบบ อภ.7)
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_15",  //หลักฐานการแจ้งขอเลิกการประกอบกิจการของหน่วยงานที่เกี่ยวข้อง เช่น กระทรวงพาณิชย์ กรมสรรพากร กรมโรงงานอุตสาหกรรม กระทรวงอุตสาหกรรม
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = false,
                    },

                     new SingleFormFileList()
                    {
                        FileName = "FRT_001_DOC_16",  //รายละเอียดกระบวนการผลิต
                        FileGroup = "FRT_001_DOC_GROUP",
                        Required = true,
                    },
                     
                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

                    // ******************** APP_FACTORY_CLASS_2_NEW ***********************
                    
                     #region New Citizen
                    //new SingleFormFileList
                    //  {
                    //    FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_LIST",
                    //    FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_REQUEST_SECTION",
                    //    Required = true,
                    //    Note = "ลงนามรับรองสำเนาถูกต้อง",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //            {
                    //                 ShowIfCitizen,
                    //            },
                    //        },
                    //    },
                    //  },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                      },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },
                    #endregion
                      
                     #region New Juristict

                      //new SingleFormFileList()//01
                      //{
                      //     FileGroup = "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                      //     FileName = "JURISTIC_COMMITTEE_ID_CARD",
                      //     Note = "ลงนามรับรองสำเนา",
                      //     Required = true,
                      //     Config = new SingleFormFileConfig
                      //     {
                      //         DisplayCondition = new SingleFormFileConfig.ConditionConfig
                      //         {
                      //             InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                      //             {
                      //                 ShowIfJuristic,
                      //             }
                      //         }
                      //     }
                      //},

                      new SingleFormFileList()
                      {
                           FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_BOARD_SECTION",
                           FileName = "APP_FACTORY_CLASS_2_NEW_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                           Required = true,
                           Note = "ลงนามรับรองสำเนาถูกต้อง",
                           Config = new SingleFormFileConfig
                           {
                               DisplayCondition = new SingleFormFileConfig.ConditionConfig
                               {
                                   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                   {
                                       ShowIfJuristic,
                                   }
                               },
                               IsOptional = true,
                               ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                               {
                                    BindToSection = true,
                                    SectionName = "COMMITTEE_INFORMATION",
                                    DataItems = new SingleFormDataItem[]
                                        {
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_TITLE"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_NAME"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                            },
                                        },
                                    FilterDataItem = new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION"
                                    },
                                    FilterDataItemText = "yes",
                               }
                           }
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_BOARD_JURISTICT_WORK_PERMIT_PERSON_IDENTIFIED_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_BOARD_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_TITLE"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_NAME"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                    },
                                },
                                FilterDataItem = new SingleFormDataItem
                                {
                                    DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION"
                                },
                                FilterDataItemText = "foreigner",
                            },
                        }
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                    #endregion
                    
                    // ****************** End APP_FACTORY_CLASS_2_NEW *********************

                    // ****************** APP_FACTORY_CLASS_2_CANCEL **********************
                    
                    #region Cancel Citizen
                       //new SingleFormFileList
                       //{
                       //     FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_LIST",
                       //     FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_REQUEST_SECTION",
                       //     Required = true,
                       //     Note = "ลงนามรับรองสำเนาถูกต้อง",
                       //     Config = new SingleFormFileConfig
                       //     {
                       //         DisplayCondition = new SingleFormFileConfig.ConditionConfig
                       //         {
                       //             InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                       //             {
                       //                  ShowIfCitizen,
                       //             },
                       //         },
                       //     },
                       //},
                       new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                      },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },
                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_MAP_PLANT_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfCitizen,
                                    },
                                },
                            },
                       },
                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_CANCEL_DETIAL_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_NOTIFY_SECTION",
                            Required = true,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfCitizen,
                                    },
                                },
                            },
                       },
                    #endregion

                    #region Cancel Juristict

                    //new SingleFormFileList()
                    //  {
                    //       FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_JURISTICT_SECTION",
                    //       FileName = "JURISTIC_COMMITTEE_ID_CARD",
                    //       Note ="ลงนามรับรองสำเนา",
                    //       Required = true,
                    //       Config = new SingleFormFileConfig
                    //       {
                    //           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //           {
                    //               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //               {
                    //                   ShowIfJuristic,
                    //               }
                    //           }
                    //       }
                    //  },

                    new SingleFormFileList()
                    {
                           FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_SECTION",
                           FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                           Required = true,
                           Note = "ลงนามรับรองสำเนาถูกต้อง",
                           Config = new SingleFormFileConfig
                           {
                               DisplayCondition = new SingleFormFileConfig.ConditionConfig
                               {
                                   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                   {
                                       ShowIfJuristic,
                                   }
                               },
                               IsOptional = true,
                               ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                               {
                                    BindToSection = true,
                                    SectionName = "COMMITTEE_INFORMATION",
                                    DataItems = new SingleFormDataItem[]
                                        {
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_TITLE"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_NAME"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                            },
                                        },
                                    FilterDataItem = new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION"
                                    },
                                    FilterDataItemText = "yes",
                               }
                           }
                    },

                    new SingleFormFileList()
                    {
                           FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_SECTION",
                           FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_PERSON_HOME_REGISTER_LIST",
                           Required = true,
                           Note = "ลงนามรับรองสำเนาถูกต้อง",
                           Config = new SingleFormFileConfig
                           {
                               DisplayCondition = new SingleFormFileConfig.ConditionConfig
                               {
                                   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                   {
                                       ShowIfJuristic,
                                   }
                               },
                               IsOptional = true,
                               ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                               {
                                    BindToSection = true,
                                    SectionName = "COMMITTEE_INFORMATION",
                                    DataItems = new SingleFormDataItem[]
                                        {
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_TITLE"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_NAME"
                                            },
                                            new SingleFormDataItem
                                            {
                                                DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                            },
                                        },
                                    FilterDataItem = new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION"
                                    },
                                    FilterDataItemText = "yes",
                               }
                           }
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_WORK_PERMIT_PERSON_IDENTIFIED_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_TITLE"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_NAME"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                    },
                                },
                                FilterDataItem = new SingleFormDataItem
                                {
                                    DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION"
                                },
                                FilterDataItemText = "foreigner",
                            },
                        }
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                    },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST_JURISTICT",
                        FileGroup = "APP_FACTORY_CLASS_2_NEW_DOC_AUTHORIZE_JURISTICT_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_CANCEL_DOC_MAP_PLANT_JURISTICT_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_PLANT_LOCATION_JURISTICT_SECTION",
                            Required = false,
                            Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },
                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_CANCEL_DETIAL_JURISTICT_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_NOTIFY_JURISTICT_SECTION",
                            Required = true,
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },
                    #endregion

                    // ****************** APP_FACTORY_CLASS_2_CANCEL **********************

                    // ****************** APP_FACTORY_CLASS_2_EDIT ************************

                    #region Edit Citizen
                    
                      //new SingleFormFileList // บุคคลผู้ขออนุญาต
                      //{
                      //  FileName = "APP_FACTORY_CLASS_2_EIDT_DOC_PERSON_IDENTIFIED_LIST",
                      //  FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_REQUEST_SECTION",
                      //  Required = true,
                      //  Note = "ลงนามรับรองสำเนาถูกต้อง",
                      //  Config = new SingleFormFileConfig
                      //  {
                      //      DisplayCondition = new SingleFormFileConfig.ConditionConfig
                      //      {
                      //          InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                      //          {
                      //               ShowIfCitizen,
                      //          },
                      //      },
                      //  },
                      //},

                       new SingleFormFileList() // มอบอำนาจ
                       {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION",
                            Required = true,
                            Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                            Config = new SingleFormFileConfig
                            {
                               DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                   {
                                       ShowIfCitizen,
                                       new SingleFormFileConfig.ConditionConfig
                                       {
                                            Condition = new SingleFormFileConfig .ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                        SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                        DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                                },
                                                ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                            },
                                       },
                                   }

                                },
                               ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                               {
                                     AllowMultipleEqualToSectionItem = true,
                                     AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                     AllowMultipleMinItem = 1,
                               },
                            },
                       },

                       new SingleFormFileList()
                       {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION",
                            Required = true,
                            Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                            Config = new SingleFormFileConfig
                            {
                               DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                   {
                                       ShowIfCitizen,
                                       new SingleFormFileConfig.ConditionConfig
                                       {
                                            Condition = new SingleFormFileConfig .ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                        SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                        DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                                },
                                                ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                            },
                                       },
                                   }

                                },
                            },
                       },
                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      //new SingleFormFileList // สถานที่ตั้งโรงงาน
                      //{
                      //      FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_PLANT_LIST",
                      //      FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                      //      Required = false,
                      //      Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                      //      Config = new SingleFormFileConfig
                      //      {
                      //          DisplayCondition = new SingleFormFileConfig.ConditionConfig
                      //          {
                      //              InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                      //              {
                      //                   ShowIfCitizen,
                      //              },
                      //          },
                      //      },
                      // },

                      new SingleFormFileList // สถานที่ตั้งโรงงาน
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_PLANT_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 }
                            }
                      },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเป็นเจ้าของสถานที่ตั้งสำนักงาใหญ่เอง",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,                                           
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                    //    }
                                                    //},
                                                }
                                            }
                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER_ID_CARD_2",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่นหรือใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },
                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = true,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                            //            }
                                            //        },
                                            //    }
                                            //}
                                        },
                                 },

                            }
                      },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PROMISE_2",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 }
                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLACE_2",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 }
                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLACE_2",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น และเป็นนิติบุคคล",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_BUILDING_RENTING_OWNER_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_TYPE2_EDIT_TITLE_DEED",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                IsOr = true,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },

                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_CONTRUCTION_2",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_BUILDING_RENTING_OWNER_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_OWN_BUILDING_DOC",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเป็นเจ้าของสถานที่ตั้งสำนักงาใหญ่เอง",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                    //    }
                                                    //},
                                                }
                                            },

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_HOUSE_REGISTRATION",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น หรือ ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_BUILDING_RENTING_OWNER_TYPE_OPTION_2",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                 },

                            }
                       },

                      // XXX
                      // Plant location

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_PLATLOCATION",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Config = new SingleFormFileConfig
                            {
                                    DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                IsOr = true,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },
                                        },
                                    },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_OWN_PLANT_DOC",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเป็นเจ้าของสถานที่ตั้งโรงงานเอง",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.OWNED,
                                                    //    }
                                                    //},
                                                }
                                            },

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PLANT_FREE_ID_CARD",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่นหรือใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PLANT_PROMISE",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 }
                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLANT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    //new SingleFormFileConfig.ConditionConfig
                                                    //{
                                                    //    Condition = new SingleFormFileConfig.ConditionItem
                                                    //    {
                                                    //        Data = new SingleFormDataItem
                                                    //        {
                                                    //            SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                                    //            DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                                    //        },
                                                    //        ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                                    //    }
                                                    //},
                                                }
                                            },
                                        },
                                 }
                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLANT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น และเป็นนิติบุคคล",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_CONTRUCTION_PLANT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = true,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                 },

                            }
                       },

                        new SingleFormFileList
                        {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_HOUSE_REGISTRATION_PLANT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่น หรือ ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                    DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                    {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                //IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT,
                                            //            }
                                            //        },
                                            //    }
                                            //},

                                            //new SingleFormFileConfig.ConditionConfig
                                            //{
                                            //    IsOr = false,
                                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            //    {
                                            //        new SingleFormFileConfig.ConditionConfig
                                            //        {
                                            //            Condition = new SingleFormFileConfig.ConditionItem
                                            //            {
                                            //                Data = new SingleFormDataItem
                                            //                {
                                            //                    SectionName = "APP_FACTORY_CLASS_2_OPT_3",
                                            //                    DataName = "APP_FACTORY_CLASS_2_EDIT_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                            //                },
                                            //                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            //            }
                                            //        },
                                            //    }
                                            //}

                                        },
                                    },

                            }
                        },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_HEAD_OFFICE",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            //Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                IsOr = true,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                }
                                            },
                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_FREE_ID_CARD",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_OFFICE_LOCATION_SECTION",
                            Required = false,
                            Note = "กรณีเช่าสถานที่ผู้อื่นหรือใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                 DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                 {
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            //ShowIfCitizen,
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                IsOr = false,
                                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                                {
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_SECTION_INFO_EDIT",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                                                            },
                                                            ExpectedValue = "true",
                                                        }
                                                    },
                                                    new SingleFormFileConfig.ConditionConfig
                                                    {
                                                        Condition = new SingleFormFileConfig.ConditionItem
                                                        {
                                                            Data = new SingleFormDataItem
                                                            {
                                                                SectionName = "APP_FACTORY_CLASS_2_OPT_2",
                                                                DataName = "APP_FACTORY_CLASS_2_EDIT_STORE_BUILDING_TYPE_OPTION_2",
                                                            },
                                                            ExpectedValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE,
                                                        }
                                                    },
                                                }
                                            },
                                        },
                                 },

                            }
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_REGISTER_HOME",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่น หรือ ใช้สถานที่ผู้อื่นแบบเสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfCitizen,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList // การแก้ไขใบแจ้งเริ่มประกอบกิจการโรงงานจำพวกที่ 2
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_CHANGE_NAME_AND_LASTNAME",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_NOTIFY_JURISTICT_SECTION",
                            Required = false,
                            Note = "กรณีเปลี่ยนแปลงชื่อ - นามสกุลผู้ประกอบกิจการโรงงาน",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfCitizen,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_APPLICATIONFORM",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_NOTIFY_JURISTICT_SECTION",
                            Required = true,
                            Note = "กรณีเปลี่ยนแปลงชื่อ - นามสกุลผู้ประกอบกิจการโรงงาน",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfCitizen,
                                    },
                                },
                            },
                       },
                    #endregion

                    #region Edit Juristict
                    
                      //new SingleFormFileList // นิติบุคคล
                      //{
                      //  FileName = "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                      //  FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_REQUEST_SECTION_JURISTICT",
                      //  Required = true,
                      //  Config = new SingleFormFileConfig
                      //  {
                      //      DisplayCondition = new SingleFormFileConfig.ConditionConfig
                      //      {
                      //          InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                      //          {
                      //               ShowIfJuristic,
                      //          },
                      //      },
                      //  },
                      //},

                      new SingleFormFileList // กรรมการผู้มีอำนาจลงนามฯ
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                      },

                      new SingleFormFileList
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_BOARD_JURISTICT_PERSON_HOME_REGISTER_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                      },

                      new SingleFormFileList() //มอบอำนาจ
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_AUTHORIZE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      new SingleFormFileList()
                      {
                        FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                      },

                      new SingleFormFileList // สถานที่ตั้งโรงงาน
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_MAP_PLANT_LIST",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัปโหลดมาด้วย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_INFORMATION_STORE_BUILDING_OWNER",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เป็นเจ้าของโรงงานเอง",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่น หรือ ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JUSTICT_RENT_PROMISE",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่น",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLACE_JURISTICT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "ใช้สถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLACE_JURISTICT",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่นและเป็นนิติบุคคล",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_CONTRUCTION",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_RENT_REGISTER_HOME",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_PLANT_LOCATION_SECTION_JURISTICT",
                            Required = false,
                            Note = "เช่าสถานที่ผู้อื่น หรือ ใช้สถานที่ผู้อื่นแบบเสียค่าใช้จ่าย",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList // การแก้ไขใบแจ้งเริ่มประกอบกิจการโรงงานจำพวกที่ 2
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_CHANGE_NAME_AND_LASTNAME",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_NOTIFY_JURISTICT_SECTION_JURISTICT",
                            Required = false,
                            Note = "กรณีเปลี่ยนแปลงชื่อ - นามสกุลผู้ประกอบกิจการโรงงาน",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                      new SingleFormFileList
                      {
                            FileName = "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_APPLICATIONFORM",
                            FileGroup = "APP_FACTORY_CLASS_2_EDIT_DOC_NOTIFY_JURISTICT_SECTION_JURISTICT",
                            Required = true,
                            Note = "กรณีเปลี่ยนแปลงชื่อ - นามสกุลผู้ประกอบกิจการโรงงาน",
                            Config = new SingleFormFileConfig
                            {
                                DisplayCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                         ShowIfJuristic,
                                    },
                                },
                            },
                       },

                    #endregion

                    // ***************** END APP_FACTORY_CLASS_2_EDIT *********************

                    // ***************** APP_FACTORY_TYPE_2_NEW ***************************

                    #region New Citizen

                    //new SingleFormFileList()
                    //{
                    //    FileName = "CITIZEN_DOC_IDENTIFIED",
                    //    FileGroup = "CITIZEN_INFORMATION",
                    //    Required = true,
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = ShowIfCitizen,
                    //    },
                    //},

                    new SingleFormFileList() //05
                    {
                        FileName = "APP_FACTOTY_TYPE2_TITLE_DEED",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = true,

                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }

                            },
                            IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_FACTORY_TYPE2_LOCATION_INFORMATION_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    //new SingleFormDataItem { DataName = "ADDRESS_APP_FACTORY_TYPE2_LOCATION_INFORMATION_CONTROL-637002536715189298" },
                                    //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                                    //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                                }
                            }
                        },
                    },

                    new SingleFormFileList() //06
                    {
                        FileName = "APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                    },

                    new SingleFormFileList() //07
                    {
                        FileName = "APP_FACTORY_TYP2_LESSOR_REGISTER_HOME",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าที่ดินผู้อื่น หรือ ใช้ที่ดินผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                    },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                    },

                    new SingleFormFileList() //08
                    {
                        FileName = "APP_FACTORY_TYP2_RENT_PROMISE",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีเช่าที่ดินผู้อื่น",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                    },

                    new SingleFormFileList() //08
                    {
                        FileName = "APP_FACTORY_TYP2_LAND_ALLOW",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีใช้ที่ดินผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                    },

                    new SingleFormFileList() //09
                    {
                        FileName = "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_ID",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note ="กรณีเช่าอาคารสถานของนิติบุคคล",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_FACTORY_TYPE2_DOC_USE_BUILDING_ALLOW_CITIZEN",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                    new SingleFormFileList() //10
                    {
                        FileName = "APP_FACTORY_TYP2_DOC_BUILDING_ALLOW",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note ="กรณีเช่าที่ดินผู้อื่น หรือ ใช้ที่ดินผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                           },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                           },
                        },
                    },

                    new SingleFormFileList() //11
                    {
                        FileName = "APP_FACTORY_TYP2_DOC_USE_BUILDING_ALLOW",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                    },
                               }
                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                           },
                        },
                    },

                    new SingleFormFileList() //12
                    {
                        FileName = "APP_FACTORY_TYPE2_PLANT_REGISTER_HOME",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีที่ตั้งโรงานมีเลขที่บ้าน",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                            },
                            IsOptional = true,
                            //ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            //{
                            //    BindToSection = true,
                            //    SectionName = "APP_FACTORY_TYPE2_LOCATION_INFORMATION_SECTION",
                            //    DataItems = new SingleFormDataItem[]
                            //    {
                            //        //new SingleFormDataItem { DataName = "ADDRESS_APP_FACTORY_TYPE2_LOCATION_INFORMATION_CONTROL-637002536715189298" },
                            //        //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                            //        //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                            //    }
                            //}
                        },
                    },

                    new SingleFormFileList() //13
                    {
                        FileName = "APP_FACTORY_TYP2_DOC_CONSTRUCTION_ALLOW",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "กรณีอยู่ในเขตควบคุมอาคาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                    },

                   new SingleFormFileList() //14
                   {
                        FileName = "APP_FACTORY_TYP2_DOC_CONTRACTOR",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้องตัวผู้ประกอบการวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Note_2 = "กรณีอยู่ในเขตควบคุมอาคาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //15
                   {
                        FileName = "APP_FACTORY_TYP2_DOC_CONTRUCTION_INSPECTION",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้องตัวผู้ประกอบการวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Note_2 = "กรณีเป็นอาคารที่ไม่ได้สร้างใหม่ หรืออาคารที่ไม่ได้รับอนุญาตให้ก่อสร้างเป็นโรงงาน",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //16
                   {
                        FileName = "APP_FACTORY_TYP2_LOCATION_MAP",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //17
                   {
                        FileName = "APP_FACTORY_TYP2_BLUE_PRINT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //18
                   {
                        FileName = "APP_FACTORY_TYP2_BLUE_PRINT_ONE_PER_HUNDRED",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //19
                   {
                        FileName = "APP_FACTORY_TYP2_ENGINE_BLUE_PRINT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //20
                   {
                        FileName = "APP_FACTORY_TYP2_INDUSTRIAL_WASTE",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfCitizen,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //21
                   {
                        FileName = "APP_FACTORY_TYPE2_DOC_IDENTIFIED_PREMIT_CITIZEN",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีเป็นเจ้าของอาคารเอง",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                   },

                   new SingleFormFileList() //22
                    {
                        FileName = "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_NOID",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าที่ดินของนิติบุคคล",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }

                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                           },
                        },
                    },

                    #endregion

                    #region New Juristict

                    //new SingleFormFileList()//01
                    //{
                    //   FileGroup = "APP_FACTORY_TYPE2_JURISTICT",
                    //   FileName = "JURISTIC_COMMITTEE_ID_CARD",
                    //   Required = true,
                    //   Config = new SingleFormFileConfig
                    //   {
                    //       DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //       {
                    //           InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //           {
                    //               ShowIfJuristic,
                    //           }
                    //       }
                    //   }
                    //},

                    new SingleFormFileList()//02
                   {
                       FileGroup = "APP_FACTORY_TYPE2_BOARD",
                       FileName = "APP_FACTORY_TYPE2_JURISTICT_IDENTIFIED_DOC",
                       Required = true,
                       Note = "ลงนามรับรองสำเนาถูกต้อง",
                       Config = new SingleFormFileConfig
                       {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                           },
                           IsOptional = true,
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                    {
                                        new SingleFormDataItem
                                        {
                                            DataName = "JURISTIC_COMMITTEE_TITLE"
                                        },
                                        new SingleFormDataItem
                                        {
                                            DataName = "JURISTIC_COMMITTEE_NAME"
                                        },
                                        new SingleFormDataItem
                                        {
                                            DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                        },
                                    },
                                FilterDataItem = new SingleFormDataItem
                                {
                                    DataName = "JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION"
                                },
                                FilterDataItemText = "yes",
                           }
                       }
                   },

                   new SingleFormFileList()//03
                   {
                        FileName = "APP_FACTORY_TYPE2_JURISTICT_ALLOW_WORK_PERMIT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "COMMITTEE_INFORMATION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_TITLE"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_NAME"
                                    },
                                    new SingleFormDataItem
                                    {
                                        DataName = "JURISTIC_COMMITTEE_LASTNAME"
                                    },
                                },
                                FilterDataItem = new SingleFormDataItem
                                {
                                    DataName = "JURISTIC_COMMITTEE_NATIONALITY_OPTION"
                                },
                                FilterDataItemText = "foreigner",
                            },
                        }
                   },

                   new SingleFormFileList() //04
                   {
                        FileName = "APP_FACTORY_TYPE2_AUTHORIZE_JURISTICT_DOC",
                        FileGroup = "APP_FACTORY_TYPE2_AUTHORIZE_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับลองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                   },

                   new SingleFormFileList() //05
                    {
                        FileName = "APP_FACTORY_TYPE2_AUTHORIZER_JURISTICT_DOC",
                        FileGroup = "APP_FACTORY_TYPE2_AUTHORIZE_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับลองสำเนาถูกต้อง โดยผู้มอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                    },

                   new SingleFormFileList() //06
                    {
                        FileName = "APP_FACTORY_TYPE2_AUTHORIZY_JURISTICT_DOC",
                        FileGroup = "APP_FACTORY_TYPE2_AUTHORIZE_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับลองสำเนาถูกต้อง โดยผู้รับมอบอำนาจ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "REQUESTOR_INFORMATION__HEADER",
                                                    DataName = "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION",
                                            },
                                            ExpectedValue = RequestorInformationValueConst.REQUEST_TYPE_NOMINEE,
                                        },
                                   },
                               }

                            },
                        },
                    },

                   new SingleFormFileList() //07
                    {
                        FileName = "APP_FACTORY_TYPE2_PLANT_TITLE_DEED_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        //Note ="กรณีเป็นเจ้าของที่ดินเอง",
                        Required = true,

                        Config = new SingleFormFileConfig
                        {
                            //DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            //{
                            //   InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                            //   {
                            //       new SingleFormFileConfig.ConditionConfig
                            //       {
                            //            Condition = new SingleFormFileConfig .ConditionItem
                            //            {
                            //                Data = new SingleFormDataItem
                            //                {
                            //                        SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                            //                        DataName = "FACTORY_TYPE2_PROPERTY",
                            //                },
                            //                ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                            //            },
                            //       },
                            //   }

                            //},
                            //IsOptional = true,
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                AllowMultipleMinItem = 1,
                                //BindToSection = true,
                                //SectionName = "APP_FACTORY_TYPE2_LOCATION_INFORMATION_SECTION",
                                //DataItems = new SingleFormDataItem[]
                                //{
                                //    //new SingleFormDataItem { DataName = "ADDRESS_APP_FACTORY_TYPE2_LOCATION_INFORMATION_CONTROL-637002536715189298" },
                                //    //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                                //    //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                                //}
                            }
                        },
                    },

                   new SingleFormFileList() //08
                    {
                        FileName = "APP_FACTORY_TYPE2_PLANT_REGISTER_HOME_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Note = "กรณีที่ตั้งโรงานมีเลขที่บ้าน",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               },
                            },
                            IsOptional = true,
                            //ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            //{
                            //    BindToSection = true,
                            //    SectionName = "APP_FACTORY_TYPE2_LOCATION_INFORMATION_SECTION",
                            //    DataItems = new SingleFormDataItem[]
                            //    {
                            //        //new SingleFormDataItem { DataName = "ADDRESS_APP_FACTORY_TYPE2_LOCATION_INFORMATION_CONTROL-637002536715189298" },
                            //        //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_FIRSTNAME" },
                            //        //new SingleFormDataItem { DataName = "APP_ANIMAL_MED_DR_LASTNAME" },
                            //    }
                            //}
                        },
                    },

                   new SingleFormFileList() //09
                     {
                        FileName = "APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าที่ดินผู้อื่น หรือ ใช้ที่ดินผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                   new SingleFormFileList() //10
                     {
                        FileName = "APP_FACTORY_TYPE2_LESSOR_REGISTER_HOME_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                    new SingleFormFileList() //11
                     {
                        FileName = "APP_FACTORY_TYPE2_RENT_PROMISE_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                 new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                    new SingleFormFileList() //12
                     {
                        FileName = "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                    //new SingleFormFileList() //13
                    //{
                    //    FileName = "APP_FACTORY_TYPE2_DOC_BUILDING_ALLOW_JURISTICT",
                    //    FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                    //    Required = false,
                    //    Config = new SingleFormFileConfig
                    //    {
                    //        DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //           InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //           {
                    //               ShowIfJuristic,
                    //               new SingleFormFileConfig.ConditionConfig
                    //               {
                    //                    Condition = new SingleFormFileConfig .ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                                SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                    //                                DataName = "FACTORY_TYPE2_PROPERTY",
                    //                        },
                    //                        ExpectedValue = "ยังไม่มีเลขที่บ้าน",
                    //                    },
                    //               },
                    //           }
                    //        },
                    //        ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                    //        {
                    //            AllowMultipleEqualToSectionItem = true,
                    //            AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                    //        },
                    //    },
                    // },

                    new SingleFormFileList() //14
                    {
                        FileName = "APP_FACTORY_TYPE2_DOC_USE_BUILDING_ALLOW_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                    new SingleFormFileList() //15
                     {
                        FileName = "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_ID_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_JURISTICT_PLANT_LOCATION_SECTION",
                        Required = false,
                        Note = "กรณีเช่าอาคารสถานของนิติบุคคล",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_FACTORY_TYPE2_PLANT_LOCATION_INFO",
                                                    DataName = "FACTORY_TYPE2_PROPERTY",
                                            },
                                            ExpectedValue = "อาคาร",
                                        },
                                   },
                               }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            },
                        },
                     },

                   new SingleFormFileList() //16
                    {
                        FileName = "APP_FACTORY_TYP2_DOC_CONSTRUCTION_ALLOW_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "กรณีอยู่ในเขตควบคุมอาคาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                    },

                   new SingleFormFileList() //17
                   {
                        FileName = "APP_FACTORY_TYP2_DOC_CONTRACTOR_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Note = "ลงนามรับรองสำเนาถูกต้องตัวผู้ประกอบการวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Note_2 = "กรณีอยู่ในเขตควบคุมอาคาร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //18
                   {
                        FileName = "APP_FACTORY_TYP2_DOC_CONTRUCTION_INSPECTION_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Note = "ลงนามรับรองสำเนาถูกต้องตัวผู้ประกอบการวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Note_2 = "กรณีเป็นอาคารที่ไม่ได้สร้างใหม่ หรืออาคารที่ไม่ได้รับอนุญาตให้ก่อสร้างเป็นโรงงาน",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //19
                   {
                        FileName = "APP_FACTORY_TYP2_LOCATION_MAP_JURITICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //20
                   {
                        FileName = "APP_FACTORY_TYP2_BLUE_PRINT_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //21
                   {
                        FileName = "APP_FACTORY_TYP2_BLUE_PRINT_ONE_PER_HUNDRED_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                   new SingleFormFileList() //22
                   {
                        FileName = "APP_FACTORY_TYP2_ENGINE_BLUE_PRINT_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้องด้วยผู้ประกอบวิชาชีพวิศวกรรมควบคุมและผู้ประกอบการทุกหน้า",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                    new SingleFormFileList() //23
                   {
                        FileName = "APP_FACTORY_TYP2_INDUSTRIAL_WASTE_JURISTICT",
                        FileGroup = "APP_FACTORY_TYPE2_CITIZEN_INFORMATION_SECTION_JURISTICT",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   ShowIfJuristic,
                               }
                            },
                        },
                   },

                    #endregion

                    // *************** END APP_FACTORY_TYPE_2_NEW *************************

                    // APP_ACCOUNTING_SERVICE_RENEW

                    new SingleFormFileList
                    {
                        FileName = "APP_ACCOUNTING_SERVICE_RENEW_PAY_RENEW_BILL",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "ACCOUNTING_SERVICE_STATEMENT_NO_CON",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Note = "กรณีเป็นนิติบุคคลที่จดทะเบียนกับสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ครั้งแรก",
                        Note_2 = "ลงนามรับรองสำเนาถูกต้องและประทับตราบริษัท (ถ้ามี)",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_COPY_COLLATERAL_ACC_RENEW",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS_ACC_RENEW",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Note = "- กรณีงบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็นโดยผู้สอบบัญชี",
                        Note_2 = "- ลงนามรับรองสำเนาถูกต้องและประทับตราบริษัท (ถ้ามี)",
                        Required = true,
                    },

                    // END APP_ACCOUNTING_SERVICE_RENEW

                    // APP_ACCOUNTING_SERVICE_CANCEL

                    new SingleFormFileList
                      {
                        FileName = "APP_ACCOUNTING_SERVICE_CANCEL_PAY_RENEW_BILL",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                      },
              
                    // END 

                    // APP_ACCOUNTING_SERVICE_EDIT

                    new SingleFormFileList
                      {
                        FileName = "APP_ACCOUNTING_SERVICE_EDIT_PAY_RENEW_BILL",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                     ShowIfJuristic,
                                },
                            },
                        },
                      },

                    new SingleFormFileList()
                    {

                        FileName = "APP_ACC_EDIT_LOST_DOC",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = false,
                        Note = "กรณีหนังสือรับรองนิติบุคคลหาย"

                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ACC_EDIT_REQUITE_TO_EDIT_DOC",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                                 AllowMultipleMinItem = 1,
                           },
                        },
                    },

                    // END APP_ACCOUNTING_SERVICE_EDIT

                    // APP_ORGANIC_PLANT_PRODUCTION_NEW
                    //new SingleFormFileList()
                    //{
                    //    FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_JURISTICT_DOC",
                    //    FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_JUSRISTICT",
                    //    Required = true,
                    //    Note = "กรณีขอใช้เครื่องหมายรับรองต้องแสดงฉลากและการกล่าวอ้างด้วย",
                    //    Config = new SingleFormFileConfig
                    //    {
                    //       DisplayCondition = new SingleFormFileConfig.ConditionConfig
                    //        {
                    //           InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                    //           {
                    //               ShowIfJuristic,
                    //               new SingleFormFileConfig.ConditionConfig
                    //               {
                    //                    Condition = new SingleFormFileConfig .ConditionItem
                    //                    {
                    //                        Data = new SingleFormDataItem
                    //                        {
                    //                                SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION",
                    //                                DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT_OPTION",
                    //                        },
                    //                        ExpectedValue = "1",
                    //                    },
                    //               },
                    //           }
                    //        },
                    //       ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                    //       {
                    //             AllowMultipleEqualToSectionItem = true,
                    //             AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                    //             AllowMultipleMinItem = 1,
                    //       },
                    //    },
                    //},
                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_CITIZEN_DOC",
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_CITIZEN",
                        Required = false,
                        Note = "กรณีขอใช้เครื่องหมายรับรองต้องแสดงฉลากและการกล่าวอ้างด้วย",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION",
                                                    DataName = "DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_ORG_AG",
                                            },
                                            ExpectedValue = "1",
                                        },
                                   },
                               }
                            },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items                                 
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH1",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK1",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH2",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK2",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH3",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK3",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH4",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK4",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH5",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK5",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH6",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK6",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH7",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK7",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH8",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK8",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH9",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK9",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH",
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH10",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {

                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK10",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },
                    // END APP_ORGANIC_PLANT_PRODUCTION_NEW
                    
                    // APP_HEALTH                    
                     //new SingleFormFileList()
                     //{
                     //   FileGroup = "HEALTH_CARE_FILE_SECTION_RENEW",
                     //   FileName = "HEALTH_CARE_SERVICE_PROVIDER_ID_CARD_RENEW",
                     //   Note = "กรุณาอัปโหลดไฟล์  เอกสารที่มีการแก้ไขเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                     //   Config = new SingleFormFileConfig
                     //   {
                     //       DisplayCondition = new SingleFormFileConfig.ConditionConfig
                     //       {
                     //           InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                     //           {
                     //                ShowIfCitizen,
                     //           },
                     //       },
                     //       ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                     //       {
                     //           AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items      
                     //           AllowMultipleMinItem = 1,
                     //       },
                     //   },
                     //},

                     //new SingleFormFileList()
                     //{
                     //   FileGroup = "HEALTH_CARE_FILE_SECTION_RENEW",
                     //   FileName = "HEALTH_CARE_MANAGE_ID_CARD_RENEW",
                     //   Note = "กรุณาอัปโหลดไฟล์  เอกสารที่มีการแก้ไขเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                     //   Config = new SingleFormFileConfig
                     //   {
                     //       DisplayCondition = new SingleFormFileConfig.ConditionConfig
                     //       {
                     //           InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                     //           {
                     //                ShowIfCitizen,
                     //           },
                     //       },
                     //       ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                     //       {
                     //           AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items      
                     //           AllowMultipleMinItem = 1,
                     //       },
                     //   },
                     //},

                    new SingleFormFileList()
                    {
                        FileName = "APP_HEALTH_RENEW_04",
                        FileGroup = "APP_HEALTH_RENEW",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_HEALTH_RENEW_02",
                        FileGroup = "APP_HEALTH_RENEW",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HEALTH_RENEW_03",
                        FileGroup = "APP_HEALTH_RENEW",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items      
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_HEALTH_RENEW_05",
                        FileGroup = "APP_HEALTH_RENEW",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items      
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList() // Last box
                     {
                        FileName = "APP_HEALTH_RENEW_01",
                        FileGroup = "APP_HEALTH_RENEW",
                        Required = true,
                     }, 

                    // APP_HEALTH_EDIT
                     new SingleFormFileList()
                     {
                        FileGroup = "APP_HEALTH_EDIT",
                        FileName = "APP_HEALTH_EDIT_DOC_DETAIL",
                        Note = "กรุณาอัปโหลดไฟล์  เอกสารที่มีการแก้ไขเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig
                        {
                            //DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            //{
                            //    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                            //    {
                            //         ShowIfCitizen,

                            //    },
                            //},
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items      
                                 AllowMultipleMinItem = 1,
                            },
                        },
                     },

                     new SingleFormFileList()
                     {
                        FileGroup = "APP_HEALTH_EDIT",
                        FileName = "APP_HEALTH_EDIT_OLD_DOC_ALLOW",
                        Required = true,
                        //Config = new SingleFormFileConfig
                        //{
                        //    DisplayCondition = new SingleFormFileConfig.ConditionConfig
                        //    {
                        //        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                        //        {
                        //             ShowIfCitizen,
                        //        },
                        //    },
                        //},
                     },

                     new SingleFormFileList()
                     {
                        FileGroup = "APP_HEALTH_EDIT",
                        FileName = "APP_HEALTH_EDIT_MUST_TO",
                        Required = true,
                        //Config = new SingleFormFileConfig
                        //{
                        //    DisplayCondition = new SingleFormFileConfig.ConditionConfig
                        //    {
                        //        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                        //        {
                        //             ShowIfCitizen,
                        //        },
                        //    },
                        //},
                     },

                    new SingleFormFileList() // Last box
                    {
                        FileGroup = "APP_HEALTH_EDIT",
                        FileName = "APP_HEALTH_EDIT_CURRENT_DOC_ALLOW",
                        Required = true,

                    },
                    // END APP_HEALTH
                    new SingleFormFileList
                    {
                        FileName = "FREE_DOC", FileGroup = "FREE_DOC_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000, // Unlimit items
                            }
                        }
                    },

                    #region APP_HOSPITAL
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_STORE",
                        FileGroup = "HOSPITAL_FILE_GROUP",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "แสดงที่ตั้งของสถานพยาบาลและสิ่งปลูกสร้าง บริเวณใกล้เคียง",
                        Note_3 = "กรุณาส่งเอกสารตัวจริงทางไปรษณีย์",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PLAN",
                        FileGroup = "HOSPITAL_FILE_GROUP",
                        Required = true,
                        Note = "แบบร่างทางสถาปัตยกรรม ระบบไฟฟ้า-ประปา ระบบระบายอากาศ ระบบระบายน้ำ ระบบบำบัดน้ำเสีย เส้นทางหนีไฟ ผังหลัก และผังบริเวณของสถานพยาบาลที่จะก่อสร้าง ในส่วนของแบบแปลนให้จัดส่งดังนี้</br>" +
                                "1.แบบแปลนในรูปเอกสาร (มาตราส่วนไม่เล็กกว่า 1:200)</br>" +
                                "2.แบบแปลนในรูปอิเล็กทรอนิกส์ไฟล์(Program AutoCAD)</br>" +
                                "3.สรุปพื้นที่ใช้สอยของแต่ละห้อง แต่ละชั้น",
                        Note_2 = "ประทับตรานิติบุคคล และลงนาม",
                        Note_3 = "กรุณาส่งเอกสารตัวจริงทางไปรษณีย์",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_EIA",
                        FileGroup = "HOSPITAL_FILE_GROUP",
                        Required = false,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMIT",
                        FileGroup = "HOSPITAL_FILE_GROUP",
                        Required = true,
                        Note = "ระบุวัตถุประสงค์เพื่อใช้เป็นอาคารโรงพยาบาล หรือหนังสือแจ้งความประสงค์จะก่อสร้าง ดัดแปลง หรือรื้อถอนอาคาร (มาตรา 39 ทวิ)",
                        Note_2 = "ลงนามรับรองสำเนาถูกต้อง",
                    },
                    #endregion

                    #region APP_LAW_OFFICE

                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_NAME_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_LOCATION_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Note = "ภาพถ่ายขนาดโปสการ์ด 4x6 นิ้ว", //อันเดิมคือ ขนาดรูปภาพ 1200x1600
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_LOCATION_INSIDE_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Note = "ขนาดรูปภาพ 1200x1600",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_LICENSE_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_LAWYER_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Note = "รูปถ่ายสี หน้าตรง ไม่ใส่หมวก สวมชุดครุยเนติบัณฑิตยสภา ขนาด 2 นิ้ว",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_PICTURE_LAWYER",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Note = "รูปถ่ายสี หน้าตรง ไม่ใส่หมวก สวมชุดครุยเนติบัณฑิตยสภา ขนาด 2 นิ้ว",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_IMPORTANT_BRAND_PHOTO",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_CANCEL_CERTIFICATE_LAWYER",
                        FileGroup = "LAW_OFFICE_CANCEL_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "LAW_OFFICE_CANCEL_MANAGER_ALLOWANCE",
                        FileGroup = "LAW_OFFICE_CANCEL_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_APPOINT_CHIEF_LAWYER",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "COMMITTEE_INFORMATION_LAWYER",
                                                DataName = "HAVE_JURISTIC_COMMITTEE_IS_LAWYER_HAVE_JURISTIC_COMMITTEE_IS_LAWYER__TRUE",
                                            },
                                            ExpectedValue = "false",
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_NOT_A_LAWYER",
                        FileGroup = "LAW_OFFICE_FILE_GROUP",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "GENERAL_INFORMATION",
                                                DataName = "APP_LAW_OFFICE_GENERAL_INFORMATION_LICENSE_NUMBER",
                                            },
                                            ExpectedValue = "",
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_LAWYER",
                        FileGroup = "APP_LAW_OFFICE_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีแจ้งทนายเข้า-ออก",
                        Note_2 = "สีหน้าตรง ไม่ใส่หมวก สวมชุดครุยเนติบัณฑิตยสภา ขนาด 2 นิ้ว"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_CONFIRM_EDIT_DOCUMENT_CHANGE",
                        FileGroup = "APP_LAW_OFFICE_EDIT_FILE_GROUP",
                        Required = true,
                        Note = "กรณีแจ้งเปลี่ยนแปลงชื่อเจ้าของ/หัวหน้าสำนักงาน",
                        Note_2 = "เอกสาร เช่น หนังสือยินยอมเจ้าของเดิม หรือหัวหน้าสำนักงานคนเดิมพร้อมคำรับรองของเจ้าของ/หัวหน้าคนใหม่",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_OFFICE",
                        FileGroup = "APP_LAW_OFFICE_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีเปลี่ยนแปลงที่อยู่สำนักงาน",
                        Note_2 = "ขนาดรูปภาพ 1200x1600",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_ADDRESS_NO",
                        FileGroup = "APP_LAW_OFFICE_EDIT_FILE_GROUP",
                        Required = false,
                        Note = "กรณีเปลี่ยนแปลงที่อยู่สำนักงาน",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_LAW_OFFICE_CONFIRM_EDIT_AGREE",
                        FileGroup = "APP_LAW_OFFICE_EDIT_FILE_GROUP",
                        Required = false,
                        DocFormUrl = "https://www.lawyerscouncil.or.th/news/wp-content/uploads/2016/10/16-Reg-16.pdf",
                        Note = "กรณีเปลี่ยนแปลงที่อยู่สำนักงาน",
                    },

                    #endregion

                    #region [ 37 ] APP_ELECTRONIC_COMMERCIAL

                    #region NEW

                    new SingleFormFileList
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_RENTAL_CONTRACT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT),
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                                                DataName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                            },
                                            ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                        },
                                    },
                                },
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_HOUSEHOLD_RENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = GetBuildingConditionCustom("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION", "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", StoreInformationBuildingTypeOptionValueConst.OWNED),
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_PICTURE_INDEX_WEBSITE",
                        FileGroup = "COMMERCIAL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfElectronicCommercial,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_PICTURE_PRODUCT",
                        FileGroup = "COMMERCIAL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfElectronicCommercial,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_PICTURE_METHOD_ORDER_AND_PAYMENT",
                        FileGroup = "COMMERCIAL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfElectronicCommercial,
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_DOCUMENT_TRADE",
                        FileGroup = "COMMERCIAL_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        }
                    },
                    #endregion

                    #region EDIT
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_EDIT_CURRENT_PERMIT",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_EDIT_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_EDIT_DOCUMENT_CHANGE",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_EDIT_SECTION",
                        Required = true,
                        Note = "กรุณาอัปโหลดไฟล์เอกสารที่มีการเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    #endregion

                    #region CANCEL
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_CANCEL_CURRENT_PERMIT",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_CANCEL_OWNER_DEATH",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION",
                        Required = false,
                        Note = "กรณีผู้ประกอบพาณิชยกิจถึงแก่กรรม",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_CANCEL_COUNT_ORDERED",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION",
                        Required = false,
                        Note = "กรณีผู้ประกอบพาณิชยกิจวิกลจริต และ/หรือ หายสาบสูญ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_CANCEL_HEIR",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION",
                        Required = false,
                        Note = "กรณีผู้ประกอบพาณิชยกิจถึงแก่กรรม",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ELECTONIC_COMMERCIAL_CANCEL_STOP_BUSINESS",
                        FileGroup = "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION",
                        Required = false,
                        Note = "กรณีผู้ขอเลิกทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์ เป็นห้างหุ้นส่วนสามัญ คณะบุคคล และกิจการร่วมค้า",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    #endregion

                    #endregion


                    #region [ 39 ] APP TRANSPORT NON REGULAR TRUCK

                    #region [ NEW ]

                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_DOC",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.OWNED),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_ID_CARD",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_HOUSEHOLD_RENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_RENTAL_CONTRACT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานของนิติบุคคล",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            IsOr = true,
                                            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            {
                                                GetBuildingConditionCustom(
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                                    StoreInformationBuildingTypeOptionValueConst.RENT),
                                                GetBuildingConditionCustom(
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                                    StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            },
                                        },
                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            Condition = new SingleFormFileConfig.ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                    SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                                    DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                                },
                                                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_USAGE_AGREEMENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },

                    #endregion

                    #region [ RENEW ]

                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_DOC",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.OWNED),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_ID_CARD",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_HOUSEHOLD_RENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น หรือ ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_RENTAL_CONTRACT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานที่ผู้อื่น",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT),
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีเช่าอาคารสถานของนิติบุคคล",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            IsOr = true,
                                            InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                            {
                                                GetBuildingConditionCustom(
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                                    StoreInformationBuildingTypeOptionValueConst.RENT),
                                                GetBuildingConditionCustom(
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                                    StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                            },
                                        },
                                        new SingleFormFileConfig.ConditionConfig
                                        {
                                            Condition = new SingleFormFileConfig.ConditionItem
                                            {
                                                Data = new SingleFormDataItem
                                                {
                                                    SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                                    DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                                },
                                                ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_USAGE_AGREEMENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_FILE_SECTION",
                        Note = "กรณีใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                    new SingleFormDataItem { DataName = "ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS_TEXT" },
                                    new SingleFormDataItem { DataName = "ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS" },
                                },
                                AdvancedCondition = new SingleFormFileConfig.ConditionConfig
                                {
                                    IsOr = true,
                                    InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                    {
                                        GetBuildingConditionCustom(
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC_OPTION",
                                            "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION",
                                            StoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                    },
                                },
                            },
                        },
                    },

                    #endregion

                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_LABEL",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ขนาด 7.6x12.70 ซม.",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_WITHIN",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ขนาด 7.6x12.70 ซม.",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_ENTRANCE",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ขนาด 7.6x12.70 ซม. เพื่อให้เจ้าหน้าที่ออกตรวจสอบสถานที่จริงให้ตรงกับเอกสารหลักฐานที่ยื่นขอ",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_PARKING",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ขนาด 7.6x12.70 ซม. เพื่อให้เจ้าหน้าที่ออกตรวจสอบสถานที่จริงให้ตรงกับเอกสารหลักฐานที่ยื่นขอ",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_TRANSPORTATION_VOLUME",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "แสดงถึงการรับจ้างขนส่งสินค้าของผู้ขออนุญาตฯ",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CERTIFICATION",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ลงนามสำเนาถูกต้อง กรุณายื่นบันทึกคำรับรองตัวจริง 2 ฉบับ ในวันที่เข้าพบเจ้าหน้าที่",
                        DocFormUrl = "http://samutprakan.dlt.go.th/e-Form_files/pbl_rubrong_70.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_SECURITIES",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "ลงนามสำเนาถูกต้อง กรุณายื่นบันทึกคำรับรองตัวจริง 1 ฉบับ ในวันที่เข้าพบเจ้าหน้าที่",
                        DocFormUrl = "http://samutprakan.dlt.go.th/e-Form_files/pbl_ctr.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PURCHASE_REQUEST",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2",
                                        DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CHECK_CAR_SECTION",
                                    },
                                    ExpectedValue = "true",
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PURCHASE_REQUEST_RENEW",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION_2",
                                        DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION_2_CHECK_CAR_SECTION",
                                    },
                                    ExpectedValue = "true",
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CAR_LICENSE",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE" },
                                FilterDataItemText = "HAVE_CAR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_REQUEST_INFORMATION_CAR_LICENSE",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_LICENSE" },
                                },
                                FilterDataItem = new SingleFormDataItem { DataName = "DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_REQUEST_TYPE" },
                                FilterDataItemText = "HAVE_CAR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_1",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "สำเนาทะเทียนรถ กรณีซื้อใหม่ให้แนบใบสั่งซื้อรถ",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "ARR_IDX" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_2",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        DocFormUrl = "~/Uploads/apps/APP_TRANSPORT_NON_REGULAR_TRUCK/Page5.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_3",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "เครื่องหมายที่จะต้องติดบนตัวรถหลังจากได้รับใบอนุญาต",
                        DocFormUrl = "~/Uploads/apps/APP_TRANSPORT_NON_REGULAR_TRUCK/Page5.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CURRENT_DOCUMENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_FILE_SECTION",
                        Note = "กรุณานำใบอนุญาตประกอบการขนส่งไม่ประจําทางตัวจริงไปด้วยในวันที่เข้าพบเจ้าหน้าที่",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_DOCUMENT_CHANGE",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_FILE_SECTION",
                        Note = "กรุณาอัปโหลดไฟล์  เอกสารที่มีการแก้ไขเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_CURRENT_DOCUMENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_FILE_SECTION",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_CURRENT_DOCUMENT",
                        FileGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_FILE_SECTION",
                    },
                    #endregion

                    #region [ 40 ] APP_SECIURITIES_BUSINESS
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_PROFILE",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "ในกรณีที่เป็นนิติบุคคลตามกฏหมายไทย ไม่จำเป็นต้องอัปโหลดเอกสารดังกล่าว",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "ในกรณีที่เป็นนิติบุคคลตามกฏหมายไทย ไม่จำเป็นต้องอัปโหลดเอกสารดังกล่าว",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_ID",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "ในกรณีที่เป็นบุคคลสัญชาติไทย ไม่จำเป็นต้องอัปโหลดเอกสารดังกล่าว",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_CRIME",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_CLUE_DOC",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "กรณีกรรมการเป็นข้าราชการที่มีหน้าที่เกี่ยวกับการควบคุมบริษัทหลักทรัพย์ พนักงานของธนาคารแห่งประเทศไทย พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_BENEFIT",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเป็นสถาบันการเงินที่จัดตั้งขึ้นตามกฎหมายอื่น พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_COMMITEE_DOC",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "กรณีกรรมการเป็นข้าราชการที่มีหน้าที่เกี่ยวกับการควบคุมบริษัทหลักทรัพย์ พนักงานของธนาคารแห่งประเทศไทย พร้อมลงนามรับรองสำเนาถูกต้อง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_LICENSE",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเป็นสถาบันการเงินที่จัดตั้งขึ้นตามกฎหมายอื่น พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_EDIT_DOC",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_EDIT_CHANGE",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION_EDIT",
                        Required = true,
                        Note = "กรุณาอัปโหลดไฟล์เอกสารที่มีการเปลี่ยนแปลงอย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SECURITIES_BUSINESS_CANCEL_DOC",
                        FileGroup = "SECURITIES_BUSINESS_FILE_SECTION_CANCEL",
                        Required = true,
                    },
                    #endregion

                    #region [ 38 ] APP_ENERGY_PRODUCTION

                    #region RENEW
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_RENEW_MAP",
                        FileGroup = "ENERGY_PRODUCTION_RENEW_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_RENEW_PLAN",
                        FileGroup = "ENERGY_PRODUCTION_RENEW_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_RENEW_ENGINEER_LICENSE",
                        FileGroup = "ENERGY_PRODUCTION_RENEW_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_RENEW_ENGINEER_PERMIT",
                        FileGroup = "ENERGY_PRODUCTION_RENEW_FILE_SECTION",
                        Required = false,
                    },
                    #endregion

                    #region EDIT
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_EDIT_MAP",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_EDIT_PLAN",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_EDIT_ENGINEER_LICENSE",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_EDIT_ENGINEER_PERMIT",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_EDIT_DOC_CHANGED",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_BOOK_CHANGED",
                        FileGroup = "ENERGY_PRODUCTION_EDIT_FILE_SECTION",
                        Required = true,
                    },
                    #endregion

                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_MAP",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_MAP_OPTIONAL",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_PLAN",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_PLAN_OPTIONAL",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_DOC",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_ENGINEER_LICENSE",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_DOC_CHANGED",
                        FileGroup = "ENERGY_PRODUCTION_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 0,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_MACHINE_UNINSTALLATION_IMG",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            //DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            //{
                            //    Condition = new SingleFormFileConfig.ConditionItem()
                            //    {
                            //        Data = new SingleFormDataItem
                            //        {
                            //            SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                            //            DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                            //        },
                            //        ExpectedValue = "SOME_NON_RENEWABLE",
                            //    },
                            //},
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_BOOK",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "SOME_NON_RENEWABLE",
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_BOOK",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "ALL",
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_SINGLE_LINE_DIAGRAM",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "SOME_RENEWABLE",
                                }
                            },
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_MAP_TO_PLACE",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "SOME_RENEWABLE",
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_ELECTRIC_ENGINEER_LICENSE",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = false,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "SOME_RENEWABLE",
                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_PRODUCTION_CANCEL_ELECTRIC_CONTROL_ENGINEER_LICENSE",
                        FileGroup = "ENERGY_PRODUCTION_CANCEL_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig()
                            {
                                Condition = new SingleFormFileConfig.ConditionItem()
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                                        DataName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                    },
                                    ExpectedValue = "SOME_RENEWABLE",
                                }
                            }
                        }
                    },
                    #endregion

                    #region [ 45 ] APP_ACCOUNTING_SERVICE
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_STATEMENT",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Note = "กรณีเป็นนิติบุคคลที่จดทะเบียนกับสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ครั้งแรก",
                        Note_2 = "ลงนามรับรองสำเนาถูกต้องและประทับตราบริษัท (ถ้ามี)",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_ACCOUNTING_DETAIL_DETAIL_SECTION",
                                                DataName = "APP_ACCOUNTING_DETAIL_DETAIL_SECTION_REASON_OPTION",
                                            },
                                            ExpectedValue = "FIRST_TIME",
                                        }

                                    },

                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_PAYMENT_PROOF",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_COPY_COLLATERAL",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Note = "กรณีงบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็นโดยผู้สอบบัญชี",
                        Note_2 = "ลงนามรับรองสำเนาถูกต้องและประทับตราบริษัท (ถ้ามี)",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                                                DataName = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_STATEMENT_OPTION",
                                            },
                                            ExpectedValue = "FORECAST",
                                        }

                                    },

                                }
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_DETAIL_PROOF",
                        FileGroup = "ACCOUTING_SERVICE_FILE_GROUP",
                        Required = true,
                    },
                    #endregion

                    #region [ 46 ] APP TOURISM BUSINESS
                    
                    #region NEW
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_PICTURE_OFFICE_CITIZEN",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "กรุณาถ่ายให้เห็นป้ายชื่อสำนักงานภาษาไทย และเลขที่ตั้งสำนักงาน อย่างชัดเจน",
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page12.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_PICTURE_OFFICE_JURISTIC",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "กรุณาถ่ายให้เห็นป้ายชื่อสำนักงานภาษาไทย และเลขที่ตั้งสำนักงาน อย่างชัดเจน",
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page25.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_PICTURE_INSIDE_OFFICE",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "ผู้มีอำนาจลงลายมือชื่อ",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_LOCATION_OFFICE_CITIZEN",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "ผู้มีอำนาจลงลายมือชื่อ",
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page13.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_LOCATION_OFFICE_JURISTIC",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "ผู้มีอำนาจลงลายมือชื่อ",
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page13.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_DOCUMENT_OWNERD_AREA",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_COPY_DOCUMENT_POLICY",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Note = "'อายุกรมธรรม์ไม่น้อยกว่า 6 เดือนนับแต่วันที่ยื่นคำขอ",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_DOCUMENT_GUARANTEE_BUSINESS",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_TOURISM_BUSINESS_INFO_SECTION",
                                                    DataName = "DROPDOWN_APP_TOURISM_BUSINESS_INFO_SECTION_GUARANTEE",
                                            },
                                            ExpectedValue = "BA",
                                        },
                                    },
                                }
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_SEAL_COMPANY",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page24.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_CITIZEN",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page17.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfCitizen,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_JURISTIC",
                        FileGroup = "APP_TOURISM_BUSINESS_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_TOURISM_BUSINESS/Page30.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_COMPANY_RULES",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีที่มีเอกสารดังกล่าว กรุณาอัปโหลด พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_MEETING_REPORT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีที่มีเอกสารดังกล่าว กรุณาอัปโหลด พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_TOURISM_BUSINESS_REPORT_AMENDMENT",
                        FileGroup = "JURISTIC_COMMITTEE_FILE_SECTION",
                        Required = false,
                        Note = "กรณีที่มีเอกสารดังกล่าว กรุณาอัปโหลด พร้อมลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "ตัวอย่างเอกสาร เช่น บอจ.4 แก้ไข บอจ.2 แก้ไข",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = ShowIfJuristic,
                        },
                    },
                    #endregion

                    #endregion

                    #region [ 48 ] APP_ENERGY_NOT_PERMIT
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_REGISTATION_AND_RESEARCH_BUILDING",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = true,
                        Note = "วิศวกรลงนามรับรองตามกฎหมายว่าด้วยวิชาชีพวิศวกรรม"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_CIVIL_ENGINEERING_PLAN",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = true,
                        Note = "วิศวกรลงนามรับรองตามกฎหมายว่าด้วยวิชาชีพวิศวกรรม"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_CIVIL_ENGINEERING_LICENSE",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_PRODUCTION_LICENSE",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = false,
                        Note = "กรณีเข้าข่ายต้องได้รับใบอนุญาต พ.ค. 2"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_PRODUCTION_PLAN",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = false,
                        Note = "กรณีไม่เข้าข่ายต้องได้รับใบอนุญาต พ.ค. 2",
                        Note_2 = "แสดงวงจรของระบบผลิตไฟฟ้า ระบบป้องกัน และระบบควบคุมไฟฟ้า โดยมวิศวกรลงนามรับรองตามกฎหมายว่าด้วยวิชาชีพวิศวกรรม"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_PRODUCTION_ENGINEERING_LICENSE",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = false,
                        Note = "กรณีไม่เข้าข่ายต้องได้รับใบอนุญาต พ.ค. 2"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_CODE_OF_PRACTICE",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_ELECTRIC_CONTRACT",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ENERGY_NOT_PERMIT_STORE_DETAIL",
                        FileGroup = "ENERGY_NOT_PERMIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1,
                            }
                        }
                    },
                    #endregion

                    #region [ 41 ] SOFTWARE HOUSE

                    #region [NEW]
                    new SingleFormFileList()
                    {
                        FileName = "SYSTEM_STRUCTURE_DOC",
                        FileGroup = "SOFTWARE_HOUSE_DOC",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SOFTWARE_MANUAL",
                        FileGroup = "SOFTWARE_HOUSE_DOC",
                        Required = true,
                        FileSize = new FileSizeConfig
                        {
                            MaxFileSize = "20mb",
                        },
                    },
                    #endregion

                    #region [EDIT]
                    new SingleFormFileList()
                    {
                        FileName = "SYSTEM_STRUCTURE_DOC_EDIT",
                        FileGroup = "SOFTWARE_HOUSE_DOC_EDIT",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "SOFTWARE_MANUAL_EDIT",
                        FileGroup = "SOFTWARE_HOUSE_DOC_EDIT",
                        Required = true,
                        FileSize = new FileSizeConfig
                        {
                            MaxFileSize = "20mb",
                        },
                    },
                    #endregion


                    #endregion

                    #region [ 42 ] APP CLINIC
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_VERIFY_DOC",
                        FileGroup = "CLINIC_DOC",
                        Required = false,
                        Note = "แบบการตรวจมาตรฐานการบริการลักษณะ และการประกอบกิจการสถานพยายาล (การตรวจมาตรฐานคุณภาพบริการในสถานพยาบาลประเภทที่ไม่รับผู้ป่วยค้างคืน)"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_PERSONAL_TABLE",
                        FileGroup = "CLINIC_DOC",
                        Required = true,
                        Note = "แบบการตรวจมาตรฐานการบริการลักษณะ และการประกอบกิจการสถานพยายาล (การตรวจมาตรฐานคุณภาพบริการในสถานพยาบาลประเภทที่ไม่รับผู้ป่วยค้างคืน)"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_EDIT_DOC",
                        FileGroup = "CLINIC_DOC",
                        Required = true
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_PROFILE_IMG",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = false,
                        Note = "หน้าตรง แต่งการสุภาพ ไม่สวมหมวกหรือแว่นตาดำ ซึ่งถ่ายไว้ไม่เกิน 1 ปึ และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Note_2 = "กรณีหมดเนื้อที่การต่ออายุใบดำเนินการ"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_ID_CARD",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_MEDICAL_CERT",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_PROFILE_IMG",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                        Note = "จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_LICENSE",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OWNER_CERT",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_CANCEL_DETAIL_DOC",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_CANCEL_CANCEL_DETAIL",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_CANCEL_FILM_OTHER",
                        FileGroup = "CLINIC_DOC_CON",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_VIDEO_FOR_CONSIDERATION",
                        FileGroup = "APP_CLINIC_LICENSE_FILE_SECTION",
                        Required = true,
                        FileSize = new FileSizeConfig
                        {
                            MaxFileSize = "50mb",
                        },
                    },
                    
                    #endregion

                    #region [ 42.1 ] APP_CLINIC
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_ID_CARD",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_HOUSEHOLD",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                     new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_MED_CERT",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_PHOTO3M",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_PHOTO13M",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Note = "ถ่ายไว้ไม่เกินหนึ่งปี และต้องนำภาพจริงแนบมา 1 รูปในวันที่พบหน่วยงาน",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_TRADITIONAL_CERT",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_DIPLOMA",
                        FileGroup = "APP_CLINIC_OWNED_FILE_SECTION",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },










                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_ID_CARD_CON",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_HOUSEHOLD_CON",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_MED_CERT",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_PHOTO3M",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_TRADITIONAL_CERT",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_CLINIC_EDIT_OPERATOR_SECTION", DataName = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_EDIT_DIPLOMA",
                        FileGroup = "APP_CLINIC_EDIT_OPARETION_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_EDIT_CERTIFICATE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_EDIT_CERTIFICATE_SECTION_TYPE_TEXT" },
                                    new SingleFormDataItem { DataName = "APP_CLINIC_EDIT_CERTIFICATE_SECTION_BRANCH" },
                                    new SingleFormDataItem { DataName = "APP_CLINIC_EDIT_CERTIFICATE_SECTION_NUMBER" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_EDIT_DOC",
                        FileGroup = "APP_CLINIC_EDIT_FILE_SECTION",
                        Note = "กรณีแก้ไขรายละเอียดใบอนุญาตประกอบกิจการสถานพยาบาล",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_CLINIC_EDIT_INFO_SECTION",
                                        DataName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_DETAIL",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    #region CONDITION DOC

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_PERMISSION_ID_CARD", FileGroup = "APP_CLINIC_PERMISSION_FILE_SECTION",
                        Required = true,
                        Note = "กรุณาแนบเอกสารตามรายการด้านล่างในรูปแบบ Zip File ประกอบไปด้วย" + 
                        " 1.หนังสือแสดงความจำนงเป็นผู้ปฎิบัติงานในสถานพยาบาลของผู้ประกอบวิชาชีพ" + 
                        " 2.เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง" + 
                        " 3. ทะเบียนบ้าน" +
                        " 4. ใบอนุญาตประกอบวิชาชีพ/ประกอบโรคศิลปะ" +
                        " 5.วุฒิบัตร หรือหนังสืออนุมัติจากสภาวิชาชีพ" +
                        " 6.รูปถ่ายขนาด 2.5 x 3 เซนติเมตร ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 1 รูปวันที่พบหน่วยงาน" + 
                        " 7.อื่นๆ",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_CLINIC_OWNED_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "EMPLOYEE",
                            },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_LICENSE_LOCATION_PHOTO",
                        FileGroup = "APP_CLINIC_LICENSE_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_PLAN_INSIDE",
                        FileGroup = "APP_CLINIC_LICENSE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "แสดงที่ตั้งของสถานพยาบาลและสิ่งปลูกสร้าง บริเวณใกล้เคียง",
                        Note_3 = "กรุณาส่งเอกสารตัวจริงทางไปรษณีย์",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_LOCATION_MAP",
                        FileGroup = "APP_CLINIC_LICENSE_FILE_SECTION",
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัพโหลดมาด้วย",
                        Required = true,
                    },

                    #endregion

                    #endregion

                    #region [ 42.2 ] APP_CLINIC

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_ID_CARD",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_HOUSEHOLD",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_MED_CERT",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_NAME_CHANGE_COPY",
                        FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_DOCTOR_SMARTCARD_COPY",
                        FileGroup = "CITIZEN_INFORMATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_PHOTO3M",
                        FileGroup = "CITIZEN_INFORMATION",
                        Required = true,
                        FileFilter = "jpg,png"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_PHOTO13M",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Note = "ถ่ายไว้ไม่เกินหนึ่งปี และต้องนำภาพจริงแนบมา 1 รูปในวันที่พบหน่วยงาน",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_TRADITIONAL_CERT",
                        FileGroup = "CITIZEN_INFORMATION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_DIPLOMA",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                                AllowMultipleMinItem = 1,
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_CLINIC_OPERATION_CERTIFICATION",
                        FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                    },

                    #region CONDITION DOC

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_PERMISSION_ID_CARD", FileGroup = "APP_CLINIC_RENEW_EMPLOYEE_FILE_SECTION",
                        Required = true,
                        Note = "1.หนังสือแสดงความจำนงเป็นผู้ปฎิบัติงานในสถานพยาบาลของผู้ประกอบวิชาชีพ " +
                        "2.เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง " +
                        "3. ทะเบียนบ้าน " +
                        "4. ใบรับรองแพทย์ " +
                        "5. ใบอนุญาตประกอบวิชาชีพ/ประกอบโรคศิลป " +
                        "6.วุฒิบัตร หรือหนังสืออนุมัติจากสภาวิชาชีพ " +
                        "7. รูปถ่ายขนาด 2.5 x 3 เซนติเมตร ถ่ายไว้ไม่เกินหนึ่งปี และต้องนำภาพจริงแนบมา 1 รูปในวันที่พบหน่วยงาน" +
                        "8. รูป 8 x 13 เซนติเมตร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_CLINIC_RENEW_INFO_SECTION",
                                                        DataName = "APP_CLINIC_RENEW_INFO_SECTION_PURPOSE__EMPLOYEE",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_LICENSE_LOCATION_PHOTO",
                        FileGroup = "APP_CLINIC_RENEW_OPARETION_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_PLAN_INSIDE",
                        FileGroup = "APP_CLINIC_RENEW_OPARETION_FILE_SECTION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "แสดงที่ตั้งของสถานพยาบาลและสิ่งปลูกสร้าง บริเวณใกล้เคียง",
                        Note_3 = "กรุณาส่งเอกสารตัวจริงทางไปรษณีย์",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_OWNED_LOCATION_MAP",
                        FileGroup = "APP_CLINIC_RENEW_OPARETION_FILE_SECTION",
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัพโหลดมาด้วย",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_RENEW_PERMISSION_CITIZEN_ID_CARD", FileGroup = "APP_CLINIC_RENEW_OPARETION_FILE_SECTION",
                        Required = true,
                        Note = "1.หนังสือแสดงความจำนงเป็นผู้ปฎิบัติงานในสถานพยาบาลของผู้ประกอบวิชาชีพ " +
                        "2.เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง " +
                        "3. ทะเบียนบ้าน " +
                        "4. ใบรับรองแพทย์ " +
                        "5. ใบอนุญาตประกอบวิชาชีพ/ประกอบโรคศิลป " +
                        "6.วุฒิบัตร หรือหนังสืออนุมัติจากสภาวิชาชีพ " +
                        "7. รูปถ่ายขนาด 2.5 x 3 เซนติเมตร ถ่ายไว้ไม่เกินหนึ่งปี และต้องนำภาพจริงแนบมา 1 รูปในวันที่พบหน่วยงาน" +
                        "8. รูป 8 x 13 เซนติเมตร",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    ShowIfJuristic,
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            new SingleFormFileConfig.ConditionConfig
                                            {
                                                Condition = new SingleFormFileConfig.ConditionItem
                                                {
                                                    Data = new SingleFormDataItem
                                                    {
                                                        SectionName = "APP_CLINIC_RENEW_INFO_SECTION",
                                                        DataName = "APP_CLINIC_RENEW_INFO_SECTION_PURPOSE__OPARETION",
                                                    },
                                                    ExpectedValue = "true",
                                                }
                                            },
                                        }
                                    },
                                },
                            }
                        }
                    },
                    #endregion
                    #endregion

                    #region [ 47 ] APP HOSPITAL PERMISSION
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_VERIFY_DOC",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Note = "แบบการตรวจมาตรฐานการบริการลักษณะ และการประกอบกิจการสถานพยายาล (การตรวจมาตรฐานคุณภาพบริการในสถานพยาบาลประเภทที่รับผู้ป่วยค้างคืน)",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_PERSONAL_TABLE",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/ตารางบุคลากร.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_ARCHITECTURAL_PLAN",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_AREA_DETAIL",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = true,
                        Note = "กรุณาแนบเป็น File Word เท่านั้น",
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/แบบฟอร์มการเปลี่ยนแปลงพื้นที่ใช้สอย_ต่ออายุ.pdf",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_BUILDING_PERMIT_1",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_BUILDING_PERMIT_6",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_XRAY_PERMIT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_STATISTIC_OF_POLLUTION",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_REPORT_OF_POLLUTION",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_CALIBRATION_DOC",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_AMBULANCE_PERMIT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_BUILDING_VERIFY_CERT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_EIA_HIA_REPORT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_OPARETION",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_PROFILE_IMG",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Required = true,
                        Note = "- หน้าตรง แต่งการสุภาพ ไม่สวมหมวกหรือแว่นตาดำ ซึ่งถ่ายไว้ไม่เกิน 1 ปึ และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Note_2 = "- กรณีหมดเนื้อที่การต่ออายุใบดำเนินการ",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE_EMPLOYEE",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_EDIT_DOC",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_DETAIL",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },                        
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_ID_CARD",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_HOUSEHOLD_REGISTRATION_COPY",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_MEDICAL_CERT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_PROFILE_IMG",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Note = "จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_LICENSE",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION",
                                        DataName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE_PURPOSE_CHANGE_OPERATOR",
                                    },
                                    ExpectedValue = "true"
                                },
                            },
                        },
                        DisplayFormatSources = new SingleFormDataItem[]
                        {
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_TITLE_TEXT"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_FIRSTNAME"  },
                            new SingleFormDataItem() { SectionName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION", DataName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_LASTNAME"  }
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_OWNER_CERT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_CON",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION",
                                DataItems = new SingleFormDataItem[]
                                {
                                    new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_TYPE_TEXT" },
                                    new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_BRANCH" },
                                    new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_NUMBER" },
                                },
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_RESIDUAL_PATIENT_REPORT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_CANCEL_BUSSINESS_TO_MEDIA_REPORT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Note = "โดยประกาศลงสื่อหรือสื่อสิ่งพิมพ์อย่างน้อย 2 ฉบับเป็นระยะเวลาไม่น้อยกว่า 2 วัน"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_MEDICAL_RECORDS_NON_RECEIVE_REPORT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_FORWARD_PATIENT_CONTRACT",
                        FileGroup = "HOSPITAL_PERMISSION_DOC",
                        Required = false,
                        Note = "กรณีที่มีการส่งต่อคนไข้ของโรงพยาบาลไปรักษาที่โรงพยาบาลอื่น"
                    },
                    #endregion

                    #region [ 47.1 ] APP HOSPITAL PERMISSION 

                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_STORE_MAP",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = true,
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัพโหลดมาด้วย"
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_WEATHER",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_BUILDING_NEW",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_BUILDING_NEW_VERIFICATION",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_DRAINAGE",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_EMERGENCY",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_WAY_PLAN",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_TOOLS_PLAN",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_FORM_DOC",
                        FileGroup = "HOSPITAL_PERMISSION_FILE_SECTION",
                        Required = false,
                        Note = "กรุณาแนบเป็นไฟล์ Word เท่านั้น",
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/ฟอร์มการถอดพื้นที่จัดตั้งสถานพยาบาลใหม่.pdf",
                    },

                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_OWNER_ID",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_OWNER_HOUSEHOLD",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_MED_CERT",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_PHOTO",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_PHOTO_BIG",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Note = "ถ่ายไว้ไม่เกินหนึ่งปี และต้องนำภาพจริงแนบมา 1 รูปในวันที่พบหน่วยงาน",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_LICENSE",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_DIPLOMA",
                        FileGroup = "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "OPARETOR",
                            },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_CONFIDENT_DOC",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_FILE_SECTION",
                        Required = true,
                        Note = "กรุณาแนบเอกสารตามรายการด้านล่างในรูปแบบ Zip File ประกอบไปด้วย" +
                        " 1.หนังสือแสดงความจำนงเป็นผู้ปฎิบัติงานในสถานพยาบาลของผู้ประกอบวิชาชีพ" +
                        " 2.เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง" +
                        " 3. ทะเบียนบ้าน" +
                        " 4. ใบอนุญาตประกอบวิชาชีพ/ประกอบโรคศิลปะ" +
                        " 5.วุฒิบัตร หรือหนังสืออนุมัติจากสภาวิชาชีพ" +
                        " 6.รูปถ่ายขนาด 2.5 x 3 เซนติเมตร ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 1 รูปวันที่พบหน่วยงาน" +
                        " 7.อื่นๆ",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                BindToSection = true,
                                SectionName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                                DataItems = new SingleFormDataItem[]
                                            {
                                                new SingleFormDataItem { DataName = "DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME" },
                                                new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME" },
                                            },
                                FilterDataItem = new SingleFormDataItem { DataName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION" },
                                FilterDataItemText = "EMPLOYEE",
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_PERMISSION_NEW_PERSEONNEL_TABLE",
                        FileGroup = "HOSPITAL_PERMISSION_DOC_FILE_SECTION",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/ตารางบุคลากร.pdf",
                    },
                    #endregion

                    #region [ 49 ] APP ORGANIC PLANT PRODUCTION
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_PLANT_PRODUCTION_CURRENT_CERTIFICATE",  //ใบรับรอง(ฉบับปัจจุบัน)
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_JUSRISTICT",
                        Required = true,
                        Note = ""
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_PLANT_PRODUCTION_CHANGED_DOC",  //เอกสารที่มีการเปลี่ยนแปลง
                        FileGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_JUSRISTICT",
                        Required = true,
                        Note = "กรุณาอัปโหลดไฟล์เอกสารที่มีการเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 50,
                                AllowMultipleMinItem = 1
                            }
                        }
                    },

                    #endregion

                    #region [ 49.2 ] APP ORGANIC PLANT PRODUCTION RENEW
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_RENEW_DOC",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = false,
                        Note = "กรณีขอใช้เครื่องหมายรับรองต้องแสดงฉลากและการกล่าวอ้างด้วย",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                       Condition = new SingleFormFileConfig .ConditionItem
                                       {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                                            },
                                            ExpectedValue = "STANDALONE",
                                       },
                                   },
                               }
                           },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000,
                           },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_RENEW_LICENSE_PRESENT",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_1",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_1",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_2",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_2",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_3",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_3",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_4",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_4",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_5",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_5",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_6",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_6",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_7",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_7",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_8",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_8",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_9",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_9",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_10",
                        FileGroup = "APP_ORGANIC_RENEW_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_10",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },
                    #endregion

                    #region [ 49.2 ] Store Condition

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_RENTAL_CONTRACT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = 
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION", 
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_HOUSEHOLD_RENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                        },
                                    },
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                DataName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE_OPTION",
                                            },
                                            ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                        },
                                    },
                                },
                            },
                        },
                    },

                    

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_HOUSEHOLD_STORE",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = 
                                GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.OWNED),
                        },
                    },
                    #endregion

                    #region [ 49.3 ] APP ORGANIC PLANT PRODUCTION EDIT
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_EDIT_DOC",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = false,
                        Note = "กรณีขอใช้เครื่องหมายรับรองต้องแสดงฉลากและการกล่าวอ้างด้วย",
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                       Condition = new SingleFormFileConfig .ConditionItem
                                       {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION",
                                                    DataName = "DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                                            },
                                            ExpectedValue = "STANDALONE",
                                       },
                                   },
                               }
                           },
                           ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                           {
                                 AllowMultipleEqualToSectionItem = true,
                                 AllowMultipleEqualToSectionItemAdjust = 1000,
                           },
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_EDIT_LICENSE_PRESENT",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ORGANIC_CANCEL_LICENSE_PRESENT",
                        FileGroup = "APP_ORGANIC_CANCEL_FILE_SECTION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_1",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_1",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_2",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_2",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_3",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_3",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_4",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_4",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_5",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_5",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_6",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_6",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_7",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_7",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_8",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_8",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_9",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_9",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_10",
                        FileGroup = "APP_ORGANIC_EDIT_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                           {
                               InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                               {
                                   new SingleFormFileConfig.ConditionConfig
                                   {
                                        Condition = new SingleFormFileConfig .ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                    SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                    DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_10",
                                            },
                                            ExpectedValue = "true",
                                        },
                                   },
                               }
                           },
                        },
                    },
                    #endregion

                    #region [ 49.3 ] Store Condition

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_RENTAL_CONTRACT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition =
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                        },
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_HOUSEHOLD_RENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                        },
                                    },
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        IsOr = true,
                                        InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                        {
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                            GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                        },
                                    },
                                    new SingleFormFileConfig.ConditionConfig
                                    {
                                        Condition = new SingleFormFileConfig.ConditionItem
                                        {
                                            Data = new SingleFormDataItem
                                            {
                                                SectionName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                DataName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE_OPTION",
                                            },
                                            ExpectedValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC,
                                        },
                                    },
                                },
                            },
                        },
                    },



                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_HOUSEHOLD_STORE",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                IsOr = true,
                                InnerConditions = new SingleFormFileConfig.ConditionConfig[]
                                {
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT),
                                    GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE),
                                },
                            },
                        },
                    },

                    new SingleFormFileList
                    {
                        FileName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        FileGroup = "INFORMATION_STORE_FILE_SECTION",
                        Config = new SingleFormFileConfig
                        {
                            DisplayCondition =
                                GetBuildingConditionCustom(
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                                "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                                OrganicStoreInformationBuildingTypeOptionValueConst.OWNED),
                        },
                    },


                    #endregion

                    #region [ 43.3 ] APP_ACCOUNTING_SERVICE_EDIT

                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_DOCUMENT_LOST", FileGroup = "APP_ACCOUNTING_SERVICE_EDIT_SECTION",
                        Required = true,
                        Config = new SingleFormFileConfig
                        {
                           DisplayCondition = new SingleFormFileConfig.ConditionConfig
                            {
                                Condition = new SingleFormFileConfig.ConditionItem
                                {
                                    Data = new SingleFormDataItem
                                    {
                                        SectionName = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION",
                                        DataName = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_REASON_FOR_ISSUING_BROKEN_OR_LOST",
                                    },
                                    ExpectedValue = "true"
                                },
                            }
                        }
                    },
                    new SingleFormFileList()
                    {
                        FileName = "ACCOUNTING_SERVICE_CHANGE_DOCUMENT", FileGroup = "APP_ACCOUNTING_SERVICE_EDIT_SECTION",
                        Required = true,
                        Note = "กรุณาอัปโหลดไฟล์เอกสารที่มีการเปลี่ยนแปลง อย่างน้อย 1 เอกสาร",
                        Config = new SingleFormFileConfig()
                        {
                            ShowMultiple = new SingleFormFileConfig.ShowMultipleConfig
                            {
                                AllowMultipleEqualToSectionItem = true,
                                AllowMultipleEqualToSectionItemAdjust = 1000,
                                AllowMultipleMinItem = 1
                            }
                        }
                    },

                    #endregion

                    #region SPA Fee Per Year
                    new SingleFormFileList()
                    {
                        FileName = "SPA_OLD_PERMIT",
                        FileGroup = "SPA_PERMIT",
                        Required = true,
                    },
                    #endregion

                    #region CLINIC_NO
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_DOC_1",
                        FileGroup = "CLINIC_DETAIL",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/clinic_renew/clinicForm23.pdf",
                    },
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_DOC_2",
                        FileGroup = "CLINIC_DETAIL",
                        Required = true,
                         DocFormUrl = "~/Uploads/apps/clinic_renew/clinicEvaluation.pdf",
                    },
                    #endregion

                    #region CLINIC_OVER_NIGHT
                    new SingleFormFileList()
                    {
                        FileName = "CLINIC_OVER_NIGHT_DOC_1",
                        FileGroup = "CLINIC_DETAIL_OVER_NIGHT",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_BUSINESS/hopital_business_doc.pdf",
                    },
                    #endregion


                    #region APP_CLINIC_BUSINESS
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_VIDEO_FOR_CONSIDERATION_CUZ",
                        FileGroup = "APP_CLINIC_BUSINESS",
                        Required = true,
                        FileSize = new FileSizeConfig
                        {
                            MaxFileSize = "50mb",
                        },
                    },

                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_LICENSE_LOCATION_PHOTO_CUSTOM",
                        FileGroup = "APP_CLINIC_BUSINESS",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileName = "HOSPITAL_STORE_CUSTOM",
                        FileGroup = "APP_CLINIC_BUSINESS",
                        Required = true,
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2 = "แสดงที่ตั้งของสถานพยาบาลและสิ่งปลูกสร้าง บริเวณใกล้เคียง",
                        Note_3 = "กรุณาส่งเอกสารตัวจริงทางไปรษณีย์",
                    },


                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OWNED_LOCATION_MAP_CUSTOM",
                        FileGroup = "APP_CLINIC_BUSINESS",
                        Note = "กรุณาระบุจุดพิกัด LAT/LONG ที่ใช้เป็นสถานที่ตั้งร้านของท่านมาให้ชัดเจนในเอกสารที่อัพโหลดมาด้วย",
                        Required = true,
                    },
                    #endregion

                    #region APP_CLINIC_OPERATION
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_A",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_B",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_C",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_D",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Note = "ลงนามรับรองสำเนาถูกต้อง",
                        Note_2="จำนวน 1 รูป ถ่ายไว้ไม่เกิน 1 ปี และต้องนำภาพจริงแนบมา 3 รูปในวันที่พบหน่วยงาน",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_E",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileName = "APP_CLINIC_OPERATION_F",
                        FileGroup = "APP_CLINIC_OPERATION",
                        Required = true,
                    },
                    #endregion

                    #region APP_HOSPITAL_BUSINESS
                    new SingleFormFileList()
                    {
                        FileName = "APP_HOSPITAL_BUSINESS_CUSTOM",
                        FileGroup = "APP_HOSPITAL_BUSINESS",
                        Required = true
                    },
                    #endregion

                    #region APP_CLINIC_BUSINESS_RENEW
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_A",
                        Required = false,
                        DocFormUrl = "~/Uploads/apps/appClinic/clinic20200529_100819.pdf"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_B",
                        Required = true,
                        DocFormUrl = "http://slatest.info.go.th/bizportal-phase2/Uploads/apps/APP_HOSPITAL_PERMISSION/%E0%B8%95%E0%B8%B2%E0%B8%A3%E0%B8%B2%E0%B8%87%E0%B8%9A%E0%B8%B8%E0%B8%84%E0%B8%A5%E0%B8%B2%E0%B8%81%E0%B8%A3.pdf"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_C",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_D",
                        Note = "กรุณาแนบเป็น File Word เท่านั้น",
                        DocFormUrl = "http://slatest.info.go.th/bizportal-phase2/Uploads/apps/APP_HOSPITAL_PERMISSION/%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%9F%E0%B8%AD%E0%B8%A3%E0%B9%8C%E0%B8%A1%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B9%81%E0%B8%9B%E0%B8%A5%E0%B8%87%E0%B8%9E%E0%B8%B7%E0%B9%89%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%83%E0%B8%8A%E0%B9%89%E0%B8%AA%E0%B8%AD%E0%B8%A2_%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AD%E0%B8%B2%E0%B8%A2%E0%B8%B8.pdf",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_E",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_F",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_G",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_H",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_I",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_J",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_K",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_L",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_M",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_N",
                        Required = false,
                        Note ="ต้องนำใบอนุญาตให้ประกอบกิจการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืนฉบับจริงไปแสดงต่อเจ้าหน้าที่เมื่อได้รับอนุมัติให้ต่ออายุใบอนุญาต"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_O",
                        Required = false,
                        Note ="ต้องนำสมุดทะเบียนสถานพยาบาล (แบบ ส.พ.8) ฉบับจริงไปแสดงต่อเจ้าหน้าที่เมื่อได้รับอนุมัติให้ต่ออายุใบอนุญาต"
                    },
                    #endregion

                    #region APP_CLINIC_OPERATION_RENEW
                    new SingleFormFileList()
                    {
                        FileGroup = "CITIZEN_INFORMATION",
                        FileName = "APP_CLINIC_OPERATION_RENEW_A",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_C",
                        Required = true,
                        Note = "คำแนะนำ รูปถ่ายหน้าตรง ไม่สวมหมวกหรือโพกศีรษะ ไม่สวมแว่นตา  พื้นหลังไม่มีลวดลาย และถ่ายไว้ไม่เกิน 6 เดือน"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_D",
                        Required = true,
                    },
                    #endregion

                    #region APP_HOSPITAL_BUSINESS_RENEW
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_A",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_B",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/ตารางบุคลากร.pdf"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_C",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_D",
                        Note = "กรุณาแนบเป็น File Word เท่านั้น",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/APP_HOSPITAL_PERMISSION/แบบฟอร์มการเปลี่ยนแปลงพื้นที่ใช้สอย_ต่ออายุ.pdf"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_E",   
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_F",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_G",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_H",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_I",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_J",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_K",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_L",
                        Required = false
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOC_M",
                        Required = false
                    },
                    #endregion

                    #region APP_HOSPITAL_OPERATION_RENEW
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOC_A",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOC_B",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOC_C",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOC_D",
                        Required = true,
                    },
                    #endregion

                    #region APP_CLINIC_BUSINESS_RENEW
                    
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_DOCA",
                        Required = false,
                        Note = "<span style=\"color:red\">ต้องนำใบอนุญาตให้ประกอบกิจการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืนฉบับจริงไปแสดงต่อเจ้าหน้าที่เมื่อได้รับอนุมัติให้ต่ออายุใบอนุญาต</span>"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_DOCB",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_DOCC",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_DOCD",
                        Required = false,
                        Note= "<span style=\"color:red\">ต้องนำสมุดทะเบียนสถานพยาบาล (แบบ ส.พ.8) ฉบับจริงไปแสดงต่อเจ้าหน้าที่เมื่อได้รับอนุมัติให้ต่ออายุใบอนุญาต</span>"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_BUSINESS_RENEW",
                        FileName = "APP_CLINIC_BUSINESS_RENEW_DOCE",
                        Required = true,
                        DocFormUrl = "~/Uploads/apps/appClinic/clinic20200529_100819_2.pdf",
                    },
                    #endregion

                    #region APP_CLINIC_OPERATION_RENEW
                    
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCA",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCB",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCC",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCD",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCE",
                        Required = true,
                        Note = "กรณีผู้ประกอบกิจการและผู้ดำเนินการไม่ใช่บุคคลเดียวกัน"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCF",
                        Required = false,
                        Note = "ลงนามสำเนาถูกต้อง"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCG",
                        Required = false,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW",
                        FileName = "APP_CLINIC_OPERATION_RENEW_DOCH",
                        Required = false,
                        FileFilter = "jpg,png",
                        Note  = "คำแนะนำ รูปถ่ายหน้าตรง ไม่สวมหมวกหรือโพกศีรษะ ไม่สวมแว่นตา พื้นหลังไม่มีลวดลาย และถ่ายไว้ไม่เกิน 6 เดือน"
                    },
                    #endregion

                    #region APP_HOSPITAL_BUSINESS_RENEW
                    
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCA",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCB",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCC",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCD",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCE",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCF",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCG",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCH",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_CLINIC_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_BUSINESS_RENEW_DOCI",
                        Required = true,
                    },

                    #endregion

                    #region APP_HOSPITAL_OPERATION_RENEW
                    
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCA",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCB",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCC",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCD",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCE",
                        Required = true,
                    },

                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCF",
                        Required = false,
                        Note = "กรณีผู้ประกอบกิจการและผู้ดำเนินการไม่ใช่บุคคลเดียวกัน"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCG",
                        Required = true,
                        Note = "ลงนามสำเนาถูกต้อง"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCH",
                        Required = true,
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCJ",
                        Required = true,
                        Note ="ลงนามรับรองสำเนาถูกต้อง"
                    },
                    new SingleFormFileList()
                    {
                        FileGroup = "APP_HOSPITAL_OPERATION_RENEW_GROUP",
                        FileName = "APP_HOSPITAL_OPERATION_RENEW_DOCI",
                        Required = true,
                        FileFilter = "jpg,png",
                        Note = "คำแนะนำ รูปถ่ายหน้าตรง ไม่สวมหมวกหรือโพกศีรษะ ไม่สวมแว่นตา พื้นหลังไม่มีลวดลาย และถ่ายไว้ไม่เกิน 6 เดือน"
                    },
                    #endregion

                };
                db.InsertMany(restaurantItems);
            }
        }

        public string FileName { get; set; }
        public string FileGroup { get; set; }
        public string Note { get; set; }
        public string Note_2 { get; set; }
        public string Note_3 { get; set; }
        public bool Required { get; set; }
        public bool PreDocApphook { get; set; }
        public bool PreDoc { get; set; }
        public string PreDocFileName { get; set; }
        public string PreDocType { get; set; }
        public string PreDocOrg { get; set; }
        public string DocFormUrl { get; set; }
        public SingleFormFileConfig Config { get; set; }
        public FileSizeConfig FileSize { get; set; }
        public string FileFilter { get; set; }
        public bool FileIsEnableUrl { get; set; }


        /// <summary>
        /// Frontis: กรณีที่ FileName มี parameter เช่น ทะเบียนบ้าน: ผู้ดำเนินการ ({0} {1} {2})  ให้ทำการระบุ source ของ parameter แต่ละ index ไว้ที่ตัวแปรนี้
        /// ใช้สำหรับเอกสารที่ไม่ได้ผูกตาม Item ใน ArrayOfForm เท่านั้น
        /// </summary>
        public SingleFormDataItem[] DisplayFormatSources { get; set; }
        
        #region [SpecificFileName]
        public bool DisplaySpecificFileName { get; set; }
        public List<string> SpecificFileNames { get; set; }
        #endregion

        public class FileSizeConfig
        {
            public string MaxFileSize { get; set; }
        }
    }

}
