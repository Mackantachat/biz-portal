using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.Apps.HSSStandard;
using BizPortal.ViewModels.SingleForm;
using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class HSSHospitalOperationEditAppHook : StoreBaseAppHook
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

            var relateLicenseNumber = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECD").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDITC_SECD_CONC"); // เลขที่ใบอนุญาตที่ใช้อ้างอิง (เลขที่ใบเก่า)
            var relateIdentityId = request.IdentityID;
            var requestLicenseOriginal = ApplicationRequestEntity.GetRelateLicense(relateLicenseNumber, relateIdentityId);
            var permitDateOld = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PeriodStart");
            var permitEndOld = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PeriodEnd");
            var files = requestLicenseOriginal.UploadedFiles.Where(e => e.Description == "HOSPITAL_PERMISSION_RECEIVE_FILE_SECTION").FirstOrDefault();
            var profilePhotoMeta = files.Files.Where(e => e.FileTypeCode == "HOSPITAL_PERMISSION_NEW_PHOTO_BIG").FirstOrDefault().Adapt<BizPortal.ViewModels.V2.FileMetadata>();
            var practitionerPhoto = profilePhotoMeta.ContentType.ToLower().Contains("image") ? Convert.ToBase64String(profilePhotoMeta.GetBytes()) : "";
            var practitionerPhotoContentType = profilePhotoMeta.ContentType;
            var practitionerPhotoUrl = "";
            var practitionerPhotoSize = profilePhotoMeta.FileSize;
            var practitionerPhotoTitle = profilePhotoMeta.FileName;

            var permitName = request.PermitName;
            var dataLicensor = request.IdentityName;
            var createDocumentDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var hospitalName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
            var hospitalBuildingNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
            var hospitalMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
            var hospitalSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
            var hospitalStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
            var hospitalTumbol = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
            var hospitalDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
            var hospitalProvince = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
            var hospitalTumbolName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            var hospitalDistrictName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            var hospitalProvinceName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            var hospitalPostCode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            var phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                string.Format("{0} ต่อ {1}",
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
            var hospitalPhone = phoneNo.ToArabicDigit();
            var hospitalFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
            var hospitalEmail = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
            var hospitalRenewType = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_TYPE");
            var hospitalRenewCharacteristics = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL_TEXT") == "สหคลินิก" ?
                                             request.Data.TryGetData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL_TEXT") + "คือ" +
                                             request.Data.TryGetData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER") :
                                             request.Data.TryGetData("APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL_TEXT");
            var availableTimes = "วันจันทร์-อาทิตย์ ๒๔ ชั่วโมง";
            var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");
            var permitEnd = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEnd");

            var titleOperator = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_EDITC_SECA_CONA_TEXT");
            var nameOperator = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDITC_SECA_CONB");
            var lastnameOperator = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDITC_SECA_CONC");
            var oparetorName = titleOperator + " " + nameOperator + " " + lastnameOperator;
            var numberOfBed = requestLicenseOriginal.Data.TryGetData("APP_HOSPITAL_PERMISSION_INFO_SECTION").ThenGetStringData("APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT");
            var professionalLicenseNumber = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDITC_SECA_CONH");
            var fieldPractitionerBranch = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("DROPDOWN_APP_HOSPITAL_PERMISSION_EDITC_SECA_CONG_TEXT");
            var fieldPractitionerLicenseDate = request.Data.TryGetData("APP_HOSPITAL_PERMISSION_EDITC_SECA").ThenGetStringData("APP_HOSPITAL_PERMISSION_EDITC_SECA_CONI");
            

            data = new {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "Request-to-edit-a-license-to-operate-a-medical-facility-hospital"
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
                        attr_value = createDocumentDate.ToArabicDigit()
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
                                    attr_value = "ส.พ.19"
                                }
                            },
                            subject = new {
                                reference = new {
                                    attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                                }
                            },
                            date = new {
                                attr_value = createDocumentDate.ToArabicDigit()
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
                        fullUrl = new
                        {
                            attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                        },
                        resource = new
                        {
                            PractitionerRole = new
                            {
                                id = new
                                {
                                    attr_value = "pr1"
                                },
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitDate.ToArabicDigit()
                                    },
                                    end = new
                                    {
                                        attr_value = permitEnd.ToArabicDigit()
                                    }
                                },
                                practitioner = new
                                {
                                    reference = new
                                    {
                                        attr_value = "https://schemas.teda.th/Practitioner/p1"
                                    }
                                },
                                healthcareService = new
                                {
                                    reference = new
                                    {
                                        attr_value = "https://schemas.teda.th/healthcareservice/h1"
                                    }
                                },
                                availableTime = new
                                {
                                    modifierExtension = new
                                    {
                                        attr_url = "https://schemas.teda.th/availableTime/at1",
                                        valueString = new
                                        {
                                            attr_value = availableTimes.ToArabicDigit()
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new {
                        fullUrl = new
                        {
                            attr_value = "https://schemas.teda.th/Practitioner/p1"
                        },
                        resource = new
                        {
                            Practitioner = new
                            {
                                id = new
                                {
                                    attr_value = "p1"
                                },
                                name = new
                                {
                                    text = new
                                    {
                                        attr_value = oparetorName.ToArabicDigit()
                                    }
                                },
                                photo = new
                                {
                                    contentType = new
                                    {
                                        attr_value = practitionerPhotoContentType.ToArabicDigit()
                                    },
                                    url = new
                                    {
                                        attr_value = practitionerPhotoUrl.ToArabicDigit()
                                    },
                                    size = new
                                    {
                                        attr_value = practitionerPhotoSize
                                    },
                                    title = new
                                    {
                                        attr_value = practitionerPhotoTitle.ToArabicDigit()
                                    }
                                },
                                qualification = new
                                {
                                    identifier = new
                                    {
                                        attr_id = professionalLicenseNumber.ToArabicDigit(),
                                        value = new
                                        {
                                            attr_value = fieldPractitionerBranch.ToArabicDigit()
                                        }
                                    },
                                    code = new
                                    {
                                        text = new
                                        {
                                            attr_value = "CER"
                                        }
                                    },
                                    period = new
                                    {
                                        start = new
                                        {
                                            attr_value = fieldPractitionerLicenseDate.ToArabicDigit().ToMonthNameThai()
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new {
                        fullUrl = new
                        {
                            attr_value = "https://schemas.teda.th/healthcareservice/h1"
                        },
                        resource = new
                        {
                            HealthcareService = new
                            {
                                id = new
                                {
                                    attr_value = "h1"
                                },
                                providedBy = new
                                {
                                    reference = new
                                    {
                                        attr_value = "https://schemas.teda.th/Organization/o1"
                                    }
                                },
                                type = new
                                {
                                    text = new
                                    {
                                        attr_value = hospitalRenewType.ToArabicDigit() //ประเภทสถานพยาบาล
                                    }
                                },
                                specialty = new
                                {
                                    text = new
                                    {
                                        attr_value = numberOfBed.ToArabicDigit() // จำนวนเตียง ถ้ามี
                                    }
                                },
                                comment = new
                                {
                                    attr_value = hospitalRenewCharacteristics.ToArabicDigit()  //ลักษณะสถานพยาบาล
                                },
                                photo = new
                                {
                                    contentType = new
                                    {
                                        attr_value = "" //"image/gif"
                                    },
                                    data = new
                                    {
                                        attr_value = "" //"MTIzNA=="
                                    }
                                }
                            }
                        }
                    },
                    new {
                        fullUrl = new
                        {
                            attr_value = "https://schemas.teda.th/Organization/o1"
                        },
                        resource = new
                        {
                            Organization = new
                            {
                                id = new
                                {
                                    attr_value = "o1"
                                },
                                name = new
                                {
                                    attr_value = hospitalName.ToArabicDigit()
                                },
                                address = new
                                {
                                    text = new
                                    {
                                        attr_value = "ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
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
                                    city = new
                                    {
                                        attr_value = hospitalTumbol.ToArabicDigit(),
                                        attr_valueString = hospitalTumbolName.ToArabicDigit()
                                    },
                                    district = new
                                    {
                                        attr_value = hospitalDistrict.ToArabicDigit(),
                                        attr_valueString = hospitalDistrictName.ToArabicDigit().RemoveTextDistrict()
                                    },
                                    state = new
                                    {
                                        attr_value = hospitalProvince.ToArabicDigit(),
                                        attr_valueString = hospitalProvinceName.ToArabicDigit()
                                    },
                                    postalCode = new
                                    {
                                        attr_value = hospitalPostCode.ToArabicDigit()
                                    },
                                    country = new
                                    {
                                        attr_value = "TH"
                                    }
                                },
                                contact = new
                                {
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
                                            system = new
                                            {
                                                attr_value = "fax"
                                            },
                                            value = new
                                            {
                                                attr_value = hospitalFax.ToArabicDigit()
                                            },
                                            use = new
                                            {
                                                attr_value = "work"
                                            }
                                        },
                                        new {
                                            system = new
                                            {
                                                attr_value = "email"
                                            },
                                            value = new
                                            {
                                                attr_value = hospitalEmail.ToArabicDigit()
                                            },
                                            use = new
                                            {
                                                attr_value = "work"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new {
                        fullUrl = new
                        {
                            attr_value = "https://schemas.teda.th/Practitioner/p2"
                        },
                                resource = new
                                {
                                    Practitioner = new
                                    {
                                        id = new
                                        {
                                            attr_value = "p2"
                                        },
                                        name = new
                                        {
                                            text = new
                                            {
                                                attr_value = dataLicensor.ToArabicDigit()
                                            }
                                        }
                                    }
                                }
                            }
                    },

                },//Bundle
                Images = new
                {
                    PersonPhoto = practitionerPhoto
                }
            };

            return JObject.FromObject(data);
        }

    }
}
