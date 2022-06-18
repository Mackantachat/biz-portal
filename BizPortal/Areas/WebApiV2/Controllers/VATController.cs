using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class VATController : ApiControllerBase
    {
        [Route("Api/v2/VAT/BranchTitle")]
        [HttpGet]
        public object BranchTitle()
        {
            List<Select2Opt> options = new List<Select2Opt>()
            {
                new Select2Opt() { ID = "5211", Text = "บริษัท" },
                new Select2Opt() { ID = "5212", Text = "บริษัทเงินทุน" },
                new Select2Opt() { ID = "5213", Text = "บริษัทหลักทรัพย์" },
                new Select2Opt() { ID = "5214", Text = "บริษัทเงินทุนหลักทรัพย์" },
                new Select2Opt() { ID = "5216", Text = "บริษัทจัดหางาน" },
                new Select2Opt() { ID = "5217", Text = "บรรษัท" },
                new Select2Opt() { ID = "5218", Text = "บริษัทขนส่งระหว่างประเทศ" },
                new Select2Opt() { ID = "5222", Text = "ห้างหุ้นส่วนจำกัด" },
                new Select2Opt() { ID = "5224", Text = "ห้างหุ้นส่วนสามัญนิติบุคคล" },
                new Select2Opt() { ID = "5231", Text = "กิจการร่วมค้า" },
                new Select2Opt() { ID = "5232", Text = "มูลนิธิ" },
                new Select2Opt() { ID = "5233", Text = "สมาคม" },
                new Select2Opt() { ID = "5234", Text = "สำนักงานผู้แทน" },
                new Select2Opt() { ID = "5236", Text = "สำนักงานผู้แทนภูมิภาค" },
                new Select2Opt() { ID = "5237", Text = "สำนักงานภูมิภาค" },
                new Select2Opt() { ID = "5238", Text = "พรรค" },
                new Select2Opt() { ID = "5240", Text = "ธนาคาร" },
                new Select2Opt() { ID = "5241", Text = "หอการค้า" },
                new Select2Opt() { ID = "5242", Text = "สมาคมหอการค้า" },
                new Select2Opt() { ID = "6110", Text = "คลีนิค" },
                new Select2Opt() { ID = "6120", Text = "ภัตตาคาร" },
                new Select2Opt() { ID = "6140", Text = "โรงงาน" },
                new Select2Opt() { ID = "6150", Text = "โรงพยาบาล" },
                new Select2Opt() { ID = "6160", Text = "โรงพยาบาลสัตว์" },
                new Select2Opt() { ID = "6170", Text = "โรงพิมพ์" },
                new Select2Opt() { ID = "6180", Text = "โรงไฟฟ้า" },
                new Select2Opt() { ID = "6190", Text = "โรงภาพยนตร์" },
                new Select2Opt() { ID = "6200", Text = "โรงรับจำนำ" },
                new Select2Opt() { ID = "6210", Text = "โรงแรม" },
                new Select2Opt() { ID = "6220", Text = "โรงเลื่อย" },
                new Select2Opt() { ID = "6230", Text = "โรงเลื่อยจักร" },
                new Select2Opt() { ID = "6240", Text = "โรงสี" },
                new Select2Opt() { ID = "6260", Text = "สถานพยาบาล" },
                new Select2Opt() { ID = "6270", Text = "สำนักงาน" },
                new Select2Opt() { ID = "6280", Text = "ห้างขายทอง" },
                new Select2Opt() { ID = "6290", Text = "ห้างขายยา" },
                new Select2Opt() { ID = "6310", Text = "ห้องอาหาร" },
                new Select2Opt() { ID = "7115", Text = "กองทุนรวม" },
                new Select2Opt() { ID = "7120", Text = "กองทุนสำรองเลี้ยงชีพ" },
                new Select2Opt() { ID = "7180", Text = "มหาวิทยาลัย" },
                new Select2Opt() { ID = "7190", Text = "โรงเรียน" },
                new Select2Opt() { ID = "7200", Text = "วิทยาลัย" },
                new Select2Opt() { ID = "7210", Text = "ศูนย์" },
                new Select2Opt() { ID = "7220", Text = "สถาบัน" },
                new Select2Opt() { ID = "7230", Text = "สมาพันธ์" },
                new Select2Opt() { ID = "7251", Text = "ชุมนุมสหกรณ์" },
                new Select2Opt() { ID = "7280", Text = "องค์การบริหารส่วนตำบล" },
                new Select2Opt() { ID = "5243", Text = "สมาคมการค้า" }
            };

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/VAT/JuristicName")]
        [HttpGet]
        public object GetJuristicName()
        {
            var name = IdentityFullName;
            Dictionary<string, string> results = new Dictionary<string, string>();

            SingleFormRequestEntity request = SingleFormRequestEntity.Get(IdentityID);
            if (request != null)
            {
                var general = request.SectionData.Where(o => o.SectionName == "GENERAL_INFORMATION").SingleOrDefault();
                if (general != null && general.FormData.ContainsKey("AJAX_DROPDOWN_COMPANY_TITLE_TEXT"))
                {
                    var titletext = general.FormData["AJAX_DROPDOWN_COMPANY_TITLE_TEXT"].DefaultString();
                    if (!string.IsNullOrEmpty(titletext))
                    {
                        name = string.Format("{0} {1}", titletext, IdentityFullName);
                    }
                }
            }
            results.Add("JuristicName", name);
            return results;
        }

        [Route("Api/v2/VAT/HQExample")]
        [HttpGet]
        public object GetHQExample()
        {
            var identityId = IdentityID;
            if (IdentityID.Length == 13)
            {
                var idenChar = IdentityID.ToCharArray();
                identityId = string.Format("{0}-{1}{2}{3}{4}-{5}{6}{7}{8}{9}-{10}{11}-{12}", idenChar[0], idenChar[1], idenChar[2], idenChar[3], idenChar[4], idenChar[5], idenChar[6], idenChar[7], idenChar[8], idenChar[9], idenChar[10], idenChar[11], idenChar[12]);
            }
            Dictionary<string, string> results = new Dictionary<string, string>()
            {
                { "IdentityID", identityId },
                { "IdentityName", IdentityFullName },
                { "HQType", "สำนักงานใหญ่" }
            };

            SingleFormRequestEntity request = SingleFormRequestEntity.Get(IdentityID);
            if (request != null)
            {
                var general = request.SectionData.Where(o => o.SectionName == "GENERAL_INFORMATION").SingleOrDefault();
                if (general != null && general.FormData.ContainsKey("AJAX_DROPDOWN_COMPANY_TITLE_TEXT"))
                {
                    var titletext = general.FormData["AJAX_DROPDOWN_COMPANY_TITLE_TEXT"].DefaultString();
                    if (!string.IsNullOrEmpty(titletext))
                    {
                        results.Remove("IdentityName");
                        results.Add("IdentityName", string.Format("{0} {1}", titletext, IdentityFullName));
                    }
                }
                var section = request.SectionData.Where(o => o.SectionName == "BRANCH0_ADDRESS_INFORMATION").SingleOrDefault();
                if (section != null && section.ArrayData.Count == 1)
                {
                    var hq = section.ArrayData[0];
                    results.Add("HQName", string.Format("{0} {1}", hq["AJAX_DROPDOWN_JURISTIC_BRANCH0_TITLE_TEXT"].DefaultString(), hq["JURISTIC_BRANCH0_NAME"].DefaultString()));
                    results.Add("Building", hq["ADDRESS_BUILDING_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("RoomNo", hq["ADDRESS_ROOMNO_JURISTIC_BRANCH0_ADDRESS"].DefaultString()); 
                    results.Add("Floor", hq["ADDRESS_FLOOR_JURISTIC_BRANCH0_ADDRESS"].DefaultString()); 
                    results.Add("Village", hq["ADDRESS_VILLAGE_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("HomeNo", hq["ADDRESS_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("Moo", hq["ADDRESS_MOO_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("Soi", hq["ADDRESS_SOI_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    if (!string.IsNullOrEmpty(hq["ADDRESS_SOI_JURISTIC_BRANCH0_ADDRESS"].DefaultString()))
                    {
                        results.Add("Yak", hq["ADDRESS_YAK_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    }
                    else
                    {
                        results.Add("Yak", string.Empty);
                    }
                    results.Add("Road", hq["ADDRESS_ROAD_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("SubDistrict", hq["ADDRESS_TUMBOL_JURISTIC_BRANCH0_ADDRESS_TEXT"].DefaultString());
                    results.Add("District", hq["ADDRESS_AMPHUR_JURISTIC_BRANCH0_ADDRESS_TEXT"].DefaultString());
                    results.Add("Province", hq["ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS_TEXT"].DefaultString());
                    results.Add("Zipcode", hq["ADDRESS_POSTCODE_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                    results.Add("TelNo", hq["ADDRESS_TELEPHONE_JURISTIC_BRANCH0_ADDRESS"].DefaultString());
                }
            }

            return results;
        }

        [Route("Api/v2/VAT/BranchExample")]
        [HttpGet]
        public object GetBranchExample()
        {
            var identityId = IdentityID;
            if (IdentityID.Length == 13)
            {
                var idenChar = IdentityID.ToCharArray();
                identityId = string.Format("{0}-{1}{2}{3}{4}-{5}{6}{7}{8}{9}-{10}{11}-{12}", idenChar[0], idenChar[1], idenChar[2], idenChar[3], idenChar[4], idenChar[5], idenChar[6], idenChar[7], idenChar[8], idenChar[9], idenChar[10], idenChar[11], idenChar[12]);
            }

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> branch = new Dictionary<string, string>();
            var titletext = string.Empty;

            SingleFormRequestEntity request = SingleFormRequestEntity.Get(IdentityID);
            if (request != null)
            {
                var general = request.SectionData.Where(o => o.SectionName == "GENERAL_INFORMATION").SingleOrDefault();
                if (general != null && general.FormData.ContainsKey("AJAX_DROPDOWN_COMPANY_TITLE_TEXT"))
                {
                    titletext = general.FormData["AJAX_DROPDOWN_COMPANY_TITLE_TEXT"].DefaultString();
                }
                var section = request.SectionData.Where(o => o.SectionName == "BRANCH_ADDRESS_INFORMATION").SingleOrDefault();
                if (section != null)
                {
                    foreach (var data in section.ArrayData)
                    {
                        branch = new Dictionary<string, string>()
                        {
                            { "IdentityID", identityId },
                            { "IdentityName", !string.IsNullOrEmpty(titletext) ? string.Format("{0} {1}", titletext, IdentityFullName) : IdentityFullName },
                            { "BranchName", data["JURISTIC_BRANCH_NAME"].DefaultString() },
                            { "BranchID", string.Format("สาขาที่ {0}", data["ARR_IDX"].DefaultString()) },
                            { "Building", data["ADDRESS_BUILDING_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "RoomNo", data["ADDRESS_ROOMNO_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "Floor", data["ADDRESS_FLOOR_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "Village", data["ADDRESS_VILLAGE_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "HomeNo", data["ADDRESS_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "Moo", data["ADDRESS_MOO_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "Soi", data["ADDRESS_SOI_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "Road", data["ADDRESS_ROAD_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "SubDistrict", data["ADDRESS_TUMBOL_JURISTIC_BRANCH_ADDRESS_TEXT"].DefaultString() },
                            { "District", data["ADDRESS_AMPHUR_JURISTIC_BRANCH_ADDRESS_TEXT"].DefaultString() },
                            { "Province", data["ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS_TEXT"].DefaultString() },
                            { "Zipcode", data["ADDRESS_POSTCODE_JURISTIC_BRANCH_ADDRESS"].DefaultString() },
                            { "TelNo", data["ADDRESS_TELEPHONE_JURISTIC_BRANCH_ADDRESS"].DefaultString() }
                        };
                        if (data.ContainsKey("AJAX_DROPDOWN_JURISTIC_BRANCH_TITLE_TEXT"))
                        {
                            branch.Add("BranchType", data["AJAX_DROPDOWN_JURISTIC_BRANCH_TITLE_TEXT"].DefaultString());
                        }
                        if (!string.IsNullOrEmpty(data["ADDRESS_SOI_JURISTIC_BRANCH_ADDRESS"].DefaultString()))
                        {
                            branch.Add("Yak", data["ADDRESS_YAK_JURISTIC_BRANCH_ADDRESS"].DefaultString());
                        }
                        else
                        {
                            branch.Add("Yak", string.Empty);
                        }

                        results.Add(branch);
                    }
                }
            }

            return results;
        }
    }
}