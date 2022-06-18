using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class DOPAHotelAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_HOTEL_SECTION").FirstOrDefault();
            
            if (sec != null)
            {
                var area = int.Parse(sec.FormData["APP_HOTEL_ROOM"].ToString());
                if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "01")
                {
                    fee += 10000 + (area * 40);
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "02")
                {
                    fee += 20000 + (area * 40); ;
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "03")
                {
                    fee += 30000 + (area * 40); ;
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "04")
                {
                    fee += 40000 + (area * 40); ;
                }
            }

            return fee;
        }
        
        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dopa/25.1_hotel.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();
            
            #region Identity
            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", ""));
                model.Add(field);

                model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = generalInfo.TryGetString("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Age", Value = generalInfo.TryGetString("GENERAL_INFORMATION__CITIZEN_AGE", "") });

                string identity = generalInfo.TryGetString("IDENTITY_ID", "");
                FillIdentityDigits(model, identity, "Identity", 13);

                model.Add(new PDFFieldValue() { FieldName = "IdenType1", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "Age", Value = generalInfo.TryGetString("GENERAL_INFORMATION__CITIZEN_AGE", "") });
                model.Add(new PDFFieldValue() { FieldName = "Origin", Value = generalInfo.TryGetString("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = generalInfo.TryGetString("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT", "") });

                var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS", "") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "");

                model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_CITIZEN_ADDRESS", "") });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo.TryGetString("COMPANY_NAME_TH", "");
                model.Add(field);

                model.Add(new PDFFieldValue() { FieldName = "IdenType0", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "JuristicType", Value = generalInfo.TryGetString("GENERAL_INFORMATION__JURISTIC_TYPE", "") });
                model.Add(new PDFFieldValue() { FieldName = "VatID", Value = generalInfo.TryGetString("IDENTITY_ID", "") });
                model.Add(new PDFFieldValue() { FieldName = "RegisterDate", Value = generalInfo.TryGetString("REGISTER_DATE", "") });
                model.Add(new PDFFieldValue() { FieldName = "RegisterAt", Value = "" });

                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "JAddress", Value = addrInfo.TryGetString("ADDRESS_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JBuilding", Value = addrInfo.TryGetString("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JRoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JFloor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JMoo", Value = addrInfo.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JSoi", Value = addrInfo.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JRoad", Value = addrInfo.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JTumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "JAmphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "JProvince", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "JPostcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS", "") });

                model.Add(new PDFFieldValue() { FieldName = "JTelephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "")))
                    model.First(o => o.FieldName == "JTelephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "");

                model.Add(new PDFFieldValue() { FieldName = "JFax", Value = addrInfo.TryGetString("ADDRESS_FAX_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "JEmail", Value = addrInfo.TryGetString("JURISTIC_EMAIL", "") });

                if (req.Data.ContainsKey("COMMITTEE_INFORMATION"))
                {
                    var commInfo = req.Data["COMMITTEE_INFORMATION"].Data;
                    int totalCommittee = int.Parse(commInfo["COMMITTEE_INFORMATION_TOTAL"]);
                    int count = 0;
                    for (int i = 0; i < totalCommittee && count < 2; i++)
                    {
                        if (commInfo["JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i].ToLower() == "yes")
                        {
                            model.Add(new PDFFieldValue() { FieldName = "CommitteeName" + (count + 1), Value = string.Format("{0}{1} {2}", commInfo["JURISTIC_COMMITTEE_TITLE_" + i], commInfo["JURISTIC_COMMITTEE_NAME_" + i], commInfo["JURISTIC_COMMITTEE_LASTNAME_" + i]) });

                            // 2019-03-26: Changed from "JURISTIC_COMMITTEE_CITIZEN_ID_" to "COMMITTEE_INFORMATION_CITIZEN_ID_"
                            string id = commInfo.TryGetString("COMMITTEE_INFORMATION_CITIZEN_ID_" + i);
                            FillIdentityDigits(model, id, "ComIden" + (count + 1), 13);

                            count++;
                        }                        
                    }
                }
            }

            #endregion
            
            #region Store

            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "StoreNameTH", Value = storeInfo["INFORMATION_STORE_NAME_TH"] });
                model.Add(new PDFFieldValue() { FieldName = "StoreNameEN", Value = storeInfo["INFORMATION_STORE_NAME_EN"] });
            }

            if (req.Data.ContainsKey("APP_HOTEL_SECTION"))
            {
                var hodelInfo = req.Data["APP_HOTEL_SECTION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "HotelType", Value = hodelInfo["DROPDOWN_APP_HOTEL_TYPE_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "TotalRoom", Value = hodelInfo["APP_HOTEL_ROOM"] });
                model.Add(new PDFFieldValue() { FieldName = "MinPrice", Value = hodelInfo["APP_HOTEL_LOWEREST_PRICE"] });
                model.Add(new PDFFieldValue() { FieldName = "MaxPrice", Value = hodelInfo["APP_HOTEL_HIGHEST_PRICE"] });
                model.Add(new PDFFieldValue() { FieldName = "HBuilding", Value = hodelInfo["APP_HOTEL_ROOM"] });
                model.Add(new PDFFieldValue() { FieldName = "TotalRoom", Value = hodelInfo["APP_HOTEL_ROOM"] });
            }

            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var addrInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "HAddress", Value = addrInfo["ADDRESS_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HBuilding", Value = addrInfo["ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HRoomNumber", Value = addrInfo["ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HFloor", Value = addrInfo["ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HMoo", Value = addrInfo["ADDRESS_MOO_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HSoi", Value = addrInfo["ADDRESS_SOI_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HRoad", Value = addrInfo["ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HTumbol", Value = addrInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HAmphur", Value = addrInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HProvince", Value = addrInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HPostcode", Value = addrInfo["ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "HTelephone", Value = addrInfo["ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"]))
                    model.First(o => o.FieldName == "HTelephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "HEmail", Value = addrInfo["INFORMATION_STORE_EMAIL"] });
                model.Add(new PDFFieldValue() { FieldName = "HFax", Value = addrInfo["ADDRESS_FAX_INFORMATION_STORE__ADDRESS"] });
            }

            #endregion

            #region Option 3: Location
            var hotelSection = req.Data["APP_HOTEL_SECTION"].Data;

            if (!(hotelSection["APP_HOTEL_LOCATION_OPTION"] == "1"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_11", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(hotelSection["APP_HOTEL_LOCATION_OPTION"] == "2"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_12", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(StringHelper.IsTrueValue(hotelSection["APP_HOTEL_DOC_TYPE_APP_HOTEL_DOC_TYPE_OPTION_ONE"])))
                model.Add(new PDFFieldValue() { FieldName = "Option3_21", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(StringHelper.IsTrueValue(hotelSection["APP_HOTEL_DOC_TYPE_APP_HOTEL_DOC_TYPE_OPTION_TWO"])))
                model.Add(new PDFFieldValue() { FieldName = "Option3_22", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(StringHelper.IsTrueValue(hotelSection["APP_HOTEL_DOC_TYPE_APP_HOTEL_DOC_TYPE_OPTION_THREE"])))
                model.Add(new PDFFieldValue() { FieldName = "Option3_23", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(StringHelper.IsTrueValue(hotelSection["APP_HOTEL_DOC_TYPE_APP_HOTEL_DOC_TYPE_OPTION_FOUR"])))
                model.Add(new PDFFieldValue() { FieldName = "Option3_24", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            model.Add(new PDFFieldValue() { FieldName = "Option3_2Other", Value = hotelSection["APP_HOTEL_DOC_OTHER"] });

            model.Add(new PDFFieldValue() { FieldName = "Option3_2Rai", Value = StringHelper.ToDecimal(hotelSection["APP_HOTEL_AREA"], 0).ToString("#,##0") });
            model.Add(new PDFFieldValue() { FieldName = "Option3_2Ngan", Value = StringHelper.ToDecimal(hotelSection["APP_HOTEL_AREA_WORK"], 0).ToString("#,##0") });
            model.Add(new PDFFieldValue() { FieldName = "Option3_2Wa", Value = StringHelper.ToDecimal(hotelSection["APP_HOTEL_AREA_SQUARE"], 0).ToString("#,##0") });


            if (!(hotelSection["APP_HOTEL_BUILDING_TYPE_OPTION"] == "APP_HOTEL_BUILDING_TYPE_ONE"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_31", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(hotelSection["APP_HOTEL_BUILDING_TYPE_OPTION"] == "APP_HOTEL_BUILDING_TYPE_TWO"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_32", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(hotelSection["APP_HOTEL_BUILDING_TYPE_OPTION"] == "APP_HOTEL_BUILDING_TYPE_THREE"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_33", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(hotelSection["APP_HOTEL_BUILDING_TYPE_OPTION"] == "APP_HOTEL_BUILDING_TYPE_FOUR"))
                model.Add(new PDFFieldValue() { FieldName = "Option3_34", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            model.Add(new PDFFieldValue() { FieldName = "Option3_3Other", Value = hotelSection["APP_HOTEL_BUILDING_TYPE_OTHER"] });
            model.Add(new PDFFieldValue() { FieldName = "Option3_3Area", Value = hotelSection["APP_HOTEL_AREA_METER"] });
            #endregion
            
            PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
