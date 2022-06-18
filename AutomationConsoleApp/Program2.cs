using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EGA.EGA_CentralLog.Util;
using System.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using BizPortal.ViewModels.V2;
using BizPortal.DAL.MongoDB;
using Mapster;
using BizPortal.Utils.Extensions;

//namespace AutomationConsoleApp2
//{
//    public class DPTRequest
//    {
//        public DPTRequest() {
//            Address = new DPTAddress();
//            Committees = new List<DPTCommittee>();
//        }

//        public string ID { get; set; }
//        public string Name { get; set; }
//        public decimal Money { get; set; }
//        public DPTAddress Address { get; set; }
//        public List<DPTCommittee> Committees { get; set; }
//    }

//    public class DPTAddress
//    {
//        public string AddrNo { get; set; }
//        public string Province { get; set; }
//        public string District { get; set; }
//        public string SubDistrict { get; set; }
//    }

//    public class DPTCommittee
//    {
//        public string ComID { get; set; }
//        public string ComName { get; set; }
//    }

//    public class MappingModel
//    {
//        public string AppSysName { get; set; }
//        public List<SectionModel> Sections { get; set; }
//    }

//    public class SectionModel
//    {
//        public string SectionName { get; set; }
//        public Dictionary<string, string> Mappers { get; set; }
//    }

//    public enum RequestDataType
//    {
//        Data,
//        ArrayData
//    }

//    public class RequestViewModel
//    {
//        public Dictionary<string, RequestDataGroupViewModel> Data { get; set; }
//    }

//    public class RequestDataGroupViewModel
//    {
//        public RequestDataGroupViewModel()
//        {
//            Data = new Dictionary<string, object>();
//            ArrayData = new List<Dictionary<string, object>>();
//        }
//        public string GroupName { get; set; }
//        public RequestDataType DataType { get; set; }
//        public Dictionary<string, object> Data { get; set; }
//        public List<Dictionary<string, object>> ArrayData { get; set; }
//    }

//    public static class ObjectExtensions
//    {
//        public static T ToOrg<T>(this RequestViewModel model, MappingModel mappers) where T : class, new()
//        {
//            var dest = new T();
//            var destType = dest.GetType();
//            var destObject = JObject.FromObject(dest);
//            var modelData = model.Data;

//            foreach (var mapper in mappers.Sections)
//            {
//                var secName = mapper.SectionName;
//                if (modelData.ContainsKey(secName) && mapper.Mappers != null && mapper.Mappers.Count > 0)
//                {
//                    if (modelData[secName].DataType == RequestDataType.Data)
//                    {
//                        foreach (var map in mapper.Mappers)
//                        {
//                            var data = modelData.TryGetData(secName).ThenGetStringData(map.Key);
//                            destObject.SelectToken(map.Value).Replace(data);
//                        }
//                    }
//                    else if (modelData[secName].DataType == RequestDataType.ArrayData)
//                    {
//                        var arrData = modelData.TryGetArrayData(secName);
//                        var parrent = string.Empty;
//                        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
//                        foreach (var arr in arrData)
//                        {
//                            Dictionary<string, object> dict = new Dictionary<string, object>();
//                            foreach (var map in mapper.Mappers)
//                            {
//                                var data = arr.ThenGetArrayData(map.Key);
//                                var splitKey = map.Value.Split('.');
                                
//                                parrent = splitKey[0];
//                                var child = splitKey[1];

//                                dict.Add(splitKey[1], data);
//                            }
//                            list.Add(dict);
//                        }
//                        if (!string.IsNullOrEmpty(parrent))
//                        {
//                            destObject.SelectToken(parrent).Replace(JArray.FromObject(list));
//                        }
//                    }
                    
//                }
//            }

//            #region [TEST]
//            //if (reqData.ContainsKey("Data"))
//            //{
//            //    var data = (Dictionary<string, DataModel>)reqData["Data"];

//            //    if (mapper.Sections != null && mapper.Sections.Count > 0)
//            //    {
//            //        foreach (var map in mapper.Sections)
//            //        {
//            //            if (data.ContainsKey(map.SectionName))
//            //            {
//            //                var mapData = map.Mappers;
//            //                var secData = data[map.SectionName].Data;
//            //                foreach (var item in mapData)
//            //                {
//            //                    if (secData.ContainsKey(item.Key))
//            //                    {
//            //                        objType.GetProperty(item.Value).SetValue(obj, secData[item.Key], null);
//            //                    }
//            //                }
//            //            }
//            //        }
//            //    }
//            //}
//            #endregion

//            dest = destObject.ToObject<T>();

//            return dest;
//        }

//        public static RequestDataGroupViewModel TryGetData(this Dictionary<string, RequestDataGroupViewModel> data, string key)
//        {
//            if (data != null && data.ContainsKey(key))
//            {
//                return data[key];
//            }
//            return null;
//        }

//        public static string ThenGetStringData(this RequestDataGroupViewModel data, string key, string defaultValue = "")
//        {
//            if (data != null && data.Data != null && data.Data.ContainsKey(key))
//            {
//                return data.Data[key].ToString();
//            }

//            return defaultValue;
//        }

//        public static List<Dictionary<string, object>> TryGetArrayData(this Dictionary<string, RequestDataGroupViewModel> data, string key)
//        {
//            if (data != null && data.ContainsKey(key))
//            {
//                return data[key].ArrayData;
//            }

//            return new List<Dictionary<string, object>>();
//        }

//        public static string ThenGetArrayData(this Dictionary<string, object> data, string key, string defaultValue = "")
//        {
//            if (data != null && data.ContainsKey(key))
//            {
//                return data[key].ToString();
//            }
//            return defaultValue;
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var mappers = new MappingModel()
//            {
//                AppSysName = "APP",
//                Sections = new List<SectionModel>()
//            };

//            var secA = new SectionModel()
//            {
//                SectionName = "DBD",
//                Mappers = new Dictionary<string, string>()
//                {
//                    { "JURISTIC_ID", "ID" },
//                    { "JURISTIC_NAME_TH", "Name" },
//                    { "REGIS_CAPTAL", "Money" }
//                }
//            };

//            var secB = new SectionModel()
//            {
//                SectionName = "JURISTIC_ADDRESS_INFORMATION",
//                Mappers = new Dictionary<string, string>()
//                {
//                    { "ADDRESS_JURISTIC_HQ_ADDRESS", "Address.AddrNo" },
//                    { "ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", "Address.Province" },
//                    { "ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", "Address.District" },
//                    { "ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", "Address.SubDistrict" }
//                }
//            };

//            var secC = new SectionModel()
//            {
//                SectionName = "COMMITTEE_INFORMATION",
//                Mappers = new Dictionary<string, string>()
//                {
//                    { "JURISTIC_COMMITTEE_CITIZEN_ID", "Committees.ComID" },
//                    { "JURISTIC_COMMITTEE_NAME", "Committees.ComName" },
//                }
//            };

//            mappers.Sections.Add(secA);
//            mappers.Sections.Add(secB);
//            mappers.Sections.Add(secC);

//            //var req = ApplicationRequestEntity.Get(new Guid("109f876f-38a0-4168-a8f5-9645c2446da0"));
//            //var model = req.Adapt<ApplicationRequestViewModel>();
            
//            var rawData = new Dictionary<string, RequestDataGroupViewModel>()
//            {
//                {
//                    "DBD", new RequestDataGroupViewModel()
//                    {
//                        GroupName = "DBD",
//                        DataType = RequestDataType.Data,
//                        Data = new Dictionary<string, object>()
//                        {
//                            { "JURISTIC_ID", "0105553077469" },
//                            { "JURISTIC_NAME_TH", "สยาม คอมพลีท โซลูชั่น จำกัด" },
//			                { "REGIS_CAPTAL", "1000000" },

//                        }
//                    }
//                },
//                {
//                    "JURISTIC_ADDRESS_INFORMATION", new RequestDataGroupViewModel()
//                    {
//                        GroupName = "JURISTIC_ADDRESS_INFORMATION",
//                        DataType = RequestDataType.Data,
//                        Data = new Dictionary<string, object>()
//                        {
//                            { "ADDRESS_JURISTIC_HQ_ADDRESS", "590" },
//                            { "ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", "กรุงเทพมหานคร" },
//                            { "ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", "เขตคลองสามวา" },
//                            { "ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", "ทรายกองดิน" }
//                        }
//                    }
//                },
//                {
//                    "COMMITTEE_INFORMATION", new RequestDataGroupViewModel()
//                    {
//                        GroupName = "COMMITTEE_INFORMATION",
//                        DataType = RequestDataType.ArrayData,
//                    }
//                }
//            };

//            rawData["COMMITTEE_INFORMATION"].ArrayData.Add(new Dictionary<string, object>()
//            {
//                { "JURISTIC_COMMITTEE_CITIZEN_ID", "3260400340448" },
//                { "JURISTIC_COMMITTEE_NAME", "สมเกียรติ" }
//            });

//            rawData["COMMITTEE_INFORMATION"].ArrayData.Add(new Dictionary<string, object>()
//            {
//                { "JURISTIC_COMMITTEE_CITIZEN_ID", "3100101225931" },
//                { "JURISTIC_COMMITTEE_NAME", "ศิริรัตน์" }
//            });

//            rawData["COMMITTEE_INFORMATION"].ArrayData.Add(new Dictionary<string, object>()
//            {
//                { "JURISTIC_COMMITTEE_CITIZEN_ID", "1100500197331" },
//                { "JURISTIC_COMMITTEE_NAME", "สมเจตน์" }
//            });

//            RequestViewModel model = new RequestViewModel() { Data = rawData };

//            var returnModel = model.ToOrg<DPTRequest>(mappers);

//            #region [TEST]
//            //var jsonstr = "{\"ID\": \"0001\",\"Name\": \"Pikachu\",\"Age\": 25,\"Money\": 100.50,\"Tac\": {\"TacID\": \"999999\",\"TacName\": \"Hail Hydra\"}}";
//            //Tic jsonConvert = JsonConvert.DeserializeObject<Tic>(jsonstr);

//            //var dataAModel = new DataModel()
//            //{
//            //    SectionName = "A_SECTION",
//            //    Data = new Dictionary<string, object>()
//            //    {
//            //        { "A_ID", "test" },
//            //        { "A_NAME", "test" },
//            //        { "A_AGE", "0" },
//            //        { "A_MONEY", "0" }
//            //    }
//            //};

//            //var dataBModel = new DataModel()
//            //{
//            //    SectionName = "B_SECTION",
//            //    Data = new Dictionary<string, object>()
//            //    {
//            //        { "B_TACID", "test" },
//            //        { "B_TOENAME", "test" },
//            //    }
//            //};

//            //var requestModel = new RequestModel()
//            //{
//            //    Data = new Dictionary<string, DataModel>()
//            //};

//            //requestModel.Data.Add("A_SECTION", dataAModel);
//            //requestModel.Data.Add("B_SECTION", dataBModel);

//            //requestModel.DgaToOrg<Tic>(mapModel);
//            #endregion
//        }
//    }
//}
