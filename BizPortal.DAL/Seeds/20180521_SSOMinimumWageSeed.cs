using BizPortal.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BizPortal.DAL.Seeds
{
    class _20180521_SSOMinimumWageSeed
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context)
        {
            List<SSOMinimumWage> wages = new List<SSOMinimumWage>()
            {
                new SSOMinimumWage() { ProvinceCode = "10", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "11", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "12", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "13", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "14", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "15", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "16", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "17", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "18", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "19", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "20", MinimumWage =  330},
                new SSOMinimumWage() { ProvinceCode = "21", MinimumWage =  330},
                new SSOMinimumWage() { ProvinceCode = "22", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "23", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "24", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "25", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "26", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "27", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "30", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "31", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "32", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "33", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "34", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "35", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "36", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "37", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "38", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "39", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "40", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "41", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "42", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "43", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "44", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "45", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "46", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "47", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "48", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "49", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "50", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "51", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "52", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "53", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "54", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "55", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "56", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "57", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "58", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "60", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "61", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "62", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "63", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "64", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "65", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "66", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "67", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "70", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "71", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "72", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "73", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "74", MinimumWage =  325},
                new SSOMinimumWage() { ProvinceCode = "75", MinimumWage =  318},
                new SSOMinimumWage() { ProvinceCode = "76", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "77", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "80", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "81", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "82", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "83", MinimumWage =  330},
                new SSOMinimumWage() { ProvinceCode = "84", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "85", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "86", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "90", MinimumWage =  320},
                new SSOMinimumWage() { ProvinceCode = "91", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "92", MinimumWage =  310},
                new SSOMinimumWage() { ProvinceCode = "93", MinimumWage =  315},
                new SSOMinimumWage() { ProvinceCode = "94", MinimumWage =  308},
                new SSOMinimumWage() { ProvinceCode = "95", MinimumWage =  308},
                new SSOMinimumWage() { ProvinceCode = "96", MinimumWage =  308}
            };
            //context.SSOMinimumWages.AddOrUpdate(o => o.ProvinceCode, wages.ToArray());
            //context.SaveChanges(false);
        }
    }
}
