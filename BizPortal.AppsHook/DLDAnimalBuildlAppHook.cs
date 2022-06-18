using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Newtonsoft.Json.Linq;
using BizPortal.Utils.Extensions;
using Org.BouncyCastle.Ocsp;
using System.Globalization;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class DLDAnimalBuildlAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
        //ขอหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์
        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
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

                var title = request.Data.TryGetData("APP_ANIMAL_BUILD_OWNER_INFO").ThenGetStringData("DROPDOWN_APP_ANIMAL_BUILD_OWNER_TITLE_TEXT");
                var firstname = request.Data.TryGetData("APP_ANIMAL_BUILD_OWNER_INFO").ThenGetStringData("APP_ANIMAL_BUILD_OWNER_FIRSTNAME");
                var lastname = request.Data.TryGetData("APP_ANIMAL_BUILD_OWNER_INFO").ThenGetStringData("APP_ANIMAL_BUILD_OWNER_LASTNAME");
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
         
            var practitionerQualificationId = request.Data.TryGetData("APP_ANIMAL_BUILD_OWNER_INFO").ThenGetStringData("APP_ANIMAL_BUILD_OWNER_LICENSE").ToThaiDigit();
    
            // var practitionerQualificationStart = "";
            var organizationName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH_CUSTOM").ToThaiDigit();
            var organizationAmountOfBed = request.Data.TryGetData("APP_ANIMAL_BUILD_SECTION").ThenGetStringData("APP_ANIMAL_BUILD_SECTION_AMOUNT").ToThaiDigit();
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

            var organizationType = string.Empty;
            if (request.Data.TryGetData("APP_ANIMAL_BUILD_SECTION").ThenGetStringData("APP_ANIMAL_BUILD_SECTION_MED_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_ONE")
            {
                organizationType = "ประเภทไม่มีที่พักสัตว์ป่วยไว้ค้างคืน";
                organizationAmountOfBed = "-";
            }
            else if (request.Data.TryGetData("APP_ANIMAL_BUILD_SECTION").ThenGetStringData("APP_ANIMAL_BUILD_SECTION_MED_TYPE_OPTION") == "APP_ANIMAL_MED_TYPE_TWO")
            {
                organizationType = "ประเภทมีที่พักสัตว์ป่วยไว้ค้างคืน";
            }

            switch (request.IdentityType)
            {
                case UserTypeEnum.Citizen:
                case UserTypeEnum.Juristic:
                    data = new
                    {
                        //ใบอนุญาตให้ตั้งสถานพยาบาลสัตว์
                        DocumentReference = new
                        {
                            extension = new object[]
                                        {
                                                   new{
                                                     attr_url= "https://oid.estandard.or.th",
                                                     valueOid = new
                                                     {
                                                       attr_id = "2.16.764.1.4.100.16.5.1.1"
                                                     }
                                                   },
                                                   new{
                                                     attr_url = "VersionNumber",
                                                     valueString  = new
                                                     {
                                                        attr_value = "1.0"
                                                     }
                                                   }
                                       },
                            identifier = new
                            {
                                attr_id = licenseNumber.ToThaiDigit()// "เลขที่ใบอนุญาต"
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
                                        attr_value = "สส.11/12"
                                    }
                                }
                            },
                            subject,
                            date = new
                            {
                                attr_value = createDate.ToThaiDigit()
                            },

                            author = new
                            {
                                practitioner = new
                                {                                   
                                    qualification = new
                                    {
                                        identifier = new
                                        {
                                            attr_id = practitionerQualificationId.ToThaiDigit(),
                                            value = new
                                            {
                                                attr_value = ""
                                            }
                                        },
                                        period = new
                                        {
                                            start = new
                                            {
                                                attr_value = ""
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
                                                       new{
                                                            attr_url="จำนวนเตียง",
                                                            valueQuantity = new{
                                                             attr_id = organizationAmountOfBed.ToThaiDigit()
                                                             }
                                                          },
                                                       new{
                                                            attr_url = "ลักษณะของสถานพยาบาล",
                                                            valueString = new{
                                                            attr_value = organizationType.ToThaiDigit()
                                                            }
                                                         }
                                                    }
                                        }
                                    },//type
                                    name = new
                                    {
                                        attr_value = organizationName.ToThaiDigit()
                                    },//name                                   
                                    address = new
                                    {
                                        text = "",
                                        line = new object[]
                                                        {
                                                                new{
                                                                        attr_id = "No.",
                                                                        attr_value = organizationAddressNumber.ToThaiDigit()
                                                                },
                                                                new{
                                                                        attr_id = "Soi",
                                                                        attr_value = organizationSoi.ToThaiDigit()
                                                                },
                                                                new{
                                                                        attr_id = "Street",
                                                                        attr_value = organizationStreet.ToThaiDigit()
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

                                    }//address
                                }// organization
                            }, //author
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitStartDate.ToThaiDigit()
                                    },
                                    end = new
                                    {
                                        attr_value = permitEndDate.ToThaiDigit()
                                    }
                                }
                            }
                        }
                    };
                    break;               
            }


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
