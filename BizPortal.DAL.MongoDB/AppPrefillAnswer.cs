using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace BizPortal.DAL.MongoDB
{
    public class AppPrefillAnswer : Entity
    {
        public static void Init()
        {
            var db = MongoFactory.GetAppPrefillAnswerCollection();
            if (db.AsQueryable().Count() == 0)
            {
                AppPrefillAnswer[] items = new AppPrefillAnswer[]
                {
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_FOOD_INFORMATION__APP_TYPE",
                                ControlPrefill = "SELL_FOOD_INFORMATION__APP_TYPE_OPTION__CERTIFICATE"
                            },
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_FOOD_INFORMATION__LICENSE_TYPE",
                                ControlPrefill = "SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION__CERTIFICATE"
                            },
                        }
                    },
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_FOOD_INFORMATION__APP_TYPE",
                                ControlPrefill = "SELL_FOOD_INFORMATION__APP_TYPE_OPTION__LICENSE"
                            },
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_FOOD_INFORMATION__LICENSE_TYPE",
                                ControlPrefill = "SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION__LICENSE"
                            },
                        }
                    },

                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200_CANCEL,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "APP_SELL_FOOD_CANCEL_TYPE",
                                ControlPrefill = "APP_SELL_FOOD_CANCEL_TYPE_OPTION__CERTIFICATE"
                            },
                        }
                    },
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200_CANCEL,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "APP_SELL_FOOD_CANCEL_TYPE",
                                ControlPrefill = "APP_SELL_FOOD_CANCEL_TYPE_OPTION__LICENSE"
                            },
                        }
                    },

                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "APP_SELL_FOOD_EDIT_SECTION_TYPE",
                                ControlPrefill = "APP_SELL_FOOD_EDIT_SECTION_TYPE_OPTION__CERTIFICATE"
                            },
                        }
                    },
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "APP_SELL_FOOD_EDIT_SECTION_TYPE",
                                ControlPrefill = "APP_SELL_FOOD_EDIT_SECTION_TYPE_OPTION__LICENSE"
                            },
                        }
                    },


                    //new AppPrefillAnswer
                    //{
                    //    AppSystemName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE,
                    //    Prefill = new ControlWithAnswer[]
                    //    {
                    //        new ControlWithAnswer
                    //        {
                    //            SectionName = "BBH_SERVICE_INFOMATION",
                    //            ControlName = "BUSINESS_BAD_HEALTH_RANK",
                    //            ControlPrefill = "5.9.7"
                    //        },
                    //        new ControlWithAnswer
                    //        {
                    //            SectionName = "BBH_SERVICE_INFOMATION",
                    //            ControlName = "BUSINESS_TYPE_SERVICE",
                    //            ControlPrefill = "การจัดให้มีการแสดงดนตรี เต้นรํา รําวง รองเง็ง ดิสโก้เทค คาราโอเกะ หรือการแสดงอื่น ๆ ในทํานองเดียวกัน"
                    //        },
                    //    },
                    //},
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_ANIMAL,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_ANIMAL_REQUEST_AT",
                                ControlPrefill = "สำนักงานปศุสัตว์พื้นที่กรุงเทพมหานคร"
                            },
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_ANIMAL_TYPE",
                                ControlPrefill = "สัตว์"
                            },
                        }
                    },
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_CARCASS,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_CARCASS_REQUEST_AT",
                                ControlPrefill = "สำนักงานปศุสัตว์พื้นที่กรุงเทพมหานคร"
                            },
                            new ControlWithAnswer
                            {
                                ControlName = "SELL_CARCASS_TYPE",
                                ControlPrefill = "ซากสัตว์"
                            },
                        }
                    },
                    new AppPrefillAnswer
                    {
                        AppSystemName = AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD,
                        Prefill = new ControlWithAnswer[]
                        {
                            new ControlWithAnswer
                            {
                                ControlName = "GENERAL_INFORMATION__CITIZEN_ID_TYPE",
                                ControlPrefill = "บัตรประชาชน" // idcard
                            },
                        }
                    },
                    
                };
                var list = new List<AppPrefillAnswer>(items);

                db.InsertMany(list.ToArray());
            }
        }
        
        public string AppSystemName { get; set; }
        public ControlWithAnswer[] Prefill { get; set; }
        public class ControlWithAnswer
        {
            public string SectionName { get; set; }
            public string ControlName { get; set; }
            public string ControlPrefill { get; set; }
        }
    }

}
