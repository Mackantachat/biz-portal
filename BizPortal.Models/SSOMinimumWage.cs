using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    /// <summary>
    /// ค่าแรงขั้นต่ำ
    /// </summary>
    public class SSOMinimumWage
    {
        public int SSOMinimumWageID { get; set; }

        [StringLength(2)]
        [Index]
        public string ProvinceCode { get; set; }

        public decimal MinimumWage { get; set; }
    }
}
