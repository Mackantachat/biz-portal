using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Integrated
{
    public class NationalityService
    {
        public static List<Select2Opt> Nationality()
        {
            List<Select2Opt> results = new List<Select2Opt>();


            return results;
        }

        public static string GetNationalityText(string id)
        {
            List<Select2Opt> results = new List<Select2Opt>();
            results = FormSectionRow.optNationality.ToList();

            var item = results.Where(o => o.ID == id).SingleOrDefault();

            return item?.Text;
        }

        public static string GetNationText(string id)
        {
            List<Select2Opt> results = new List<Select2Opt>();
            results = FormSectionRow.optNation.ToList();

            var item = results.Where(o => o.ID == id).SingleOrDefault();

            return item != null ? item.Text : null;

        }

        public static string GetTitleTextThai(string TitleCode)
        {
            string result = null;
            if (!string.IsNullOrEmpty(TitleCode))
            {
                List<Select2Opt> options = new List<Select2Opt>()
                    {
                        new Select2Opt() { ID = "001", Text = "นาย" },
                        new Select2Opt() { ID = "002", Text = "นางสาว" },
                        new Select2Opt() { ID = "003", Text = "นาง" },
                         new Select2Opt() { ID = "800", Text = "ไม่ระบุ" },
                         new Select2Opt() { ID = "801", Text = "บริษัท" },
                        new Select2Opt() { ID = "802", Text = "ห้างหุ้นส่วนสามัญนิติบุคคล" },
                        new Select2Opt() { ID = "803", Text = "ห้างหุ้นส่วนจำกัด" },
                        new Select2Opt() { ID = "804", Text = "ธนาคาร" },
                    };

                var res = options.SingleOrDefault(ex => ex.ID == TitleCode);
                result = (res != null) ? res.Text : null;
            }

            return result;
        }
        public static string GetInvestType(string InvestCode)
        {
            string result = null;
            if (!string.IsNullOrEmpty(InvestCode))
            {
                List<Select2Opt> options = new List<Select2Opt>()
                    {
                        new Select2Opt() { ID = "01", Text = "CASH" },
                        new Select2Opt() { ID = "02", Text = "CHATTEL" },

                    };

                var res = options.SingleOrDefault(ex => ex.ID == InvestCode);
                result = (res != null) ? res.Text : null;
            }

            return result;
        }
        public static string GetPartnerStatus(string Status)
        {
            string result = null;
            if (!string.IsNullOrEmpty(Status))
            {
                List<Select2Opt> options = new List<Select2Opt>()
                    {
                        new Select2Opt() { ID = "1", Text = "อยู่" },
                        new Select2Opt() { ID = "2", Text = "ออก" },
                        new Select2Opt() { ID = "3", Text = "ตาย" },
                    };

                var res = options.SingleOrDefault(ex => ex.ID == Status);
                result = (res != null) ? res.Text : null;
            }

            return result;
        }
        public static string GetTitleTextEng(string TitleCode)
        {
            string result = null;
            if (!string.IsNullOrEmpty(TitleCode))
            {
                List<Select2Opt> options = new List<Select2Opt>()
                {
                    new Select2Opt() { ID = "001", Text = "Mr." },
                    new Select2Opt() { ID = "002", Text = "Mrs." },
                    new Select2Opt() { ID = "003", Text = "Miss" },
                };
                var res = options.SingleOrDefault(ex => ex.ID == TitleCode);
                result = (res != null) ? res.Text : null;
            }
            return result;
        }

    }
}
