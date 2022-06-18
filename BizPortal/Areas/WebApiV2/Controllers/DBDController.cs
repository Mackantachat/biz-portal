using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using BizPortal.ViewModels.SingleForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps.DBDStandard;
using BizPortal.Integrated;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class DBDController : ApiControllerBase
    {
        [Route("Api/v2/DBD/BusinessTypes")]
        [HttpGet]
        public object BusinessTypes()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "01", Text = "การศึกษา" },
                new Select2Opt() { ID = "02", Text = "การเงิน กฎหมาย และบัญชี" },
                new Select2Opt() { ID = "03", Text = "การแพทย์ และสุขภาพ" },
                new Select2Opt() { ID = "04", Text = "เกมส์/ของเล่น/ของขวัญ/เบ็ดเตล็ด" },
                new Select2Opt() { ID = "05", Text = "กีฬา และสันทนาการ" },
                new Select2Opt() { ID = "06", Text = "ข่าว-สื่อ" },
                new Select2Opt() { ID = "07", Text = "คอมพิวเตอร์และอินเทอร์เน็ต" },
                new Select2Opt() { ID = "08", Text = "เครื่องมือสื่อสาร/กล้อง" },
                new Select2Opt() { ID = "09", Text = "เครื่องมือเครื่องใช้อุตสาหกรรม" },
                new Select2Opt() { ID = "10", Text = "ค้าปลีก" },
                new Select2Opt() { ID = "11", Text = "ท่องเที่ยว" },
                new Select2Opt() { ID = "12", Text = "บุคคล/สังคม/มานุษยวิทยา" },
                new Select2Opt() { ID = "13", Text = "บันเทิง และนันทนาการ" },
                new Select2Opt() { ID = "14", Text = "ศิลปะและวัฒนธรรม" },
                new Select2Opt() { ID = "15", Text = "ยานยนต์" },
                new Select2Opt() { ID = "16", Text = "แฟชั่น/เครื่องแต่งกาย/เครื่องประดับ" },
                new Select2Opt() { ID = "17", Text = "อาหาร และเครื่องดื่ม" },
                new Select2Opt() { ID = "18", Text = "อสังหาริมทรัพย์/ออกแบบ/ตกแต่ง/บ้านและสวน" },
                new Select2Opt() { ID = "99", Text = "อื่นๆ" },
            };

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/DBD/BusinessCloseReasons")]
        [HttpGet]
        public object BusinessCloseReasons()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "01", Text = "ขาดทุน" },
                new Select2Opt() { ID = "02", Text = "ต้นทุนและค่าใช้จ่ายเพิ่มสูงขึ้น" },
                new Select2Opt() { ID = "03", Text = "เทคโนโลยีการผลิตการบริหารเปลี่ยนแปลงไปอย่างมาก" },
                new Select2Opt() { ID = "04", Text = "ตลาดมีการแข่งขันสูงมาก ไม่สามารถรักษาส่วนแบ่งในตลาดได้" },
                new Select2Opt() { ID = "05", Text = "รสนิยมหรือความต้องการของผู้บริโภคเปลี่ยนแปลงไป" },
                new Select2Opt() { ID = "06", Text = "สภาวะเศรษฐกิจตกต่ำ ผลตอบแทนน้อย" },
                new Select2Opt() { ID = "07", Text = "ภัยธรรมชาติ" },
                new Select2Opt() { ID = "08", Text = "โอนกิจการให้ผู้อื่น" },
                new Select2Opt() { ID = "99", Text = "อื่นๆ" },
            };

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/DBD/NameTitles")]
        [HttpGet]
        public object NameTitles()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "001", Text = "นาย" },
                new Select2Opt() { ID = "002", Text = "นางสาว" },
                new Select2Opt() { ID = "003", Text = "นาง" },
                             
            };
            return new { results = options.ToArray() };
        }

        [Route("Api/v2/DBD/TranferNameTitles")]
        [HttpGet]
        public object TranferNameTitles()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "001", Text = "นาย" },
                new Select2Opt() { ID = "002", Text = "นางสาว" },
                new Select2Opt() { ID = "003", Text = "นาง" },
                new Select2Opt() { ID = "800", Text = "ไม่ระบุ" },
                new Select2Opt() { ID = "801", Text = "บริษัท" },
                new Select2Opt() { ID = "802", Text = "ห้างหุ้นส่วนสามัญนิติบุคคล" },
                new Select2Opt() { ID = "803", Text = "ห้างหุ้นส่วนจำกัด" },
                new Select2Opt() { ID = "804", Text = "ธนาคาร" },

            };
            return new { results = options.ToArray() };
        }
        [Route("Api/v2/DBD/NameTitlesEN")]
        [HttpGet]
        public object NameTitlesEN()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "001", Text = "Mr." },
                new Select2Opt() { ID = "002", Text = "Mrs." },
                new Select2Opt() { ID = "003", Text = "Miss" },
            };

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/DBD/ServiceTypes")]
        [HttpGet]
        public object ServiceTypes(string Code = "", string Description = "")
        {
            List<Select2Opt> options = new List<Select2Opt>();

            var query = DB.DBDCommerceObjectives.AsQueryable();

            query = !string.IsNullOrEmpty(Code) ? query.Where(o => o.Code.Contains(Code)) : query;
            query = !string.IsNullOrEmpty(Description) ? query.Where(o => o.DescriptionTh.Contains(Description)) : query;

            var res = query.Select(o => new BusinessTypeSelect2Opt() { ID = o.Code, Text = o.DescriptionTh, Flag = o.CommerceFlag })
                .OrderBy(o => o.ID)
                .ToList();

            options = res.Cast<Select2Opt>().ToList();

            return new { results = options.ToArray() };

        }

        [Route("Api/v2/DBD/Office")]
        [HttpGet]
        public object Office(string Code = "", string OfficeName = "", string AmphurCode = "", string ProvinceCode = "", string Pagesize = "", string PageNumber = "")
        {
            int Pagesizes = 20000;
            int PageNumbers = 1;

            if (!string.IsNullOrEmpty(Pagesize))
            {
                int.TryParse(Pagesize, out Pagesizes);
            }

            if (!string.IsNullOrEmpty(PageNumber))
            {
                int.TryParse(PageNumber, out PageNumbers);
            }

            List<Select2Opt> options = new List<Select2Opt>();
            var query = DB.DBDCommerceOffices.AsQueryable();
            
            query = !string.IsNullOrEmpty(Code) ? query.Where(o => o.Code.Equals(Code)) : query;
            query = !string.IsNullOrEmpty(OfficeName) ? query.Where(o => o.OfficeNameTh.Contains(OfficeName)) : query;
            //query = !string.IsNullOrEmpty(AmphurCode) ? query.Where(o => o.AmphurCode.Equals(AmphurCode)) : query;
            query = !string.IsNullOrEmpty(ProvinceCode) ? query.Where(o => o.ProvinceCode.Equals(ProvinceCode)) : query;
            if (ProvinceCode != "10")
            {
                query = !string.IsNullOrEmpty(AmphurCode) ? query.Where(o => o.AmphurCode.Equals(AmphurCode)) : query;
                //query = !string.IsNullOrEmpty(ProvinceCode) ? query.Where(o => o.ProvinceCode.Equals(ProvinceCode)) : query;

            }
            else
            {
                query = !string.IsNullOrEmpty(AmphurCode) ? query.Where(o => o.AmphurCode.Equals(AmphurCode)||o.Code.Equals("000")) : query;
            }

            var res = query.Select(o => new BusinessTypeSelect2Opt() { ID = String.Concat(o.ProvinceCode.ToString(), o.Code.ToString()), Text = o.OfficeNameTh, Flag = o.ActiveFlag })
                .OrderBy(o => o.ID).Skip((PageNumbers - 1) * Pagesizes).Take(Pagesizes)
                .ToList();

            options = res.Cast<Select2Opt>().ToList();

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/DBD/CheckCommerceById")]
        [HttpPost]
        public object CheckCommerceById(CheckCommerceModel model)
        {
            var status = false;
            object data = new { };           
            try
            {
                var url = ConfigurationManager.AppSettings["DBD_COMMERCE_CHECK_WS_URL"];
                var args = new Dictionary<string, string> { { "commerceNo", model.CommerceNo }, { "registerNo", model.RegisterNo } };
                var result = Api.Get(url, args, EGA.WS.ContentType.ApplicationJson);                              

                Api.OnCheckingApplicationError += (ex) =>
                {
                    throw ex;
                };
                
                if (result.HasValues)
                {
                    var JDBDResult = JsonConvert.DeserializeObject<CommerceRegistInfo>(result.ToString());

                    // เฉพาะใบที่กำลังดำเนินการเท่านั้นถึงจะนำยกเลิกหรือเปลี่ยนแปลง แก้ไขได้ (commerceStatus 1)
                    if (JDBDResult.commerceStatus == (int)CommerceStatus.Process)
                    {
                        status = true;
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return new
            {
                Success = status,
                Data = data
            };
        }

        [Route("Api/v2/DBD/CheckTransferCommerceById")]
        [HttpPost]
        public object CheckTransferCommerceById(CheckCommerceModel model)
        {
            var status = false;
            var data = new object { };

            try
            {
                var url = ConfigurationManager.AppSettings["DBD_COMMERCE_CHECK_WS_URL"];
                var args = new Dictionary<string, string> { { "commerceNo", model.CommerceNo }, { "registerNo", model.RegisterNo } };
                var result = Api.Get(url, args, EGA.WS.ContentType.ApplicationJson);

                Api.OnCheckingApplicationError += (ex) =>
                {
                    throw ex;
                };

                if (result != null)
                {
                    var JDBDResult = JsonConvert.DeserializeObject<CommerceRegistInfo>(result.ToString());

                    // เฉพาะใบที่ถูกยกเลิกแล้วเท่านั้นถึงจะนำมาโอนได้ (commerceStatus 4)
                    if (JDBDResult.commerceStatus == (int)CommerceStatus.Dissolve)
                    {
                        SingleFormSectionDataViewModel sectionData = new SingleFormSectionDataViewModel();
                        sectionData.SectionName = model.SectionGroup + "_TRANSFER_SECTION";
                        sectionData.Type = "Form";
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_COMMERCIAL_NUMBER", model.CommerceNo);
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_REQUEST_NUMBER", model.RegisterNo);
                        sectionData.FormData.Add("AJAX_DROPDOWN_" + model.SectionGroup + "_TRANSFER_SECTION_TITLE", JDBDResult.owner.titleCode);
                        sectionData.FormData.Add("AJAX_DROPDOWN_" + model.SectionGroup + "_TRANSFER_SECTION_TITLE_TEXT", GetJuristicTitleTEXT(JDBDResult.owner.titleCode));
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_FIRSTNAME", JDBDResult.owner.firstNameTH);
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_LASTNAME", ((JDBDResult.owner.lastNameTH == null) ? "-" : JDBDResult.owner.lastNameTH));
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_ID_CARD", JDBDResult.owner.identityID);
                        sectionData.FormData.Add("DROPDOWN_" + model.SectionGroup + "_TRANSFER_SECTION_NATIONALITY", JDBDResult.owner.nationCode);
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_COMMERCIAL_NAME", JDBDResult.commerceNameTH);
                        sectionData.FormData.Add(model.SectionGroup + "_TRANSFER_SECTION_DATE", "-");
                        sectionData.FormData.Add("ADDRESS_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.houseNo);
                        sectionData.FormData.Add("ADDRESS_MOO_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.moo);
                        sectionData.FormData.Add("ADDRESS_VILLAGE_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.village);
                        sectionData.FormData.Add("ADDRESS_SOI_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.soi);
                        sectionData.FormData.Add("ADDRESS_BUILDING_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.building);
                        sectionData.FormData.Add("ADDRESS_ROOMNO_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.roomNo);
                        sectionData.FormData.Add("ADDRESS_FLOOR_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.buildingFloor);
                        sectionData.FormData.Add("ADDRESS_ROAD_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.road);

                        sectionData.FormData.Add("ADDRESS_PROVINCE_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(JDBDResult.owner.provinceCode));
                        sectionData.FormData.Add("ADDRESS_PROVINCE_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.provinceCode);
                        sectionData.FormData.Add("ADDRESS_AMPHUR_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(JDBDResult.owner.provinceCode, JDBDResult.owner.amphurCode));
                        sectionData.FormData.Add("ADDRESS_AMPHUR_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.amphurCode);
                        sectionData.FormData.Add("ADDRESS_TUMBOL_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(JDBDResult.owner.provinceCode, JDBDResult.owner.amphurCode, JDBDResult.owner.tumbonCode));
                        sectionData.FormData.Add("ADDRESS_TUMBOL_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.tumbonCode);
                        sectionData.FormData.Add("ADDRESS_POSTCODE_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.postCode);
                        sectionData.FormData.Add("ADDRESS_TELEPHONE_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.telephone);
                        sectionData.FormData.Add("ADDRESS_TELEPHONE_EXT_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.telephone_ext);
                        sectionData.FormData.Add("ADDRESS_FAX_" + model.SectionGroup + "_TRANSFER_SECTION_ADDRESS", JDBDResult.owner.fax);


                        #region Head Office
                        SingleFormSectionDataViewModel storeData = new SingleFormSectionDataViewModel();
                        storeData.SectionName = model.SectionGroup + "_INFORMATION_STORE_SECTION";
                        storeData.Type = "Form";
                        if (JDBDResult.headOffice != null)
                        {
                            storeData.FormData.Add("INFORMATION_STORE_NAME_TH", JDBDResult.commerceNameTH);
                            storeData.FormData.Add("INFORMATION_STORE_NAME_EN", JDBDResult.commerceNameEN);
                            storeData.FormData.Add("ADDRESS_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.houseNo);
                            storeData.FormData.Add("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.moo);
                            storeData.FormData.Add("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.soiTH);
                            storeData.FormData.Add("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.buildingTH);
                            storeData.FormData.Add("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.roomNo);
                            storeData.FormData.Add("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.buildingFloor);
                            storeData.FormData.Add("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.roadTH);
                            storeData.FormData.Add("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.tumbonCode);
                            storeData.FormData.Add("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetTambolText(JDBDResult.headOffice.provinceCode, JDBDResult.headOffice.amphurCode, JDBDResult.headOffice.tumbonCode));
                            storeData.FormData.Add("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.amphurCode);
                            storeData.FormData.Add("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetAmphurText(JDBDResult.headOffice.provinceCode, JDBDResult.headOffice.amphurCode));
                            storeData.FormData.Add("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.provinceCode);
                            storeData.FormData.Add("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetProvinceText(JDBDResult.headOffice.provinceCode));
                            storeData.FormData.Add("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.postCode);
                            storeData.FormData.Add("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.telephone);
                            storeData.FormData.Add("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.telephone_ext);
                            storeData.FormData.Add("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", JDBDResult.headOffice.fax);
                        }
                        #endregion

                        // TODO: ต้องดึงข้อมูลชนิดแห่งพาณิชยกิจ, ข้อมูลที่ตั้งสำนักงานสาขา, ข้อมูลที่ตั้งโรงเก็บสินค้า, ข้อมูลตัวแทนค้าต่าง จาก record การโอนที่ดึง api มา ส่งกลับไปด้วย



                        status = true;

                        data = new
                        {
                            SectionData = new object[]
                            {
                              sectionData,
                              storeData
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return new
            {
                Success = status,
                Data = data
            };
        }

        #region DBD models
        public class CheckCommerceModel
        {
            public string CommerceNo { get; set; }
            public string RegisterNo { get; set; }
            public string SectionGroup { get; set; }
        }

        public class GetCommerceListModel
        {
            public string IdentityID { get; set; }
            public string RegisterNo { get; set; }
            public string SectionGroup { get; set; }
        }

        public class BusinessTypeSelect2Opt : Select2Opt
        {

            [JsonProperty(Order = 3)]
            public string Flag { get; set; }
        }

        public enum CommerceStatus
        {
            [StringValue("ยังดำเนินกิจการอยู่")]
            Process = 1,
            [StringValue("เลิก")]
            Dissolve = 4,
            [StringValue("เพิกถอน")]
            Revoke = 7
        }

        #endregion
        public static string GetJuristicTitleTEXT(string juristic_code)
        {

            if (!String.IsNullOrEmpty(juristic_code))
            {

                try
                {
                    string titleCode = string.Empty;
                    if (juristic_code.Contains("001"))
                    {
                        titleCode = "นาย";
                    }
                    else if (juristic_code.Contains("002"))
                    {
                        titleCode = "นางสาว";
                    }
                    else if (juristic_code.Contains("003"))
                    {
                        titleCode = "นาง";
                    }
                    else if (juristic_code.Contains("801"))
                    {
                        titleCode = "บริษัท";
                    }
                    else if (juristic_code.Contains("802"))
                    {
                        titleCode = "ห้างหุ้นส่วนสามัญนิติบุคคล";
                    }
                    else if (juristic_code.Contains("803"))
                    {
                        titleCode = "ห้างหุ้นส่วนจำกัด";
                    }
                    else if (juristic_code.Contains("804"))
                    {
                        titleCode = "ธนาคาร";
                    }
                    else
                    {
                        titleCode = "ไม่ระบุ"; //ไม่ระบุ
                    }
                    return titleCode;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}