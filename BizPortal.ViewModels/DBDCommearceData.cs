using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class DBDCommearceData
    {
        //public class Id
        //{
        //    public int commerceGenId { get; set; }
        //    public int seqNo { get; set; }
        //}

        public class Agent
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public string agentName { get; set; }
            public string building { get; set; }
            public string houseNo { get; set; }
            //public Id id { get; set; }
            public string moo { get; set; }
            public int orderNo { get; set; }
            public string postCode { get; set; }
            public string road { get; set; }
            public string roomNo { get; set; }
            public string soi { get; set; }
            public string telephone { get; set; }
            public string village { get; set; }
        }

        public class HeadOffice
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public string addressDisplayEN { get; set; }
            public string buildingFloor { get; set; }
            public string buildingTH { get; set; }
            public int commerceGenId { get; set; }
            public string fax { get; set; }
            public string houseNo { get; set; }
            public string moo { get; set; }
            public string postCode { get; set; }
            public string roadTH { get; set; }
            public string roomNo { get; set; }
            public string soiTH { get; set; }
            public string telephone { get; set; }
        }

        //public class Id2
        //{
        //    public int commerceGenId { get; set; }
        //    public int seqNo { get; set; }
        //}

        public class Manager
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public int age { get; set; }
            public string birthdate { get; set; }
            public string firstnameTH { get; set; }
            public string fullname { get; set; }
            public string houseNo { get; set; }
            //public Id2 id { get; set; }
            public string identityID { get; set; }
            public string lastnameEN { get; set; }
            public string lastnameTH { get; set; }
            public int orderNo { get; set; }
            public string postCode { get; set; }
            public string telephone { get; set; }
        }

        //public class Id3
        //{
        //    public int commerceGenId { get; set; }
        //    public int seqNo { get; set; }
        //}

        public class Objective
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string description { get; set; }
            //public Id3 id { get; set; }
            public string objectiveCode { get; set; }
            public string objectiveSubCode { get; set; }
            public int orderNo { get; set; }
        }

        public class Owner
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public int age { get; set; }
            public string birthdate { get; set; }
            public string buildingFloor { get; set; }
            public int commerceGenId { get; set; }
            public string fax { get; set; }
            public string houseNo { get; set; }
            public string moo { get; set; }
            public string postCode { get; set; }
            public string race { get; set; }
            public string road { get; set; }
            public string roomNo { get; set; }
            public string soi { get; set; }
            public string telephone { get; set; }
        }

        public class Transfer
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public string building { get; set; }
            public string buildingFloor { get; set; }
            public int commerceGenId { get; set; }
            public string fax { get; set; }
            public string houseNo { get; set; }
            public string moo { get; set; }
            public string oldCommerceName { get; set; }
            public string postCode { get; set; }
            public string refCommerceNo { get; set; }
            public string refRegisterNo { get; set; }
            public string road { get; set; }
            public string roomNo { get; set; }
            public string soi { get; set; }
            public string telephone { get; set; }
            public string transferDate { get; set; }
            public string transferReason { get; set; }
            public int transfererAge { get; set; }
            public string transfererBirthdate { get; set; }
            public string transfererFirstnameTH { get; set; }
            public string transfererFullname { get; set; }
            public string transfererIdentityID { get; set; }
            public string transfererLastnameTH { get; set; }
            public string transfererNationCode { get; set; }
            public string transfererTitleCode { get; set; }
            public string village { get; set; }
        }

        //public class Id4
        //{
        //    public int commerceGenId { get; set; }
        //    public int seqNo { get; set; }
        //}

        public class Warehouse
        {
            public string createDatetime { get; set; }
            public string createUser { get; set; }
            public string updDatetime { get; set; }
            public string updUser { get; set; }
            public int versionId { get; set; }
            public string addressDisplay { get; set; }
            public string building { get; set; }
            public string buildingFloor { get; set; }
            public string fax { get; set; }
            public string houseNo { get; set; }
            //public Id4 id { get; set; }
            public string moo { get; set; }
            public int orderNo { get; set; }
            public string postCode { get; set; }
            public string road { get; set; }
            public string roomNo { get; set; }
            public string soi { get; set; }
            public string telephone { get; set; }
            public string village { get; set; }
        }
        public class CommerceDetail
        {
            public List<Agent> agents { get; set; }
            public List<object> attachs { get; set; }
            public List<object> branchs { get; set; }
            public string budgetAmt { get; set; }
            public string commerceNameEN { get; set; }
            public string commerceNameTH { get; set; }
            public string commerceNo { get; set; }
            public string commerceStatus { get; set; }
            public HeadOffice headOffice { get; set; }
            public string isElectronic { get; set; }
            public List<Manager> managers { get; set; }
            public string messageCode { get; set; }
            public string messageDesc { get; set; }
            public List<Objective> objectives { get; set; }
            public string officeCode { get; set; }
            public string otherInfo { get; set; }
            public Owner owner { get; set; }
            public List<object> partners { get; set; }
            public string processId { get; set; }
            public string registerDate { get; set; }
            public string registerNo { get; set; }
            public string registrarCode { get; set; }
            public string registrarFirstname { get; set; }
            public string registrarLastname { get; set; }
            public List<object> shareholders { get; set; }
            public string startDate { get; set; }
            public Transfer transfer { get; set; }
            public List<Warehouse> warehouses { get; set; }
        }
    }
}
