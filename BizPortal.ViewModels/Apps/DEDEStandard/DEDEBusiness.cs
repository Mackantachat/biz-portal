using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BizPortal.ViewModels.Apps.DEDEStandard
{

    public class BusinessData
    {
        public request request { get; set; }
        public List<authorizes> authorizes { get; set; }
        public smartgrid smartgrid { get; set; }
        public List<drivers> drivers { get; set; }
        public List<owner> owner { get; set; }
        public personel personel { get; set; }
        public sites sites { get; set; }
        public List<gen> gen { get; set; }
        public List<co_id> co_id { get; set; }
        public List<attached> attacheds { get; set; }

    }


    public class request
    {

        public int is_company { get; set; }
        public int is_authorize { get; set; }
        public int is_samartgrid { get; set; }
        public int ref_purposes_id { get; set; }
        public int ref_permittype_id { get; set; }
        public string powerplant { get; set; }
        public string inspector { get; set; }
        public string from { get; set; }
        public string application_request_id { get; set; }
    }

    public class authorizes
    {
        public string position { get; set; }

        public int is_authorize { get; set; }

        public personel personel { get; set; }

        public communication communication { get; set; }

    }

    public class smartgrid
    {
        public string installation { get; set; }
        public string internal_used { get; set; }

        [JsonProperty("smartgrid")]
        public string Smartgrid { get; set; }
        public string directcustomer { get; set; }
        public string scod { get; set; }

    }

    public class drivers
    {
        public int no { get; set; }
        public int power_drive { get; set; }
        public int is_solar { get; set; }
        public int mainfuel { get; set; }
        public string installed_year { get; set; }
        public string install_type { get; set; }
        public string brand { get; set; }
        public string horse_power { get; set; }
        public string machine_num { get; set; }
        public List<details> details { get; set; }

    }
    public class details
    {
        public int ref_solar_id { get; set; }
        public int ref_solartype_id { get; set; }
        public int power { get; set; }
        public int num_cell { get; set; }
        public int cell_type { get; set; }
        public string solar_type { get; set; }
    }
    public class owner
    {
        public string name { get; set; }
        public int ref_company_id { get; set; }
        public string id_number { get; set; }
        public List<personel> board { get; set; }

    }

    public class sites
    {
        public string name { get; set; }
        public string about { get; set; }
        public string tsic { get; set; }
        public communication communication { get; set; }
        public gis gis { get; set; }
        public address address { get; set; }

    }

    public class gis
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

    }

    public class address
    {
        public string name { get; set; }
        public string code { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string addr4 { get; set; }

        public int district_id { get; set; }
    }

    public class gen
    {
        public int no { get; set; }
        public int ref_gentype_id { get; set; }
        public string g_type { get; set; }
        public string c_power { get; set; }
        public string c_voltage { get; set; }
        public string c_current { get; set; }
        public string c_factor { get; set; }
        public string g_pole { get; set; }
        public string g_used { get; set; }
        public string c_installed_year { get; set; }
        public string c_brand { get; set; }
        public string c_serie { get; set; }
        public string c_machine_num { get; set; }
        public int c_purpose_id { get; set; }
        public int c_permittype_id { get; set; }
        public string install_type { get; set; }
    }

    public class co_id
    {
        public string user_profile_id { get; set; }
        public string communication_id { get; set; }
        public personel personel { get; set; }
        public communication communication { get; set; }
    }

    public class attached
    {
        public string ref_document_id { get; set; }
        public string url { get; set; }
        public int is_upload { get; set; }
        public int is_approved { get; set; }

    }

    public class UPrefixList
    {

        public List<PrefixInfo> data { get; set; }

    }
    public class CPrefixList
    {

        public List<PrefixInfo> data { get; set; }

    }

    public class PrefixInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class DocumentList
    {

        public List<DocumentInfo> document { get; set; }

    }
    public class DocumentInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string KEY { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string filetypecode { get; set; }
    }
}
