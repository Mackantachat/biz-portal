using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class BusinessSecuredViewModel
    {
        public string AssetsType { get; set; }//ประเภทหลักประกัน
        public List<CreditorsBusinessSecured> CreditorsList { get; set; }//ผู้รับหลักประกัน
        public string PromiseNO { get; set; }//เลขทะเบียนสัญญา
        public List<OwnersBusinessSecured> OwnerList { get; set; }//เจ้าของหลักประกัน
        public string RegisterDate { get; set; }//ประเภทหลักประกัน
        public string Organization { get; set; }//หน่วยงานเจ้าของเรื่อง
        public string AssetsStatus { get; set; }//สถานะหลักประกัน
        public string RegistrationName { get; set; }//ชื่อเลขทะเบียน
        public string SearchNo { get; set; }// เลขที่ใช้ค้นหา
        public string SearchName { get; set; }//ชื่อเลขที่ใช้ค้นหา
    }
    public class CreditorsBusinessSecured
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class OwnersBusinessSecured
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class SearchBusinessSecured
    {
        public string typeSearch { get; set; }      //ประเภทการค้นหา
        public string ownerType { get; set; }        //ประเภทผู้ขอหลักประกัน P บุคคล C นิติบุคคล
        public string ownerIdCard { get; set; }      // รหัสบัตรประชาชนผู้ขอหลักประกัน  
        public string ownerName { get; set; }        //ชื่อผู้ขอหลักประกัน  
        public string ownerSurname { get; set; }     //นามสกุลผู้ขอหลักประกัน  
        public string ownerJuristicId { get; set; }  //เลขนิติบุคคลของผู้ขอหลักประกัน
        public string ownerJuristicName { get; set; } //ชื่อนิติบุคคลของผู้ขอหลักประกัน
        public string assetType { get; set; }         //ประเภททรัพย์
        public string carPlateNo { get; set; }         // เลขทะเบียนรถ
        public string engineNo { get; set; }       // เลขเครื่อง
        public string frameNo { get; set; }        // เลขตัวถัง
        public string machineNo { get; set; }      // เลขเครื่องจักร
        public string shipPlateNo { get; set; }    // เลขทะเบียนเรือ
        public string identityTicket { get; set; } // ตั๋วรูปพรรณ
        public string beastOfBurdenTypeId { get; set; } //ประเภทสัตว์พาหนะ 1 = ช้าง, 2 = ม้า, 3 = โค, 4 = กระบือ, 5 = ล่อ, 6 = ลา
        public string provinceID { get; set; } // รหัสจังหวัด

    }




    public class DIWDetail
    {
        public string BookNo { get; set; }
        public string MachineNo { get; set; }
        public string MortgageDate { get; set; }
        public string Mortgagee { get; set; }
        public string Obligation { get; set; }

    }

    public class DIWhBusiness
    {     
        public List<DIWDetail> ListMachine { get; set; }
        public string OwnerShip { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnStatus { get; set; }

    }

    public class DOPADetail
    {
        public string StatusCancel { get; set; }
        public string AnimalType { get; set; }
        public string DocDate { get; set; }
        public string DocTime { get; set; }
        public string RegisterNo { get; set; }
    }

    public class MDDetail
    {
        public string SHIPSERIAL { get; set; }
        public string SHIPCODE { get; set; }
        public string THANAME { get; set; }
        public string ENGNAME { get; set; }
        public string SHIPTYPE { get; set; }
        public string USETYPENAME { get; set; }
        public string SHIPSTATUS { get; set; }
        public string LOCREG { get; set; }
        public string REGLOC { get; set; }
        public string TGROSS { get; set; }
        public string TNET { get; set; }
        public string WIDTH { get; set; }
        public string SHIP_LENGTH { get; set; }
        public string SHIP_LENGTH1 { get; set; }
        public string DEPTH { get; set; }
        public List<MDShipOwner> SHIPOWNER { get; set; }
        public List<MDPromise> PROMISE { get; set; }
        
    }
    public class MDShipOwner
    {
        public string IDCARD { get; set; }
        public string CITIZEN { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
    } 
    public class MDPromise
    {
        public string PROMISE_NO { get; set; }
    }

}
