using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.DAL.MongoDB
{
    public class FormSectionGroup : Entity
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            if (db.AsQueryable().Count() == 0)
            {
                FormSectionGroup[] items = new FormSectionGroup[]
                {
                    new FormSectionGroup() { SectionGroup = "APP_VAT_AGREEMENT", Ordering = 1, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_VAT }, NotFormPage = true, IsBeginPage = true },
                    new FormSectionGroup() { SectionGroup = "INFORMATION", Ordering = 3 },
                    new FormSectionGroup() { SectionGroup = "APP_SSO", Ordering = 4, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_SSO } }, //, "SSO Register Employee"
                    new FormSectionGroup() { SectionGroup = "APP_VAT", Ordering = 5, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_VAT } },
                    new FormSectionGroup() { SectionGroup = "APP_MEA", Ordering = 6, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_MEA } },
                    new FormSectionGroup() { SectionGroup = "APP_MWA", Ordering = 7, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_MWA } },
                    new FormSectionGroup() { SectionGroup = "APP_TOT", Ordering = 8, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_TOT } },
                    new FormSectionGroup() { SectionGroup = "APP_PEA", Ordering = 9, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_PEA } },
                    new FormSectionGroup() { SectionGroup = "APP_PWA", Ordering = 10, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_PWA } },
                    new FormSectionGroup() { SectionGroup = "APP_VAT_CONFIRMATION", Ordering = 11, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_VAT }, NotFormPage = true, IsBeginPage = false },
                    new FormSectionGroup() { SectionGroup = "APP_BUILDING_G1", Ordering = 129, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_G1 } },

                    new FormSectionGroup() { SectionGroup = "APP_FACTORY_CLASS_2_NEW_SEARCH", Ordering = 1, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW },NotFormPage = true, IsBeginPage = true },

                    new FormSectionGroup() { SectionGroup = "APP_BUILDING_R6_SEARCH", ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 }, Ordering = 0, SearchPage = true },
                    new FormSectionGroup() { SectionGroup = "APP_BUILDING_R6", Ordering = 130, ShowOnSpecificApps = true, AppSystemNames = new string[] { AppSystemNameTextConst.APP_BUILDING_R6 } },

                };

                db.InsertMany(items);

                InitForConsent(db);
                InitForRestaurant(db);
                InitForBusinessBadHealth(db);
                InitForProductInPublic(db);
                InitForRetail(db);
                InitForVeterinaryHospital(db);
                InitForSoftwareHouse(db);
            }
        }

        private static void InitForConsent(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                new FormSectionGroup()
                {
                    SectionGroup = "CONSENT",
                    Ordering = 2,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.APP_SOFTWARE_HOUSE,
                    IsBeginPage = true,
                    ResourceName = "Apps_SingleForm_SectionGroup"
                },
            };

            db.InsertMany(items);

        }

        private static void InitForRetail(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_ANIMAL_FOOD", Ordering = 105,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_CARD", Ordering = 128,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_CARD,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_BUILDING", Ordering = 129,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_BUILDING,
                        AppSystemNameTextConst.APP_BUILDING_OTHER,
                        AppSystemNameTextConst.APP_BUILDING_DPW,
                        AppSystemNameTextConst.APP_BUILDING_DPW_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_DISTRICT,
                        AppSystemNameTextConst.APP_BUILDING_DISTRICT_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_RENEW,
                        AppSystemNameTextConst.APP_BUILDING_OTHER_RENEW,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_HOTEL", Ordering = 133,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HOTEL,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_HOTEL_RENEW", Ordering = 134,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HOTEL_RENEW,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_COMMERCIAL", Ordering = 134,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_COMMERCIAL,
                    }
                },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_TAX", Ordering = 135,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_TAX,
                        AppSystemNameTextConst.APP_TAX_RENEW,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_RADIO", Ordering = 135,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_RADIO,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_RADIO_RENEW", Ordering = 135,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_RADIO_RENEW,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_ANIMAL_MED", Ordering = 137,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ANIMAL_MED,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_ANIMAL_MED_RENEW", Ordering = 137,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ANIMAL_MED_RENEW,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_ANIMAL_LICENSE", Ordering = 138,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_ANIMAL_LICENSE_RENEW", Ordering = 138,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ANIMAL_LICENSE_RENEW,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_HEALTH", Ordering = 139,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_MOVIE", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_MOVIE,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_KARAOKE", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_KARAOKE,
                    }
                 },
                 new FormSectionGroup
                 {
                    SectionGroup = "APP_SELL_FOOD_EDIT", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT,
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT,
                    }
                 },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_FOOD_RENEW", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_RENEW,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_FOOD_CANCEL", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200_CANCEL,
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_CANCEL,
                    }
                },
                 new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_FOOD_RENEW_LT", Ordering = 140,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_CARCASS", Ordering = 104,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_CARCASS,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SELL_ANIMAL", Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SELL_ANIMAL,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_MARKETING_EDIT", Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_EDIT
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_MARKETING_CANCEL", Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_MARKETING", Ordering = 102,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_MARKETING,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_SELL_EDIT", Ordering = 102,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_SELL_EDIT,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_SELL", Ordering = 101,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_SELL,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_DIRECT_SELL_CANCEL", Ordering = 102,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SECURITIES_BUSINESS_EDIT", Ordering = 4002,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_EDIT,
                    }
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_SECURITIES_BUSINESS_CANCEL", Ordering = 4002,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SECURITIES_BUSINESS_CANCEL,
                    }
                },



                 new FormSectionGroup
                 {
                    SectionGroup = "APP_FACTORY_TYPE2_NEW",
                    Ordering = 10,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FACTORY_TYPE2,
                    }
                 },

                 new FormSectionGroup
                 {
                    SectionGroup = "APP_FACTORY_TYPE2_NEW_13",
                    Ordering = 11,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FACTORY_TYPE2,
                    }
                 }
                //new FormSectionGroup() { SectionGroup = "APP_SELL_TOBACCO", Ordering = 100,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_SELL_TOBACCO,
                //    },
                //},
            };
            db.InsertMany(items);
        }

        private static void InitForRestaurant(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                // แยกไฟล์เฉพาะใบ
                //new FormSectionGroup() { SectionGroup = "APP_SELL_ALCOHOL", Ordering = 10,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_SELL_ALCOHOL,
                //    },
                //},
                new FormSectionGroup() { SectionGroup = "APP_SELL_FOOD", Ordering = 11,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
                        AppSystemNameTextConst.APP_SELL_FOOD_GE_200,
                    },
                }

                
            //FRT
                //FRT-NEW
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FRT_NEW_001",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_NEW_001,

                    }
                }
                //FRT-RENEW
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FRT_RENEW_001",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_RENEW_001,
                    }
                }
                //FRT-EDIT
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FRT_EDIT_001",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_EDIT_001,
                    }
                }

                //FRT-CANCEL
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FRT_CANCEL_001",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FRT_CANCEL_001,
                    }
                }

                //FRT-FACTORY
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FACTORY_CLASS_2_NEW",
                    Ordering = 3,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
                    }
                }
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FACTORY_CLASS_2_EDIT",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                    }
                }
                ,new FormSectionGroup
                {
                    SectionGroup = "APP_FACTORY_CLASS_2_CANCEL",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL,
                    },             
                }

                //,new FormSectionGroup
                //{
                //    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC",
                //    Ordering = 103,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[]
                //    {
                //        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                //    }
                //}

                ,new FormSectionGroup
                {
                    SectionGroup = "APP_HEALTH_CANCEL_SPECIFICALLY",
                    Ordering = 104,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_CANCEL,
                    }
                }

                ,new FormSectionGroup
                {
                    SectionGroup = "APP_HEALTH_RENEW_SPECIFICALLY",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_RENEW,
                    }
                }

                ,new FormSectionGroup
                {
                    SectionGroup = "APP_HEALTH_EDIT_SPECIFICALLY",
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_HEALTH_EDIT,
                    }
                }
            };
            //ข้อมูลการจำหน่ายหรือสะสมอาหาร
            db.InsertMany(items);
        }

        private static void InitForBusinessBadHealth(IMongoCollection<FormSectionGroup> db)
        {
            var items = new List<FormSectionGroup>()
            {
                //new FormSectionGroup() {
                //    SectionGroup = FormSectionGroupTextConstant.APP_BUSINESS_BAD_HEALTH_GENERAL,
                //    Ordering = 3000,
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = AppSystemNameTextConst.APP_DANGER_ALL,
                //    HideOnSpecificApps = true,
                //    HideAppSystemNames = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                //        AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL).ConcatItems(
                //        AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RENEW,
                //        AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_CANCEL),
                //},
            };

            #region FOOD

            for (int i = 0; i < AppSystemNameTextConst.APP_Danger_Food.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_Danger_Food[i];
                var foodSysName = AppSystemNameTextConst.APP_Danger_Food_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3200 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3300 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3400 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            #endregion

            #region SERVICES

            #region RESTAURANT

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3200 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3300 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3400 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            #endregion

            #region HOTEL
            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3200 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3300 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3400 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }
            #endregion

            #region FITNESS
            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3200 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3300 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL_SYSTEM_NAMES[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 3400 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        food,
                    },
                });
            }
            #endregion

            #endregion

            #region CONSTRUCTION

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 23100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }
            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 24100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            #endregion

            #region OTHER

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 25100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 25100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            #endregion

            #region COSMATIC
            //for (int i = 0; i < 1; i++)
            //{
            //    var food = AppSystemNameTextConst.APP_DANGER_COSMATIC_ALL[i];
            //    var foodSysName = AppSystemNameTextConst.APP_DANGER_COSMATIC_SystemNames[i];
            //    items.Add(new FormSectionGroup()
            //    {
            //        SectionGroup = foodSysName,
            //        Ordering = 25100 + i,
            //        ShowOnSpecificApps = true,
            //        AppSystemNames = new string[] { food },
            //    });
            //}

            //for (int i = 0; i < 2; i++)
            //{
            //    var food = AppSystemNameTextConst.ALL_APP_DANGER_COSMATIC_RENEW[i];
            //    var foodSysName = AppSystemNameTextConst.ALL_APP_DANGER_COSMATIC_RENEW_SystemNames[i];
            //    items.Add(new FormSectionGroup()
            //    {
            //        SectionGroup = foodSysName,
            //        Ordering = 25100 + i,
            //        ShowOnSpecificApps = true,
            //        AppSystemNames = new string[] { food },
            //    });
            //}
            #endregion

            #region AUTOMOTIVE

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 33100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 33200 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            #endregion

            #region PETROLEUM

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM[i];
                var foodSysName = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 34100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }
            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW[i];
                var foodSysName = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW_SystemNames[i];
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = foodSysName,
                    Ordering = 35100 + i,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] { food },
                });
            }

            #endregion



            db.InsertMany(items.ToArray());
        }

        private static void InitForProductInPublic(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                new FormSectionGroup() { SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC, Ordering = 15,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA
                    },
                },
                new FormSectionGroup() { SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_RENEW, Ordering = 16,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW
                    },
                },
                new FormSectionGroup() { SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_EDIT, Ordering = 17,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT
                    },
                },
                new FormSectionGroup() { SectionGroup = FormSectionGroupTextConstant.APP_SELL_PRODUCT_IN_PUBLIC_CANCEL, Ordering = 18,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL
                    },
                }
            };

            db.InsertMany(items);
        }

        private static void InitForVeterinaryHospital(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                new FormSectionGroup() {
                    SectionGroup = "APP_ANIMAL_BUILD",
                    Ordering = 32100,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    },
                },
                new FormSectionGroup
                {
                    SectionGroup = "APP_ANIMAL_BUILD_RENEW",
                    Ordering = 32200,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ANIMAL_BUILD_RENEW,
                    },
                },
            };

            db.InsertMany(items);
        }

        private static void InitForSoftwareHouse(IMongoCollection<FormSectionGroup> db)
        {
            FormSectionGroup[] items = new FormSectionGroup[]
            {
                //new FormSectionGroup() {
                //    SectionGroup = "APP_SOFTWARE_HOUSE",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,
                //    },
                //    Ordering = 32300
                //},
                new FormSectionGroup
                {
                    SectionGroup = "APP_SOFTWARE_HOUSE_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW,
                    },
                    Ordering = 32400
                },
                //new FormSectionGroup
                //{
                //    SectionGroup = "APP_SOFTWARE_HOUSE_EDIT",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[]
                //    {
                //        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                //    },
                //    Ordering = 32500
                //},
            };

            db.InsertMany(items);
        }

        public int RevisionCode { get; set; } = 0;
        public string RevisionName { get; set; } = "Before Config Revision";

        public string SectionGroup { get; set; }

        public UserTypeEnum[] IdentityTypes { get; set; }

        public int Ordering { get; set; }

        public bool ShowOnSpecificApps { get; set; }

        public string[] AppSystemNames { get; set; }

        public bool HideOnSpecificApps { get; set; }

        public string[] HideAppSystemNames { get; set; }
        public string ResourceName { get; set; }

        #region [Not Form Page]
        public bool NotFormPage { get; set; }
        public bool IsBeginPage { get; set; }
        #endregion

        #region [Search Page]
        public bool SearchPage { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSystemNames">Support Apps</param>
        /// <param name="index">Group Index, Zero based</param>
        /// <returns></returns>
        public static FormSectionGroup GetSectionGroup(string[] appSystemNames, int index)
        {
            return GetSectionGroupQuery(appSystemNames).Skip(index).Take(1).FirstOrDefault();
        }

        public static List<FormSectionGroup> GetAllSectionGroup(string[] appSystemNames)
        {
            return GetSectionGroupQuery(appSystemNames).ToList();
        }

        public static bool TryGetAllSectionGroupRevision(string[] appSystemNames, int revisionCode, UserTypeEnum? idenType, out FormSectionGroup[] outSectionGroups)
        {
            var sectionGroups = getSectionGroupRevisionQuery(appSystemNames, revisionCode, idenType);
            if (sectionGroups.Length > 0)
            {
                outSectionGroups = sectionGroups;
                return true;
            }
            else
            {
                // Revision not found, just return the current config
                outSectionGroups = null;
                return false;
            }
        }

        private static FormSectionGroup[] getSectionGroupRevisionQuery(string[] appSystemNames, int revisionCode, UserTypeEnum? idenType = null)
        {
            var db = MongoFactory.GetFormSectionGroupRevisionCollection().AsQueryable();
            FormSectionGroup[] sectionGroup = null;
            var query = db.AsQueryable().OrderBy(o => o.Ordering).AsQueryable();
            if (idenType != null)
            {
                sectionGroup = query.Where(o => o.IdentityTypes == null || o.IdentityTypes.Count() == 0 || o.IdentityTypes.Contains(idenType.Value))
                    .ToArray();
            }
            else
            {
                sectionGroup = query.ToArray();
            }

            if (appSystemNames != null)
            {
                foreach (var group in sectionGroup)
                {
                    sectionGroup = sectionGroup.Where(x => x.RevisionCode == revisionCode)
                        .Where(o => (!o.HideOnSpecificApps || (o.HideOnSpecificApps && appSystemNames.Any(p => !o.HideAppSystemNames.Contains(p)))) &&
                    (!o.ShowOnSpecificApps || (o.ShowOnSpecificApps && o.AppSystemNames.Any(p => appSystemNames.Contains(p))))).ToArray();
                }
            }

            return sectionGroup;
        }

        public static FormSectionGroup[] GetSectionGroupQuery(string[] appSystemNames, UserTypeEnum? idenType = null)
        {
            var db = MongoFactory.GetFormSectionGroupCollection().AsQueryable();
            FormSectionGroup[] sectionGroup = null;

            var query = db.AsQueryable().OrderBy(o => o.Ordering).AsQueryable();

            if (idenType != null)
            {
                sectionGroup = query.Where(o => o.IdentityTypes == null || o.IdentityTypes.Count() == 0 || o.IdentityTypes.Contains(idenType.Value))
                    .ToArray();
            }
            else
            {
                sectionGroup = query.ToArray();
            }

            if (appSystemNames != null)
            {
                foreach (var group in sectionGroup)
                {
                    sectionGroup = sectionGroup.Where(o => (!o.HideOnSpecificApps || (o.HideOnSpecificApps && appSystemNames.Any(p => !o.HideAppSystemNames.Contains(p)))) &&
                    (!o.ShowOnSpecificApps || (o.ShowOnSpecificApps && o.AppSystemNames.Any(p => appSystemNames.Contains(p))))).ToArray();
                }
            }

            return sectionGroup;
        }
    }
}
