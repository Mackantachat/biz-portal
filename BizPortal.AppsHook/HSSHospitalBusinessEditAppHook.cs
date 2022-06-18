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
    public class HSSHospitalBusinessEditAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
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
            var identityName = request.IdentityName;
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var hospitalName = string.Empty;
            var hospitalBuildingNumber = string.Empty;
            var hospitalMoo = string.Empty;
            var hospitalSoi = string.Empty;
            var hospitalStreet = string.Empty;
            var hospitalTumbol = string.Empty;
            var hospitalDistrict = string.Empty;
            var hospitalProvince = string.Empty;
            var hospitalTumbolName = string.Empty;
            var hospitalDistrictName = string.Empty;
            var hospitalProvinceName = string.Empty;
            var hospitalPostCode = string.Empty;
            var phoneNo = string.Empty;
            var hospitalPhone = string.Empty;
            var hospitalFax = string.Empty;
            var hospitalEmail = string.Empty;

            if (request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE_REQUEST_CHANGE_NAME") == true)
            {
                 hospitalName = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME");
            }
            else
            {
                 hospitalName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
            }

            if (request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE_REQUEST_CHANGE_ADDRESS") == true)
            {
                hospitalBuildingNumber = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalMoo = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_MOO_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalSoi = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_SOI_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalStreet = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_ROAD_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalTumbol = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalDistrict = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalProvince = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                hospitalTumbolName = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                hospitalDistrictName = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                hospitalProvinceName = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS_TEXT");
                hospitalPostCode = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_POSTCODE_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
                phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                    string.Format("{0} ต่อ {1}",
                        request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                        request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
                hospitalPhone = phoneNo.ToArabicDigit();
                hospitalFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
                hospitalEmail = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").ThenGetStringData("ADDRESS_EMAIL_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS");
            }
            else
            {
                 hospitalBuildingNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
                 hospitalMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
                 hospitalSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
                 hospitalStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
                 hospitalTumbol = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
                 hospitalDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
                 hospitalProvince = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
                 hospitalTumbolName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
                 hospitalDistrictName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
                 hospitalProvinceName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
                 hospitalPostCode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
                 phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                    string.Format("{0} ต่อ {1}",
                        request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                        request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
                 hospitalPhone = phoneNo.ToArabicDigit();
                 hospitalFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
                 hospitalEmail = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
            }

            var relateIdentityId = request.IdentityID;
            var relateLicenseNumber = request.Data.TryGetData("").ThenGetStringData(""); // เลขที่ใบอนุญาตเดิมที่อ้างอิงถึง กรอกจาก back office
            var requestLicenseOriginal = ApplicationRequestEntity.GetRelateLicense(relateLicenseNumber, relateIdentityId);
            var hospitalType = requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER_TYPE");
            var numberOfBed = requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT");
            var relateClinicCharacteristics = requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL_TEXT");
            var services = requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTERNAL_MEDICINE") == true ? "อายุรกรรม," : string.Empty;

            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_SURGERY") == true ? "ศัลยกรรม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OBSTETRICS") == true ? "สูตินรีเวชกรรม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_PEDIATRICS") == true ? "กุมารเวชกรรม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MEDICAL_TECHNOLOGY") == true ? "แผนกเทคนิคการแพทย์," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ORTHOPEDIC_DEPARTMENT") == true ? "แผนกออร์โธปิดิกส์," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DERMATOLOGY_DEPARTMENT") == true ? "แผนกโรคผิวหนัง," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ARTIFICIAL_INSEMINATION") == true ? "แผนกการผสมเทียม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_PHYSICAL_THERAPY") == true ? "แผนกกายภาพบำบัด," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_THAI_TRADITIONAL_MEDICINE") == true ? "แผนกการแพทย์แผนไทย," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_NUTRITION_DEPARTMENT") == true ? "แผนกโภชนาการ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_LAUNDRY_DEPARTMENT") == true ? "แผนกซักฟอก," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTENSIVE_CARE_UNIT") == true ? "หอผู้ป่วยหนัก," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTERNAL_EXAMINATION") == true ? "ห้องตรวจภายในและขูดมดลูก," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_SMALL_OPERATING_ROOM") == true ? "ห้องผ่าตัดเล็ก," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_TREATMENT_ROOM") == true ? "ห้องให้การรักษา," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_AFTER_BIRTH") == true ? "ห้องทารกหลังคลอด," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ORGAN_TRANSPLANT") == true ? "การผ่าตัดเปลี่ยนอวัยวะ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_HEMODIALYSIS_ROOM") == true ? "ห้องไตเทียม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DENTAL_ROOM") == true ? "ห้องทันตกรรม," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DIAGNOSTIC_RADIATION") == true ? "รังสีวินิจฉัยด้วยคอมพิวเตอร์," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OPEN_HEART_SURGERY") == true ? "การผ่าตัดเปิดหัวใจ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_CARDIAC_CATHETERIZATION") == true ? "การสวนหัวใจ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_RADIATION_THERAPY") == true ? "รังสีบำบัด," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_EXAMINATION_MAGNETIC_FIELD") == true ? "การตรวจอวัยวะภายในชนิดสนามแม่เหล็กไฟ้ฟ้า," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_BREAKDOWN") == true ? "การสลายนิ่วด้วยเครื่องมือ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MORGUE") == true ? "ห้องเก็บศพ," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_THAI_TRADITIONAL_MEDICINE_APPLIED") == true ? "แผนกการแพทย์แผนไทยประยุกต์," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MASSAGE_DEPARTMENT") == true ? "แผนกการนวด," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_CHINESE_MEDICINE") == true ? "แผนกการแพทย์แผนจีน," : string.Empty);
            services = services + (requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetBooleanData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER") == true ? requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT") + "," : string.Empty);
            services = services.Substring(0, services.Length - 1);

            var openServicesDate = "วันจันทร์ - วันอาทิตย์ ๒๔ ชั่วโมง";


            var permitEnd = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEnd");
            var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");
            var permitDateOld = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PeriodStart");
            var permitEndOld = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PeriodEnd");

            data = new
            {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "Request-amend-permission-to-operate-a-medical-facility-hospital"
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
                        attr_value = createDate
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
                                        attr_value = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)"
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
                                                    attr_value = permitDateOld.ToArabicDigit()
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
                                            attr_value = hospitalType.ToArabicDigit()
                                        }
                                    },
                                    specialty = new {
                                        text = new {
                                            attr_value = numberOfBed.ToArabicDigit() // จำนวนเตียง 
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
                                        attr_value = hospitalName.ToArabicDigit()
                                    },
                                    address = new {
                                        text = new {
                                            attr_value = "",//"ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                                        },
                                        line = new object[] {
                                            new {
                                            attr_id = "BuildingNumber",
                                            attr_value = hospitalBuildingNumber.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Moo",
                                            attr_value = hospitalMoo.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Soi",
                                            attr_value = hospitalSoi.ToArabicDigit()
                                            },
                                            new {
                                            attr_id = "Street",
                                            attr_value = hospitalStreet.ToArabicDigit()
                                            }
                                        },
                                        city = new {
                                            attr_value = hospitalTumbol.ToArabicDigit(),
                                            attr_valueString = hospitalTumbolName.ToArabicDigit()
                                        },
                                        district = new {
                                            attr_value = hospitalDistrict.ToArabicDigit(),
                                            attr_valueString = hospitalDistrictName.ToArabicDigit().RemoveTextDistrict()
                                        },
                                        state = new {
                                            attr_value = hospitalProvince.ToArabicDigit(),
                                            attr_valueString = hospitalProvinceName.ToArabicDigit()
                                        },
                                        postalCode = new {
                                            attr_value = hospitalPostCode.ToArabicDigit()
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
                                                attr_value = hospitalPhone.ToArabicDigit()
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
                                                attr_value = hospitalFax.ToArabicDigit()
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
                                                attr_value = hospitalEmail.ToArabicDigit()
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

            return JObject.FromObject(data);
        }
    }
}
