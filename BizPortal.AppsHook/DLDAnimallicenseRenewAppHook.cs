using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Helpers;
using Newtonsoft.Json.Linq;
using Mapster;
using System.Globalization;

namespace BizPortal.AppsHook
{
    public class DLDAnimallicenseRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_ANIMAL_LICENSE_RENEW_SECTION").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_ANIMAL_LICENSE_RENEW_TYPE_OPTION", "") == "APP_ANIMAL_LICENSE_RENEW_TYPE_ONE")
                {
                    fee += 400;
                }
                if (sec.FormData.TryGetString("APP_ANIMAL_LICENSE_RENEW_TYPE_OPTION", "") == "APP_ANIMAL_LICENSE_RENEW_TYPE_TWO")
                {
                    fee += 800;
                }
            }

            return fee;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var uploadFile = request.UploadedFiles.Where(e => e.Description == "ANIMAL_LICENSE_SECTION").FirstOrDefault();
            var phofilePhotoMeta = uploadFile.Files.Where(e => e.FileTypeCode == "ANIMAL_PICTURE").FirstOrDefault().Adapt<BizPortal.ViewModels.V2.FileMetadata>();

            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier").ToThaiDigit();
            var relateLicenseId = request.Data.TryGetData("APP_ANIMAL_MED_RENEW_SECTION").ThenGetStringData("APP_ANIMAL_MED_RENEW_LICENSE");
            var relateLicenseIdentityId = request.IdentityID;
            var requestLicenseOriginal = ApplicationRequestEntity.GetRelateLicense(relateLicenseId, relateLicenseIdentityId);
            var licenseOriginalNumber = requestLicenseOriginal.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var fullName = string.Empty;
            var partOf = new object();

            var identityName = request.IdentityName;
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th")).ToThaiDigit();
            var practitionerPhoto = Convert.ToBase64String(phofilePhotoMeta.GetBytes());
            var practitionerPhotoContentType = "image/jpg";
            var practitionerPhotoUrl = "";
            var practitionerPhotoSize = phofilePhotoMeta.FileSize;
            var practitionerPhotoTitle = phofilePhotoMeta.FileName;
            var practitionerQualificationId = request.Data.TryGetData("APP_ANIMAL_LICENSE_SECTION").ThenGetStringData("APP_ANIMAL_LICENSE_SECTION_NUMBER");
            var practitionerQualificationType = "";
            var practitionerQualificationStart = "";
            var permitName = request.PermitName;

            var organizationName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH_CUSTOM").ToThaiDigit(); ;
            var organizationAmountOfBed = request.Data.TryGetData("APP_ANIMAL_LICENSE_INFO").ThenGetStringData("APP_ANIMAL_LICENSE_INFO_AMOUNT").ToThaiDigit(); ;
            var phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") :
                string.Format("{0} ต่อ {1}",
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
            var organizationPhoneNumber = phoneNo.ToThaiDigit(); ;
            var organizationfaxNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS").ToThaiDigit(); ;
            var organizationEmailAddress = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS").ToThaiDigit(); ;
            var organizationAddressNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationAddressNumber))
            {
                organizationAddressNumber = "-";
            }
            var organizationSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationSoi))
            {
                organizationSoi = "-";
            }
            var organizationStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            if (String.IsNullOrEmpty(organizationStreet))
            {
                organizationStreet = "-";
            }
            var organizationMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            var organizationCity = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
            var organizationCityValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
            var organizationDistrictValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationState = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
            var organizationStateValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationPostalCode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            var organizationCountry = "TH";
            var permitStartDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitStartDate").ToThaiDigit(); ;
            var permitEndDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndDate").ToThaiDigit(); ;
            var organizationType = string.Empty;
            if (request.Data.TryGetData("APP_ANIMAL_LICENSE_INFO").ThenGetStringData("APP_ANIMAL_LICENSE_INFO_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_ONE")
            {
                organizationType = "ประเภทไม่มีที่พักสัตว์ป่วยไว้ค้างคืน";
                organizationAmountOfBed = "-";
            }
            else if (request.Data.TryGetData("APP_ANIMAL_LICENSE_INFO").ThenGetStringData("APP_ANIMAL_LICENSE_INFO_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_TWO")
            {
                organizationType = "ประเภทมีที่พักสัตว์ป่วยไว้ค้างคืน";
            }

            var schemaEngName = string.Empty;
            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                fullName = request.IdentityName; //ชื่อคนที่ขอ
                schemaEngName = "Renewal-of-license-to-operate-an-animal-hospital";
            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {
                schemaEngName = "Renewal-of-license-to-operate-an-animal-hospital-juristic";

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
                        attr_value = schemaEngName
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
                                     attr_value = "ส.ส.4"
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
                                     attr_id = "RENEW",
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
