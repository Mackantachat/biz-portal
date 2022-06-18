//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MongoDB.Driver;
//using BizPortal.Utils.Extensions;
//using BizPortal.ViewModels.SingleForm;

//namespace BizPortal.DAL.MongoDB
//{
//    public class ApplicationStatusOther : Entity
//    {
//        public string ApplicationStatusID { get; set; }
//        public string ApplicationStatusOtherID { get; set; }
//        public string ApplicationStatusOtherText { get; set; }
//        public bool isHasStatusOther { get; set; }

//        public static void Init()
//        {
//            var db = MongoFactory.GetApplicationStatusOtherCollection();
//            if (db.AsQueryable().Count() == 0)
//            {
//                var items = new ApplicationStatusOther[]
//                {
//                    //Step 1
//                    new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.DRAFT.ToString(),
//                        isHasStatusOther = false,
//                        ApplicationStatusOtherID = null,
//                        ApplicationStatusOtherText = null
//                    }
//                    //Step 2
//                    ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.CHECK.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.WAITING_OF_STATUS_CHECK,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.WAITING_OF_STATUS_CHECK
//                    }
//                    ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.CHECK.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.CHECKING_OF_STATUS_CHECK,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.CHECKING_OF_STATUS_CHECK
//                    }
//                    ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.CHECK.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.ADJUST_OF_STATUS_CHECK,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.ADJUST_OF_STATUS_CHECK
//                    }
//                    ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.CHECK.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.REJECT_OF_STATUS_CHECK,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.REJECT_OF_STATUS_CHECK
//                    }
//                    // Step 3
//                    ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PENDING.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.CHECKING_OF_STATUS_PENDING,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.CHECKING_OF_STATUS_PENDING
//                    }
//                     ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PENDING.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.ADJUST_OF_STATUS_PENDING,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.ADJUST_OF_STATUS_PENDING
//                    }
//                      ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PENDING.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.REJECT_OF_STATUS_PENDING,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.REJECT_OF_STATUS_PENDING
//                    }
//                    //Step 4
//                     ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.WAITING_OF_STATUS_PAY_FEE,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.WAITING_OF_STATUS_PAY_FEE
//                    }
//                      ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.REJECT_OF_STATUS_PAY_FEE,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.REJECT_OF_STATUS_PAY_FEE
//                    }
//                    //Step 5
//                     ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.DOING_LICENSE_OF_STATUS_CREATING_LICENSE,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.DOING_LICENSE_OF_STATUS_CREATING_LICENSE
//                    }
//                     ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.COMPLETED_OF_STATUS_CREATING_LICENSE,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.COMPLETED_OF_STATUS_CREATING_LICENSE
//                    }
//                     ,new ApplicationStatusOther
//                    {
//                        ApplicationStatusID = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(),
//                        isHasStatusOther = true,
//                        ApplicationStatusOtherID = ApplicationStatusOtherValueConst.REJECT_OF_STATUS_CREATING_LICENSE,
//                        ApplicationStatusOtherText = ApplicationStatusOtherTextConst.REJECT_OF_STATUS_CREATING_LICENSE
//                    }
//                };
//                db.InsertMany(items);
//            }
//        }
//    }
//}
