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
    public class DLDAnimalMedAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_ANIMAL_MED_SECTION").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_ANIMAL_MED_TYPE_OPTION", "") == "APP_ANIMAL_MED_TYPE_ONE")
                {
                    fee += 600;
                }
                if (sec.FormData.TryGetString("APP_ANIMAL_MED_TYPE_OPTION", "") == "APP_ANIMAL_MED_TYPE_TWO")
                {
                    var rest = int.Parse(sec.FormData["APP_ANIMAL_MED_REST_AMOUNT"].ToString());
                    if (rest < 11)
                    {
                        fee += 1000;
                    }
                    else
                    {
                        fee += 2000;
                    }

                }
            }

            return fee;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dld/30.1_form_animal_hospital.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();


            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return false;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }



        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var uploadFile = request.UploadedFiles.Where(e => e.Description == "ANIMAL_MED_SECTION").FirstOrDefault();
            var phofilePhotoMeta = uploadFile.Files.Where(e => e.FileTypeCode == "MED_ANIMAL_PICTURE").FirstOrDefault().Adapt<BizPortal.ViewModels.V2.FileMetadata>();
            var identityName = string.Empty;
            var identifier = string.Empty;
            var subject = new object();


            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                identityName = request.IdentityName;
                subject = new
                {

                    display = new
                    {
                        attr_value = identityName
                    }
                };
            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {

                var title = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("DROPDOWN_APP_ANIMAL_MED_OWNER_TITLE_TEXT");
                var firstname = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_FIRSTNAME");
                var lastname = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_LASTNAME");
                identityName = title + " " + firstname + " " + lastname;
                identifier = request.IdentityName;
                subject = new
                {
                    identifier = new
                    {
                        attr_id = identifier.ToThaiDigit()
                    },
                    display = new
                    {
                        attr_value = identityName
                    }
                };
            }


            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier").ToThaiDigit();

            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th")).ToThaiDigit();
            var practitionerPhoto = Convert.ToBase64String(phofilePhotoMeta.GetBytes());
            var practitionerPhotoContentType = "image/jpg";
            var practitionerPhotoUrl = "";
            var practitionerPhotoSize = phofilePhotoMeta.FileSize;
            var practitionerPhotoTitle = phofilePhotoMeta.FileName;
            var practitionerQualificationId = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_LICENSE");
            var practitionerQualificationType = string.Empty;
            var practitionerQualificationTypeList = new List<string>();

            if (request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_ONE"))
            {
                practitionerQualificationTypeList.Add("อายุรศาสตร์");
            }
            if (request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_TWO"))
            {
                practitionerQualificationTypeList.Add("ศัลยศาสตร์");
            }
            if (request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_THREE"))
            {
                practitionerQualificationTypeList.Add("สัตวแพทยสาธารณสุข");
            }
            if (request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_FOUR"))
            {
                practitionerQualificationTypeList.Add("พยาธิวิทยา");
            }
            if (request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetBooleanData("APP_ANIMAL_MED_OWNER_BRANCH_APP_ANIMAL_MED_OWNER_BRANCH_TYPE_FIVE"))
            {
                practitionerQualificationTypeList.Add("เวชศาสตร์ระบบสืบพันธุ์");
            }
            practitionerQualificationType = String.Join(",", practitionerQualificationTypeList);
            // var practitionerQualificationStart = "";
            var organizationName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH_CUSTOM").ToThaiDigit();
            var organizationAmountOfBed = request.Data.TryGetData("APP_ANIMAL_MED_SECTION").ThenGetStringData("APP_ANIMAL_MED_REST_AMOUNT").ToThaiDigit();
            var phoneNo = string.IsNullOrEmpty(request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS")) ?
                request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS") :
                string.Format("{0} ต่อ {1}",
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                    request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"));
            var organizationPhoneNumber = phoneNo.ToThaiDigit();
            var organizationfaxNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS").ToThaiDigit();
            var organizationEmailAddress = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS").ToThaiDigit();
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
            var permitStartDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitStartDate").ToThaiDigit();
            var permitEndDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndDate").ToThaiDigit();
            //var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");

            var organizationType = string.Empty;
            if (request.Data.TryGetData("APP_ANIMAL_MED_SECTION").ThenGetStringData("APP_ANIMAL_MED_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_ONE")
            {
                organizationType = "ประเภทไม่มีที่พักสัตว์ป่วยไว้ค้างคืน";
                organizationAmountOfBed = "-";
            }
            else if (request.Data.TryGetData("APP_ANIMAL_MED_SECTION").ThenGetStringData("APP_ANIMAL_MED_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_TWO")
            {
                organizationType = "ประเภทมีที่พักสัตว์ป่วยไว้ค้างคืน";
            }

            var permitName = request.PermitName;
            var fullName = string.Empty;
            var partOf = new object();
            var schemaEngName = string.Empty;
            if (request.IdentityType == UserTypeEnum.Citizen)
            { 
                fullName = request.IdentityName; //ชื่อคนที่ขอ
                schemaEngName = "License-to-set-up-an-animal-hospital-Renew";
            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {
                schemaEngName = "License-to-set-up-an-animal-hospital-Juristic-Renew";
                var title = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("DROPDOWN_APP_ANIMAL_MED_OWNER_TITLE_TEXT");
                var firstname = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_FIRSTNAME");
                var lastname = request.Data.TryGetData("APP_ANIMAL_MED_OWNER_INFO").ThenGetStringData("APP_ANIMAL_MED_OWNER_LASTNAME");
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

            // ใบอนุญาตให้ตั้งสถานพยาบาลสัตว์ ตามมาตรฐาน xml schema ใหม่
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
                                        attr_value = "สส.3"
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
            var json = JObject.FromObject(data);
            return JObject.FromObject(data);
        }

        public override JObject GenerateEReceiptData(Guid applicationrequestId)
        {
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var item = new JArray();
            item.Add(new
            {
                // ลำดับรายการ
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
