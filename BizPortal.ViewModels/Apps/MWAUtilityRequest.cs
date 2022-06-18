using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class MWAUtilityRequest
    {
        public MWAUtilityRequest()
        {
            //Attachments = new List<MWAUtilityFile>();
            requestDetailWSList = new List<requestDetailWSList>();
        }

        public Guid? requestIdBIZ { get; set; }
        public string requestIdWLMAR { get; set; }
        public string requestChannel { get; set; }
        public string branchCode { get; set; }
        public string receiveChannel { get; set; }
        public string requestCode { get; set; }
        public string requestDetail { get; set; }
        public string prefixCode { get; set; }
        public string firstName { get; set; }
        public string houseNo { get; set; }
        public string moo { get; set; }
        public string village { get; set; }
        public string building { get; set; }
        public string soi { get; set; }
        public string road { get; set; }
        public string districtCode { get; set; }
        public string amphurCode { get; set; }
        public string provinceCode { get; set; }
        public string zipCode { get; set; }
        public string createdBy { get; set; }
        public string screenId { get; set; }
        public string ipaddress { get; set; }
        public List<requestDetailWSList> requestDetailWSList { get; set; }
        public string lastName { get; set; }
        public string receiveChannelOther { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string phoneExtenstion { get; set; }
        public string fax { get; set; }
        //public string faxExtension { get; set; }
        public string mobile { get; set; }


        //===========Old==========
        //public string MwaRequestID { get; set; }
        //public string MwaRequestBranch { get; set; }
        //public string MwaRequestDate { get; set; }
        //public string MwaRequestTime { get; set; }
        //public string BizRefferenceNo { get; set; }
        //public string BizDate { get; set; }
        //public string BizTime { get; set; }
        //public string BizBranch { get; set; }
        //public string ContractCardID { get; set; }
        //public string ContractName { get; set; }
        //public string Mobile { get; set; }
        //public string Email { get; set; }
        //public string JuristicName { get; set; }
        //public string ClaimName { get; set; }
        //public string JuristicID { get; set; }
        //public string HouseID { get; set; }
        //public string HouseNo { get; set; }
        //public string HouseMoo { get; set; }
        //public string AddressSoi { get; set; }
        //public string AddressRoad { get; set; }
        //public string AddressDistrictCode { get; set; }
        //public string AddressDistrict { get; set; }
        //public string AddressAmphurCode { get; set; }
        //public string AddressAmphur { get; set; }
        //public string AddressProvinceCode { get; set; }
        //public string AddressProvince { get; set; }
        //public string Postcode { get; set; }
        //public string Latitude { get; set; }
        //public string Longtitude { get; set; }
        //public string RecCardID { get; set; }
        //public string RecName { get; set; }
        //public string RecOperatorCode { get; set; }
        //public string RecHouseNo { get; set; }
        //public string RecHouseMoo { get; set; }
        //public string RecAddressSoi { get; set; }
        //public string RecAddressRoad { get; set; }
        //public string RecAddressDistrictCode { get; set; }
        //public string RecAddressDistrict { get; set; }
        //public string RecAddressAmphurCode { get; set; }
        //public string RecAddressAmphur { get; set; }
        //public string RecAddressProvinceCode { get; set; }
        //public string RecAddressProvince { get; set; }
        //public string RecPostcode { get; set; }
        //public string MwaStatusCode { get; set; }
        //public string BizStatusSent { get; set; }
        //public List<MWAUtilityFile> Attachments { get; set; }
        //public string InsertDate { get; set; }
        //public string InsertSystem { get; set; }
        //public string UpdateDate { get; set; }
        //public string UpdateSystem { get; set; }
        //public int ApplicationID { get; set; }
        //public string CustomerType { get; set; }
        //public string UserReferenceNo { get; set; }
    }

    public class MWAUtilityFile
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64String { get; set; }
        public string DocType { get; set; }
    }
    public class requestDetailWSList
    {
        public string accountCode { get; set; }
        public string useType { get; set; }
        public string accountType { get; set; }
        public string taxId { get; set; }
        public int taxBranch { get; set; }
        public string individualType { get; set; }
        public string cardType { get; set; }
        public string cardId { get; set; }
        public string businessName { get; set; }
        public string prefixCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string houseId { get; set; }
        public string houseNo { get; set; }
        public string moo { get; set; }
        public string village { get; set; }
        public string building { get; set; }
        public string soi { get; set; }
        public string road { get; set; }
        public string districtCode { get; set; }
        public string amphurCode { get; set; }
        public string provinceCode { get; set; }
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        public string phoneExtenstion { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string meterSize { get; set; }
        public string accountClassCode { get; set; }
        public string billTaxId { get; set; }
        public string billPrefixCode { get; set; }
        public string billFirstname { get; set; }
        public string billLastname { get; set; }
        public string billHouseNo { get; set; }
        public string billMoo { get; set; }
        public string billVillage { get; set; }
        public string billBuilding { get; set; }
        public string billSoi { get; set; }
        public string billRoad { get; set; }
        public string billDistrictCode { get; set; }
        public string billAmphurCode { get; set; }
        public string billProvinceCode { get; set; }
        public string billZipCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string paidTaxId { get; set; }
        public int paidTaxBranch { get; set; }
        public string paidTaxName { get; set; }
        public string paidHouseNo { get; set; }
        public string paidMoo { get; set; }
        public string paidVillage { get; set; }
        public string paidBuilding { get; set; }
        public string paidSoi { get; set; }
        public string paidRoad { get; set; }
        public string paidDistrictCode { get; set; }
        public string paidAmphurCode { get; set; }
        public string paidProvinceCode { get; set; }
        public string paidZipCode { get; set; }
        public List<requestAttachmentWSList> requestAttachmentWSList { get; set; }
        public requestDetailWSList()
        {
            requestAttachmentWSList = new List<requestAttachmentWSList>();
        }
    }
    public class requestAttachmentWSList
    {
        public string attachmentCode { get; set; }
        public string attachmentTypeCode { get; set; }
        public string attachmentFile { get; set; }
        public string attachmentDescription { get; set; }
    }
}
