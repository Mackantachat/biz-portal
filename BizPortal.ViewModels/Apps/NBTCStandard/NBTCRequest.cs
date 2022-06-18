using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.NBTCStandard
{
    public class NBTCRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Contact
        {
            public string AddressNo { get; set; }
            public string Building { get; set; }
            public string RoomNo { get; set; }
            public string Floor { get; set; }
            public string VilageName { get; set; }
            public string Moo { get; set; }
            public string Soi { get; set; }
            public string Road { get; set; }
            public string SubDistrict { get; set; }
            public string District { get; set; }
            public string Province { get; set; }
            public string Postcode { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
            public string MobilePhone { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime ModifiedDate { get; set; }
        }

        public class OPENIDAUTHEN
        {
            public string eAuthID { get; set; }
            public string eAuthUserName { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string NationalID { get; set; }
            public bool NationalVerification { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public Contact Contact { get; set; }
        }

        public class CITIZENADDRESS
        {
            public string ADDRESS_NUMBER { get; set; }
            public string ADDRESS_MOO { get; set; }
            public string ADDRESS_SOI { get; set; }
            public string ADDRESS_BUILDING { get; set; }
            public string ADDRESS_ROOMNO { get; set; }
            public string ADDRESS_FLOOR { get; set; }
            public string ADDRESS_ROAD { get; set; }
            public string ADDRESS_PROVINCE { get; set; }
            public string ADDRESS_AMPHUR { get; set; }
            public string ADDRESS_TUMBOL { get; set; }
            public string ADDRESS_POSTCODE { get; set; }
            public string ADDRESS_TELEPHONE { get; set; }
            public string ADDRESS_TELEPHONE_EXT { get; set; }
            public string ADDRESS_FAX { get; set; }
            public string ADDRESS_EMAIL { get; set; }
            public string ADDRESS_MOBILE { get; set; }
            public double ADDRESS_LAT { get; set; }
            public double ADDRESS_LNG { get; set; }
        }

        public class CITIZENINFORMATION
        {
            public string CITIZEN_TITLE { get; set; }
            public string CITIZEN_NAME { get; set; }
            public string CITIZEN_LASTNAME { get; set; }
            public string CITIZEN_NATIONALITY { get; set; }
            public string CITIZEN_IDENTITY_ID { get; set; }
            public string CITIZEN_BIRTH_DATE { get; set; }
            public string CITIZEN_GENERAL_EMAIL { get; set; }
            public CITIZENADDRESS CITIZEN_ADDRESS { get; set; }
        }

        public class STOREINFORMATION
        {
            public bool USE_CITIZEN_ADDRESS { get; set; }
            public string STORE_NAME_TH { get; set; }
            public string STORE_NAME_EN { get; set; }
            public int STORE_BUILDING_TYPE { get; set; }
            public int STORE_BUILDING_RENTING_OWNER_TYPE { get; set; }
            public string ADDRESS_NUMBER { get; set; }
            public string ADDRESS_MOO { get; set; }
            public string ADDRESS_SOI { get; set; }
            public string ADDRESS_BUILDING { get; set; }
            public string ADDRESS_ROOMNO { get; set; }
            public string ADDRESS_FLOOR { get; set; }
            public string ADDRESS_ROAD { get; set; }
            public string ADDRESS_PROVINCE { get; set; }
            public string ADDRESS_AMPHUR { get; set; }
            public string ADDRESS_TUMBOL { get; set; }
            public string ADDRESS_POSTCODE { get; set; }
            public string ADDRESS_TELEPHONE { get; set; }
            public string ADDRESS_TELEPHONE_EXT { get; set; }
            public string ADDRESS_FAX { get; set; }
            public string ADDRESS_EMAIL { get; set; }
            public string ADDRESS_MOBILE { get; set; }
            public double ADDRESS_LAT { get; set; }
            public double ADDRESS_LNG { get; set; }
        }

        public class JURISTICADDRESS
        {
            public string ADDRESS_NUMBER { get; set; }
            public string ADDRESS_MOO { get; set; }
            public string ADDRESS_SOI { get; set; }
            public string ADDRESS_BUILDING { get; set; }
            public string ADDRESS_ROOMNO { get; set; }
            public string ADDRESS_FLOOR { get; set; }
            public string ADDRESS_ROAD { get; set; }
            public string ADDRESS_PROVINCE { get; set; }
            public string ADDRESS_AMPHUR { get; set; }
            public string ADDRESS_TUMBOL { get; set; }
            public string ADDRESS_POSTCODE { get; set; }
            public string ADDRESS_TELEPHONE { get; set; }
            public string ADDRESS_TELEPHONE_EXT { get; set; }
            public string ADDRESS_FAX { get; set; }
            public string ADDRESS_EMAIL { get; set; }
            public string ADDRESS_MOBILE { get; set; }
            public double ADDRESS_LAT { get; set; }
            public double ADDRESS_LNG { get; set; }
        }

        public class BOARDOFDIRECTOR
        {
            public string CITIZEN_NAME { get; set; }
            public string CITIZEN_LASTNAME { get; set; }
            public string CITIZEN_NATIONALITY { get; set; }
            public string CITIZEN_IDENTITY_ID { get; set; }
        }

        public class JURISTICGENERALINFORMATION
        {
            public string JURISTIC_NAME_TH { get; set; }
            public string JURISTIC_NAME_EN { get; set; }
            public string REGISTERED_NO { get; set; }
            public string REGISTERED_DATE { get; set; }
            public string JURISTICN_GENERAL_EMAIL { get; set; }
            public JURISTICADDRESS JURISTIC_ADDRESS { get; set; }
            public List<BOARDOFDIRECTOR> BOARD_OF_DIRECTORS { get; set; }
        }
        public class Attachment 
        {
            public string Base64String { get; set; }
            public string ContentType { get; set; }
            public string FileID { get; set; }
            public string FileName { get; set; }
            public string FileType { get; set; }
        }
        public class NbtcRadio
        {
            public List<Attachment> Attachments { get; set; }
            public string BizReqNo { get; set; }
            public string BizReqDateTime { get; set; }
            public string ReqNo { get; set; }
            public int ApplicationID { get; set; }
            public string REF_CASE_ID { get; set; }
            public string Remark { get; set; }
        }
            public class NbtcCitizen
        {
            public string REF_CASE_ID { get; set; }
            public OPENIDAUTHEN OPENID_AUTHEN { get; set; }
            public CITIZENINFORMATION CITIZEN_INFORMATION { get; set; }
            public int REQUESTOR_TYPE { get; set; }
            public STOREINFORMATION STORE_INFORMATION { get; set; }
            public bool STORE_COMMERCE_REGISTRATION { get; set; }
            public int REQUEST_TYPE { get; set; }
            public List<Attachment> Attachments { get; set; }
            public string BizReqNo { get; set; }
            public string BizReqDateTime { get; set; }
            public string ReqNo { get; set; }
            public int ApplicationID { get; set; }
        }
        public class NbtcJuristic
        {
            public string REF_CASE_ID { get; set; }
            public OPENIDAUTHEN OPENID_AUTHEN { get; set; }
            public JURISTICGENERALINFORMATION JURISTIC_GENERAL_INFORMATION { get; set; }
            public int REQUESTOR_TYPE { get; set; }
            public STOREINFORMATION STORE_INFORMATION { get; set; }
            public int REQUEST_TYPE { get; set; }

           public List<Attachment> Attachments { get; set; }
            public string BizReqNo { get; set; }
            public string BizReqDateTime { get; set; }
            public string ReqNo { get; set; }
            public int ApplicationID { get; set; }
        }


       




    }
}
