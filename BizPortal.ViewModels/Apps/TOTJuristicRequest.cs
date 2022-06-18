using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class TOTJuristicRequest
    {
        public TOTJuristicRequest()
        {
            Attachments = new List<TOTBase64File>();
        }

        [StringLength(50)]
        [JsonProperty("applicationreqid")]
        public string ApplicationReqID { get; set; }

        [StringLength(50)]
        [JsonProperty("requestid")]
        public string RequestID { get; set; }

        [StringLength(13)]
        [JsonProperty("idcard")]
        public string IDCard { get; set; }

        [StringLength(13)]
        [JsonProperty("traderegisterno")]
        public string TraderRegisterNo { get; set; }

        [StringLength(50)]
        [JsonProperty("company_titlename")]
        public string CompanyTitleName { get; set; }

        [StringLength(150)]
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [StringLength(5)]
        [JsonProperty("branch_code")]
        public string BranchCode { get; set; }

        [StringLength(150)]
        [JsonProperty("companytype")]
        public string CompanyType { get; set; }

        [StringLength(150)]
        [JsonProperty("business_type")]
        public string BusinessType { get; set; }

        [StringLength(25)]
        [JsonProperty("houseno")]
        public string HouseNo { get; set; }

        [StringLength(10)]
        [JsonProperty("villageno")]
        public string VillageNo { get; set; }

        [StringLength(10)]
        [JsonProperty("floorno")]
        public string FloorNo { get; set; }

        [StringLength(150)]
        [JsonProperty("buildname")]
        public string BuildName { get; set; }

        [StringLength(150)]
        [JsonProperty("lane")]
        public string Lane { get; set; }

        [StringLength(150)]
        [JsonProperty("road")]
        public string Road { get; set; }

        [StringLength(150)]
        [JsonProperty("subdistrict")]
        public string SubDistrict { get; set; }

        [StringLength(150)]
        [JsonProperty("district")]
        public string District { get; set; }

        [StringLength(150)]
        [JsonProperty("province")]
        public string Province { get; set; }

        [StringLength(10)]
        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }

        [StringLength(50)]
        [JsonProperty("telno")]
        public string TelNo { get; set; }

        [StringLength(150)]
        [JsonProperty("operatedby1")]
        public string OperatedBy1 { get; set; }

        [StringLength(150)]
        [JsonProperty("operatedby2")]
        public string OperatedBy2 { get; set; }

        [StringLength(150)]
        [JsonProperty("operatedby3")]
        public string OperatedBy3 { get; set; }

        [StringLength(150)]
        [JsonProperty("operatedby4")]
        public string OperatedBy4 { get; set; }

        [StringLength(150)]
        [JsonProperty("operatedby5")]
        public string OperatedBy5 { get; set; }

        [StringLength(13)]
        [JsonProperty("contact_idcard")]
        public string ContactIDCard { get; set; }

        [StringLength(50)]
        [JsonProperty("contact_titlename")]
        public string ContactTitleName { get; set; }

        [StringLength(150)]
        [JsonProperty("contact_firstname")]
        public string ContactFirstname { get; set; }

        [StringLength(150)]
        [JsonProperty("contact_lastname")]
        public string ContactLastname { get; set; }

        [StringLength(50)]
        [JsonProperty("contact_telno")]
        public string ContactTelNo { get; set; }

        [StringLength(50)]
        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }

        [StringLength(15)]
        [JsonProperty("home_based_id")]
        public string HomeBasedID { get; set; }

        [StringLength(25)]
        [JsonProperty("service_houseno")]
        public string ServiceHouseNo { get; set; }

        [StringLength(10)]
        [JsonProperty("service_villageno")]
        public string ServiceVillageNo { get; set; }
        [StringLength(10)]
        [JsonProperty("service_floorno")]
        public string ServiceFloorNo { get; set; }

        [StringLength(150)]
        [JsonProperty("service_buildname")]
        public string ServiceBuildName { get; set; }

        [StringLength(150)]
        [JsonProperty("service_lane")]
        public string ServiceLane { get; set; }

        [StringLength(150)]
        [JsonProperty("service_road")]
        public string ServiceRoad { get; set; }

        [StringLength(150)]
        [JsonProperty("service_subdistrict")]
        public string ServiceSubDistrict { get; set; }

        [StringLength(150)]
        [JsonProperty("service_district")]
        public string ServiceDistrict { get; set; }

        [StringLength(150)]
        [JsonProperty("service_province")]
        public string ServiceProvince { get; set; }

        [StringLength(10)]
        [JsonProperty("service_zipcode")]
        public string ServiceZipCode { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [StringLength(5)]
        [JsonProperty("service_location_code")]
        public string ServiceLocationCode { get; set; }

        [StringLength(5)]
        [JsonProperty("tot_service_code")]
        public string TOTServiceCode { get; set; }

        [StringLength(25)]
        [JsonProperty("request_dtm")]
        public string RequestDtm { get; set; }

        [JsonProperty("attachments")]
        public List<TOTBase64File> Attachments { get; set; }
    }

    public class TOTBase64File
    {
        public string Base64String { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}
