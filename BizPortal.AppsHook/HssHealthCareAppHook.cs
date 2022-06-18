                                              using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using EGA.WS;
using MongoDB.Driver;
using BizPortal.ViewModels.Apps.HSSStandard;
using BizPortal.Utils.Extensions;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BizPortal.AppsHook
{
    public class HSSHealthCareAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal feePerYear = 1000;
            decimal fee = 0 + feePerYear;
            var sec = sectionData.Where(x => x.SectionName == "APP_HEALTH_SECTION").FirstOrDefault();
            if (sec != null)
            {
                var area = Convert.ToDecimal(sec.FormData["APP_HEALTH_CARE_SPA_AREA"].ToString());
                if (area <= 100)
                {
                    fee += 1000;
                }
                else if (area > 100 && area <= 200)
                {
                    fee += 3000;
                }
                else if (area > 200 && area <= 400)
                {
                    fee += 6000;
                }
                else if (area > 400)
                {
                    fee += 10000;
                }
            }
            return fee;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            try
            {
              
                result.DisabledSendingSystemEmail = false;

                var storeInfo = model.Data["INFORMATION_STORE"].Data;
                if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"))
                {
                    request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"].ToString());
                    request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"].ToString());
                    request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"].ToString());

                    request.Province = storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"].ToString();
                    request.Amphur = storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"].ToString();
                    request.Tumbol = storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"].ToString();
                }

                string regisUrl = ConfigurationManager.AppSettings["HSS_HEALTHCARE_WS_URL"];

                switch (stage)
                {
                    case AppsStage.UserCreate:
                    case AppsStage.UserUpdate:
                        var post = new SpaRegisterRequest()
                        {
                            Data = new Dictionary<string, SpaRegisterFormData>(),
                            Files = new List<HSSFileMetaData>()
                        };
                        var postTmp = new SpaRegisterRequest()
                        {
                            Data = new Dictionary<string, SpaRegisterFormData>(),
                            Files = new List<HSSFileMetaData>()
                        };

                        var formData = new SpaRegisterFormData()
                        {
                            ApplicationRequestID = model.ApplicationRequestID.ToString(),
                            ApplicationRequestNumber = model.ApplicationRequestNumber,
                            ApplicationID = model.ApplicationID,
                            IdentityID = model.IdentityID,     
                            IdentityType = model.IdentityType.ToString(),
                            IdentityName = model.IdentityName,
                            DataFiltered = new Dictionary<string, ApplicationRequestDataGroupViewModel>(),
                            UploadedFiles = model.UploadedFiles,
                            PaymentMedthod = model.PaymentMethod,
                            PermitDeliveryType = model.PermitDeliveryType,
                            PermitDeliveryAddress = model.PermitDeliveryAddress
                        };

                        if (model.IdentityType == UserTypeEnum.Juristic)
                        {
                            if (model.Data.ContainsKey("JURISTIC_ADDRESS_INFORMATION"))
                            {
                                formData.DataFiltered.Add("JURISTIC_ADDRESS_INFORMATION", model.Data["JURISTIC_ADDRESS_INFORMATION"]);
                            }
                        }
                        else if (model.IdentityType == UserTypeEnum.Citizen)
                        {
                            if (model.Data.ContainsKey("CITIZEN_ADDRESS_INFORMATION"))
                            {
                                formData.DataFiltered.Add("CITIZEN_ADDRESS_INFORMATION", model.Data["CITIZEN_ADDRESS_INFORMATION"]);
                            }
                        }

                        if (model.Data.ContainsKey("GENERAL_INFORMATION"))
                        {
                            formData.DataFiltered.Add("GENERAL_INFORMATION", model.Data["GENERAL_INFORMATION"]);
                        }
                        if (model.Data.ContainsKey("INFORMATION_STORE"))
                        {
                            formData.DataFiltered.Add("INFORMATION_STORE", model.Data["INFORMATION_STORE"]);
                        }
                        if (model.Data.ContainsKey("APP_HEALTH_SECTION"))
                        {
                            formData.DataFiltered.Add("APP_HEALTH_SECTION", model.Data["APP_HEALTH_SECTION"]);
                        }
                        if (model.Data.ContainsKey("APP_HEALTH_CARE_MANAGER"))
                        {
                            formData.DataFiltered.Add("APP_HEALTH_CARE_MANAGER", model.Data["APP_HEALTH_CARE_MANAGER"]);
                        }
                        if (model.Data.ContainsKey("APP_HEALTH_CARE_MANAGER_PART"))
                        {
                            formData.DataFiltered.Add("APP_HEALTH_CARE_MANAGER_PART", model.Data["APP_HEALTH_CARE_MANAGER_PART"]);
                        }
                        if (model.Data.ContainsKey("APP_HEALTH_CARE_SERVICE"))
                        {
                            formData.DataFiltered.Add("APP_HEALTH_CARE_SERVICE", model.Data["APP_HEALTH_CARE_SERVICE"]);
                        }
                        post.Data.Add("formData", formData);
                        postTmp.Data.Add("formData", formData);
                        foreach (var upload in model.UploadedFiles)
                        {
                            foreach (var f in upload.Files)
                            {
                                var fileMetaModel = new FileMetadata
                                {
                                    FileID = f.FileID,
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Extras = new Dictionary<string, string>()
                                };

                                if (f.Extras != null)
                                {
                                    foreach (var extra in f.Extras)
                                    {
                                        fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                    }
                                }

                                string Description = "";

                                if (f.Extras.ContainsKey("DISPLAYNAME"))
                                {
                                    Description = f.Extras["DISPLAYNAME"].ToString();
                                }

                                var fileTypeCode = (upload.Description == "REQUESTED_FILE" && f.Extras.ContainsKey("FILETYPENAME")) ? f.Extras["FILETYPENAME"].DefaultString() : f.FileTypeCode;

                                var file = new HSSFileMetaData
                                {
                                    Name = fileTypeCode,
                                    Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Description = Description,
                                };
                                var fileTmp = new HSSFileMetaData
                                {
                                    Name = fileTypeCode,
                                    Content = "",// Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Description = Description,
                                };

                                postTmp.Files.Add(fileTmp);
                                post.Files.Add(file);
                            }
                        }
                       // regisUrl = "/hss/spa/register";
                        var jsonPost = JsonConvert.SerializeObject(post);
                        //แก้ปัญหาขนาดไฟล์ใหญ่ momgoDB
                        var jsonPostTmp = JsonConvert.SerializeObject(postTmp);
                        /* string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();
                         var client2 = new RestClient(DGA_WS_URL + "/ws" + regisUrl);
                         var request_ = new RestRequest(Method.POST);
                         request_.RequestFormat = DataFormat.Json;
                         request_.AddHeader("Token", Api.AccessToken);
                         request_.AddHeader("Consumer-Key", Api.ConsumerKey);
                         request_.AddHeader("Content-Type", "application/json");
                         request_.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                         IRestResponse resp = client2.Execute(request_);
                         if (resp.StatusCode == HttpStatusCode.OK)
                         {
                             DotResponse DotRes = JsonConvert.DeserializeObject<DotResponse>(resp.Content);
                             if (DotRes.IsSuccess == "true")
                             {
                                 result.Success = true;
                             }
                             else
                             {
                                 result.Success = false;
                             }
                         }
                         else
                         {
                             if (resp.StatusCode == HttpStatusCode.BadRequest)
                             {
                                 DotErrorResponse DotErrRes = JsonConvert.DeserializeObject<DotErrorResponse>(resp.Content);
                                 throw new Exception(DotErrRes.Err);
                             }
                             result.Success = false;

                         }*/




                        Api.OnCheckingApplicationError += (ex) =>
                        {
                            result.Exception = ex;
                            var egaEx = ex as EGAWSAPIException;
                            if (egaEx != null)
                            {
                                var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                result.Message = egaEx.ResponseData["Message"].ToString();
                            }
                            else
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                result.Message = ex.Message;
                            }
                        };

                        var apiResponse = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);

                        if (apiResponse != null && apiResponse.HasValues && apiResponse["data"].HasValues && apiResponse["data"]["RESULT"].ToString() == "SUCCESS") 
                        {
                            var message = "Success";
                            result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, message, apiResponse.ToString(), jsonPostTmp);
                            //result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, message, apiResponse.ToString(), jsonPost);
                            result.Success = true;
                        }
                        else
                        {
                            string error = "ไม่สามารถส่งข้อมุลคำร้องได้";
                            //result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, apiResponse.ToString(), jsonPost, true);
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, apiResponse.ToString(), jsonPostTmp, true);
                            result.Success =  false;
                            throw new Exception(error);
                        }
                        break;
                    case AppsStage.None:
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    default:
                        result.Success = true;
                        break;
                }
                
                //return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                //throw ex;

            }
            return result;
        }

        public override bool IsEnabledChat() => false;

        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var identityName = request.IdentityName;
            var permitName = request.PermitName;
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var organizationNameTH = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
            var organizationNameEN = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN");
            var organizationNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
            var organizationSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
            var organizationMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
            var organizationStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
            var organizationCity = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
            var organizationDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
            var organizationState = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
            var organizationCityValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationDistrictValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationStateValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationPostcode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            var organizationCountry = "TH";
            var partOfOrganization = "สปา";
            var permitStartDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitStartDate");
            var permitEndDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndDate");

            switch (request.IdentityType) 
            {
                case UserTypeEnum.Citizen:
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
                                        attr_value = "ส.พ.12"
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
                                name = new
                                {
                                    attr_value = organizationNameTH
                                },
                                alias = new
                                {
                                    attr_value = organizationNameEN
                                },
                                address = new
                                {
                                    text = "",
                                    line = new object[] {
                                        new {
                                        attr_id = "No.",
                                        attr_value = organizationNumber
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
                                        attr_value = organizationPostcode
                                    },
                                    country = new
                                    {
                                        attr_value = organizationCountry
                                    }
                                },
                                partOf = new
                                {
                                    attr_id = partOfOrganization
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitStartDate // ให้ไว้ ณ วันที่ 
                                    },
                                    end = new
                                    {
                                        attr_value = permitEndDate // ใช้ได้ถึงวันที่
                                    }
                                }
                            }
                        }

                    };
                    break;
                case UserTypeEnum.Juristic:
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
                                        attr_value = "ส.พ.12"
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
                                name = new
                                {
                                    attr_value = organizationNameTH
                                },
                                alias = new
                                {
                                    attr_value = organizationNameEN
                                },
                                address = new
                                {
                                    text = "",
                                    line = new object[] {
                                        new {
                                        attr_id = "No.",
                                        attr_value = organizationNumber
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
                                        attr_value = organizationPostcode
                                    },
                                    country = new
                                    {
                                        attr_value = organizationCountry
                                    }
                                },
                                partOf = new
                                {
                                    attr_id = partOfOrganization
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitStartDate // ให้ไว้ ณ วันที่ 
                                    },
                                    end = new
                                    {
                                        attr_value = permitEndDate // ใช้ได้ถึงวันที่
                                    }
                                }
                            }
                        }

                    };
                    break;
            }

            data = new {
                Bundle = new
                {
                    attr_schemaLocation = "http://hl7.org/fhir file:../schema/fhir/bundle.xsd",
                    id = new
                    {
                        attr_value = "Request-for-health-care-spa"
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
                         fullUrl  = new {
                            attr_value  = "https://schemas.teda.th/Composition/1"
                         },
                         resource  = new {
                            Composition  = new {
                               id  = new {
                                  attr_value  = "1"
                               },
                               extension  = new {
                                  attr_url  = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber",
                                  valueString  = new {
                                     attr_value  = "1.0.0"
                                  }
                               },
                               identifier  = new {
                                  attr_id  = licenseNumber
                               },
                               status  = new {
                                  attr_value  = "final"
                               },
                               type  = new {
                                  text  = new {
                                     attr_value  = "สส"
                                  }
                               },
                               subject  = new {
                                  reference  = new {
                                     attr_value  = "https://schemas.teda.th/practitionerrole/pr1"
                                  }
                               },
                               date  = new {
                                  attr_value  = createDate
                               },
                               author  = new {
                                  reference  = new {
                                     attr_value  = "Practitioner/p2"
                                  }
                               },
                               title  = new {
                                  attr_value  = permitName
                               },
                               relatesTo  = new {
                                  code  = new {
                                     attr_id  = "NEW",
                                     attr_value  = "signs"
                                  },
                                  targetIdentifier  = new {
                                     attr_id  = "" // ทส.12345
                                  }
                               }
                            }
                         }
                      },
                      new {
                         fullUrl  = new {
                            attr_value  = "https://schemas.teda.th/practitionerrole/pr1"
                         },
                         resource  = new {
                            PractitionerRole  = new {
                               id  = new {
                                  attr_value  = "pr1"
                               },
                               period  = new {
                                  start  = new {
                                     attr_value  = permitStartDate//"2020-06-10"
                                  },
                                  end  = new {
                                     attr_value  = permitEndDate//"2021-06-10"
                                  }
                               },
                               healthcareService  = new {
                                  reference  = new {
                                     attr_value  = "https://schemas.teda.th/healthcareservice/h1"
                                  }
                               }
                            }
                         }
                      },
                      new {
                         fullUrl  = new {
                            attr_value  = "https://schemas.teda.th/healthcareservice/h1"
                         },
                         resource  = new {
                            HealthcareService  = new {
                               id  = new {
                                  attr_value  = "h1"
                               },
                               providedBy  = new {
                                  reference  = new {
                                     attr_value  = "https://schemas.teda.th/Organization/o1"
                                  }
                               },
                               type  = new {
                                  text  = new {
                                     attr_value  = "สปา"
                                  }
                               }
                            }
                         }
                      },
                      new {
                         fullUrl  = new {
                            attr_value  = "https://schemas.teda.th/Organization/o1"
                         },
                         resource  = new {
                            Organization  = new {
                               id  = new {
                                  attr_value  = "o1"
                               },
                               name  = new {
                                  attr_value  = organizationNameTH//"ชื่อสถานพยาบาล"
                               },
                               alias  = new {
                                  attr_value  = organizationNameEN//"ชื่อสถานพยาบาลภาษาอังกฤษ"
                               },
                               address  = new {
                                  text  = new {
                                     attr_value  = "ระบุที่ตั้งสถานประกอบการแบบ Unstructure"
                                  },
                                  line  = new object[] {
                                     new {
                                        attr_id  = "No.",
                                        attr_value  = organizationNumber//"เลขที่อยู่"
                                     },
                                     new {
                                        attr_id  = "Moo",
                                        attr_value  = organizationMoo//"หมู่"
                                     },
                                     new {
                                        attr_id  = "Soi",
                                        attr_value  = organizationSoi//"ซอย"
                                     },
                                     new {
                                        attr_id  = "Street",
                                        attr_value  = organizationStreet//"ถนน"
                                     }
                                  },
                                  city  = new {
                                     attr_value  = organizationCity,
                                     attr_valueString = organizationCityValue
                                  },
                                  district  = new {
                                     attr_value  = organizationDistrict,
                                     attr_valueString = organizationDistrictValue
                                  },
                                  state  = new {
                                     attr_value  = organizationState,
                                     attr_valueString = organizationStateValue
                                  },
                                  postalCode  = new {
                                     attr_value  = organizationPostcode//"12000"
                                  },
                                  country  = new {
                                     attr_value  = organizationCountry//"TH"
                                  }
                               },
                               contact  = new {
                                  telecom  = new object[] {
                                     new {
                                        system  = new {
                                           attr_value  = "phone"
                                        },
                                        value  = new {
                                           attr_value  = ""//"05 600 0111"
                                        },
                                        use  = new {
                                           attr_value  = "work"
                                        }
                                     },
                                     new {
                                        system  = new {
                                           attr_value  = "fax"
                                        },
                                        value  = new {
                                           attr_value  = ""//"05 622 5505"
                                        },
                                        use  = new {
                                           attr_value  = "work"
                                        }
                                     },
                                     new {
                                        system  = new {
                                           attr_value  = "email"
                                        },
                                        value  = new {
                                           attr_value  = ""//"mail:to"
                                        },
                                        use  = new {
                                           attr_value  = "work"
                                        }
                                     }
                                  }
                               }
                            }
                         }
                      },
                      new {
                         fullUrl  = new {
                            attr_value  = "https://schemas.teda.th/Practitioner/p2"
                         },
                         resource  = new {
                            Practitioner  = new {
                               id  = new {
                                  attr_value  = "p2"
                               },
                               name  = new {
                                  text  = new {
                                     attr_value  =  identityName//"ระบุชื่อ-นามสกุลลผู้รับรองข้อมูล"
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
