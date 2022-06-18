using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DBDStandard
{
    public class CommerceDataService
    {
        public string bizReqNo { get; set; }
        public string bizReqDateTime { get; set; }
        public string reqNo { get; set; }
        public string changeDesc { get; set; }
        public string  changeDate { get; set; }
        public CommerceRegistInfo commerceRegistInfo { get; set; }

       
    }
    public class CommerceAddAttach
    {
        public string bizReqNo { get; set; }
        public string reqNo { get; set; }
        public string bizReqDateTime { get; set; }
        public string commerceNo { get; set; }
        public string registerNo { get; set; }
        public string remark { get; set; }
        public List<Attach> attachs { get; set; }
    }
    public class CommerceRegistInfo
    {
        public string commerceNo { get; set; }
        public string registerNo { get; set; }
        public string ownerType { get; set; }
        public string officeCode { get; set; }
        public Owner owner { get; set; }
        public string commerceNameTH { get; set; }
        public string commerceNameEN { get; set; }
        public string startDate { get; set; }
        public string registerDate { get; set; }
        public double? budgetAmt { get; set; }
        public int commerceStatus { get; set; }
        public string closeDate { get; set; }
        public List<Objective> objectives { get; set; }
        public List<Manager> managers { get; set; }
        public List<Partner> partners { get; set; }
        public HeadOffice headOffice { get; set; }
        public List<Branch> branchs { get; set; }
        public List<Warehouse> warehouses { get; set; }
        public List<Agent> agents { get; set; }
        public List<Shareholder> shareholders { get; set; }
        public Transfer transfer { get; set; }
        public List<Attach> attachs { get; set; }
        public string isElectronic { get; set; }
        public ECommerceWebsite webSite { get; set; }
        public string registrarCode { get; set; }
        public string registrarTitle { get; set; }
        public string registrarFirstname { get; set; }
        public string registrarLastname { get; set; }
        public string reasonCloseCode { get; set; }
        public string reasonClose { get; set; }
        public string delistDate { get; set; }
        public string otherInfo { get; set; }
        public string remark { get; set; }
    }

    public class Shareholder
    {
        public int seqNo { get; set; }
        public string nationality { get; set; }
        public string nationCode { get; set; }
        public string shareAmt { get; set; }
        public string shareValue { get; set; }
    }

    public class Transfer : BaseAddress
    {
        public string refCommerce { get; set; }
        public string refCommerceNo { get; set; } // for cancel
        public string refRegisterNo { get; set; }
        public string transfererIdentityID { get; set; }
        public string transfererTitle { get; set; }
        public string transfererBirthDate { get; set; }
        public string transfererAge { get; set; }
        public string transfererNation { get; set; }
        public string transfererFirstName { get; set; }
        public string transfererLastName { get; set; }
        public string oldCommerceName { get; set; }
        public string transferDate { get; set; }
        public string transferRece { get; set; }
        public string transferReason { get; set; }
        public string trnPid { get; set; }
        public string firstnameTH { get; set; }
        public string titleCode { get; set; }
        
    }

    public class Attach : BaseFileMetaData
    {
        public string seqNo { get; set; }
        public string description { get; set; }
        public string fileType { get; set; }
        public string fileId { get; set; }
    }

    public class ECommerceWebsite
    {
        public string websiteURL { get; set; }
        public List<TypeOfBussiness> typeOfBussiness { get; set; }
        public string otherDescription { get; set; }
        public double webBudget { get; set; }
        public string Telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string urlChannel { get; set; }
        
        public List<OrderMethod> orderMethods { get; set; }
        public List<PaymentMethod> paymentMethods { get; set; }
        public List<DeliveryMethod> deliveryMethods { get; set; }
    }

    public class TypeOfBussiness
    {
        public int seqNo { get; set; }
        public string bussinessCode { get; set; }
        public string description { get; set; }
        // for DBD Cancel
        public string typeOfBussinessCode { get; set; }
       
    }

    public class OrderMethod
    {
        public int seqNo { get; set; }
      
        public string description { get; set; }
        // for DBD Cancel
        public string orderMethodCode { get; set; }
    
        
    }

    public class PaymentMethod
    {
        public int seqNo { get; set; }
 
        public string description { get; set; }
        // for DBD Cancel
        public string paymentMethodCode { get; set; }

      
    }

    public class DeliveryMethod
    {
        public int seqNo { get; set; }
       
        public string description { get; set; }
        // for DBD Cancel
       // public string deliveryMethodCode { get; set; }
       
        public string deliverMethodCode { get; set; }
    }

    public class ObjectiveCode
    {
        public int ojectiveCode { get; set; }
        public string description { get; set; }
        public string objectiveType { get; set; }
        public string activeFlag { get; set; }
    }

    public class Objective
    {
        public int seqNo { get; set; }
        public string objectiveCode { get; set; }
        public string description { get; set; }
    }
    public class CommerceDataServiceCancel
    {
        public string reqNo { get; set; }
        public string bizReqNo { get; set; }
        public string bizReqDateTime { get; set; }
        public string commerceNo { get; set; }
        public string registerNo { get; set; }
        public string reasonCloseCode { get; set; }
        public string reasonClose { get; set; }
        public string closeDate { get; set; }
        public List<Attach> attach { get; set; }
    }
}
