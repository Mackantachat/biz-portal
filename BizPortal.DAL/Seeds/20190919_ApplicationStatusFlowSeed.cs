using BizPortal.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BizPortal.DAL.Seeds
{
    public class _20190919_ApplicationStatusFlowSeed
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser adminUser)
        {
            var apps = new Dictionary<string, string>
            {
                { "การขอขึ้นทะเบียนนายจ้างและผู้ประกันตน", "CHECK,PENDING,COMPLETED" },
                { "SSO Register Employee", "CHECK,PENDING,COMPLETED" },
                { "แบบฟอร์มขอใช้ไฟฟ้า", "CHECK,PENDING,COMPLETED" },
                { "แบบฟอร์มขอใช้น้ำประปา", "CHECK,PENDING,COMPLETED" },
                { "ขอใช้บริการโทรศัพท์พื้นฐาน และอินเทอร์เน็ต", "CHECK,PENDING,COMPLETED" },
                { "แบบฟอร์มขอใช้ไฟฟ้า(ภูมิภาค)", "CHECK,PENDING,COMPLETED" },
                { "แบบฟอร์มขอจดทะเบียนภาษีมูลค่าเพิ่ม", "CHECK,PENDING,COMPLETED" },
                { "แบบฟอร์มขอใช้น้ำประปา(ภูมิภาค)", "CHECK,PENDING,COMPLETED" }
            };

            var applications = context.Applications.Where(e => apps.Any(o=>o.Key == e.ApplicationSysName) && e.StatusSequence == null).ToList();

            foreach (var application in applications)
            {
                application.StatusSequence = "CHECK,PENDING,COMPLETED";
            }

            context.Applications.AddOrUpdate(e => e.ApplicationID, applications.ToArray());
            context.SaveChanges(false);
        }
    }
}
