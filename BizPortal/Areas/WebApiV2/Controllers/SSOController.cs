using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Integrated;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Select2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class SSOController : ApiControllerBase
    {
        [Route("Api/v2/SSO/JuristicBranch")]
        [HttpGet]
        public object JuristicBranch()
        {
            var provinces = GeoService.Provinces(string.Empty);
            var provinceID = IdentityID.Substring(1, 2);
            var wages = DB.SSOMinimumWages.ToList();
            foreach (var province in provinces)
            {
                province.MinimumWage = wages.Where(o => o.ProvinceCode == province.ID).Select(o => o.MinimumWage).DefaultIfEmpty(0).Single();
            }

            var HQMinWage = provinces.Where(o => o.ID == provinceID).Select(o => o.MinimumWage).SingleOrDefault();

            List<BranchSelect2Opt> options = new List<BranchSelect2Opt>()
            {
                new BranchSelect2Opt() { ID = "00000", Text = "สำนักงานใหญ่", MinimumWage = HQMinWage }
            };

            SingleFormRequestEntity request = SingleFormRequestEntity.Get(IdentityID);
            if (request != null)
            {
                var section = request.SectionData.Where(o => o.SectionName == "BRANCH_ADDRESS_INFORMATION").SingleOrDefault();
                if (section != null)
                {
                    foreach (var branch in section.ArrayData)
                    {
                        var branchName = branch["JURISTIC_BRANCH_NAME"].DefaultString();
                        var branchProvinceID = branch["ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS"].DefaultString();
                        var branchMinWage = provinces.Where(o => o.ID == branchProvinceID).Select(o => o.MinimumWage).SingleOrDefault();
                        options.Add(new BranchSelect2Opt() { ID = branchName, Text = branchName, MinimumWage = branchMinWage });
                    }
                }
            }

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/SSO/GetMinimumWage")]
        [HttpGet]
        public object GetMinimumWage()
        {
            var provinces = GeoService.Provinces(string.Empty);
            var wages = DB.SSOMinimumWages.ToList();
            foreach (var province in provinces)
            {
                province.MinimumWage = wages.Where(o => o.ProvinceCode == province.ID).Select(o => o.MinimumWage).DefaultIfEmpty(0).Single();
            }

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            SingleFormRequestEntity request = SingleFormRequestEntity.Get(IdentityID);
            if (request != null)
            {
                var employeeSection = request.SectionData.Where(o => o.SectionName == "SSO_EMPLOYEE_INFORMATION").SingleOrDefault();
                var branchSection = request.SectionData.Where(o => o.SectionName == "BRANCH_ADDRESS_INFORMATION").SingleOrDefault();

                if (employeeSection != null && branchSection != null)
                {
                    foreach (var emp in employeeSection.ArrayData)
                    {
                        decimal branchMinWage = 0;
                        var employeeBr = string.Empty;
                        var empStatus = emp["SSO_EMPLOYEE_REG_STATUS"].DefaultString();
                        if (empStatus == "true")
                        {
                            employeeBr = emp["AJAX_DROPDOWN_SSO_EMPLOYEE_BRANCH_TRUE"].DefaultString();
                        }
                        else if (empStatus == "false")
                        {
                            employeeBr = emp["AJAX_DROPDOWN_SSO_EMPLOYEE_BRANCH_FALSE"].DefaultString();
                        }

                        if (employeeBr == "00000")
                        {
                            var provinceID = IdentityID.Substring(1, 2);
                            branchMinWage = provinces.Where(o => o.ID == provinceID).Select(o => o.MinimumWage).SingleOrDefault();
                        }
                        else
                        {
                            var br = branchSection.ArrayData.Where(o => o["JURISTIC_BRANCH_NAME"].DefaultString() == employeeBr).SingleOrDefault();
                            if (br != null)
                            {
                                var brProvince = br["ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS"].DefaultString();
                                branchMinWage = provinces.Where(o => o.ID == brProvince).Select(o => o.MinimumWage).SingleOrDefault();
                            }
                        }

                        results.Add(new Dictionary<string, string>()
                        {
                            { "ARR_IDX", emp["ARR_IDX"].DefaultString() },
                            { "MIN_WAGE", branchMinWage == 0 ? "null" : branchMinWage.ToString() }
                        });
                    }
                }
            }

            return results;
        }

        [Route("Api/v2/SSO/EmployeeRegStatus")]
        [HttpGet]
        public JObject EmployeeRegStatus(string IdentityID)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("CitizenID", IdentityID);

            JObject regStatus = Api.Get(ConfigurationManager.AppSettings["SSO_CITIZEN_STATUS_WS_URL"], args);

            if (regStatus != null)
            {
                return regStatus;
            }

            return null;
        }

        [Route("Api/v2/SSO/EmployeeRegStatusCheck")]
        [HttpGet]
        public object EmployeeRegStatusCheck(string IdentityID)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("CitizenID", IdentityID);

            JObject regStatus = Api.Get(ConfigurationManager.AppSettings["SSO_CITIZEN_STATUS_WS_URL"], args);

            if (regStatus != null)
            {
                if (regStatus.HasValues && regStatus["Result"]["Status"].ToString() == "A")
                {
                    return new { result = true };
                }
                else
                {
                    return new { result = false };
                }
            }

            return null;
        }

        [Route("Api/v2/SSO/HospitalList")]
        [HttpGet]
        public object HospitalList(string query = "")
        {
            List<Select2Opt> options = new List<Select2Opt>();
            //Dictionary<string, string> args = new Dictionary<string, string>();
            //args.Add("ProvinceID", ProvinceID);

            JObject hospList = Api.Get(ConfigurationManager.AppSettings["SSO_HOSPITAL_LIST_WS_URL"]); //, args);
            if (hospList != null && hospList.HasValues)
            {
                var hospitals = hospList["Result"].ToList();
                if (hospitals != null && hospitals.Count > 0)
                {
                    foreach (var hospital in hospitals)
                    {
                        options.Add(new Select2Opt() { ID = hospital["Code"].DefaultString(), Text = hospital["Name"].DefaultString() });
                    }
                }
            }

            options = options.Where(o => o.Text.Contains(string.IsNullOrEmpty(query) ? string.Empty : query)).ToList();

            return new { results = options.ToArray() };
        }
    }
}