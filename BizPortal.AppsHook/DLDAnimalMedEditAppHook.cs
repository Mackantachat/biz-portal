using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.SingleForm;
using Mapster;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
namespace BizPortal.AppsHook
{
    public class DLDAnimalMedEditAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return -1;
        }

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var uploadFile = request.UploadedFiles.Where(e => e.Description == "ANIMAL_MED_SECTION").FirstOrDefault();
            var phofilePhotoMeta = uploadFile.Files.Where(e => e.FileTypeCode == "MED_ANIMAL_PICTURE").FirstOrDefault().Adapt<BizPortal.ViewModels.V2.FileMetadata>();
            var identityName = string.Empty;
            var identifier = string.Empty;
            var fullName = string.Empty;
            var partOf = new object();
            var permitName = request.PermitName;

            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier").ToThaiDigit();

            var relateLicenseId = request.Data.TryGetData("").ThenGetStringData(""); // เลขที่ใบอนุญาตเดิมที่อ้างอิงถึง กรอกจาก backoffice 
            var relateLicenseIdentityId = request.IdentityID;
            var requestLicenseOriginal = ApplicationRequestEntity.GetRelateLicense(relateLicenseId, relateLicenseIdentityId);
            var licenseOriginalNumber = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th")).ToThaiDigit();
            var practitionerPhoto = Convert.ToBase64String(phofilePhotoMeta.GetBytes());
            var practitionerPhotoContentType = "image/jpg";
            var practitionerPhotoUrl = "";
            var practitionerPhotoSize = phofilePhotoMeta.FileSize;
            var practitionerPhotoTitle = phofilePhotoMeta.FileName;
            var practitionerQualificationId = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_LICENSE");
            var practitionerQualificationType = string.Empty;
            var practitionerQualificationTypeList = new List<string>();


            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_ONE"))
            {
                practitionerQualificationTypeList.Add("อายุรศาสตร์");
            }
            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_TWO"))
            {
                practitionerQualificationTypeList.Add("ศัลยศาสตร์");
            }
            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_THREE"))
            {
                practitionerQualificationTypeList.Add("สัตวแพทยสาธารณสุข");
            }
            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_FOUR"))
            {
                practitionerQualificationTypeList.Add("พยาธิวิทยา");
            }
            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_FIVE"))
            {
                practitionerQualificationTypeList.Add("เวชศาสตร์ระบบสืบพันธุ์");
            }
            practitionerQualificationType = String.Join(",", practitionerQualificationTypeList);
            // var practitionerQualificationStart = "";
            var organizationName = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH").ToThaiDigit();
            var organizationAmountOfBed = requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_RENEW_SECTION").ThenGetStringData("APP_ANIMAL_MED_RENEW_AMOUNT").ToThaiDigit();
            var phoneNo = string.IsNullOrEmpty(requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                string.Format("{0} ต่อ {1}",
                    requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                    requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
            var organizationPhoneNumber = phoneNo.ToThaiDigit();
            var organizationfaxNumber = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            var organizationEmailAddress = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            var organizationAddressNumber = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationAddressNumber))
            {
                organizationAddressNumber = "-";
            }
            var organizationSoi = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationSoi))
            {
                organizationSoi = "-";
            }
            var organizationStreet = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationStreet))
            {
                organizationStreet = "-";
            }
            var organizationMoo = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            var organizationCity = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY");
            var organizationCityValue = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT");
            var organizationDistrict = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY");
            var organizationDistrictValue = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT");
            var organizationState = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY");
            var organizationStateValue = request.Data.TryGetData("INFORMATION_STORE_HAS_PROVINCE_ONLY").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT");
            var organizationPostalCode = requestLicenseOriginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            var organizationCountry = "TH";
            var permitStartDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitStartDate").ToThaiDigit();
            var permitEndDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndDate").ToThaiDigit();
            //var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");

            var organizationType = string.Empty;
            if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_RENEW_SECTION").ThenGetStringData("APP_ANIMAL_MED_RENEW_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_ONE")
            {
                organizationType = "ประเภทไม่มีที่พักสัตว์ป่วยไว้ค้างคืน";
                organizationAmountOfBed = "-";
            }
            else if (requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_RENEW_SECTION").ThenGetStringData("APP_ANIMAL_MED_RENEW_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_TWO")
            {
                organizationType = "ประเภทมีที่พักสัตว์ป่วยไว้ค้างคืน";
            }

            var schemaEngName = string.Empty;
            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                fullName = request.IdentityName; //ชื่อคนที่ขอ
                schemaEngName = "License-to-set-up-an-animal-hospital-Renew";
            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {
                schemaEngName = "License-to-set-up-an-animal-hospital-Juristic-Renew";
                var title = requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("DROPDOWN_APP_ANIMAL_MED_OWNER_TITLE_TEXT");
                var firstname = requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_FIRSTNAME");
                var lastname = requestLicenseOriginal.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_LASTNAME");
                fullName = title + " " + firstname + " " + lastname;
                identityName = request.IdentityName; //ชื่อนิติที่ขอ
                partOf = new
                {
                    display = new
                    {
                        attr_value = identityName
                    }
                };
            }

            data = new
            {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "License-to-set-up-an-animal-hospital-Juristic-Renew"
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
                  attr_id = licenseNumber
               },
               status = new {
                  attr_value = "final"
               },
               type = new {
                  text = new {
                     attr_value = "ส.ส.3"
                  }
               },
               subject = new {
                  reference = new {
                     attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                  }
               },
               date = new {
                  attr_value = createDate
               },
               author = new {
                  reference = new {
                     attr_value = "Practitioner/p2"
                  }
               },
               title = new {
                  attr_value = permitName
               },
               relatesTo = new {
                  code = new {
                     attr_id = "EDIT",
                     attr_value = "replaces"
                  },
                  targetIdentifier = new {
                     attr_id = licenseOriginalNumber
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
                     attr_value = permitStartDate
                  },
                  end = new {
                     attr_value = permitEndDate
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
                     attr_value = fullName
                  }
               },
               photo = new {
                  contentType = new {
                     attr_value = practitionerPhotoContentType
                  },
                  url = new {
                     attr_value = practitionerPhotoUrl
                  },
                  size = new {
                     attr_value = practitionerPhotoSize
                  },
                  title = new {
                     attr_value = practitionerPhotoTitle
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
                     attr_value = organizationType
                  }
               },
               specialty = new {
                  text = new {
                     attr_value = organizationAmountOfBed
                  }
               },
               comment = new {
                  attr_value = organizationType
               },
               photo = new {
                  contentType = new {
                     attr_value = "image/gif"
                  },
                  data = new {
                     attr_value = "MTIzNA=="
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
                  attr_value = organizationName
               },
               address = new {
                  text = new {
                     attr_value = "ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                  },
                  line = new object[] {
                     new {
                        attr_id = "BuildingNumber",
                        attr_value = organizationAddressNumber
                     },
                     new {
                        attr_id = "Moo",
                        attr_value = organizationMoo
                     },
                     new {
                        attr_id = "Soi",
                        attr_value = organizationSoi
                     },
                     new {
                        attr_id = "Street",
                        attr_value = organizationStreet
                     }
                  },
                  city = new {
                     attr_value = organizationCity,
                     attr_valueString = organizationCityValue
                  },
                  district = new {
                     attr_value = organizationDistrict,
                     attr_valueString = organizationDistrictValue
                  },
                  state = new {
                     attr_value = organizationState,
                     attr_valueString = organizationStateValue
                  },
                  postalCode = new {
                     attr_value = organizationPostalCode
                  },
                  country = new {
                     attr_value = "TH"
                  }
               },
               partOf,
               contact = new {
                  telecom = new object[] {
                     new {
                        system = new {
                           attr_value = "phone"
                        },
                        value = new {
                           attr_value = organizationPhoneNumber
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
                           attr_value = organizationfaxNumber
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
                           attr_value = organizationEmailAddress
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
                     attr_value = fullName
                  }
               }
            }
         }
      }
   },

                },
                Images = new
                {
                    PersonPhoto = practitionerPhoto
                },

            };


            return JObject.FromObject(data);

        }
    }
}
