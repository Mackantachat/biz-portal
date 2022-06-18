using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.SRATStandard
{
    public interface IApplicant { }

    public class PersonApplicant : Person, IApplicant {
        public string Type { get; set; }
        public PersonApplicant()
        {
            Type = "citizen";
        }
    }

    public class JuristicPersonApplicant : JuristicPerson, IApplicant
    {
        /// <summary>
        /// ผู้รับมอบอำนาจ
        /// </summary>
        public Person DelegatedPerson { get; set; }
        public string Type { get; set; }
        public JuristicPersonApplicant()
        {
            Type = "juristic";
        }
    }

    public class Person
    {
        /// <summary>
        /// หมายเลขสมาชิก
        /// </summary>
        public string MemberNo { get; set; }

        /// <summary>
        /// คำนำหน้า
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// เลขที่ประชาชน
        /// </summary>
        public string CitizenId { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// ที่อยู่ที่ติดต่อได้
        /// </summary>
        public Address ContactAddress { get; set; }
        /// <summary>
        /// ช่องทางการติดต่อ
        /// </summary>
        public Contact[] Contacts { get; set; }

        public string Telephone { get; set; }
        public string Email { get; set; }
    }
    public class JuristicPerson
    {
        public string JuristicId { get; set; }
        /// <summary>
        /// ชื่อนิติบุคคล
        /// </summary>
        public string Name { get; set; }

        public string NameEN { get; set; }
        /// <summary>
        /// เลขที่นิติบุคคล
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// ประเภทนิติบุคคล
        /// "ห้างหุ้นส่วนจำกัด = 1
        /// บริษัทจำกัด = 2
        /// ห้างหุ้นส่วนสามัญ = 3
        /// สมาคม = 4
        /// มูลนิธิ = 5
        /// </summary>
        public string JuristicType { get; set; }
        /// <summary>
        /// วันที่ละทะเบียนนิติบุคคล
        /// </summary>
        public string RegisterDate { get; set; }
        public string RegisterCapital { get; set; }
        public string RegisterCapitalPaid { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public Address Address { get; set; }
        public Contact[] Contacts { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public Committees[] Committees { get; set; }

    }
    public class Committees : Person
    {
        public int Order { get; set; }
        public string CommitteeId { get; set; }
        public bool CanSigned { get; set; }
        public string IsAuthorizedText { get; set; }
        public bool IsLawyer { get; set; }
        public bool IsLawyerText { get; set; }
    }
    public class Contact
    {
        /// <summary>
        /// Telephone = 1,
        /// Mobile = 2,
        /// Email = 3,
        /// Fax = 4,
        /// Facebook = 5,
        /// Line = 6,
        /// Website = 7,
        /// </summary>
        public int ContactType { get; set; }
        /// <summary>
        /// ข้อมูล
        /// </summary>
        public string Detail { get; set; }
    }

    public class Address
    {
        public string AddressNo { get; set; }
        public string VillageNo { get; set; }
        public string VillageName { get; set; }
        public string BuildingName { get; set; }
        public string FloorNo { get; set; }
        public string RoomNo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string GeoCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public class FileMetaData
    {
        /// <summary>
        /// FileName ชื่อไฟล์ที่อัพโหลด
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// FileTypeCode
        /// </summary>
        public string FileTypeCode { get; set; }
        /// <summary>
        /// FileID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// FileSize
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// FileTypeCode ที่แปลเป็นภาษาไทย
        /// </summary>
        public string DocName { get; set; }
        /// <summary>
        /// Extras
        /// </summary>
        public Dictionary<string, object> Extras { get; set; }
        /// <summary>
        /// fileUrl ไฟล์แนบเป็น google drive หรือ DrobBox
        /// </summary>
        public string FileUrl { get; set; }

    }
}
