using BizPortal.ViewModels.Apps.DPTStandard;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class DPTR6Request : DPTRequestBase
    {
        /// <summary>
        /// ประเภทของใบอนุญาตที่ต้องการยื่นขอ
        /// 1 ก่อสร้างอาคาร
        /// 2 ดัดแปลงอาคาร
        /// 3 เคลื่อนย้ายอาคาร
        /// 4 รื้อถอนอาคาร
        /// </summary>
        public int PurposeType { get; set; }
        /// <summary>
        /// ไฟล์บัตรประชาชน (กรณีประชาชน)
        /// </summary>
        public DPTFileMetaData[] CitizenIDCards { get; set; }
        /// <summary>
        /// หนังสือแสดงความเป็นตัวแทนของเจ้าของอาคาร
        /// </summary>
        public DPTFileMetaData[] DelegationDocuments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DPTFileMetaData[] BuildingOwnershipDocuments { get; set; }
        /// <summary>
        /// หนังสือแสดงความเป็นยินยอมจากของเจ้าของอาคาร (กรณีผู้ครอบครองเป็นผู้ยื่น)
        /// </summary>
        //public DPTFileMetaData[] HolderConsentDocuments { get; set; }

        /// <summary>
        /// หนังสือรับรองการจดทะเบียนนิติบุคคล (กรณีนิติบุคคล)
        /// </summary>
        public DPTFileMetaData[] JuristicPersonRegisterationDocuments { get; set; }
        /// <summary>
        /// หนังสือแสดงว่าเป็นผู้จัดการหรือผู้แทนซึ่งเป็นผู้ดำเนินกิจการของนิติบุคคล (กรณีนิติบุคคล)
        /// </summary>
        public DPTFileMetaData[] DelegationRepresentationDocuments { get; set; }
        
        /// <summary>
        /// รายละเอียด สถาปนิกควมคุมงาน
        /// </summary>
        public DPTEADocument[] SiteArchitectDocuments { get; set; }
        
        /// <summary>
        /// รายละเอียด วิศวกรควบคุมงาน
        /// </summary>
        public DPTEADocument[] SiteEngineerDocuments { get; set; }
        /// <summary>
        /// โฉนดที่ดิน
        /// </summary>
        public DPTFileMetaData[] TitleDeedDocuments { get; set; }
        /// <summary>
        /// เอกสารอื่น ๆ
        /// </summary>
        public DPTFileMetaData[] OtherDocuments { get; set; }
        /// <summary>
        /// เอกสารเพิ่มเติม
        /// </summary>
        public DPTFileMetaData[] AdditionalDocuments { get; set; }
        /// <summary>
        /// สำเนาหรือภาพถ่ายใบอนุญาตก่อสร้าง/ดัดแปลง/เคลื่อนย้ายอาคาร
        /// </summary>
        public DPTFileMetaData[] A1LicenseDocuments { get; set; }
        /// <summary>
        /// วันที่ก่อสร้างแล้วเสร็จ
        /// </summary>
        public DateTime ConstructionCompletedDate { get; set; }

        public string A1LicenseNo { get; set; }
        public string A1ReferenceCode { get; set; }
        public DateTime A1LicenseReleasedDate { get; set; }
        public DateTime A1LicenseExpirationDate { get; set; }
        
        public Nullable<DateTime> ConsiderationEndDate { get; set; }
        public string IssueNo { get; set; }
        public Nullable<DateTime> IssueReceivedDate { get; set; }
        public string IssueByMName { get; set; }
        public string IssueByMId { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }

        [JsonConverter(typeof(PersonArrayJsonConverter))]
        public IApplicant[] Owners { get; set; }

        public IApplicant[] Possessors { get; set; }
    }

    public class DPTR6RequestJuristic : DPTR6Request
    {
        public DPTR6RequestJuristic()
        {
            Applicant = new DPTJuristicPersonApplicant();
            //Owners = new DPTPersonApplicant[] { };
        }
    }

    public class DPTR6RequestCitizen : DPTR6Request
    {
        public DPTR6RequestCitizen()
        {
            Applicant = new DPTPersonApplicant();
            //Owners = new DPTPersonApplicant[] { };
        }
    }

    public class PersonJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IApplicant);
        }
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var applicant = default(IApplicant);

            try
            {
                var jsonObject = JObject.Load(reader);
                if (jsonObject["JuristicPersonNo"] != null)
                {
                    applicant = new DPTJuristicPersonApplicant();
                }
                else
                {
                    applicant = new DPTPersonApplicant();
                }

                serializer.Populate(jsonObject.CreateReader(), applicant);
            }
            catch { }

            return applicant;
        }
    }

    public class PersonArrayJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IApplicant[]);
        }
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var result = default(IApplicant[]);

            try
            {
                var jsonArray = JArray.Load(reader);
                result = new IApplicant[jsonArray.Count()];
                int i = 0;
                foreach (JObject jsonObject in jsonArray.Children<JObject>())
                {
                    IApplicant item;
                    if (jsonObject["JuristicPersonNo"] != null)
                    {
                        item = jsonObject.ToObject<DPTJuristicPersonApplicant>();
                    }
                    else
                    {
                        item = jsonObject.ToObject<DPTPersonApplicant>();
                    }
                    result[i] = item;
                    i++;
                }
            }
            catch { }

            return result;
        }
    }
}
