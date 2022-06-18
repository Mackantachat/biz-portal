using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.AppsHook;
using BizPortal.ViewModels.V2;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class RenderDataAppHookTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RenderDataAppHook rd = new RenderDataAppHook();
            ApplicationRequestViewModel model = new ApplicationRequestViewModel();
            model.ApplicationID = 16;
            model.ApplicationRequestID = new Guid("919bcde7-1f4b-4bb8-a9c4-66125fcdc1c2");
            model.ApplicationRequestNumber = "J600912001";
            model.ApplicationRequestRunningNumber = 1;
            model.IdentityID = "923560000213";
            model.IdentityType = UserTypeEnum.Juristic;
            model.Status = ApplicationStatusV2Enum.PENDING;

            rd.RenderData(RenderStage.UserTracking, model);
        }
    }
}
