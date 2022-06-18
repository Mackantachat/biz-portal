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

namespace BizPortal.AppsHook
{
    public class HSSSpaFeeApphook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            //decimal fee = 0;
            //ISectionData sec;
            //try
            //{
            //    sec = sectionData.Where(x => x.SectionName == "APP_SPA_FEE_PER_YEAR_SECTION_B").FirstOrDefault();
            //    if (sec != null)
            //    {
            //        switch (sec.FormData.TryGetString("APP_SPA_FEE_PER_YEAR_SECTION_B_CONTROL_B_OPTION", string.Empty))
            //        {

            //            case "1000":
            //                fee = 1000;
            //                break;
            //            case "3000":
            //                fee = 3000;
            //                break;
            //            case "6000":
            //                fee = 6000;
            //                break;
            //            case "10000":
            //                fee = 10000;
            //                break;
            //            default:
            //                fee = 0;
            //                break;
            //        }
            //    }
            //}

            //catch
            //{
            //    return fee;
            //}

            return 1000;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            var canExportStatus = new ApplicationStatusV2Enum[]
            {
                ApplicationStatusV2Enum.COMPLETED
            };

            return canExportStatus.Contains(status);
        }

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);

            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var identityName = request.IdentityName;
            var licenseNumberToPay = request.Data.TryGetData("APP_SPA_FEE_PER_YEAR_SECTION_A").ThenGetStringData("APP_SPA_FEE_PER_YEAR_SECTION_A_CONTROL_A");
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var fee = request.Fee;
            var feeText = "";
            var organizationAddressNumber = string.Empty;
            var organizationSoi = string.Empty;
            var organizationStreet = string.Empty;
            var organizationCity = string.Empty;
            var organizationCityValue = string.Empty;
            var organizationDistrict = string.Empty;
            var organizationDistrictValue = string.Empty;
            var organizationState = string.Empty;
            var organizationStateValue = string.Empty;
            var organizationPostalCode = string.Empty;
            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                organizationAddressNumber = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                organizationSoi = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                organizationStreet = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                organizationCity = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS");
                organizationCityValue = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");
                organizationDistrict = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS");
                organizationDistrictValue = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT");
                organizationState = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS");
                organizationStateValue = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT");
                organizationPostalCode = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");

            }
            else if (request.IdentityType == UserTypeEnum.Juristic)
            {
                organizationAddressNumber = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                organizationSoi = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                organizationStreet = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                organizationCity = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS");
                organizationCityValue = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                organizationDistrict = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS");
                organizationDistrictValue = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                organizationState = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS");
                organizationStateValue = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                organizationPostalCode = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
            }
            var organizationCountry = "TH";
            var periodStart = "";
            var periodEnd = "";

            var requestLienseOrginal = ApplicationRequestEntity.GetRelateLicense(licenseNumberToPay,identityName);
            var oganizationNameLicenseOriginal = requestLienseOrginal.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");


            data = new
            {

                DocumentReference = new
                {
                    extension = new object[] {
                       new {
                          attr_url = "https://oid.estandard.or.th",
                          valueOid = new {
                             attr_id = "2.16.764.1.4.100.16.1.1.1"
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
                        attr_id = licenseNumber // ตาม docx คือ เลขที่รับคำขอ
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
                                attr_value = "สพส.17"
                            }
                        }
                    },
                    subject = new
                    {
                        type = new
                        {
                            attr_value = "สปา"
                        },
                        identifier = new
                        {
                            attr_id = licenseNumberToPay
                        },
                        display = new
                        {
                            attr_value = identityName
                        }
                    },
                    date = new
                    {
                        attr_id = createDate
                    },
                    author = new
                    {
                        type = new
                        {
                            text = new
                            {
                                extension = new object[] {
                                    new {
                                        attr_url = "ค่าธรรมเนียม",
                                        valueMoney = new {
                                            attr_id = fee
                                        }
                                    },
                                    new {
                                        attr_url = "ค่าธรรมเนียมแบบข้อความ",
                                        valueString = new {
                                            attr_value = feeText
                                        }
                                    }
                                }
                            }
                        },
                        address = new
                        {
                            text = "",
                            line = new object[] {
                                new {
                                    attr_id = "No",
                                    attr_value = organizationAddressNumber
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
                                attr_value = organizationPostalCode,
                            },
                            country = new
                            {
                                attr_value = organizationCountry
                            }
                        }
                    },
                    custodian = new
                    {
                        attr_id = "กรมสนับสนุนบริการสุขภาพ"
                    },
                    context = new
                    {
                        period = new
                        {
                            start = new
                            {
                                attr_value = periodStart // วันที่เริ่มชำระ
                            },
                            end = new
                            {
                                attr_value = periodEnd // ชำระได้ถึงวันที่
                            }
                        }
                    }
                }

            };

            // สพส.17 สปา ตามาตรฐาน xml schema ใหม่
            data = new
            {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "Annual-Fee-Payment-Form-Spa"
                    },
                    identifier = new
                    {
                        system = new
                        {
                            attr_value = "https://oid.estandard.or.th"
                        },
                        value = new
                        {
                            attr_value = "2.16.764.1.4.100.16.4.1.1"
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
                                    attr_id = licenseNumber//"ใบอนุญาตที่"
                                },
                                status = new {
                                    attr_value = "final"
                                },
                                type = new {
                                    text = new {
                                        attr_value = "สพส.17"
                                    }
                                },
                                subject = new {
                                    reference = new {
                                        attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                                    }
                                },
                                date = new {
                                    attr_value = createDate//"2013-02-01T12:30:02Z"
                                },
                                author = new {
                                    reference = new {
                                        attr_value = "Practitioner/p2"
                                    }
                                },
                                title = new {
                                    attr_value = "การชำระค่าธรรมเนียมรายปี ใบอนุญาตประกอบกิจการสถานพยาบาล (สปา)"
                                },
                                relatesTo = new {
                                    code = new {
                                        attr_id = "RENEW",
                                        attr_value = "signs"
                                    },
                                    targetIdentifier = new {
                                        attr_id = licenseNumberToPay//"ทส.123456"
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
                                        attr_value = periodStart//"2013-10-01"  วันที่ชำระเงิน
                                    },
                                    end = new {
                                        attr_value = periodEnd//"2013-12-31" วันครบกำหนดชำระเงินใบปีต่อไป
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
                                        attr_value = "สปา"
                                    }
                                },
                                specialty = new {
                                    text = new {
                                        attr_value = "-"
                                    }
                                },
                                comment = new {
                                    attr_value = "สปา"
                                },
                                extraDetails = new {
                                    attr_value = ""//"ประจำปีพ.ศ.ระบุปี"
                                },
                                photo = new {
                                    contentType = new {
                                        attr_value = "image/gif"
                                    },
                                    data = new {
                                        attr_value = "MTIzNA=="
                                    }
                                },
                                eligibility = new {
                                    comment = new {
                                        attr_id = fee,//"200,000.00",
                                        attr_value = feeText//"สองแสนบาทถ้วน"
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
                                    attr_value = oganizationNameLicenseOriginal//"ชื่อสถานพยาบาล"
                                },
                                address = new {
                                    text = new {
                                        attr_value = "ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                                    },
                                    line = new {
                                        attr_value = organizationAddressNumber//"ระบุเลขทีอยู่"
                                    },
                                    city = new {
                                        attr_value = organizationCity,//"100101"
                                        attr_valueString = organizationCityValue
                                    },
                                    district = new {
                                        attr_value = organizationDistrict,//"1001"
                                        attr_valueString = organizationDistrictValue
                                    },
                                    state = new {
                                        attr_value = organizationState,//"10"
                                        attr_valueString = organizationStateValue
                                    },
                                    postalCode = new {
                                        attr_value = organizationPostalCode//"12000"
                                    },
                                    country = new {
                                        attr_value = organizationCountry//"TH"
                                    }
                                },
                                contact = new {
                                    telecom = new object[] {
                                        new {
                                        system = new {
                                            attr_value = "phone"
                                        },
                                        value = new {
                                            attr_value = ""//
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
                                            attr_value = ""//
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
                                            attr_value = ""//
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
                                        attr_value = identityName//"ระบุชื่อ-นามสกุลลผู้รับรองข้อมูล"
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
