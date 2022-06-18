using System;
using System.Collections.Generic;
using System.Globalization;

namespace BizPortal.ViewModels
{
    public class BillpaymentParamViewModel
    {
        public Guid ApplicationRequestID { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public int PersonType { get; set; }
        public string CitizenNo { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string BusinessNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string HouseNo { get; set; }
        public string BuildingName { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string Tambon { get; set; }
        public string Amphur { get; set; }
        public string Province { get; set; }
        public string TambonCode { get; set; }
        public string AmphurCode { get; set; }
        public string ProvinceCode { get; set; }
        public string PostCode { get; set; }
        public bool CanSendToEmail { get; set; }
        public string RefNo1 { get; set; }
        public string InvoiceCreateDate { get; set; }
        public string InvoiceStartDate { get; set; }
        public string InvoiceEndDate { get; set; }
        public string OrgNameTH { get; set; }
        public string OrgNameEN { get; set; }
        public string OrgPhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<BillpaymentCatalogViewModel> PaymentCatalog { get; set; }
    }

    public class BillpaymentViewModel 
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public List<BillpaymentCatalogViewModel> Catalogs { get; set; }
    }

    public class BillpaymentCatalogViewModel
    {
        public string HomeCostcenterCode { get; set; }
        public string HomeCostcenterName { get; set; }
        public string CatalogCode { get; set; }
        public string CatalogName { get; set; }        
        public decimal Amount { get; set; }
    }

    public class BillpaymentManageViewModel
    {
        public int order { get; set; }
        public string costCenterCode { get; set; }
        public string catalogCode { get; set; }
        public string catalogDesc { get; set; }
        public string invoiceCreateDate { get; set; }
        public string invoiceStartDate { get; set; }
        public string invoiceEndDate { get; set; }
        public decimal amount { get; set; }
        public string refNo1 { get; set; }
        public string refNo2 { get; set; }
        public string refNo3 { get; set; }
        public string citizenNo { get; set; }
        public string titleName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string businessNo { get; set; }
        public string businessName { get; set; }
        public string gDepartmentCode { get; set; }
        public string gCostCenterCode { get; set; }
        public string taxID { get; set; }
        public string personGroupName { get; set; }
        public string tradingParter { get; set; }
        public string houseNo { get; set; }
        public string buildingName { get; set; }
        public string moo { get; set; }
        public string soi { get; set; }
        public string road { get; set; }
        public string tambonCode { get; set; }
        public string amphurCode { get; set; }
        public string provinceCode { get; set; }
        public string postcode { get; set; }
        public string mobileNo { get; set; }
        public string canSendToMobile { get; set; }
        public string email { get; set; }
        public string canSendToEmail { get; set; }
        public string billPaymentGroupingCode { get; set; }
        public int personType { get; set; }
        public string sendDate { get; set; }
        public string dataFlag { get; set; }
    }   

    public class BillpaymentManageResponseViewModel<T>
    {
        public bool returnStatus { get; set; }
        public string returnCode { get; set; }
        public int sendRecordNo { get; set; }
        public decimal? allPaymentAmount { get; set; }
        public List<T> billPaymentReturnData { get; set; }
    }

    public class BillPaymentReturnDataViewModel
    {
        public int order { get; set; }
        public string costCenterCode { get; set; }
        public string businessAreaCode { get; set; }
        public string catalogCode { get; set; }
        public string refNo1 { get; set; }
        public string refNo2 { get; set; }
        public string refNo3 { get; set; }
        public string citizenNo { get; set; }
        public DateTime cendDate { get; set; }
        public string dataFlag { get; set; }
        public string transectionID { get; set; }
        public string billPaymentCode { get; set; }
        public string cgdRef1 { get; set; }
        public string cgdRef2 { get; set; }
        public string cgdRef3 { get; set; }
        public decimal amount { get; set; }
        public DateTime billCreateDate { get; set; }
        public string barcodeString { get; set; }
        public string qrCodeString { get; set; }
        public string billerID { get; set; }
        public string smsSendStatus { get; set; }
        public string emailSendStatus { get; set; }
        public DateTime responseTimeStamp { get; set; }
    }

    public class BillPaymentReturnStatusViewModel
    {
        public string transectionID { get; set; }

        public string costCenterCode { get; set; }

        public string cgdRef1 { get; set; }

        public string cgdRef2 { get; set; }

        public string cgdRef3 { get; set; }

        public string citizenNo { get; set; }

        public string invoiceCode { get; set; }

        public decimal amount { get; set; }

        public DateTime? billCreateDate { get; set; }

        public string responseCode { get; set; }

        public string bankCode { get; set; }

        public DateTime? paymentDate { get; set; }

        public string reconcileDate { get; set; }

        public string sourceID { get; set; }

        public string receiptCode { get; set; }

        public DateTime? receiptCreateDate { get; set; }

        public string responseTimeStamp { get; set; }

        public string etc1Data { get; set; }

        public string etc2Data { get; set; }
    }

    public class BillPaymentUpdateStatus
    {
        public string IdentityID { get; set; }

        public Guid? ApplicationRequestID { get; set; }

        public string Token { get; set; }
    }

}
