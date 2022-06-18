using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BizPortal.ViewModels.Apps
{
    public class TOTCitizenRequest
    {
        /// <summary>
        /// บัญชีเชื่อมต่อบริการ
        /// </summary>
        [Required]
        [JsonProperty("auth_contracts")]
        public string AuthContracts { get; set; }

        /// <summary>
        /// รหัสเชื่อมต่อบริการ
        /// </summary>
        [Required]
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }

        [Required]
        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }

        [Required]
        [JsonProperty("lang")]
        public string Language { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty("applicationreqid")]
        public string ApplicationRequestID { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("requestnumber")]
        public string RequestNumber { get; set; }

        [Required]
        [JsonProperty("national_id")]
        public string NationalID { get; set; }

        /// <summary>
        /// Mr., Mrs., Miss : นาย, นาง, นางสาว
        /// </summary>
        [Required]
        [StringLength(150)]
        [JsonProperty("prefix_name")]
        public string PrefixName { get; set; }

        [Required]
        [StringLength(250)]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Male, Female : ชาย, หญิง
        /// </summary>
        [StringLength(25)]
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// DD-MM-YYYY
        /// </summary>
        [StringLength(20)]
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [StringLength(15)]
        [JsonProperty("phone_no")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        [JsonProperty("home_no")]
        public string HomeNumber { get; set; }

        [StringLength(250)]
        [JsonProperty("village_building")]
        public string VillageBuilding { get; set; }

        [StringLength(10)]
        [JsonProperty("floor")]
        public string Floor { get; set; }

        [StringLength(5)]
        [JsonProperty("moo")]
        public string Moo { get; set; }

        [StringLength(150)]
        [JsonProperty("soi")]
        public string Soi { get; set; }

        [StringLength(150)]
        [JsonProperty("road")]
        public string Road { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("subdistrict")]
        public string SubDistrict { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("district")]
        public string District { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("province")]
        public string Province { get; set; }

        [Required]
        [StringLength(5)]
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [StringLength(150)]
        [JsonProperty("cont_email")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// Mobile Phone : 09xxxxxxxx
        /// </summary>
        [Required]
        [StringLength(15)]
        [JsonProperty("cont_number")]
        public string ContactNumber { get; set; }

        [StringLength(15)]
        [JsonProperty("ins_home_based_id")]
        public string InstallHomeBasedID { get; set; }

        [Required]
        [StringLength(20)]
        [JsonProperty("ins_home_no")]
        public string InstallHomeNumber { get; set; }

        [StringLength(250)]
        [JsonProperty("ins_village_building")]
        public string InstallVillageBuilding { get; set; }

        [StringLength(10)]
        [JsonProperty("ins_floor")]
        public string InstallFloor { get; set; }

        [StringLength(5)]
        [JsonProperty("ins_moo")]
        public string InstallMoo { get; set; }

        [StringLength(150)]
        [JsonProperty("ins_soi")]
        public string InstallSoi { get; set; }

        [StringLength(150)]
        [JsonProperty("ins_road")]
        public string InstallRoad { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("ins_subdistrict")]
        public string InstallSubDistrict { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("ins_district")]
        public string InstallDistrict { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty("ins_province")]
        public string InstallProvince { get; set; }

        [Required]
        [StringLength(5)]
        [JsonProperty("ins_zipcode")]
        public string InstallZipcode { get; set; }

        [Required]
        [JsonProperty("ins_latitude")]
        public double InstallLatitude { get; set; }

        [Required]
        [JsonProperty("ins_longitude")]
        public double InstallLongitude { get; set; }
    }
}
