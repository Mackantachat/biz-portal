using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.HSSStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class HSSClinicBusinessEditAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 1000;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);

            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var identityName = string.Empty;
            switch (request.IdentityType)
            {
                case UserTypeEnum.Citizen:
                    identityName = request.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT") + " " + request.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME") +
                                   " " + request.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME");
                    break;
                case UserTypeEnum.Juristic:
                    identityName = "บริษัท " + request.IdentityName;
                    break;
            }
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var clinicType = "ไม่รับผู้ป่วยค้างคืน";
            var clinicName = string.Empty;
            var permitName = request.PermitName;
            if (request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetBooleanData("APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE_REQUEST_CHANGE_NAME") == true)
            {
                clinicName = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("APP_CLINIC_EDIT_INFO_SECTION_2_NAME");
            }
            else
            {
                clinicName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
            }

            var clinicBuildingNumber = string.Empty;
            var clinicMoo = string.Empty;
            var clinicSoi = string.Empty;
            var clinicStreet = string.Empty;
            var clinicTumbol = string.Empty;
            var clinicDistrict = string.Empty;
            var clinicProvince = string.Empty;
            var clinicTumbolName = string.Empty;
            var clinicDistrictName = string.Empty;
            var clinicProvinceName = string.Empty;
            var clinicPostCode = string.Empty;
            var phoneNo = string.Empty;
            var clinicPhone = string.Empty;
            var clinicFax = string.Empty;
            var clinicEmail = string.Empty;

            if (request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetBooleanData("APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE_REQUEST_CHANGE_ADDRESS") == true)
            {
                clinicBuildingNumber = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicMoo = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_MOO_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicSoi = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_SOI_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicStreet = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_ROAD_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicTumbol = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicDistrict = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicProvince = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                clinicTumbolName = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                clinicDistrictName = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                clinicProvinceName = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                clinicPostCode = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_POSTCODE_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
                phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                   request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                   string.Format("{0} ต่อ {1}",
                       request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                       request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
                clinicPhone = phoneNo.ToArabicDigit();
                clinicFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
                clinicEmail = request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_EMAIL_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS");
            }
            else
            {
                clinicBuildingNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
                clinicMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
                clinicSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
                clinicStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
                clinicTumbol = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
                clinicDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
                clinicProvince = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
                clinicTumbolName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
                clinicDistrictName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
                clinicProvinceName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
                clinicPostCode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
                phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                   request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                   string.Format("{0} ต่อ {1}",
                       request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                       request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
                clinicPhone = phoneNo.ToArabicDigit();
                clinicFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
                clinicEmail = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
            }


            var relateIdentityId = request.IdentityID;
            var relateLicenseNumber = request.Data.TryGetData("").ThenGetStringData(""); // เลขที่ใบอนุญาตเดิมที่อ้างอิงถึง
            var requestLicenseOriginal = ApplicationRequestEntity.GetRelateLicense(relateLicenseNumber, relateIdentityId);
            var relateClinicCharacteristics = requestLicenseOriginal.Data.TryGetData("APP_CLINIC_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_TEXT");
            var services = requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_XRAY_ROOM") == true ? "ห้องเอกซเรย์," : string.Empty;
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ARTIFICIAL_ROOM") == true ? "ห้องไตเทียม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_SMALL_ROOM") == true ? "ห้องผ่าตัดเล็ก," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_MAJOR_ROOM") == true ? "ห้องผ่าตัดใหญ่," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ACUPUNCTURE_ROOM") == true ? "ห้องฝังเข็ม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER") == true ? requestLicenseOriginal.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT") + "," : string.Empty);
            services = services.Length > 0 ? services.Substring(0, services.Length - 1) : services;

            var totalDate = 0;
            var getOpenServicesDate = requestLicenseOriginal.Data.TryGetData("APP_CLINIC_LICENSE_DETAIL_SECTION").Data;
            var openServicesDate = string.Empty;

            var editWorkingDay = request.Data.TryGetData("APP_CLINIC_EDIT_WORKING_DAY_SECTION").Data;
            int editTotal = 0;

            if (request.Data.TryGetData("APP_CLINIC_EDIT_INFO_SECTION_2").ThenGetBooleanData("APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE_REQUEST_CHANGE_WORKING_DATE") == true)
            {
                if (int.TryParse(editWorkingDay["APP_CLINIC_EDIT_WORKING_DAY_SECTION_TOTAL"], out editTotal))
                {
                    for (int i = 0; i < editTotal; i++)
                    {
                        openServicesDate = openServicesDate + editWorkingDay["DROPDOWN_APP_CLINIC_EDIT_WORKING_DAY_SECTION_DAY_TEXT_" + i] + " " + editWorkingDay["DROPDOWN_APP_CLINIC_EDIT_WORKING_DAY_SECTION_START_TIME_TEXT_" + i] + "-" + editWorkingDay["DROPDOWN_APP_CLINIC_EDIT_WORKING_DAY_SECTION_END_TIME_TEXT_" + i] + ",";
                    }
                    openServicesDate = openServicesDate.Substring(0, openServicesDate.Length - 1);
                }
                else
                {
                    throw new NullReferenceException("Cannot parse editWorkingDayTotal APP_CLINIC_EDIT_WORKING_DAY_SECTION_TOTAL to int ");
                }
            }
            else
            {
                if (int.TryParse(getOpenServicesDate["APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL"], out totalDate))
                {
                    for (int i = 0; i < totalDate; i++)
                    {
                        openServicesDate = openServicesDate + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_TEXT_" + i] + " " + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_TEXT_" + i] + "-" + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_TEXT_" + i] + ",";
                    }
                    openServicesDate = openServicesDate.Substring(0, openServicesDate.Length - 1);
                }
                else
                {
                    throw new NullReferenceException("Cannot parse APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL to int");
                }
            }


            var permitEnd = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEnd");
            var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");
            var permitDateOld = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");
            var permitEndOld = "๓๑ ธันวาคม พ.ศ. " + requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndYear");

            data = new
            {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "Request-for-permission-to-operate-a-medical-facility-clinic"
                    },
                    identifier = new
                    {
                        system = new
                        {
                            attr_value = "https://oid.estandard.or.th"
                        },
                        value = new
                        {
                            attr_value = "2.16.764.1.4.100.16.5.1.1"
                        }
                    },
                    type = new
                    {
                        attr_value = "document"
                    },
                    timestamp = new
                    {
                        attr_value = createDate.ToArabicDigit()
                    },
                    entry = new object[] {
                        new {
                            fullUrl = new {
                                attr_value = "https://schemas.teda.th/Composition/1"
                            },
                            resource = new {
                                Composition = new {
                                    id = new {
                                        attr_value = "1"
                                    },
                                    extension = new {
                                        attr_url = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber",
                                        valueString = new {
                                            attr_value = "1.0.0"
                                        }
                                    },
                                    identifier = new {
                                        attr_id = licenseNumber.ToArabicDigit()
                                    },
                                    status = new {
                                        attr_value = "final"
                                    },
                                    type = new {
                                        text = new {
                                            attr_value = "ส.พ.7"
                                        }
                                    },
                                    subject = new {
                                        reference = new {
                                            attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                                        }
                                    },
                                    date = new {
                                        attr_value = createDate.ToArabicDigit()
                                    },
                                    author = new {
                                        reference = new {
                                            attr_value = "Practitioner/p2"
                                        }
                                    },
                                    title = new {
                                        attr_value = permitName.ToArabicDigit()
                                    },
                                    relatesTo = new {
                                        code = new {
                                            attr_id = "EDIT",
                                            attr_value = "replaces"
                                        },
                                        targetIdentifier = new {
                                            attr_id = relateLicenseNumber.ToArabicDigit(),
                                            peroid = new {
                                                start = new
                                                {
                                                    attr_value =  permitDateOld.ToArabicDigit()
                                                },
                                                end = new
                                                {
                                                    attr_value = permitEndOld.ToArabicDigit()
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new {
                            fullUrl = new {
                                attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                            },
                            resource = new {
                                PractitionerRole = new {
                                    id = new {
                                        attr_value = "pr1"
                                    },
                                    period = new {
                                        start = new {
                                            attr_value = permitDate.ToArabicDigit()
                                        },
                                        end = new {
                                            attr_value = permitEnd.ToArabicDigit()
                                        }
                                    },
                                    practitioner = new {
                                        reference = new {
                                            attr_value = "https://schemas.teda.th/Practitioner/p1"
                                        }
                                    },
                                    healthcareService = new {
                                        reference = new {
                                            attr_value = "https://schemas.teda.th/healthcareservice/h1"
                                        }
                                    },
                                    availableTime = new {
                                        modifierExtension = new {
                                            attr_url = "https://schemas.teda.th/availableTime/at1",
                                            valueString = new {
                                            attr_value = openServicesDate.ToArabicDigit()
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new {
                            fullUrl = new {
                                attr_value = "https://schemas.teda.th/Practitioner/p1"
                            },
                            resource = new {
                                Practitioner = new {
                                    id = new {
                                        attr_value = "p1"
                                    },
                                    name = new {
                                        text = new {
                                            attr_value = identityName.ToArabicDigit()
                                        }
                                    },
                                    qualification = new {
                                        identifier = new {
                                            attr_id = "",//"เลขที่ใบประกอบวิชาชีพ",
                                            value = new {
                                            attr_value = ""//"เวชกรรม"
                                            }
                                        },
                                        code = new {
                                            text = new {
                                            attr_value = "CER"
                                            }
                                        },
                                        period = new {
                                            start = new {
                                            attr_value = "",//"2020-06-16"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new {
                            fullUrl = new {
                                attr_value = "https://schemas.teda.th/healthcareservice/h1"
                            },
                            resource = new {
                                HealthcareService = new {
                                    id = new {
                                        attr_value = "h1"
                                    },
                                    providedBy = new {
                                        reference = new {
                                            attr_value = "https://schemas.teda.th/Organization/o1"
                                        }
                                    },
                                    type = new {
                                        text = new {
                                            attr_value = clinicType.ToArabicDigit()
                                        }
                                    },
                                    specialty = new {
                                        text = new {
                                            attr_value = "-" // จำนวนเตียง คลินิคไม่มีจำนวนเตียง
                                        }
                                    },
                                    comment = new {
                                        attr_value = relateClinicCharacteristics.ToArabicDigit()
                                    },
                                    eligibility = new {
                                        comment = new {
                                            attr_value = services.ToArabicDigit()
                                        }
                                    }
                                }
                            }
                        },
                        new {
                            fullUrl = new {
                            attr_value = "https://schemas.teda.th/Organization/o1"
                            },
                            resource = new {
                                Organization = new {
                                    id = new {
                                        attr_value = "o1"
                                    },
                                    name = new {
                                        attr_value = clinicName.ToArabicDigit()
                                    },
                                    address = new {
                                        text = new {
                                            attr_value = "",//"ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                                        },
                                        line = new object[] {
                                            new {
                                            attr_id = "BuildingNumber",
                                            attr_value = clinicBuildingNumber.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Moo",
                                            attr_value = clinicMoo.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Soi",
                                            attr_value = clinicSoi.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Street",
                                            attr_value = clinicStreet.ToArabicDigit()
                                            }
                                        },
                                        city = new {
                                            attr_value = clinicTumbol.ToArabicDigit(),
                                            attr_valueString = clinicTumbolName.ToArabicDigit()
                                        },
                                        district = new {
                                            attr_value = clinicDistrict.ToArabicDigit(),
                                            attr_valueString = clinicDistrictName.ToArabicDigit().RemoveTextDistrict()
                                        },
                                        state = new {
                                            attr_value = clinicProvince.ToArabicDigit(),
                                            attr_valueString = clinicProvinceName.ToArabicDigit()
                                        },
                                        postalCode = new {
                                            attr_value = clinicPostCode.ToArabicDigit()
                                        },
                                        country = new {
                                            attr_value = "TH"
                                        }
                                    },
                                    contact = new {
                                        telecom = new object[] {
                                            new {
                                            system = new {
                                                attr_value = "phone"
                                            },
                                            value = new {
                                                attr_value = clinicPhone.ToArabicDigit()
                                            },
                                            use = new {
                                                attr_value = "work"
                                            }
                                            },
                                            new {
                                            system = new {
                                                attr_value = "fax"
                                            },
                                            value = new {
                                                attr_value = clinicFax.ToArabicDigit()
                                            },
                                            use = new {
                                                attr_value = "work"
                                            }
                                            },
                                            new {
                                            system = new {
                                                attr_value = "email"
                                            },
                                            value = new {
                                                attr_value = clinicEmail.ToArabicDigit()
                                            },
                                            use = new {
                                                attr_value = "work"
                                            }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new {
                            fullUrl = new {
                                attr_value = "https://schemas.teda.th/Practitioner/p2"
                            },
                            resource = new {
                                Practitioner = new {
                                    id = new {
                                        attr_value = "p2"
                                    },
                                    name = new {
                                        text = new {
                                            attr_value = identityName.ToArabicDigit()
                                        }
                                    }
                                }
                            }
                        }
                    },

                }

            };
            var json = JObject.FromObject(data);
            return JObject.FromObject(data);
        }

    }
}
