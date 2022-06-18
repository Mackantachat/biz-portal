using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.TFACStandard
{
    public class Guarantee
    {
        public int Guarantee_Type_ID { get; set; }
        public string Description { get; set; }     
        public string Bank_Year { get; set; }
        public string Bank_ID { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string Bank_Account_ID { get; set; }
        public string Bank_Account_Name { get; set; }
        public string Bond_Of { get; set; }
        public string Bond_No { get; set; }
        public string Bond_Date { get; set; }
        public string Bond_Due_Date { get; set; }
        public string Amount { get; set; }
        public string Year_Number { get; set; }
        public string Month_Number { get; set; }

    }

    public enum GuaranteeType
    {
        [StringValue("DEPOSIT")] // บัญชีเงินฝากประจำ
        DEPOSIT = 1,
        [StringValue("DEPOSIT_CARD")] // บัตรเงินฝาก
        DEPOSIT_CARD = 2,
        [StringValue("THAI_BONDS")] // พันธบัตรรัฐบาลไทย
        THAI_BONDS = 3,
        [StringValue("CORPARATE_BONDS")] // พันธบัตรองค์กรหรือรัฐวิสาหกิจ
        CORPARATE_BONDS = 4,
        [StringValue("POLICY")] // กรมธรรม์ประกันภัย
        POLICY = 5,
    }
}
