using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using iTextSharp.text.pdf;
using System.IO;
using BizPortal.Utils.Helpers;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;

namespace BizPortal.AppsHook
{
    class AppsHookFactoryType2 : StoreBaseAppHook
    {
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;

            if (stage == AppsStage.UserCreate)
            {
                if (request.AppSysName == AppSystemNameTextConst.APP_FACTORY_TYPE2)
                {
                    var storeInfo = request.Data["APP_FACTORY_TYPE2_PLANT_LOCATION_INFO"].Data;
                    if (storeInfo.ContainsKey("ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM") &&
                        storeInfo.ContainsKey("ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM") &&
                        storeInfo.ContainsKey("ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM"))
                    {
                        request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);
                        request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);
                        request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);

                        request.Province = (storeInfo["ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                        request.Amphur = (storeInfo["ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                        request.Tumbol = (storeInfo["ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "ไม่พบข้อมูลตำบล อำเภอ จังหวัดของที่ตั้งโรงงาน";
                    }
                }              
                else
                {
                    result.Success = false;
                    result.Message = "AppHook ไม่ถูกต้อง";
                }

            }

            return result;
        }
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        private string str;
        private decimal sumtxtEnNbr = 0;
        private decimal sumtxtHpTotal = 0;

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/factory/FACTORY_Report.pdf");
            List<PDFFieldValue> model = new List<PDFFieldValue>();
            //PDFFieldValue field;

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            string strDate = generalInfo["INFORMATION_HEADER__REQUEST_DATE"].ToString();
            string[] strDateArr = strDate.Split('/');
            string dd = strDateArr[0];
            string mm = strDateArr[1];
            string strYY = strDateArr[2];
            string[] yyArr = strYY.Split(' ');
            string yy = yyArr[0];
            model.Add(new PDFFieldValue() { FieldName = "txtDay", Value = dd }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtMonth", Value = CastToMonth(mm) }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtYear", Value = yy });
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtNoHouse", Value = addrInfo["ADDRESS_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo["ADDRESS_MOO_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtAlley", Value = addrInfo["ADDRESS_SOI_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = addrInfo["ADDRESS_ROAD_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = addrInfo["ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = addrInfo["ADDRESS_TELEPHONE_CITIZEN_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"]))
                    model.First(o => o.FieldName == "txtTell").Value += " ต่อ. " + addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"];
                model.Add(new PDFFieldValue() { FieldName = "txtNOCitizenAndJuristict", Value = "อยู่บ้าน" });
                model.Add(new PDFFieldValue() { FieldName = "txtRegisterCapital", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtName", Value = string.Format("{0} {1} {2}", generalInfo["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo["CITIZEN_NAME"], generalInfo["CITIZEN_LASTNAME"]) });
                model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = generalInfo["GENERAL_INFORMATION__CITIZEN_AGE"] });
                model.Add(new PDFFieldValue() { FieldName = "txtRace", Value = generalInfo["DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"] });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtNoHouse", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtAlley", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                    model.First(o => o.FieldName == "txtTell").Value += " ต่อ. " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];
                model.Add(new PDFFieldValue() { FieldName = "txtNOCitizenAndJuristict", Value = "สำนักงานเลขที่" });              
                model.Add(new PDFFieldValue() { FieldName = "txtRegisterCapital", Value = Convert.ToDecimal(generalInfo["REGISTER_CAPITAL"]).ToString("n2") });
                model.Add(new PDFFieldValue() { FieldName = "txtName", Value = generalInfo["COMPANY_NAME_TH"] });
                model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtRace", Value = "-" });
            }

            var factInfo = req.Data["APP_FACTORY_TYPE2_INFO"].Data;
            model.Add(new PDFFieldValue() { FieldName = "txtFactName", Value = factInfo["FACTORY_TYPE2_NAME"] });
            model.Add(new PDFFieldValue() { FieldName = "txtkindOfPlant", Value = factInfo["FACTORY_TYPE2_FACTYPE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtKindOfProduction", Value = factInfo["FACTORY_TYPE2_PRODUCT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtEngineHP", Value = factInfo["FACTORY_TYPE2_ENGINE_HP"] });
            model.Add(new PDFFieldValue() { FieldName = "txtLandCapital", Value = factInfo["FACTORYP_TYP2_CAPITAL"] });
            model.Add(new PDFFieldValue() { FieldName = "txtBuildingCapital", Value = factInfo["FACTORYP_TYP2_CAPITAL_CONSTRUCTION"] });
            model.Add(new PDFFieldValue() { FieldName = "txtEngineCapital", Value = factInfo["FACTORYP_TYP2_CAPITAL_ENGINE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtWorkingCapital", Value = factInfo["FACTORYP_TYP2_WORKING_CAPITAL"] });
            model.Add(new PDFFieldValue() { FieldName = "txtTotalCapital", Value = factInfo["FACTORYP_TYP2_TOTAL_CAPITAL"] });

            var factRankInfo = req.Data["APP_FACTORY_TYPE2_RANKS_INFO"].Data;
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfMan", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_TOTAL_OFFICER"] });
            model.Add(new PDFFieldValue() { FieldName = "txtStartWorkingTime", Value = factRankInfo["DROPDOWN_FACTORY_TYPE2_START_WORKING_TIME_DD_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtEndWorkingTime", Value = factRankInfo["DROPDOWN_FACTORY_TYPE2_END_WORKING_TIME_DD_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtTimeToWork", Value = factRankInfo["FACTORY_TYPE2_WORK_TIME"] });
            model.Add(new PDFFieldValue() { FieldName = "txtWorkHour", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_SHIFT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtWorkDayPerYear", Value = factRankInfo["APP_FACTORY_TYPE2_WORKDAY"] });
            model.Add(new PDFFieldValue() { FieldName = "txtHoliDay", Value = factRankInfo["APP_FACTORY_TYPE2_HOLIDAY"] });
            //model.Add(new PDFFieldValue() { FieldName = "txtRegisterCapital", Value = "-" });

            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfOfficer", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_ACADEMIC_OFFICER_OVERSEA"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfMaleTechnical", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_ENGINEER_MALE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfFemaleTechnical", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_ENGINEER_FEMALE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfManOperating", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_OPERATIONAL_MALE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfFemaleOperating", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_OPERATIONAL_FEMALE"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfSpecialistOversea", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_TECHNICAL_OFFICER_OVERSEA"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNumberOfTechnicalOversea", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_TECHNICAL_DOMESTIC"] });
            model.Add(new PDFFieldValue() { FieldName = "txtTotalOfMan", Value = factRankInfo["FACTORY_TYPE2_NUMBER_OF_TOTAL_OFFICER"] });

            var factLocInfo = req.Data["APP_FACTORY_TYPE2_PLANT_LOCATION_INFO"].Data;
            model.Add(new PDFFieldValue() { FieldName = "txtPlantArea", Value = factLocInfo["FACTORY_TYPE2_BUILDING_AREA"] });
            model.Add(new PDFFieldValue() { FieldName = "txtLandArea", Value = factLocInfo["FACTORY_TYPE2_BUILDING_AND_PLANT_AREA"] });
            model.Add(new PDFFieldValue() { FieldName = "txtOldArea", Value = factLocInfo["FACTORY_TYPE2_BUILDING_OWNER_NAME"] });
            if (Convert.ToString(factLocInfo["DROPDOWN_FACTORY_TYPE2_BUILDING_STYLE_TEXT"]).Equals("อื่นๆ"))
            {
                model.Add(new PDFFieldValue() { FieldName = "txtPlantAttribute", Value = factLocInfo["FACTORY_TYPE2_BUILDING_STYLE_OTHER"] });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "txtPlantAttribute", Value = factLocInfo["DROPDOWN_FACTORY_TYPE2_BUILDING_STYLE_TEXT"] });
            }           
            model.Add(new PDFFieldValue() { FieldName = "txtRoofCoverBy", Value = factLocInfo["FACTORY_TYPE2_BUILDING_ROOF_COVER_BY"] });
            model.Add(new PDFFieldValue() { FieldName = "txtPlantAround", Value = factLocInfo["FACTORY_TYPE2_AROUND"] });
            model.Add(new PDFFieldValue() { FieldName = "txtAroundNorth", Value = factLocInfo["FACTORY_TYPE2_AROUND_NORTH"] });
            model.Add(new PDFFieldValue() { FieldName = "txtAroundSouth", Value = factLocInfo["FACTORY_TYPE2_AROUND_SOUTH"] });
            model.Add(new PDFFieldValue() { FieldName = "txtAroundEast", Value = factLocInfo["FACTORY_TYPE2_AROUND_EAST"] });
            model.Add(new PDFFieldValue() { FieldName = "txtAroundWest", Value = factLocInfo["FACTORY_TYPE2_AROUND_WEST"] });
            model.Add(new PDFFieldValue() { FieldName = "txtNo2", Value = factLocInfo["FACTORY_TYPE2_NBR"] });//
            if (Convert.ToString(factLocInfo["FACTORY_TYPE2_NBR"]).Equals(string.Empty))
            {
                model.Add(new PDFFieldValue() { FieldName = "txtNo2", Value = factLocInfo["FACTORY_TYPE2_NBR_2"] });
            }
            model.Add(new PDFFieldValue() { FieldName = "txtSOI2", Value = factLocInfo["ADDRESS_SOI_FACTORY_TYPE2_AREA_INFORMATION_FORM"] });
            model.Add(new PDFFieldValue() { FieldName = "txtStreet2", Value = factLocInfo["ADDRESS_ROAD_FACTORY_TYPE2_AREA_INFORMATION_FORM"] });
            model.Add(new PDFFieldValue() { FieldName = "txtCanel2", Value = factLocInfo["APP_FACTORY_TYPE2_WATERWAY"] });
            model.Add(new PDFFieldValue() { FieldName = "txtRiver2", Value = factLocInfo["APP_FACTORY_TYPE2_RIVER"] });
            model.Add(new PDFFieldValue() { FieldName = "txtMoo2", Value = factLocInfo["ADDRESS_MOO_FACTORY_TYPE2_AREA_INFORMATION_FORM"] });
            model.Add(new PDFFieldValue() { FieldName = "txtTumbol2", Value = factLocInfo["ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtAmphur2", Value = factLocInfo["ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtProvince2", Value = factLocInfo["ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "txtTell2", Value = factLocInfo["ADDRESS_FAX_FACTORY_TYPE2_AREA_INFORMATION_FORM"] });
            if (Convert.ToString(factLocInfo["FACTORY_TYPE2_BUILDING_OLD_OR_NEW_OPT"]).Equals("มีอยู่เดิม"))
            {
                model.Add(new PDFFieldValue() { FieldName = "txtNotExistsBuilding", Value = "----------------------" });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "txtExistsBuilding", Value = "----------------------------------" });
            }

            try
            {
                var mate = req.Data["APP_FACTORY_TYPE2_MATERIAL_INFORMATION_SECTION"].Data;
                for (int i = 1; i <= 4; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtMaterial" + i, Value = mate["APP_FACTORY_MATERIAL_NAME_MODAL_" + (i - 1)] });
                    model.Add(new PDFFieldValue() { FieldName = "txtMatDosePerYear" + i, Value = 
                        Convert.ToString(mate["APP_FACTORY_MATERIAL_DOSE_PER_YEAR_MODAL_" + (i - 1)]) + " " +
                        Convert.ToString(mate["DROPDOWN_APP_FACTORY_MATERIAL_UNIT_MODAL_TEXT_" + (i - 1)])
                    });
                    model.Add(new PDFFieldValue() { FieldName = "txtMatResource" + i, Value = mate["DROPDOWN_APP_FACTORY_MATERIAL_COUNTY_LIST_MODAL_TEXT_" + (i - 1)] });
                }
            }
            catch { }

            try
            {
                var product = req.Data["APP_FACTORY_TYPE2_PRODUCT_INFORMATION_SECTION"].Data;
                for (int i = 1; i <= 4; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtProduct" + i, Value = product["APP_FACTORY_TYPE2_PRODUCT_NAME_MODAL_" + (i - 1)] });
                    model.Add(new PDFFieldValue() { FieldName = "txtProDosePerYear" + i, Value = 
                        Convert.ToString(product["APP_FACTORY_TYPE2_PRODUCT_DOSE_PER_YAER_MODAL_" + (i - 1)]) + " " +
                        Convert.ToString(product["DROPDOWN_APP_FACTORY_TYPE2_PRODUCT_UNIT_MODAL_TEXT_" + (i - 1)])
                    });
                    model.Add(new PDFFieldValue() { FieldName = "txtProTo" + i, Value = product["DROPDOWN_APP_FACTORY_TYPE2_PRODUCT_COUNTY_LIST_MODAL_TEXT_" + (i - 1)] });
                }
            }
            catch { }

            try
            {
                var byProduct = req.Data["APP_FACTORY_TYPE2_BY_PRODUCT_INFORMATION_SECTION"].Data;
                for (int i = 1; i <= 4; i++)
                {
                    str = string.Format("{0} ปริมาณ {1} {2} แหล่งที่มา : {3} , "
                        , byProduct["APP_FACTORY_TYPE2_BY_PRODUCT_NAME_MODAL_" + (i - 1)]
                        , byProduct["APP_FACTORY_TYPE2_BY_PRODUCT_DOSE_PER_YAER_MODAL_" + (i - 1)]
                        , byProduct["DROPDOWN_APP_FACTORY_TYPE2_BY_PRODUCT_UNIT_MODAL_TEXT_" + (i - 1)]
                        , byProduct["DROPDOWN_APP_FACTORY_TYPE2_BY_PRODUCT_COUNTY_LIST_MODAL_TEXT_" + (i - 1)]);
                    str = str + str;
                    //model.Add(new PDFFieldValue() { FieldName = "txtProduct" + i, Value = product["APP_FACTORY_TYPE2_PRODUCT_NAME_MODAL_" + (i - 1)] });
                    //model.Add(new PDFFieldValue() { FieldName = "txtProDosePerYear" + i, Value = product["APP_FACTORY_TYPE2_PRODUCT_DOSE_PER_YAER_MODAL_" + (i - 1)] });
                    //model.Add(new PDFFieldValue() { FieldName = "txtProTo" + i, Value = product["DROPDOWN_APP_FACTORY_TYPE2_PRODUCT_COUNTY_LIST_MODAL_TEXT_" + (i - 1)] });
                }
            }
            catch
            {
                str = str.Substring(0, str.Length - 2);
                model.Add(new PDFFieldValue() { FieldName = "txtByProduct", Value = str });
            }

            try
            {
                var engine = req.Data["APP_FACTORY_TYPE2_ENGINE_INFORMATION_SECTION"].Data;
                int i = 0;
                while (1 > 0)
                {
                    string txtCopName = string.Empty;
                    model.Add(new PDFFieldValue() { FieldName = "txtOrder" + (i + 1), Value = engine["ARR_IDX_" + i] });
                    //model.Add(new PDFFieldValue() { FieldName = "txtCopName" + (i + 1), Value = engine["APP_FACTORY_TYPE2_ENGINE_NAME_MODAL_" + i] });
                    txtCopName = engine["APP_FACTORY_TYPE2_ENGINE_NAME_MODAL_" + i];
                    txtCopName = txtCopName + " " + "ประเทศผู้ผลิต" + " " +engine["DROPDOWN_APP_FACTORY_TYPE2_ENGINE_COUNTY_LIST_MODAL_TEXT_" + i];
                    model.Add(new PDFFieldValue() { FieldName = "txtCopName" + (i + 1), Value = txtCopName });

                    if (Convert.ToString(engine["APP_FACTORY_TYPE2_HP_MODAL_" + i]).Equals(string.Empty))
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHP" + (i + 1), Value = string.Empty });
                    }
                    else
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHP" + (i + 1), Value = Convert.ToDecimal(engine["APP_FACTORY_TYPE2_HP_MODAL_" + i]).ToString("n") });
                    }

                    if (Convert.ToString(engine["APP_FACTORY_TYPE2_HP_COMPARE_MODAL_" + i]).Equals(string.Empty))
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHPCompare" + (i + 1), Value = string.Empty });
                    }
                    else
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHPCompare" + (i + 1), Value = Convert.ToDecimal(engine["APP_FACTORY_TYPE2_HP_COMPARE_MODAL_" + i]).ToString("n") });
                    }

                    if (Convert.ToString(engine["APP_FACTORY_TYPE2_NUMBER_OF_ENGINE_MODAL_" + i]).Equals(string.Empty))
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtEnNbr" + (i + 1), Value = string.Empty });
                    }
                    else
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtEnNbr" + (i + 1), Value = Convert.ToDecimal(engine["APP_FACTORY_TYPE2_NUMBER_OF_ENGINE_MODAL_" + i]).ToString("n") });
                    }

                    if (Convert.ToString(engine["APP_FACTORY_TYPE2_TOTAL_HP_COMPARE_MODAL_" + i]).Equals(string.Empty))
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHPTotal" + (i + 1), Value = string.Empty });
                    }
                    else
                    {
                        model.Add(new PDFFieldValue() { FieldName = "txtHPTotal" + (i + 1), Value = Convert.ToDecimal(engine["APP_FACTORY_TYPE2_TOTAL_HP_COMPARE_MODAL_" + i]).ToString("n") });
                    }

                    model.Add(new PDFFieldValue() { FieldName = "txtRemark" + (i + 1), Value = engine["APP_FACTORY_TYPE2_ENGINE_REMARK_MODAL_" + i] });
                    sumtxtEnNbr += Convert.ToDecimal(engine["APP_FACTORY_TYPE2_NUMBER_OF_ENGINE_MODAL_" + i]);
                    sumtxtHpTotal += Convert.ToDecimal(engine["APP_FACTORY_TYPE2_TOTAL_HP_COMPARE_MODAL_" + i]);
                    i++;
                }
            }
            catch
            {
                model.Add(new PDFFieldValue() { FieldName = "txtEnNbr16", Value = sumtxtEnNbr.ToString("n2") });
                model.Add(new PDFFieldValue() { FieldName = "txtHPTotal16", Value = sumtxtHpTotal.ToString("n2") });
            }

            //PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }
        private string CastToMonth(string strM)
        {
            switch (strM)
            {
                case "1":
                    return "มกราคม";
                case "2":
                    return "กุมภาพันธ์";
                case "3":
                    return "มีนาคม";
                case "4":
                    return "เมษายน";
                case "5":
                    return "พฤษภาคม";
                case "6":
                    return "มิถุนายน";
                case "7":
                    return "กรกฎาคม";
                case "8":
                    return "สิงหาคม";
                case "9":
                    return "กันยายน";
                case "10":
                    return "ตุลาคม";
                case "11":
                    return "พฤศจิกายน";
                case "12":
                    return "ธันวาคม";
            }
            return null;
        }
    }
}
