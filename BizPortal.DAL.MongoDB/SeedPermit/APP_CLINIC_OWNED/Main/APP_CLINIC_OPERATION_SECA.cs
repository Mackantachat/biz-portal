using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_OWNED
{
    
    public class APP_CLINIC_OPERATION_SECA
    {

        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_OPERATION_SECA").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_OPERATION_SECA",
                    SectionGroup = "APP_CLINIC_OWNED",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION
                    },
                    Ordering = 1,
                    HideSectionHeader = true,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_OWNED",
                }); 
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            InitFormSectionRow();
        }

        private static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_OPERATION_SECA").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_OPERATION_SECA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_CLINIC_OPERATION_SECA_CONA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_OPERATION_SECA_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.JSExpression
                            , JSExpression = "return $(\"[name='APP_CLINIC_OPERATION_SECA_CONA']\").val().length < 10 ? false : true"
                            , ErrorMessage = "เลขที่อ้างอิงคำขอใบอนุญาตประกอบกิจการสถานพยาบาลไม่ถูกต้อง" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_OWNED",
                            //DisplayMaskInput = true,
                            //MaskInputPattern = "C000000000",
                        },
                    }
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

    }

}
