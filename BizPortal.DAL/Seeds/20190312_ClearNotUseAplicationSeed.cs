using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;

namespace BizPortal.DAL.Seeds
{
    public class _20190312_ClearNotUseAplicationSeed
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser adminUser)
        {
            //// update applicationId ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตรม.)(12 เป็น 59)
            //var memberService12Updates = context.MemberServices.Where(e => e.ApplicationID == 12).ToList();

            //foreach (var memberService in memberService12Updates)
            //{
            //    memberService.ApplicationID = 59;
            //    memberService.UpdatedDate = DateTime.Now;
            //}
            //context.SaveChanges(false);

            //// update applicationId ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตรม.)(13 เป็น 60)
            //var memberService13Updates = context.MemberServices.Where(e => e.ApplicationID == 13).ToList();

            //foreach (var memberService in memberService12Updates)
            //{
            //    memberService.ApplicationID = 60;
            //    memberService.UpdatedDate = DateTime.Now;
            //}
            //context.SaveChanges(false);

            //// remove application จาก file Bizportal Application_DGA_ClearNotUse.xlsx ที่คุณ yong ส่งมาให้

            //// Streaming: ใบ 13 คือ ขอใบอนุญาตขายสุรา  ยังมีการอ้างอิงในระบบอยู่
            ////var removeAplpicationIds = new List<int>{12, 13, 14, 24, 61, 62, 63, 67, 68, 70, 72, 75, 79, 80, 81, 82, 83, 84, 85, 179, 180, 181, 182, 183, 184, 187, 188, 189, 190, 212, 214, 215, 216, 219, 220, 224, 236, 238, 239, 243, 252 };
            //var removeAplpicationIds = new List<int> { 12, 14, 24, 61, 62, 63, 67, 68, 70, 72, 75, 79, 80, 81, 82, 83, 84, 85, 179, 180, 181, 182, 183, 184, 187, 188, 189, 190, 212, 214, 215, 216, 219, 220, 224, 236, 238, 239, 243, 252 };
            //var applications = context.Applications.Where(e => removeAplpicationIds.Contains(e.ApplicationID)).ToList();
            //var applicationTranslations = context.ApplicationTranslations.Where(e => removeAplpicationIds.Contains(e.ApplicationID)).ToList();
            //var memberServices = context.MemberServices.Where(e => removeAplpicationIds.Contains(e.ApplicationID)).ToList();
            //var removeMemberServiceIds = memberServices.Select(e => e.MemberServiceID).ToList();
            //var memberServiceAreas = context.MemberServiceAreas.Where(e => removeMemberServiceIds.Contains(e.MemberServiceID)).ToList();

            //context.MemberServiceAreas.RemoveRange(memberServiceAreas);
            //context.MemberServices.RemoveRange(memberServices);
            //context.ApplicationTranslations.RemoveRange(applicationTranslations);
            //context.Applications.RemoveRange(applications);
            //context.SaveChanges(false);
        }
    }
}
