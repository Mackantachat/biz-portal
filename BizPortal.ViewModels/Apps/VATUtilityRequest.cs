using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class VATUtilityRequest
    {
        public VATUtilityRequest()
        {

        }

        [JsonProperty("tin")]
        public Tin TIN { get; set; }

        [JsonProperty("vat")]
        public Vat VAT { get; set; }

    }

    public class Tin
    {
        [JsonProperty("corpInfo")]
        public CorpInfo CORPINFO { get; set; }

        [JsonProperty("committee")]
        public List<Committee> COMMITTEE { get; set; }

        [JsonProperty("currentAddressInformation")]
        public CurrentAddressInformation CURRENTADDRESSINFORMATION { get; set; }
    }

    public class CorpInfo
    {
        [JsonProperty("registerNumber")]
        [StringLength(13)]
        public string REGISTERNUMBER { get; set; }

        [JsonProperty("registerName")]
        [StringLength(100)]
        public string REGISTERNAME { get; set; }

        [JsonProperty("registerDate")]
        public string REGISTERDATE { get; set; }

        [JsonProperty("accountingDate")]
        public string ACCOUNTINGDATE { get; set; }
    }

    public class Committee
    {
        [JsonProperty("idNumber")]
        [StringLength(13)]
        public string IDNUMBER { get; set; }

        [JsonProperty("titleCode")]
        public string TITLECODE { get; set; }

        [JsonProperty("name")]
        [StringLength(160)]
        public string NAME { get; set; }

        [JsonProperty("surName")]
        [StringLength(80)]
        public string SURNAME { get; set; }

        [JsonProperty("nationality")]
        public string NATIONALITY { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DATEOFBIRTH { get; set; }
    }

    public class CurrentAddressInformation
    {
        [JsonProperty("houseIdNumber")]
        [StringLength(11)]
        public string HOUSEIDNUMBER { get; set; }

        [JsonProperty("buildingName")]
        [StringLength(40)]
        public string BUILDINGNAME { get; set; }

        [JsonProperty("roomNumber")]
        [StringLength(30)]
        public string ROOMNUMBER { get; set; }

        [JsonProperty("floorNumber")]
        [StringLength(30)]
        public string FLOORNUMBER { get; set; }

        [JsonProperty("villageName")]
        [StringLength(40)]
        public string VILLAGENAME { get; set; }

        [JsonProperty("houseNumber")]
        [StringLength(30)]
        public string HOUSENUMBER { get; set; }

        [JsonProperty("mooNumber")]
        [StringLength(10)]
        public string MOONUMBER { get; set; }

        [JsonProperty("soiName")]
        [StringLength(60)]
        public string SOINAME { get; set; }

        [JsonProperty("yaek")]
        [StringLength(60)]
        public string YAEK { get; set; }

        [JsonProperty("streetName")]
        [StringLength(40)]
        public string STREETNAME { get; set; }

        [JsonProperty("thambolCode")]
        [StringLength(6)]
        public string THAMBOLCODE { get; set; }

        [JsonProperty("ampurCode")]
        [StringLength(6)]
        public string AMPURCODE { get; set; }

        [JsonProperty("provinceCode")]
        [StringLength(6)]
        public string PROVINCECODE { get; set; }

        [JsonProperty("postCode")]
        [StringLength(5)]
        public string POSTCODE { get; set; }

        [JsonProperty("telNumber")]
        [StringLength(25)]
        public string TELNUMBER { get; set; }
    }

    public class Vat
    {
        [JsonProperty("pp01Info")]
        public Pp01Info PP01INFO { get; set; }

        [JsonProperty("pp01bInfo")]
        public List<Branch> PP01BINFO { get; set; }

        [JsonProperty("pp01cInfo")]
        public Pp01cInfo PP01CINFO { get; set; }

        [JsonProperty("pp011Info")]
        public Pp011Info PP011INFO { get; set; }
    }

    public class Pp01Info
    {
        [JsonProperty("tin")]
        public string TIN { get; set; }

        [JsonProperty("branum")]
        public string BRANUM { get; set; }

        [JsonProperty("buswatdat")]
        public string BUSWATDAT { get; set; }

        [JsonProperty("forrecdat")]
        public string FORRECDAT { get; set; }

        [JsonProperty("busfirdat")]
        public string BUSFIRDAT { get; set; }

        [JsonProperty("busincdat")]
        public string BUSINCDAT { get; set; }

        [JsonProperty("buscptamo")]
        public string BUSCPTAMO { get; set; }

        [JsonProperty("estmonincamo")]
        public string ESTMONINCAMO { get; set; }

        [JsonProperty("email")]
        public string EMAIL { get; set; }

        [JsonProperty("agree1")]
        public string AGREE1 { get; set; }

        [JsonProperty("agree2")]
        public string AGREE2 { get; set; }

        [JsonProperty("agree3")]
        public string AGREE3 { get; set; }

        [JsonProperty("agree4")]
        public string AGREE4 { get; set; }

        [JsonProperty("agree5")]
        public string AGREE5 { get; set; }
    }

    public class Branch
    {
        [JsonProperty("brano")]
        public string BRANO { get; set; }

        [JsonProperty("titcod")]
        [StringLength(8)]
        public string TITCOD { get; set; }

        [JsonProperty("branam")]
        [StringLength(160)]
        public string BRANAM { get; set; }

        [JsonProperty("bldgnam")]
        [StringLength(40)]
        public string BLDGNAM { get; set; }

        [JsonProperty("roomno")]
        [StringLength(30)]
        public string ROOMNO { get; set; }

        [JsonProperty("floorno")]
        [StringLength(30)]
        public string FLOORNO { get; set; }

        [JsonProperty("village")]
        [StringLength(40)]
        public string VILLAGE { get; set; }

        [JsonProperty("addno")]
        [StringLength(30)]
        public string ADDNO { get; set; }

        [JsonProperty("moono")]
        [StringLength(10)]
        public string MOONO { get; set; }

        [JsonProperty("soinam")]
        [StringLength(60)]
        public string SOINAM { get; set; }

        [JsonProperty("yaek")]
        [StringLength(60)]
        public string YAEK { get; set; }

        [JsonProperty("thnnam")]
        [StringLength(40)]
        public string THNNAM { get; set; }

        [JsonProperty("tamcod")]
        [StringLength(6)]
        public string TAMCOD { get; set; }

        [JsonProperty("ampcod")]
        [StringLength(6)]
        public string AMPCOD { get; set; }

        [JsonProperty("provcod")]
        [StringLength(6)]
        public string PROVCOD { get; set; }

        [JsonProperty("poscod")]
        [StringLength(5)]
        public string POSCOD { get; set; }

        [JsonProperty("telno")]
        [StringLength(25)]
        public string TELNO { get; set; }
    }

    public class Pp01cInfo
    {
        [JsonProperty("bustypcod1")]
        public string BUSTYPCOD1 { get; set; }

        [JsonProperty("gootypdes1")]
        [StringLength(250)]
        public string GOOTYPDES1 { get; set; }

        [JsonProperty("bustypcod2")]
        public string BUSTYPCOD2 { get; set; }

        [JsonProperty("gootypdes2")]
        [StringLength(250)]
        public string GOOTYPDES2 { get; set; }

        [JsonProperty("bustypcod3")]
        public string BUSTYPCOD3 { get; set; }

        [JsonProperty("gootypdes3")]
        [StringLength(250)]
        public string GOOTYPDES3 { get; set; }

        [JsonProperty("bustypcod4")]
        public string BUSTYPCOD4 { get; set; }

        [JsonProperty("gootypdes4")]
        [StringLength(250)]
        public string GOOTYPDES4 { get; set; }

        [JsonProperty("bustypcod5")]
        public string BUSTYPCOD5 { get; set; }

        [JsonProperty("gootypdes5")]
        [StringLength(250)]
        public string GOOTYPDES5 { get; set; }

        [JsonProperty("bustypcod6")]
        public string BUSTYPCOD6 { get; set; }

        [JsonProperty("gootypdes6")]
        [StringLength(250)]
        public string GOOTYPDES6 { get; set; }
    }

    public class Pp011Info
    {
        [JsonProperty("bus1")]
        public string BUS1 { get; set; }

        [JsonProperty("bus2")]
        public string BUS2 { get; set; }

        [JsonProperty("bus3")]
        public string BUS3 { get; set; }

        [JsonProperty("bus4")]
        public string BUS4 { get; set; }

        [JsonProperty("bus5")]
        public string BUS5 { get; set; }

        [JsonProperty("bus6")]
        public string BUS6 { get; set; }

        [JsonProperty("bus7")]
        public string BUS7 { get; set; }

        [JsonProperty("bus8")]
        public string BUS8 { get; set; }

        [JsonProperty("bus9")]
        public string BUS9 { get; set; }

        [JsonProperty("bus10")]
        public string BUS10 { get; set; }

        [JsonProperty("pp011sta")]
        public string PP011STA { get; set; }
    }
}
