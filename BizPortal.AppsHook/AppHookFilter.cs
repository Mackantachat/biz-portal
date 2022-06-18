using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public class AppHookFilter
    {
        private static List<string> disableOrgPdfForms = new List<string>()
            {
                "แก้ไขใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม",
                "ยกเลิกใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม",
                "แก้ไขการยื่นชำระภาษีป้าย",
                "ยกเลิกภาษีป้าย",
                "แก้ไขใบอนุญาตประกอบธุรกิจโรงแรม",
                "ยกเลิกใบอนุญาตประกอบธุรกิจโรงแรม",
                "แก้ไขจดทะเบียนพาณิชย์",
                "ยกเลิกจดทะเบียนพาณิชย์",
            };

        public static bool IsDisableOrgPdfForm(string appSysName)
        {
            if (appSysName == null) appSysName = "";
            return disableOrgPdfForms.Contains(appSysName);
        }
    }
}
