
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PERMISSION_EDIT").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                    },
					Ordering = 4731,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT"
                });

                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITA",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                    },
                    Ordering = 4731,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT"
                });

                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITB",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                    },
                    Ordering = 4731,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT"
                });

                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                    },
                    Ordering = 4731,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_3.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_4.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_WORKING_DAY_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION.Init();
            APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION.Init();
            APP_CLINIC_OPERATION_EDIT_B_GROUP.Init();
        }
    }
}
