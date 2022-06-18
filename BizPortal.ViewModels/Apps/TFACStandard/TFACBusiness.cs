using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.TFACStandard
{

    public class TFACDataService
    {
        public Data  data { get; set; }
    }
    public class Data
    {
        public string ApplicationRequest_ID { get; set; }
        public string Registration_No { get; set; }
        public string Tax_ID { get; set; }
        public string Business_Type_ID { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string Capital { get; set; }
        public string Registration_Date { get; set; }
        public string DBD_Registration_Date { get; set; }
        public string Corporate_Service_Type_ID { get; set; }
        //public string Revenue_Year { get; set; }
        public string Fiscal_Year_End_Date { get; set; }
      //  public string Accounting_Revenue { get; set; }
     //   public string Auditing_Revenue { get; set; }
        //public decimal Other_Revenue { get; set; }
        public decimal Total_Revenue { get; set; }
        public string Rate_Type { get; set; }
        public string Request_Date { get; set; }
        public string Request_Date_type { get; set; }
        public string Request_Date_Type_Date { get; set; }
       // public string Objective_Registraion_Date { get; set; }
        public string Document_Receive_Date { get; set; }
        public int Membership_Period { get; set; }
        public string Request_Form_ID { get; set; }
        public decimal? Fee { get; set; }
        public string Is_First_Time_Guarantee { get; set; }
        public string Is_Audited_Financial_Report { get; set; }

        public int Accounting_Customer_Amount { get; set; }
        public decimal Accounting_Customer_Income { get; set; }
        public int Accounting_Customer_None_Amount { get; set; }
        public decimal Accounting_Customer_None_Income { get; set; }
        public decimal Accounting_Total_Income { get; set; }
        public int Auditoring_Customer_Amount { get; set; }
        public decimal Auditoring_Customer_Income { get; set; }
        public int Auditoring_Customer_None_Amount { get; set; }
        public decimal Auditoring_Customer_None_Income { get; set; }
        public decimal Auditoring_Total_Income { get; set; }

        public List<Address> Address { get;set;}
        public List<Person> Persons { get; set; }
        public List<Guarantee> Guarantee { get; set; }
        public List<Attach> Attachments { get; set; }
    }

    public class Attach : BaseFileMetaData
    {
        public string description { get; set; }
        public string fileType { get; set; }
        public string seqNo { get; set; }
       // public string fileId { get; set; }
    }

    public class TFCAddAttach
    {
        //public string bizReqNo { get; set; }
        //public string reqNo { get; set; }
        //public string bizReqDateTime { get; set; }
        //public string commerceNo { get; set; }
        //public string registerNo { get; set; }
        public string ApplicationRequest_ID { get; set; }
        public string remark { get; set; }
        public List<Attach> attachs { get; set; }
    }

    public class TFACDataGetRequest
    {
        [JsonProperty(PropertyName = "data")]
        public data data { get; set; }


    }

    public class data
    {
        public string Registration_No { get; set; }
        
    }



    public class TFACDataResponse
    {

        [JsonProperty(PropertyName = "data")]
        public  ResultData ResultData { get; set; }
    }

    public class ResultData
    {
           public bool IsSuccess { get; set; }
           public string Msg { get; set; }
           public Data data { get; set; }
    }


}
