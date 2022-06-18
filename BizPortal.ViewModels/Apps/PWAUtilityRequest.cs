using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class PWAUtilityRequest
    {
        [StringLength(50)]
        [Required]
        public string ApplicationRequestID { get; set; }

        [StringLength(15)]
        [Required]
        public string ApplicationRequestNumber { get; set; }

        [JsonProperty("installType")]
        [Required]
        public byte InstallType { get; set; }

        [JsonProperty("usertype")]
        [Required]
        public byte UserType { get; set; }

        [JsonProperty("cardID")]
        [StringLength(13)]
        public string CardID { get; set; }

        /// <summary>
        /// Format dd/MM/yyyy B.E.
        /// </summary>
        [JsonProperty("cardBegin")]
        [StringLength(10)]
        public string CardBegin { get; set; }

        /// <summary>
        /// Format dd/MM/yyyy B.E.
        /// </summary>
        [JsonProperty("cardExpire")]
        [StringLength(10)]
        public string CardExpire { get; set; }

        [JsonProperty("customerTitle")]
        [Required]
        [StringLength(50)]
        public string CustomerTitle { get; set; }

        [JsonProperty("customerName")]
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [JsonProperty("customerSurname")]
        [Required]
        [StringLength(100)]
        public string CustomerSurname { get; set; }

        [JsonProperty("career")]
        [StringLength(100)]
        public string Career { get; set; }

        [JsonProperty("email")]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Format dd/MM/yyyy B.E.
        /// </summary>
        [JsonProperty("birthDate")]
        [StringLength(10)]
        public string BirthDate { get; set; }

        [JsonProperty("dptID")]
        [StringLength(20)]
        public string DptID { get; set; }

        /// <summary>
        /// Format dd/MM/yyyy B.E.
        /// </summary>
        [JsonProperty("dptBegin")]
        [StringLength(10)]
        public string DptBegin { get; set; }

        /// <summary>
        /// Format dd/MM/yyyy B.E.
        /// </summary>
        [JsonProperty("dptExpire")]
        [StringLength(10)]
        public string DptExpire { get; set; }

        [JsonProperty("taxNo")]
        [StringLength(13)]
        public string TaxNo { get; set; }

        [JsonProperty("branchNo")]
        [StringLength(5)]
        public string BranchNo { get; set; }

        [JsonProperty("cusdptPosition")]
        public string CusDptPosition { get; set; }

        [JsonProperty("addressNo")]
        [Required]
        [StringLength(50)]
        public string AddressNo { get; set; }

        [JsonProperty("building")]
        [StringLength(100)]
        public string Building { get; set; }

        [JsonProperty("floor")]
        [StringLength(5)]
        public string Floor { get; set; }

        [JsonProperty("villageNo")]
        [StringLength(3)]
        public string VillageNo { get; set; }

        [JsonProperty("village")]
        [StringLength(50)]
        public string Village { get; set; }

        [JsonProperty("soi")]
        [StringLength(30)]
        public string Soi { get; set; }

        [JsonProperty("road")]
        [StringLength(50)]
        public string Road { get; set; }

        [JsonProperty("changwat")]
        [Required]
        [StringLength(200)]
        public string Province{ get; set; }

        [JsonProperty("amphoe")]
        [Required]
        [StringLength(200)]
        public string Amphoe { get; set; }

        [JsonProperty("tambon")]
        [Required]
        [StringLength(200)]
        public string Tambon { get; set; }

        [JsonProperty("ownerId")]
        [Required]
        [StringLength(4)]
        public string OwnerID { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("zipcode")]
        [Required]
        [StringLength(5)]
        public string Zipcode { get; set; }

        [JsonProperty("tel")]
        [StringLength(20)]
        public string Tel { get; set; }

        [JsonProperty("fax")]
        [StringLength(20)]
        public string Fax { get; set; }

        [JsonProperty("mobile")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [JsonProperty("nearLocation")]
        [StringLength(100)]
        public string NearLocation { get; set; }

        [JsonProperty("addrId")]
        [StringLength(11)]
        public string AddrID { get; set; }
    }
}
