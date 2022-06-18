using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TOURISM_BUSINESS
{
    public partial class APP_TOURISM_BUSINESS_APP_TOURISM_BUSINESS_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_PLAN_TOURISM()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {

                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Local",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Domestic",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Inbound",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Outbound",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_COUNTRY()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Inbound",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                        ControlAnswer = "Outbound",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_TOURISM_BUSINESS_INFO_SECTION_BANK()
        {
            return new Select2Opt[]
            {
                new Select2Opt() { ID = "1STCHOICE", Text = "เฟิร์สช้อยส์" },
                new Select2Opt() { ID = "AGRI", Text = "เพื่อการเกษตรและสหกรณ์การเกษตร" },
                new Select2Opt() { ID = "AMEX", Text = "อเมริกัน เอ็กซ์เพรส (ไทย) จำกัด" },
                new Select2Opt() { ID = "AMRO", Text = "เดอะรอยัลแบงก์อ๊อฟสกอตแลนด์ พีแอลซี" },
                new Select2Opt() { ID = "BA", Text = "แห่งอเมริกาเนชั่นแนลแอสโซซิเอชั่น" },
                new Select2Opt() { ID = "BAY", Text = "กรุงศรีอยุธยา จำกัด (มหาชน)" },
                new Select2Opt() { ID = "BBC", Text = "กรุงเทพฯ พาณิชย์การ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "BBL", Text = "กรุงเทพ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "BIGC", Text = "บิ๊กซี" },
                new Select2Opt() { ID = "BMB", Text = "ศรีนคร" },
                new Select2Opt() { ID = "BNP", Text = "บีเอ็นพี พารีบาส์" },
                new Select2Opt() { ID = "BOA", Text = "เอเชีย" },
                new Select2Opt() { ID = "BOC", Text = "แห่งประเทศจีน (ไทย) จำกัด (มหาชน)" },
                new Select2Opt() { ID = "BOT", Text = "ธนาคารแห่งประเทศไทย" },
                new Select2Opt() { ID = "BT", Text = "ไทยธนาคาร จำกัด (มหาชน)" },
                new Select2Opt() { ID = "CALYON", Text = "เครดิต อะกริกอล คอร์ปอเรทแอนด์อินเวสเมนท์แบงก์" },
                new Select2Opt() { ID = "CCC", Text = "เซ็นทรัล เครดิตคาร์ด" },
                new Select2Opt() { ID = "CIMBT", Text = "ซีไอเอ็มบี ไทย จำกัด (มหาชน)" },
                new Select2Opt() { ID = "CITI", Text = "ซิตี้แบงก์" },
                new Select2Opt() { ID = "COOP", Text = "สหกรณ์" },
                new Select2Opt() { ID = "CPAY", Text = "Cental Payment" },
                new Select2Opt() { ID = "CTS", Text = "เคาท์เตอร์ เซอร์วิส" },
                new Select2Opt() { ID = "DBS", Text = "ดีบีเอส ไทยทนุ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "DEUTSCHE", Text = "ดอยซ์แบงก์" },
                new Select2Opt() { ID = "EXIM", Text = "เพื่อการส่งออกและนำเข้าแห่งประเทศไทย" },
                new Select2Opt() { ID = "FBCB", Text = "มหานคร" },
                new Select2Opt() { ID = "GE", Text = "จีอี มันนี่ เพื่อรายย่อย" },
                new Select2Opt() { ID = "GHB", Text = "อาคารสงเคราะห์" },
                new Select2Opt() { ID = "GSB", Text = "ออมสิน" },
                new Select2Opt() { ID = "HSBC", Text = "ฮ่องกงและเซี่ยงไฮ้แบงกิ้งคอร์ปอเรชั่น จำกัด" },
                new Select2Opt() { ID = "ICBCT", Text = "ไอซีบีซี (ไทย) จำกัด (มหาชน)" },
                new Select2Opt() { ID = "IOB", Text = "อินเดียนโอเวอร์ซีส์" },
                new Select2Opt() { ID = "ISBT", Text = "อิสลามแห่งประเทศไทย" },
                new Select2Opt() { ID = "JPMC", Text = "เจพีมอร์แกน เชส" },
                new Select2Opt() { ID = "KBANK", Text = "กสิกรไทย จำกัด (มหาชน)" },
                new Select2Opt() { ID = "KK", Text = "เกียรตินาคิน จำกัด (มหาชน)" },
                new Select2Opt() { ID = "KTB", Text = "กรุงไทย จำกัด (มหาชน)" },
                new Select2Opt() { ID = "LHBANK", Text = "แลนด์ แอนด์ เฮ้าส์ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "MEGAICBC", Text = "เมกะ สากลพาณิชย์ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "MHCB", Text = "มิซูโฮ จำกัด สาขากรุงเทพฯ" },
                new Select2Opt() { ID = "NTB", Text = "นครธน" },
                new Select2Opt() { ID = "OCBC", Text = "โอเวอร์ซี-ไชนีสแบงกิ้งคอร์ปอเรชั่น จำกัด" },
                new Select2Opt() { ID = "OTH", Text = "อื่น ๆ" },
                new Select2Opt() { ID = "RHB", Text = "อาร์ เอช บี จำกัด" },
                new Select2Opt() { ID = "SCB", Text = "ไทยพาณิชย์ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "SCBT", Text = "สแตนดาร์ดชาร์เตอร์ด (ไทย) จำกัด (มหาชน)" },
                new Select2Opt() { ID = "SCIB", Text = "นครหลวงไทย" },
                new Select2Opt() { ID = "SEIC", Text = "บริษัท อาคเนย์ ประกันภัย จำกัด" },
                new Select2Opt() { ID = "SMBC", Text = "ซูมิโตโม มิตซุย แบงกิ้ง คอร์ปอเรชั่น" },
                new Select2Opt() { ID = "SMEBANK", Text = "พัฒนาวิสาหกิจขนาดกลางและขนาดย่อมแห่งประเทศไทย" },
                new Select2Opt() { ID = "TBANK", Text = "ธนชาต จำกัด (มหาชน)" },
                new Select2Opt() { ID = "TCRBANK", Text = "ไทยเครดิต เพื่อรายย่อย จำกัด (มหาชน)" },
                new Select2Opt() { ID = "TES", Text = "เทสโก้ คาร์ด เซอร์วิสเซส จำกัด" },
                new Select2Opt() { ID = "TISCO", Text = "ทิสโก้ จำกัด (มหาชน)" },
                new Select2Opt() { ID = "TMB", Text = "ทหารไทย จำกัด (มหาชน)" },
                new Select2Opt() { ID = "TRUE", Text = "True Money" },
                new Select2Opt() { ID = "UB", Text = "สหธนาคาร" },
                new Select2Opt() { ID = "UOBT", Text = "ยูโอบี จำกัด (มหาชน)" },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_BANK()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TOURISM_BUSINESS_INFO_SECTION_GUARANTEE",
                        ControlAnswer = "BA",
                    },
                },
            };
        }
    }
}
