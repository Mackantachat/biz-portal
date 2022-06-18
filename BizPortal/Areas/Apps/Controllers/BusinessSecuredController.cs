using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizPortal.ViewModels;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Annotations;
using BizPortal.DAL.MongoDB;
using System.Text.RegularExpressions;
using System.Globalization;
using BizPortal.Utils;
using BizPortal.Integrated;
using BizPortal.ViewModels.Select2;

namespace BizPortal.Areas.Apps.Controllers
{
    public class BusinessSecuredController : AppsControllerBase
    {
        // GET: BusinessSecured
        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Index()
        {           
            var provinces = GeoService.Provinces("", "", CurrentCulture).ToList();
            provinces.Add(new ProvinceSelect2Opt {
                ID = "98",
                Text = "เบตง"
            });
            ViewBag.ProvinceList = provinces;
            return View();
        }

        public ActionResult Agreement()
        {
            return View();           
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public JsonResult GetBussinessData(SearchBusinessSecured searchView)
        {
            #region [Logging]
            var logData = new
            {
                IdentityID,
                IdentityType = IdentityType.ToString(),
                Data = searchView
            };
            var log = new ActivityLog { Type = "BusinessSecured", Path = "BusinessSecured/GetBusinessData", Data = logData };
            log.Create();
            #endregion

            string RESULT = "FALSE";
            string RESULTMD = "FALSE";
            string RESULTDBD = "FALSE";
            string RESULTDIW = "FALSE";
            string RESULTDOPA = "FALSE";
            string VALUE = "ไม่พบข้อมูล";
            string md_ws_url = string.Empty;
            string dbd_ws_url = string.Empty;
            string diw_ws_url = string.Empty;
            string dopa_ws_url = string.Empty;
            string searchNo = string.Empty;
            string searchName = string.Empty;
            string assetName = string.Empty;
            Dictionary<string, string> DataMD = new Dictionary<string, string>();
            Dictionary<string, string> DataDBD = new Dictionary<string, string>();
            Dictionary<string, string> DataDIW = new Dictionary<string, string>();
            Dictionary<string, string> DataDOPA = new Dictionary<string, string>();
            List<BusinessSecuredViewModel> businessSecviewlist = new List<BusinessSecuredViewModel>();
            //if (User.Identity.IsAuthenticated)
            {
                if (searchView.typeSearch.ToString().Equals("owner")) //ค้นหาตามผู้ให้หลักประกัน 
                {
                    switch (searchView.ownerType)
                    {
                        case "P":    //บุคคลธรรมดา
                            md_ws_url = ConfigurationManager.AppSettings["MD_WS_URL_BY_CITIZEN_JURISTIC"];
                            DataMD.Add("CitizenID", searchView.ownerIdCard);
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_OWNER"];
                            DataDBD.Add("OwnerType", searchView.ownerType);
                            DataDBD.Add("CitizenID", searchView.ownerIdCard);
                            DataDBD.Add("OwnerName", searchView.ownerName);
                            DataDBD.Add("OwnerSurname", searchView.ownerSurname);
                            //CitizenID
                            break;
                        case "C":    //นิติบุคคล
                            md_ws_url = ConfigurationManager.AppSettings["MD_WS_URL_BY_CITIZEN_JURISTIC"];
                            DataMD.Add("CitizenID", searchView.ownerJuristicId);
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_OWNER"];
                            DataDBD.Add("OwnerType", searchView.ownerType);
                            DataDBD.Add("JuristicID", searchView.ownerJuristicId);
                            DataDBD.Add("JuristicName", searchView.ownerJuristicName);
                            break;
                        default:
                            // Do Something
                            break;
                    }
                }
                else if (searchView.typeSearch.ToString().Equals("asset")) //ค้นหาตามทรัพย์สิน
                {
                    switch (searchView.assetType)
                    {
                        case "machine":
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_MATCHINE"];
                            if (!string.IsNullOrEmpty(searchView.machineNo))
                                DataDBD.Add("MatchineNo", StringReplace(searchView.machineNo)); //หมายเลขทะเบียนเครื่องจักร
                            diw_ws_url = ConfigurationManager.AppSettings["DIW_WS_URL_BY_MACHINEMORTGAGE"];
                            if (!string.IsNullOrEmpty(searchView.machineNo))
                            {                              
                                string mNo = StringReplace(searchView.machineNo);  
                                // Substring เลข machineno เพราะตอน call service ต้องส่งแยกตาม filed ที่ required
                                string year = SafeSubstring(mNo, 0, 2);
                                string province = SafeSubstring(mNo, 2, 3);
                                string classMachine = SafeSubstring(mNo, 5, 3);
                                string start = SafeSubstring(mNo, 8, 4);
                                string end = SafeSubstring(mNo, 12, mNo.Length - 12);

                                DataDIW.Add("YearMachine", year);
                                DataDIW.Add("ProvinceCode", province);
                                DataDIW.Add("ClassMachine", classMachine);
                                DataDIW.Add("StartMachineNo", start);
                                DataDIW.Add("EndMachineNo", end);
                            }
                            searchNo = searchView.machineNo;
                            searchName = "หมายเลขทะเบียนเครื่องจักร";
                            assetName = "เครื่องจักร";
                            break;
                        case "car":
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_CAR"];
                            if (!string.IsNullOrEmpty(searchView.carPlateNo))
                            {

                                DataDBD.Add("PlateNo", (searchView.carPlateNo).Trim());
                                //DataDBD.Add("PlateNo",StringReplace(searchView.carPlateNo));
                                DataDBD.Add("ProvinceID", StringReplace(searchView.provinceID));
                                searchNo = searchView.carPlateNo;
                                searchName = "หมายเลขทะเบียนรถยนต์";
                            }
                            if (!string.IsNullOrEmpty(searchView.engineNo))
                            {
                                /*if (searchView.engineNo.Trim().Equals("-"))
                                    DataDBD.Add("EngineNo", (searchView.engineNo));
                                else
                                    DataDBD.Add("EngineNo", StringReplace(searchView.engineNo));
                                */
                                DataDBD.Add("EngineNo", (searchView.engineNo).Trim());
                                //DataDBD.Add("EngineNo", StringReplace(searchView.engineNo));
                                // searchNo = searchView.engineNo;
                                //searchName = "เลขเครื่องยนต์";
                            }
                            if (!string.IsNullOrEmpty(searchView.frameNo))
                            {

                                /*if (searchView.frameNo.Trim().Equals("-"))
                                    DataDBD.Add("FrameNo", (searchView.frameNo));
                                else
                                    DataDBD.Add("FrameNo", StringReplace(searchView.frameNo));
                                    */
                                DataDBD.Add("FrameNo", (searchView.frameNo).Trim());
                                //DataDBD.Add("FrameNo", StringReplace(searchView.frameNo));
                                //searchNo = searchView.frameNo;
                                //searchName = "เลขตัวถังรถ";
                            }
                            assetName = "รถยนต์";
                            break;
                        case "ship":
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_SHIPCODE"];
                            if (!string.IsNullOrEmpty(searchView.shipPlateNo))
                                DataDBD.Add("PlateNo", StringReplace(searchView.shipPlateNo));
                            md_ws_url = ConfigurationManager.AppSettings["MD_WS_URL_BY_SHIPCODE"];
                            if (!string.IsNullOrEmpty(searchView.shipPlateNo))
                                DataMD.Add("ShipCode", StringReplace(searchView.shipPlateNo));
                            searchNo = searchView.shipPlateNo;
                            searchName = "เลขที่จดทะเบียนเรือ";
                            //ShipCode
                            assetName = "เรือ";
                            break;
                        case "animal":
                            dopa_ws_url = ConfigurationManager.AppSettings["DOPA_WS_URL_BY_ANIMAL"];
                            if (!string.IsNullOrEmpty(searchView.identityTicket))
                                DataDOPA.Add("AnimalID", searchView.identityTicket);
                            dbd_ws_url = ConfigurationManager.AppSettings["DBD_WS_URL_BY_BEASTOFBURDEN"];
                            DataDBD.Add("Identityticket", searchView.identityTicket);
                            if (searchView.beastOfBurdenTypeId != "0")
                                DataDBD.Add("beastOfBurdenTypeId", searchView.beastOfBurdenTypeId);
                            searchNo = searchView.identityTicket;
                            searchName = "เลขทะเบียนสัตว์พาหนะ";
                            assetName = "สัตว์พาหนะ";
                            break;
                        default:
                            // Do Something
                            break;
                    }
                }

                //ApiDBD.Call(dbd_ws_url, EGA.WS.HttpVerb.POST,)
                //DBD กรมพัฒนาธุรกิจการค้า
                if (!string.IsNullOrEmpty(dbd_ws_url))
                {
                    try
                    {
                        var dbd_response = ApiDBD.Call(dbd_ws_url, EGA.WS.HttpVerb.GET, DataDBD, null, EGA.WS.ContentType.ApplicationJson);
                        if (dbd_response != null && dbd_response.HasValues)
                        {
                            if (dbd_response != null && dbd_response.HasValues)
                            {
                                VALUE = dbd_response["SecureTransactionInfoItems"].ToString();
                                JArray b = JArray.Parse(VALUE);
                                for (int i = 0; i < b.Count; i++)
                                {

                                    List<CreditorsBusinessSecured> CreditorsBusinessSecuredlist = new List<CreditorsBusinessSecured>();
                                    List<OwnersBusinessSecured> OwnersBusinessSecuredlist = new List<OwnersBusinessSecured>();

                                    JArray arrAssetType = JArray.Parse(b[i]["Assets"].ToString());
                                    JArray arrCreditorsList = JArray.Parse(b[i]["Creditors"].ToString());
                                    JArray arrOwnerList = JArray.Parse(b[i]["Owners"].ToString());

                                    string assetType = string.Empty;
                                    for (int j = 0; j < arrAssetType.Count; j++)
                                    {
                                        if ( String.IsNullOrEmpty(assetName))
                                        assetType = arrAssetType[j].ToString();
                                        else
                                        assetType = assetName;

                                    }
                                    
                                    for (int j = 0; j < arrCreditorsList.Count; j++)
                                    {
                                        CreditorsBusinessSecuredlist.Add(new CreditorsBusinessSecured
                                        {
                                            Name = arrCreditorsList[j].ToString()
                                        });
                                    }
                                    for (int j = 0; j < arrOwnerList.Count; j++)
                                    {
                                        OwnersBusinessSecuredlist.Add(new OwnersBusinessSecured
                                        {
                                            Name = arrOwnerList[j].ToString() == String.Empty ? "ไม่ได้ระบุ" : arrOwnerList[j].ToString()
                                        });
                                    }

                                    string dbdunixtime = b[i]["RegisterDatetime"].ToString();
                                    double timestamp = Convert.ToDouble(dbdunixtime.Substring(0, 10));
                                    // Format our new DateTime object to start at the UNIX Epoch
                                    System.DateTime dateTime = new System.DateTime(1970, 1, 1, 7, 0, 0, 0, DateTimeKind.Utc);
                                    // Add the timestamp (number of seconds since the Epoch) to be converted
                                    dateTime = dateTime.AddSeconds(timestamp);
                                    //Console.WriteLine(dateTime);
                                    businessSecviewlist.Add(new BusinessSecuredViewModel
                                    {
                                        AssetsType = assetType,
                                        CreditorsList = CreditorsBusinessSecuredlist,
                                        Organization = "กรมพัฒนาธุรกิจการค้า",
                                        PromiseNO = b[i]["ESecureNo"].ToString(),
                                        RegisterDate = dateTime.ToString(),
                                        OwnerList = OwnersBusinessSecuredlist,
                                        AssetsStatus = b[i]["RegisterStatus"].ToString(),
                                        RegistrationName = "เลขทะเบียนสัญญา",
                                        SearchNo = searchNo,
                                        SearchName = searchName

                                    });
                                }

                            }
                        }
                        else
                        {
                            RESULTDBD = "FALSE";
                        }
                    }
                    catch (Exception)
                    {
                        RESULTDBD = "FALSE";
                    }
                    
                }
                //MD กรมเจ้าท่า
                if (!string.IsNullOrEmpty(md_ws_url))
                {
                    try
                    {
                        var md_response = Api.Call(md_ws_url, EGA.WS.HttpVerb.GET, DataMD, null, EGA.WS.ContentType.ApplicationJson);
                        if (md_response != null && md_response.HasValues)
                        {

                            //RESULTMD = md_response["result"].ToString();
                            if (md_response["result"].ToString() == "True")
                            {
                                if (md_response["data"].ToString() != "[]")
                                {
                                    VALUE = md_response["data"].ToString();
                                    RESULTMD = "TRUE";
                                    var mdResponse = JsonConvert.DeserializeObject<List<MDDetail>>(md_response["data"].ToString());
                                    if (mdResponse.Count > 0)
                                    {
                                        for (int i = 0; i < mdResponse.Count; i++)
                                        {
                                            //Check if had this PromiseNO in DBD then don't display data from MD
                                            MDDetail data = mdResponse[i];
                                            if (data.PROMISE.Count > 0)
                                            {
                                                for (int j = 0; j < data.PROMISE.Count; j++)
                                                {
                                                    var promiseNO = data.PROMISE[j].PROMISE_NO;
                                                    if (!businessSecviewlist.Any(p => p.PromiseNO == promiseNO))
                                                    {
                                                        List<CreditorsBusinessSecured> CreditorsBusinessSecuredlist = new List<CreditorsBusinessSecured>();
                                                        CreditorsBusinessSecuredlist.Add(new CreditorsBusinessSecured
                                                        {
                                                            Name = "-"
                                                        });
                                                        List<OwnersBusinessSecured> OwnersBusinessSecuredlist = new List<OwnersBusinessSecured>();
                                                        if (data.SHIPOWNER.Count > 0)
                                                        {
                                                            for (int n = 0; n < data.SHIPOWNER.Count; n++)
                                                            {
                                                                OwnersBusinessSecuredlist.Add(new OwnersBusinessSecured
                                                                {
                                                                    Name = data.SHIPOWNER[n].NAME
                                                                });

                                                            }
                                                        }
                                                        // Add Data to List
                                                        businessSecviewlist.Add(new BusinessSecuredViewModel
                                                        {
                                                            AssetsType = data.SHIPTYPE,
                                                            CreditorsList = CreditorsBusinessSecuredlist,
                                                            Organization = "กรมเจ้าท่า",
                                                            PromiseNO = promiseNO.ToString(),
                                                            RegisterDate = "-",
                                                            OwnerList = OwnersBusinessSecuredlist,
                                                            //AssetsStatus = b[i]["SHIPSTATUS"].ToString(),
                                                            AssetsStatus = "( กรุณาติดต่อหน่วยงานแหล่งข้อมูล )",
                                                            RegistrationName = "เลขทะเบียนสัญญา",
                                                            SearchNo = searchNo,
                                                            SearchName = searchName
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    RESULTMD = "FALSE";
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                        RESULTMD = "FALSE";
                    }                  
                }
                //DIW กรมโรงงาน
                if (!string.IsNullOrEmpty(diw_ws_url))
                {
                    try
                    {
                        var diw_response = Api.Call(diw_ws_url, EGA.WS.HttpVerb.GET, DataDIW, null, EGA.WS.ContentType.ApplicationJson);
                        if (diw_response != null && diw_response.HasValues)
                        {
                            if (diw_response["ReturnCode"].ToString() == "000")
                            {
                                VALUE = diw_response.ToString();

                                RESULTDIW = "TRUE";

                                //List<DIWhBusiness> ListDIWhBusiness_ = new List<DIWhBusiness>();
                                DIWhBusiness DIWhBusiness_ = new DIWhBusiness();
                                DIWhBusiness_ = JsonConvert.DeserializeObject<DIWhBusiness>(VALUE);
                                //ListDIWhBusiness_.Add(DIWhBusiness_);


                                /*******version เร่งด่วนเดี๋ยวกลับมาปรับ แก้ กรณี 1 ทะเบียนเครื่องจักร ขึ้นหลายรายการ เลือกรายการล่าสุด และ มีภารพผูกพัน ********/
                                List<BusinessSecuredViewModel> diwbusinessSecviewlist = new List<BusinessSecuredViewModel>();
                                foreach (var root in DIWhBusiness_.ListMachine)
                                {
                                    if (root.Obligation.ToString() != "N")
                                    {
                                        List<CreditorsBusinessSecured> CreditorsBusinessSecuredlist = new List<CreditorsBusinessSecured>();
                                        List<OwnersBusinessSecured> OwnersBusinessSecuredlist = new List<OwnersBusinessSecured>();

                                        OwnersBusinessSecuredlist.Add(new OwnersBusinessSecured
                                        {
                                            Name = DIWhBusiness_.OwnerShip
                                        });
                                        CreditorsBusinessSecuredlist.Add(new CreditorsBusinessSecured
                                        {
                                            Name = root.Mortgagee
                                        });


                                        diwbusinessSecviewlist.Add(new BusinessSecuredViewModel
                                        {
                                            AssetsType = "เครื่องจักร",
                                            CreditorsList = CreditorsBusinessSecuredlist,
                                            Organization = "กรมโรงงานอุตสาหกรรม",
                                            PromiseNO = root.BookNo,
                                            RegisterDate = root.MortgageDate.DefaultString(),
                                            OwnerList = OwnersBusinessSecuredlist,
                                            AssetsStatus = root.Obligation.ToString() == "N" ? "ไม่มีภาระผูกพัน" : "มีภาระผูกพัน",
                                            RegistrationName = "เล่ม ร.2/1",
                                            SearchNo = searchNo,
                                            SearchName = "หมายเลขทะเบียนเครื่องจักร"
                                        });
                                    }
                                }//if 

                                List<BusinessSecuredViewModel> tmp = diwbusinessSecviewlist.OrderByDescending(x => x.RegisterDate).Take(1).ToList();
                                foreach (var obj in tmp)
                                {
                                    businessSecviewlist.Add(new BusinessSecuredViewModel
                                    {
                                        AssetsType = "เครื่องจักร",
                                        CreditorsList = obj.CreditorsList,
                                        Organization = "กรมโรงงานอุตสาหกรรม",
                                        PromiseNO = obj.PromiseNO,
                                        RegisterDate = obj.RegisterDate,
                                        OwnerList = obj.OwnerList,
                                        AssetsStatus = obj.AssetsStatus,
                                        RegistrationName = "เล่ม ร.2/1",
                                        SearchNo = searchNo,
                                        SearchName = "หมายเลขทะเบียนเครื่องจักร"
                                    });
                                }
                                /*******************************************************************************************/
                                ////////foreach (var root in DIWhBusiness_.ListMachine)
                                ////////{

                                ////////    List<CreditorsBusinessSecured> CreditorsBusinessSecuredlist = new List<CreditorsBusinessSecured>();
                                ////////    List<OwnersBusinessSecured> OwnersBusinessSecuredlist = new List<OwnersBusinessSecured>();

                                ////////    OwnersBusinessSecuredlist.Add(new OwnersBusinessSecured
                                ////////    {
                                ////////        Name = DIWhBusiness_.OwnerShip
                                ////////    });
                                ////////    CreditorsBusinessSecuredlist.Add(new CreditorsBusinessSecured
                                ////////    {
                                ////////        Name = root.Mortgagee
                                ////////    });
                                ////////    businessSecviewlist.Add(new BusinessSecuredViewModel
                                ////////    {
                                ////////        AssetsType = "เครื่องจักร",
                                ////////        CreditorsList = CreditorsBusinessSecuredlist,
                                ////////        Organization = "กรมโรงงานอุตสาหกรรม",
                                ////////        PromiseNO = root.BookNo + " | " + root.MachineNo,
                                ////////        RegisterDate = root.MortgageDate.DefaultString(),
                                ////////        OwnerList = OwnersBusinessSecuredlist,
                                ////////        AssetsStatus = root.Obligation.ToString() != "Y" ? "ไม่มีภาระผูกพัน" : "มีภาระผูกพัน",
                                ////////        RegistrationName = "เลขทะเบียนเครื่องจักร"

                                ////////    });
                                ////////}
                            }
                            else
                            {
                                RESULTDIW = "FALSE";
                            }
                        }
                    } catch (Exception)
                    {
                        RESULTDIW = "FALSE";
                    }
                   
                }
                //DOPA กรมการปกครอง
                if (!string.IsNullOrEmpty(dopa_ws_url))
                {
                    try
                    {
                        var dopa_response = Api.Call(dopa_ws_url, EGA.WS.HttpVerb.GET, DataDOPA, null, EGA.WS.ContentType.ApplicationJson);
                        if (dopa_response != null && dopa_response.HasValues)
                        {
                            RESULTDOPA = "TRUE";
                            List<CreditorsBusinessSecured> CreditorsBusinessSecuredlist = new List<CreditorsBusinessSecured>();
                            CreditorsBusinessSecuredlist.Add(new CreditorsBusinessSecured
                            {
                                Name = "-"
                            });
                            List<OwnersBusinessSecured> OwnersBusinessSecuredlist = new List<OwnersBusinessSecured>();
                            OwnersBusinessSecuredlist.Add(new OwnersBusinessSecured
                            {
                                Name = "-"
                            });

                            DOPADetail dopaResponse = new DOPADetail();
                            dopaResponse = JsonConvert.DeserializeObject<DOPADetail>(dopa_response.ToString());
                            if (!string.IsNullOrEmpty(dopaResponse.RegisterNo))
                            {
                                businessSecviewlist.Add(new BusinessSecuredViewModel
                                {
                                    AssetsType = dopaResponse.AnimalType,
                                    CreditorsList = CreditorsBusinessSecuredlist,
                                    Organization = "กรมการปกครอง",
                                    PromiseNO = dopaResponse.RegisterNo,
                                    RegisterDate = DBDUtility.GetDateFormat(dopaResponse.DocDate),
                                    OwnerList = OwnersBusinessSecuredlist,
                                    AssetsStatus = dopaResponse.StatusCancel,
                                    RegistrationName = "เลขทะเบียนสัญญา",
                                    SearchNo = searchNo,
                                    SearchName = searchName
                                });
                            }
                            else
                            {
                                RESULTDOPA = "FALSE";
                            }                          
                        }
                        else
                        {
                            RESULTDOPA = "FALSE";
                        }

                    }
                    catch (Exception)
                    {
                        RESULTDOPA = "FALSE";
                    }
                    
                }
            }
            //return Json(businessSecviewlist, JsonRequestBehavior.AllowGet);
            if (RESULTMD == "FALSE" && RESULTDBD == "FALSE" && RESULTDIW == "FALSE" && RESULTDOPA == "FALSE")
            {
                RESULT = "FALSE";
            }
            else
            {
                RESULT = "TRUE";
            }
            return Json(new { RESULT, businessSecviewlist }, JsonRequestBehavior.AllowGet);
        }
        // StringReplace ใช้เพื่อ Replace dash and whitespace from input
        private string StringReplace(string str)
        {          
            string value = Regex.Replace(str, "[ .-]+", "");

            return value;
        }
        
        // SafeSubstring เพื่อแก้ปัญหา substring out of range
        public string SafeSubstring(string value, int startIndex, int length)
        {
            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }
    }
}