using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    #region[IDP]

    public class NDIDIdp
    {
        public string NameEN { get; set; }

        public string NameTH { get; set; }

        public string ImageUrl { get; set; }

        public List<string> NodeIds { get; set; }

        public string Code { get; set; }

        public float MaxIAL { get; set; }

        public float MaxAAL { get; set; }

    }

    public class NDIDIdpResponse
    {
        public string node_id { get; set; }

        public string node_name { get; set; }

        public float max_ial { get; set; }

        public float max_aal { get; set; }

        public NDIDIdpResponseDetail Detail
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<NDIDIdpResponseDetail>(this.node_name);

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }

    public class NDIDIdpResponseDetail
    {
        public string industry_code { get; set; }

        public string company_code { get; set; }

        public string marketing_name_th { get; set; }

        public string marketing_name_en { get; set; }

        public string proxy_or_subsidiary_name_th { get; set; }

        public string proxy_or_subsidiary_name_en { get; set; }

        public string role { get; set; }

        public string running { get; set; }
    }
    #endregion

    #region[AS]
    public class NDIDAs
    {
        public string node_id { get; set; }

        public string node_name { get; set; }

        public float min_ial { get; set; }

        public float min_aal { get; set; }

        public NDIDAsDetail Detail
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<NDIDAsDetail>(node_name);
                }
                catch (Exception ex)
                {
                }

                return null;
            }
        }
    }

    public class NDIDAsDetail
    {
        public string industry_code { get; set; }

        public string company_code { get; set; }

        public string marketing_name_th { get; set; }

        public string marketing_name_en { get; set; }

        public string proxy_or_subsidiary_name_th { get; set; }

        public string proxy_or_subsidiary_name_en { get; set; }

        public string role { get; set; }

        public int running { get; set; }
    }
    #endregion

    #region[RP Request]

    public class NDIDRequestIdentity
    {
        public int mode { get; set; }

        public string reference_id { get; set; }

        public List<string> idp_id_list { get; set; }

        public string callback_url { get; set; }

        public string request_message { get; set; }

        public float min_ial { get; set; }

        public float min_aal { get; set; }

        public int min_idp { get; set; }

        public int request_timeout { get; set; }

        public List<NDIDRequestList> data_request_list { get; set; }

    }

    public class NDIDRequestList
    {
        public string service_id { get; set; }

        public List<string> as_id_list { get; set; }

        public int min_as { get; set; }

        public string request_params { get; set; }
    }

    public class NDIDRequestIdentityResponse
    {
        public string reference_id { get; set; }

        public string request_id { get; set; }

        public string initial_salt { get; set; }
    }

    public enum NDIDIdentityNamespace
    {
        [StringValue("citizen_id")]
        CitizenId
    }
    #endregion

    #region[RP Get Data]
    public class NDIDGetData
    {
        public string source_node_id { get; set; }

        public string service_id { get; set; }

        public string source_signature { get; set; }

        public string signature_sign_method { get; set; }

        public string data_salt { get; set; }

        public string data { get; set; }
    }

    // 001.cust_info_001 and 001.basic_cust_info_001
    public class NDIDIdentity
    {
        public NDIDIdentityCustomerInfoTH customer_info_th { get; set; }

        public NDIDIdentityCustomerInfoEN customer_info_en { get; set; }

        public string birth_date { get; set; }

        public NDIDIdentityIdentifier identifier { get; set; }

        public NDIDIdentityCustomerCardAddress customer_address_id_card { get; set; }

        public NDIDIdentityCustomerContact customer_contact { get; set; }
    }

    // 001.contact_cust_info_001
    public class NDIDIdentityContact
    {
        public NDIDIdentityIdentifier identifier { get; set; }

        public NDIDIdentityCustomerContactAddress customer_address_contact { get; set; }

        public NDIDIdentityCustomerContact customer_contact { get; set; }
    }

    #region[Detail]
    public class NDIDIdentityCustomerInfoTH
    {
        public string thai_title { get; set; }

        public string thai_first_name { get; set; }

        public string thai_middle_name { get; set; }

        public string thai_last_name { get; set; }

        public string thai_full_name { get; set; }
    }

    public class NDIDIdentityCustomerInfoEN
    {
        public string en_title { get; set; }

        public string en_first_name { get; set; }

        public string en_middle_name { get; set; }

        public string en_last_name { get; set; }

        public string en_full_name { get; set; }
    }

    public class NDIDIdentityIdentifier
    {
        public string card_number { get; set; }

        public string card_type { get; set; }

        public string card_issuing_country { get; set; }

        public string card_issue_date { get; set; }

        public string card_expiry_date { get; set; }
    }

    public class NDIDIdentityCustomerCardAddress
    {
        public string id_card_street_address1 { get; set; }

        public string id_card_street_address2 { get; set; }

        public string id_card_address_subdistrict { get; set; }

        public string id_card_address_district { get; set; }

        public string id_card_address_province { get; set; }

        public string id_card_address_zipcode { get; set; }

        public string id_card_address_country { get; set; }

        public string id_card_address_full { get; set; }
    }

    public class NDIDIdentityCustomerContactAddress
    {
        public string contact_street_address1 { get; set; }

        public string contact_street_address2 { get; set; }

        public string contact_address_subdistrict { get; set; }

        public string contact_address_district { get; set; }

        public string contact_address_province { get; set; }

        public string contact_address_zipcode { get; set; }

        public string contact_address_country { get; set; }

        public string contact_address_full { get; set; }
    }

    public class NDIDIdentityCustomerContact
    {
        public string gender { get; set; }

        public string marital_status { get; set; }

        public string nationality { get; set; }

        public string non_iso_nationality_desciption { get; set; }

        public string home_tel_no { get; set; }

        public string home_tel_no_ext { get; set; }

        public string mobile_tel_no { get; set; }

        public string email_addr { get; set; }

        public string income { get; set; }
    }
    #endregion

    public enum NDIDServiceId
    {
        [StringValue("001.cust_info_001")]
        IdentityInfo,       // ทั้งหมด

        [StringValue("001.basic_cust_info_001")]
        IdentityInfoBasic,  // 1. ชื่อ- นามสกุล
                            // 2. วัน เดือน ปีเกิด
                            // 3. เลขประจำตัวประชาชน/เลขหนังสือเดินทาง
                            // 4. ที่อยู่ตามเอกสารสำคัญ
                            // 5. เพศ
                            // 6. สถานะ
                            // 7. สัญชาติ

        [StringValue("001.contact_cust_info_001")]
        IdentityContact,    // 8. ที่อยู่ที่ติดต่อได้ในปัจจุบัน
                            // 9. เบอร์โทรที่ติดต่อได้

        [StringValue("001.office_cust_info_001")]
        IdentityOffice,     // 10. อาชีพ
                            // 11. ชื่อสถานทที่ทำางาน
                            // 12. ที่อยู่ที่ทำงาน

        [StringValue("001.income_cust_info_001")]
        IdentityIncome,     // 13. รายได้

        [StringValue("001.biodata_cust_info_001")]
        IdentityBio,        // 14. รูปภาพลูกค้าที่ผ่านการทำ BiometricComparison กับรูปใน chip

        [StringValue("001.createddate_cust_info_001")]
        IdentityCreateDate, // 15. วันที่สร้างข้อมูลลูกค้า
    }

    #endregion

    #region[RP CallBack]

    public class NDIDCallBackData
    {
        public string type { get; set; }

        public string request_id { get; set; }


        // only type : create_request_result 
        public string reference_id { get; set; }

        public bool? success { get; set; }

        public NDIDCreateRequestResultError error { get; set; }


        // only type : request_status 
        public NDIDRequestStatusMode mode { get; set; }

        [Description("NDIDRequestStatus")]
        public string status { get; set; }

        public int? min_idp { get; set; }

        public int? answered_idp_count { get; set; }

        public bool? closed { get; set; }

        public bool? timed_out { get; set; }

        public List<NDIDRequestStatusServiceList> service_list { get; set; }

        public List<NDIDRequestStatusResponseValidList> response_valid_list { get; set; }

        public string block_height { get; set; }


        // for display only
        public bool IsResponse
        {
            get
            {
                return type == NDIDCallBackType.RequestStatus.GetEnumStringValue();
            }
        }
    }

    public class NDIDCreateRequestResultError
    {
        public int? code { get; set; }

        public string message { get; set; }
    }

    public class NDIDRequestStatusServiceList
    {
        public string service_id { get; set; }

        public int? min_as { get; set; }

        public int? signed_data_count { get; set; }

        public int? received_data_count { get; set; }
    }

    public class NDIDRequestStatusResponseValidList
    {
        public string idp_id { get; set; }

        public bool? valid_signature { get; set; }

        public bool? valid_proof { get; set; }

        public bool? valid_ial { get; set; }
    }

    public enum NDIDCallBackType
    {
        [StringValue("create_request_result")]
        CreateRequestResult,

        [StringValue("request_status")]
        RequestStatus,
    }

    public enum NDIDRequestStatus
    {
        [StringValue("pending")]
        Pending,

        [StringValue("confirmed")]
        Confirmed,

        [StringValue("rejected")]
        Rejected,

        [StringValue("completed")]
        Completed,

        [StringValue("complicated")]
        Complicated
    }

    public enum NDIDRequestStatusMode
    {
        // TODO: แต่ละ mode คืออะไร
        Normal = 1,

        High = 3
    }
    #endregion

    #region[Utility]
    public class NDIDCheckRequest
    {
        public string request_id { get; set; }

        public int? min_idp { get; set; }

        public float? min_aal { get; set; }

        public float? min_ial { get; set; }

        public int? request_timeout { get; set; }

        public string request_message_hash { get; set; }

        public List<NDIDCheckRequestData> data_request_list { get; set; }

        public List<NDIDCheckRequestResponse> response_list { get; set; }

        public bool? closed { get; set; }

        public bool? timed_out { get; set; }

        public int? mode { get; set; }

        public string requester_node_id { get; set; }

        public string status { get; set; }
    }

    public class NDIDCheckRequestData
    {
        public string service_id { get; set; }

        public List<string> as_id_list { get; set; }

        public int? min_as { get; set; }

        public string request_params_hash { get; set; }

        public List<string> answered_as_id_list { get; set; }

        public List<string> received_data_from_list { get; set; }
    }

    public class NDIDCheckRequestResponse
    {
        public float? ial { get; set; }

        public float? aal { get; set; }

        public string status { get; set; }

        public string signature { get; set; }

        public string idp_id { get; set; }

        public string identity_proof { get; set; }

        public string private_proof_hash { get; set; }

        public bool? valid_signature { get; set; }

        public bool? valid_proof { get; set; }

        public bool? valid_ial { get; set; }
    }
    #endregion

    #region [Transaction]
    public enum NDIDTransactionType
    {
        RPRequest,
        RPGetData,
        RPCallback,
        RPCheckRequestStatus
    }

    #endregion

    
}
