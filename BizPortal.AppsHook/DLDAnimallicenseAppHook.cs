using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.SingleForm;
using Mapster;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class DLDAnimallicenseAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_ANIMAL_LICENSE_INFO").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_ANIMAL_LICENSE_INFO_TYPE_OPTION", "") == "APP_ANIMAL_MED_TYPE_ONE")
                {
                    fee += 400;
                }
                if (sec.FormData.TryGetString("APP_ANIMAL_LICENSE_INFO_TYPE_OPTION", "") == "APP_ANIMAL_MED_TYPE_TWO")
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
                request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS") :
                string.Format("{0} ต่อ {1}",
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
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
            var organizationMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
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
                schemaEngName = "Renewal-of-license-to-operate-an-animal-hospital";
            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {
                schemaEngName = "Renewal-of-license-to-operate-an-animal-hospital-juristic";
            }

            switch (request.IdentityType)
            {
                case UserTypeEnum.Citizen:
                    data = new
                    {
                        DocumentReference = new
                        {
                            extension = new object[]
                                {
                                    new {
                                        attr_url = "https://oid.estandard.or.th",
                                        valueOid = new  {
                                            attr_id = "2.16.764.1.4.100.16.5.1.1"
                                        }
                                    },
                                    new {
                                        attr_url = "VersionNumber",
                                        valueString = new {
                                            attr_value = "1.0"
                                        }
                                    }
                                },
                            identifier = new
                            {
                                attr_id = licenseNumber
                            },
                            status = new
                            {
                                attr_value = "current"
                            },
                            type = new
                            {
                                coding = new
                                {
                                    code = new
                                    {
                                        attr_value = "สส.4"
                                    }
                                }
                            },
                            subject = new
                            {
                                display = new
                                {
                                    attr_value = identityName
                                }
                            },
                            date = new
                            {
                                attr_value = createDate
                            },
                            author = new
                            {
                                practitioner = new
                                {
                                    photo = new
                                    {
                                        contentType = new
                                        {
                                            attr_value = practitionerPhotoContentType
                                        },
                                        url = new
                                        {
                                            attr_value = practitionerPhotoUrl
                                        },
                                        size = new
                                        {
                                            attr_value = practitionerPhotoSize
                                        },
                                        title = new
                                        {
                                            attr_value = practitionerPhotoTitle
                                        }
                                    },
                                    qualification = new
                                    {
                                        identifier = new
                                        {
                                            attr_id = practitionerQualificationId,
                                            value = new
                                            {
                                                attr_value = practitionerQualificationType
                                            }
                                        },
                                        period = new
                                        {
                                            start = new
                                            {
                                                attr_value = practitionerQualificationStart
                                            }
                                        }
                                    }
                                },
                                organization = new
                                {
                                    type = new
                                    {
                                        text = new
                                        {
                                            attr_value = "ประเภทสถานประกอบการ",
                                            extension = new object[]
                                            {
                                                new
                                                {
                                                    attr_url = "จำนวนเตียง",
                                                    valueQuantity = new
                                                    {
                                                     attr_id = organizationAmountOfBed
                                                    }
                                                },
                                                new
                                                {
                                                    attr_url = "ลักษณะของสถานพยาบาล",
                                                    valueString = new
                                                    {
                                                        attr_value = organizationType
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    name = new
                                    {
                                        attr_value = organizationName
                                    },
                                    telecom = new object[]
                                        {
                                                new
                                                {
                                                    system = new
                                                    {
                                                    attr_value = "phone"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationPhoneNumber
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                },
                                                new
                                                {
                                                    system = new
                                                    {
                                                        attr_value = "fax"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationfaxNumber
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                },
                                                new  {
                                                    system = new
                                                    {
                                                        attr_value = "email"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationEmailAddress
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                }
                                        },
                                    address = new
                                    {
                                        text = "",
                                        line = new object[]
                                            {
                                                    new {
                                                        attr_id = "No.",
                                                        attr_value = organizationAddressNumber,
                                                    },
                                                    new
                                                    {
                                                        attr_id = "Soi",
                                                        attr_value = organizationSoi
                                                    },
                                                    new
                                                    {
                                                        attr_id = "Street",
                                                        attr_value = organizationStreet
                                                    }
                                            },
                                        city = new
                                        {
                                            attr_value = organizationCity,
                                            attr_valueString = organizationCityValue

                                        },
                                        district = new
                                        {
                                            attr_value = organizationDistrict,
                                            attr_valueString = organizationDistrictValue
                                        },
                                        state = new
                                        {
                                            attr_value = organizationState,
                                            attr_valueString = organizationStateValue
                                        },
                                        postalCode = new
                                        {
                                            attr_value = organizationPostalCode
                                        },
                                        country = new
                                        {
                                            attr_value = organizationCountry
                                        }
                                    }
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitStartDate
                                    },
                                    end = new
                                    {
                                        attr_value = permitEndDate
                                    }
                                }
                            }
                        },
                        Images = new
                        {
                            PersonPhoto = practitionerPhoto
                        }
                    };
                    break;
                case UserTypeEnum.Juristic:
                    data = new
                    {
                        DocumentReference = new
                        {
                            extension = new object[]
                                {
                                    new {
                                        attr_url = "https://oid.estandard.or.th",
                                        valueOid = new  {
                                            attr_id = "2.16.764.1.4.100.16.5.1.1"
                                        }
                                    },
                                    new {
                                        attr_url = "VersionNumber",
                                        valueString = new {
                                            attr_value = "1.0"
                                        }
                                    }
                                },
                            identifier = new
                            {
                                attr_id = licenseNumber
                            },
                            status = new
                            {
                                attr_value = "current"
                            },
                            type = new
                            {
                                coding = new
                                {
                                    code = new
                                    {
                                        attr_value = "สส.4"
                                    }
                                }
                            },
                            subject = new
                            {
                                display = new
                                {
                                    attr_value = identityName
                                }
                            },
                            date = new
                            {
                                attr_value = createDate
                            },
                            author = new
                            {
                                practitioner = new
                                {
                                    photo = new
                                    {
                                        contentType = new
                                        {
                                            attr_value = practitionerPhotoContentType
                                        },
                                        url = new
                                        {
                                            attr_value = practitionerPhotoUrl
                                        },
                                        size = new
                                        {
                                            attr_value = practitionerPhotoSize
                                        },
                                        title = new
                                        {
                                            attr_value = practitionerPhotoTitle
                                        }
                                    },
                                    qualification = new
                                    {
                                        identifier = new
                                        {
                                            attr_id = practitionerQualificationId,
                                            value = new
                                            {
                                                attr_value = practitionerQualificationType
                                            }
                                        },
                                        period = new
                                        {
                                            start = new
                                            {
                                                attr_value = practitionerQualificationStart
                                            }
                                        }
                                    }
                                },
                                organization = new
                                {
                                    type = new
                                    {
                                        text = new
                                        {
                                            attr_value = "ประเภทสถานประกอบการ",
                                            extension = new object[]
                                            {
                                                new
                                                {
                                                    attr_url = "จำนวนเตียง",
                                                    valueQuantity = new
                                                    {
                                                     attr_id = organizationAmountOfBed
                                                    }
                                                },
                                                new
                                                {
                                                    attr_url = "ลักษณะของสถานพยาบาล",
                                                    valueString = new
                                                    {
                                                        attr_value = organizationType
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    name = new
                                    {
                                        attr_value = organizationName
                                    },
                                    telecom = new object[]
                                        {
                                                new
                                                {
                                                    system = new
                                                    {
                                                    attr_value = "phone"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationPhoneNumber
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                },
                                                new
                                                {
                                                    system = new
                                                    {
                                                        attr_value = "fax"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationfaxNumber
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                },
                                                new  {
                                                    system = new
                                                    {
                                                        attr_value = "email"
                                                    },
                                                    value = new
                                                    {
                                                        attr_value = organizationEmailAddress
                                                    },
                                                    use = new
                                                    {
                                                        attr_value = "work"
                                                    }
                                                }
                                        },
                                    address = new
                                    {
                                        text = "",
                                        line = new object[]
                                            {
                                                    new {
                                                        attr_id = "No.",
                                                        attr_value = organizationAddressNumber,
                                                    },
                                                    new
                                                    {
                                                        attr_id = "Soi",
                                                        attr_value = organizationSoi
                                                    },
                                                    new
                                                    {
                                                        attr_id = "Street",
                                                        attr_value = organizationStreet
                                                    }
                                            },
                                        city = new
                                        {
                                            attr_value = organizationCity,
                                            attr_valueString = organizationCityValue

                                        },
                                        district = new
                                        {
                                            attr_value = organizationDistrict,
                                            attr_valueString = organizationDistrictValue
                                        },
                                        state = new
                                        {
                                            attr_value = organizationState,
                                            attr_valueString = organizationStateValue
                                        },
                                        postalCode = new
                                        {
                                            attr_value = organizationPostalCode
                                        },
                                        country = new
                                        {
                                            attr_value = organizationCountry
                                        }
                                    }
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitStartDate
                                    },
                                    end = new
                                    {
                                        attr_value = permitEndDate
                                    }
                                }
                            }
                        },
                        Images = new
                        {
                            practitionerPhoto = practitionerPhoto
                        }

                    };
                    break;
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
                                     attr_id = "NEW",
                                     attr_value = "signs"
                                  },
                                  targetIdentifier = new {
                                     attr_id = ""
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
                                     attr_value = identityName
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
                                     attr_value = identityName
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
            var json = JObject.FromObject(data);
            return JObject.FromObject(data);
        }

        public override JObject GenerateEReceiptData(Guid applicationrequestId)
        {
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var item = new JArray();
            item.Add(new
            {
                //ลำดับรายการ
                AssociatedDocumentLineDocument = JObject.FromObject(new
                {
                    LineID = "1",
                }),

                // ชื่อสินค้าหรือรายการ 
                SpecifiedTradeProduct = JObject.FromObject(new
                {
                    Name = "ค่าธรรมเนียมใบอนุญาต" + request.PermitName
                }),

                // มูลค่าต่อหน่วย
                SpecifiedLineTradeAgreement = JObject.FromObject(new
                {
                    GrossPriceProductTradePrice = JObject.FromObject(new
                    {
                        ChargeAmount = request.Fee,
                    }),
                }),

                //มูลค่ารวมต่อรายการ
                SpecifiedLineTradeSettlement = JObject.FromObject(new
                {
                    SpecifiedTradeSettlementLineMonetarySummation = JObject.FromObject(new
                    {
                        LineTotalAmount = string.Empty,
                    })
                })
            });

            item.Add(new
            {
                //ลำดับรายการ
                AssociatedDocumentLineDocument = JObject.FromObject(new
                {
                    LineID = "2",
                }),

                // ชื่อสินค้าหรือรายการ 
                SpecifiedTradeProduct = JObject.FromObject(new
                {
                    Name = "ค่าจัดส่งใบอนุญาตทางไปรษณีย์"
                }),

                // มูลค่าต่อหน่วย
                SpecifiedLineTradeAgreement = JObject.FromObject(new
                {
                    GrossPriceProductTradePrice = JObject.FromObject(new
                    {
                        ChargeAmount = request.EMSFee,
                    }),
                }),

                //มูลค่ารวมต่อรายการ
                SpecifiedLineTradeSettlement = JObject.FromObject(new
                {
                    SpecifiedTradeSettlementLineMonetarySummation = JObject.FromObject(new
                    {
                        LineTotalAmount = string.Empty,
                    })
                })
            });
            return JObject.FromObject(new
            {
                ExchangedDocument = JObject.FromObject(new
                {
                    ID = string.Empty,
                    BillPaymentNo = string.Empty,
                    Name = "ใบเสร็จรับเงิน/Receipt",
                    IssueDateTime = string.Empty,
                    PrintBy = string.Empty
                }),

                SupplyChainTradeTransaction = JObject.FromObject(new
                {
                    ApplicableHeaderTradeSettlement = JObject.FromObject(new
                    {
                        PayeeTradeParty = JObject.FromObject(new
                        {
                            Name = request.OrgNameTH,
                            DefinedTradeContact = JObject.FromObject(new
                            {
                                PersonName = string.Empty,
                            }),
                            PostalTradeAddress = JObject.FromObject(new
                            {
                                BuildingName = string.Empty,
                                BuildingNumber = string.Empty,
                                LineOne = request.OrgAddress,
                                LineTwo = string.Empty,
                                LineThree = string.Empty,
                                LineFour = string.Empty,
                                StreetName = string.Empty,
                                CityName = string.Empty,
                                CitySubDivisionName = string.Empty,
                                CountrySubDivisionID = string.Empty,
                                PostcodeCode = string.Empty
                            }),
                        }),
                        PayerTradeParty = JObject.FromObject(new
                        {
                            ID = request.IdentityID,
                            Name = request.IdentityName,
                        }),
                        InvoiceCurrencyCode = "THB",
                        SpecifiedTradeSettlementPaymentMeans = JObject.FromObject(new
                        {
                            Information = request.PermitDeliveryType,
                        }),
                        SpecifiedTradeSettlementHeaderMonetarySummation = JObject.FromObject(new
                        {
                            LineTotalAmount = request.Fee + request.EMSFee,
                            LineTotalAmountText = string.Empty,
                        }),
                    }),

                    IncludedSupplyChainTradeLineItem = item

                }),
            });
        }
    }
}
