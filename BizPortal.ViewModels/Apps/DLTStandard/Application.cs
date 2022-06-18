using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BizPortal.ViewModels.Apps.DLTStandard
{

  
    public class Tran
    {
        public string province_t { get; set; }
    }

    public class Truck
    {
        public string license_no { get; set; }
        public string register_pro_th { get; set; }
        public string chassis_no { get; set; }
        public string engine_no { get; set; }
        public string truck_type { get; set; }
        public string truck_type_sub { get; set; }
        public string truck_category { get; set; }
        public string brand { get; set; }
    }

    public class Place
    {
        public string land_id { get; set; }
        public string land_no { get; set; }
        public string land_code { get; set; }
        public string address_no { get; set; }
        public string address_moo { get; set; }
        public string address_village { get; set; }
        public string address_lane { get; set; }
        public string address_building { get; set; }
        public string address_room { get; set; }
        public string address_floor { get; set; }
        public string address_road { get; set; }
        public string tumbol { get; set; }
        public string amphoe { get; set; }
        public string province { get; set; }
        public string postal { get; set; }
        public string tel_no { get; set; }
        public string tel_no_ext { get; set; }
        public string fax_no { get; set; }
        public string email { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string address_type { get; set; }
        public string phone { get; set; }
        public string area { get; set; }
        public string num_trucks_stored { get; set; }
        public string address_owner { get; set; }
        public string address_area_doc { get; set; }
        public string address_area_unit { get; set; }
        public string rent_type { get; set; }
        public string rai { get; set; }
        public string nkan { get; set; }
        public string tarankwa { get; set; }
    }

    public class Application
    {
        public string bizAppId { get; set; }
        public string bizReqNo { get; set; }
        public string bizReqDateTime { get; set; }
        public string reqNo { get; set; }
        public string company_name_th { get; set; }
        public string company_name_en { get; set; }
        public string company_type { get; set; }
        public string org_name_th { get; set; }
        public string org_name_en { get; set; }
        public string org_type { get; set; }
        public string org_no { get; set; }
        public string org_regis_date { get; set; }
        public string org_email { get; set; }
        public string title_name_th { get; set; }
        public string name_th { get; set; }
        public string surname_th { get; set; }
        public string title_name_en { get; set; }
        public string name_en { get; set; }
        public string surname_en { get; set; }
        public string citizen_no { get; set; }
        public string birthdate { get; set; }
        public string nationality { get; set; }
        public string operator_type { get; set; }
        public string address_no { get; set; }
        public string address_moo { get; set; }
        public string address_lane { get; set; }
        public string address_building { get; set; }
        public string address_room { get; set; }
        public string address_floor { get; set; }
        public string address_road { get; set; }
        public string tel_no { get; set; }
        public string tel_no_ext { get; set; }
        public string is_yourself { get; set; }
        public string is_same_address { get; set; }
        public string fax_no { get; set; }
        public string email { get; set; }
        public string tumbol { get; set; }
        public string amphoe { get; set; }
        public string province { get; set; }
        public string postal { get; set; }
        public string to_all_provinces { get; set; }
        public string regis_capital { get; set; }
        public string regis_date { get; set; }
        public string days_to_fill_fleet { get; set; }
        public string has_stop_point { get; set; }
        public string transfer_animal { get; set; }
        public string transfer_goods { get; set; }
        public string can_operate_on_approval { get; set; }
        public string operation_date { get; set; }
        public string total_trucks_by_regulation { get; set; }
        public string reason { get; set; }
        public string note { get; set; }
        public string num_truck1 { get; set; }
        public string num_truck2 { get; set; }
        public string num_truck3 { get; set; }
        public string num_truck4 { get; set; }
        public string num_truck5 { get; set; }
        public string num_truck6 { get; set; }
        public string num_truck7 { get; set; }
        public string num_truck8 { get; set; }
        public string num_truck9 { get; set; }
        public string cnt_car10 { get; set; }
        public string total_trucks { get; set; }
        public string total_drivers { get; set; }
        public string storage_area { get; set; }
        public string num_trucks_stored { get; set; }
        public string address_landno_1 { get; set; }
        public string address_no_1 { get; set; }
        public string address_moo_1 { get; set; }
        public string address_village_1 { get; set; }
        public string address_lane_1 { get; set; }
        public string address_building_1 { get; set; }
        public string address_room_1 { get; set; }
        public string address_floor_1 { get; set; }
        public string address_road_1 { get; set; }
        public string tumbol_1 { get; set; }
        public string amphoe_1 { get; set; }
        public string province_1 { get; set; }
        public string postal_1 { get; set; }
        public string phone_1 { get; set; }
        public string tel_no_1 { get; set; }
        public string tel_no_ext_1 { get; set; }
        public string fax_no_1 { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public List<Tran> trans { get; set; }
        public List<Truck> truck { get; set; }
        public List<Place> place { get; set; }
       
    }

    public class BusinessData
    {
        public List<Application> application { get; set; }
    }

    public class BusinessDataFile
    {
        public List<ApplicationFile> application_file { get; set; }
    }

    public class File
    {
        public string file_topic { get; set; }
        public string file_type { get; set; }
        public string file_name { get; set; }
        public string file_name64 { get; set; }
    }

    public class ApplicationFile
    {
        public string app_token { get; set; }
        public string message { get; set; }
        public List<File> file { get; set; }
    }
}
