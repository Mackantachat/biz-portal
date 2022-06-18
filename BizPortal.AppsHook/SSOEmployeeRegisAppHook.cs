using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;

namespace BizPortal.AppsHook
{
    public class SSOEmployeeRegisAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            // คืนค่าธรรมเนียม
            return null; // Return null ระบบจะแสดงค่าธรรมเนียมเหมือนหน้า permit summary 
        }
        public override decimal CalculateEMSFee(List<ISectionData> sectionData)
        {
            return 0; // ไม่มีค่าส่ง ems
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;

            if (stage == AppsStage.UserCreate)
            {
                request.NoDocument = true; // ไม่มีการรับใบอนุญาต
            }
            
            return result;
        }
    }
}
