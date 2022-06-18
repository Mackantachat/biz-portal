using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Report.Page1
{
    public class SummaryPermitGroupApiQueryModel
    {
        public string[] Year { get; set; }
        public string[] Quater { get; set; }
        public string[] Month { get; set; }
    }
    public class SummaryPermitByBusinessGroupModel
    {
        public string BusinessID { get; set; }
        public string BusinessNameTH { get; set; }
        public string BusinessNameEN { get; set; }
        public int Year { get; set; }
        public int? DisplayValue { get; set; }
        public int? QtyPermitInThisYear { get; set; }
        public int?[] QtyPermitEachQuaterIfQuery { get; set; }
        public int?[] QtyPermitEachMonthIfQuery { get; set; }
        public string Tooltip { get; set; }
    }

    public class SummaryPermitByMomentGroupModel
    {
        public string ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public int Year { get; set; }
        public int? DisplayValue { get; set; }
        public int? QtyPermitInThisYear { get; set; }
        public int?[] QtyPermitEachQuaterIfQuery { get; set; }
        public int?[] QtyPermitEachMonthIfQuery { get; set; }
        public string Tooltip { get; set; }
    }

    public class SummaryPermitByProvinceGroupModel
    {
        public string ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int Year { get; set; }
        public int? DisplayValue { get; set; }
        public int? QtyPermitInThisYear { get; set; }
        public int?[] QtyPermitEachQuaterIfQuery { get; set; }
        public int?[] QtyPermitEachMonthIfQuery { get; set; }
        public string Tooltip { get; set; }
    }
}
