using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using iTextSharp.text.pdf;
using System.IO;
using BizPortal.Utils.Helpers;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Configuration;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.HSSStandard;

namespace BizPortal.AppsHook
{
    public class HSSHospitalFeeApphook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            ISectionData sec;
            try
            {
                sec = sectionData.Where(x => x.SectionName == "APP_CLINIC_OVER_NIGHT_SECTION_B").FirstOrDefault();
                if (sec != null)
                {

                    switch (sec.FormData.TryGetString("APP_CLINIC_OVER_NIGHT_SECTION_B_CONTROL_B_OPTION", string.Empty))
                    {

                        case "1":
                            fee = 500;
                            break;
                        case "2":
                            fee = 1250;
                            break;
                        case "3":
                            fee = 2500;
                            break;
                        case "4":
                            fee = 5000;
                            break;
                        case "5":
                            fee = CalculateFee(Convert.ToInt32(sec.FormData.TryGetString("APP_CLINIC_OVER_NIGHT_SECTION_B_CONTROL_D", string.Empty)));
                            break;
                        default:
                            fee = 0;
                            break;
                    }


                }
            }

            catch
            {
                return fee;
            }

            return fee;
        }

        private decimal CalculateFee(int bed)
        {
            decimal feeNew = 0;

            try
            {

                feeNew = 5000 + (bed * 100);
                if (feeNew > 10000)
                {
                    feeNew = 10000;
                }

            }
            catch
            {
                return 0;
            }


            return feeNew;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            //applicationrequestId = new Guid("e61bf909-f758-4e79-98c4-a45cb16983e9");
            string runningNo = string.Empty;
            int docNo = 0;
            try
            {
                // get ApplicationNumber

                // List<string> listAppID = ConfigurationManager.AppSettings["HSS_APPLICATIONID_EDOC"].Split(',').ToList();    
                List<string> listAppName = ConfigurationManager.AppSettings["HSS_APPLICATIONID_EDOC"].Split(',').ToList();
                List<string> listAppID = DB.Applications.Where(x => x.ApplicationID > 0 && listAppName.Contains(x.ApplicationSysName)).Select(c => c.ApplicationID.ToString()).ToList();

                var requestsApp = MongoFactory.GetDatabase().GetCollection<BsonDocument>("ApplicationRequest");
                //'ApplicationID':414
                string exp = "{'ApplicationRequestID':'" + applicationrequestId + "'}";
                string ApplicationRequestNumber = string.Empty;
                var result = requestsApp.Find(exp).ToList();                    
                foreach (var item in result)
                {
                    ApplicationRequestNumber = item["ApplicationRequestNumber"].ToString();
                }
                // get bYear from applicationRequestId
                //applicationrequestId =   "dc198db5-f71d-384c-93f8-1de9e18a5e0a";
                var requestPayment = MongoFactory.GetDatabase().GetCollection<BsonDocument>("PaymentTransaction");
                //var resappreqId = requestapp.Find("{'ApplicationRequestId':'LUUID(" + applicationrequestId + ")','Ststus':'1'}").ToList();
                
                //ApplicationRequestNumber = "J631022002";
                exp = "{'Status':1,'ApplicationRequestNumber':'"+ ApplicationRequestNumber + "'}";
                //var bytes = GuidConverter.ToBytes(applicationrequestId, GuidRepresentation.PythonLegacy);
                //var csuuid = new Guid(bytes);
                var resPayment = requestPayment.Find(exp).ToList();
                //var resappreqId = requestapp.Find("{'ApplicationRequestId':'"+ csuuid + "'}").ToList();
                int bYear = 0;              
                foreach (var item in resPayment)
                {
                    bYear =    Convert.ToDateTime(item["UpdatedDate"]).Year;
                    int month = Convert.ToDateTime(item["UpdatedDate"]).Month;
                    if (month > 9) bYear = bYear + 1;
                    //var ApplicationRequestId = item["ApplicationRequestId"].ToString();
                }
                var requestsEdoc = MongoFactory.GetDatabase().GetCollection<BsonDocument>("EDocument");
                //'ApplicationID':414
                var resEdoc = requestsEdoc.Find("{'SigningStatus':'COMPLETED','ApplicationID':{ $exists: true, $ne: null }}").ToList();
               
                List<HSS_EDOCDetail> collections = new List<HSS_EDOCDetail>();
                // request level
                foreach (var item in resEdoc)
                {

                    HSS_EDOCDetail edoc = new HSS_EDOCDetail();
                    edoc.ApplicationID= item["ApplicationID"].ToString();
                    edoc.ApplicationRequestID = item["ApplicationRequestID"].ToString();
                    edoc.SigningStatus = item["SigningStatus"].ToString();
                    edoc.UpdatedDate = (DateTime)item["UpdatedDate"];
                    collections.Add(edoc);
                }
                int fiscalYear = bYear;
                DateTime start = new DateTime(fiscalYear - 1, 10, 1, 0, 0, 0, 0, DateTimeKind.Local);
                DateTime end = new DateTime(fiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Local);
                var res = collections.Where(x => x.UpdatedDate >= start && x.UpdatedDate < end && listAppID.Contains(x.ApplicationID)).Count();
                docNo = res+1;               
                runningNo = "e" + docNo.ToString("D5");

            }
            catch (Exception ex)
            {
                runningNo = "e" + docNo.ToString("D5");
            }

            //int fiscalYear = 2020;
            //DateTime start = new DateTime(fiscalYear - 1, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //DateTime end = new DateTime(fiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            //DateTime end2 = new DateTime(fiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Local);
            //DateTime end3 = new DateTime(fiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Unspecified);
            
            //string stratd = start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            //string endd = end.ToString("yyyy-MM-dd'T'HH:mm:ss"); //end.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");
            //DateTime start2 = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            //DateTime date = DateTime.Parse("2020-01-10");

            //string foo = start.ToUniversalTime()
            //             .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK");

            //var text = end.ToString("o");


            ////var result = requests.Find("{'UpdatedDate':{ $gte:ISODate('2020-06-24 07:51:22.053Z'), $lt:ISODate('2020-11-11 07:51:22.053Z')}}").ToList();  
            //try
            //{
            //    //var result = requests.Find("{'UpdatedDate':{ $gte:ISODate('2020-06-24 07:51:22.053Z'), $lt:ISODate('2020-11-11 07:51:22.053Z')}}").ToList();

            //    //foreach (var item in result)
            //    //{

            //    //}
            //}
            //catch (Exception e)
            //{

            //}
                //var result = requests.Find("{'SigningStatus':'COMPLETED','UpdatedDate':{ $gte:ISODate("+ stratd + "), $lt:ISODate("+endd+")}").ToList();


                /*FilterDefinition<DPTLicense> filter = builder.Where(entity =>
                                                                    (entity.SubmitDate >= start)
                                                                    && (entity.SubmitDate < end));*/

            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var identityName = request.IdentityName;
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var organizationName = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_A");
            var organizationAddressNumber = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            if (String.IsNullOrEmpty(organizationAddressNumber))
            {
                organizationAddressNumber = "-";
            }
            var organizationSoi = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_SOI_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            if (String.IsNullOrEmpty(organizationSoi))
            {
                organizationSoi = "-";
            }
            var organizationStreet = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_ROAD_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            if (String.IsNullOrEmpty(organizationStreet))
            {
                organizationStreet = "-";
            }
            var organizationCity = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            var organizationCityValue = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C_TEXT");
            var organizationDistrict = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            var organizationDistrictValue = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C_TEXT");
            var organizationState = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            var organizationStateValue = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C_TEXT");
            var organizationPostalCode = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("ADDRESS_POSTCODE_APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_C");
            var organizationCountry = "TH";
            var yearOfFee = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("YearOfFee");
            var nextYear = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Year");
            var qrcodeItemId = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("QRCodeItemId");
            
            var relateCode = "signs"; // สถานะของเอกสารที่ถูกอ้างอิง ตาม xml schema ถูกกำหนดให้มีสถานะได้แค่ดังนี้ transforms , appends , replaces , signs
            var relateTarget = request.Data.TryGetData("APP_CLINIC_OVER_NIGHT_SECTION_A").ThenGetStringData("APP_CLINIC_OVER_NIGHT_SECTION_A_CONTROL_B"); // เลขที่ของเอกสารที่ถูกอ้างอิง

          
            // json ตามโครงสร้างมาตรฐานใหม่ของ xml schema 
            data = new {
                Bundle = new
                {
                    id = new
                    {
                        attr_value = "Annual-Fee-Payment-Form"
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
                    runningNo = new
                    {
                        attr_value = runningNo.ToThaiDigit()
                    },
                    type = new
                    {
                        attr_value = "document"
                    },
                    timestamp = new
                    {
                        attr_value = createDate.ToThaiDigit()
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
                                         attr_value = "ส.พ.12"
                                      }
                                   },
                                   subject = new {
                                      reference = new {
                                         attr_value = "https://schemas.teda.th/practitionerrole/pr1"
                                      }
                                   },
                                   date = new {
                                      attr_value = createDate.ToThaiDigit()
                                   },
                                   author = new {
                                      reference = new {
                                         attr_value = "Practitioner/p2"
                                      }
                                   },
                                   title = new {
                                      attr_value = "การชำระค่าธรรมเนียมรายปี ใบอนุญาตประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)"
                                   },
                                   relatesTo = new {
                                      code = new {
                                         attr_id = "RENEW",
                                         attr_value = relateCode
                                      },
                                      targetIdentifier = new {
                                         attr_id = relateTarget.ToThaiDigit()
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
                                         attr_value = yearOfFee.ToArabicDigit()
                                      },
                                      end = new {
                                         attr_value = nextYear.ToArabicDigit()
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
                                         attr_value = "รับผู้ป่วยไว้ค้างคืน"
                                      }
                                   },
                                   specialty = new {
                                      text = new {
                                         attr_value = "-"
                                      }
                                   },
                                   comment = new {
                                      attr_value = ""
                                   },
                                   extraDetails = new {
                                      attr_value = yearOfFee.ToArabicDigit() //ชำระงเงินประจำปี
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
                                      attr_value = organizationName.ToThaiDigit()
                                   },
                                   address = new {
                                      text = new {
                                         attr_value = "ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                                      },
                                      line = new {
                                         attr_value = organizationAddressNumber.ToThaiDigit()
                                      },
                                      city = new {
                                         attr_value = organizationCity.ToThaiDigit(),
                                         attr_valueString = organizationCityValue.ToThaiDigit()
                                      },
                                      district = new {
                                         attr_value = organizationDistrict.ToThaiDigit(),
                                         attr_valueString = organizationDistrictValue.ToThaiDigit().RemoveTextDistrict()
                                      },
                                      state = new {
                                         attr_value = organizationState.ToThaiDigit(),
                                         attr_valueString = organizationStateValue.ToThaiDigit()
                                      },
                                      postalCode = new {
                                         attr_value = organizationPostalCode.ToThaiDigit()
                                      },
                                      country = new {
                                         attr_value = organizationCountry
                                      }
                                   },
                                   contact = new {
                                      telecom = new object[] {
                                         new {
                                            system = new {
                                               attr_value = "phone"
                                            },
                                            value = new {
                                               attr_value = ""
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
                                               attr_value = ""
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
                                               attr_value = ""
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
                                         attr_value = identityName.ToThaiDigit()
                                      }
                                   }
                                }
                             }
                          }
                       },
                    },
                Links = new
                {
                    QrCode = "http://hssc.hss.moph.go.th/medicalViewSp19.php?itemId=" + qrcodeItemId
                }

            };

            var json = JObject.FromObject(data);
            return JObject.FromObject(data);

        }

    }
    
   

}
