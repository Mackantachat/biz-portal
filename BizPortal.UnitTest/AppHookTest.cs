using System;
using BizPortal.AppsHook;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapster;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class AppHookTest
    {
        [TestMethod]
        public void TestOldVat()
        {
            //Guid requestID = Guid.Parse("78be04a3-11aa-4fbb-a93d-b85b4909df26");
            //VATUtilityHookV2 hook = new VATUtilityHookV2();
            //ApplicationRequestEntity request = ApplicationRequestEntity.Get(requestID);
            //ApplicationRequestViewModel model = request.Adapt<ApplicationRequestViewModel>();
            //AppHookInfo info = new AppHookInfo();
            //var result = hook.Invoke(AppsStage.UserCreate, model, info, request);
            //Assert.IsTrue(result.Success);
        }
    }
}
