using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.V2;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class BKKTAXBoardAppHook : StoreBaseAppHook
    {
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;

            // คำร้องใหม่
            if (stage == AppsStage.UserCreate)
            {
                // ใส่ข้อมูลพื้นที่เจ้าของคำร้องจากที่อยู่ร้านค้า
                var storeInfo = request.Data["APP_TAX_ALL_AMOUNT"].Data;
                if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"))
                {
                    request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                    request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                    request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);

                    request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                    request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                    request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                }
            }

            return result;
        }

        private Dictionary<string, decimal> BOARD_TYPE = new Dictionary<string, decimal>
        {
            { "THAI_LANGUAGE_ONLY", 3 },
            { "THAI_AND_OTHER_LANGUAGE", 20 },
            { "OTHER_LANGUAGE_ONLY", 40 },
        };

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            //decimal fee = 0;
            //var sec = sectionData.Where(x => x.SectionName == "APP_TAX_BOARD_INFO_SECTION").FirstOrDefault();
            //foreach(var item in sec.ArrayData)
            //{
            //    decimal Area = Convert.ToDecimal(item.TryGetString("APP_TAX_CENTIMETER", ""));
            //    if (item.TryGetString("APP_TAX_TYPE_OPTION", "") == "1")
            //    {
            //        fee = fee + Math.Ceiling(Area / BOARD_TYPE["THAI_LANGUAGE_ONLY"]);
            //    }
            //    else if (item.TryGetString("APP_TAX_TYPE_OPTION", "") == "2")
            //    {
            //        fee = fee + Math.Ceiling(Area / BOARD_TYPE["THAI_AND_OTHER_LANGUAGE"]);
            //    }
            //    else if (item.TryGetString("APP_TAX_TYPE_OPTION", "") == "3")
            //    {
            //        fee = fee + Math.Ceiling(Area / BOARD_TYPE["OTHER_LANGUAGE_ONLY"]);
            //    }
            //}
            //if(fee < 200)
            //{
            //    fee = 200;
            //}
            return null;
            throw new Exception("ไม่พบข้อมูลการยื่นชำระภาษีป้ายใน SingleFormRequest");
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            List<byte[]> files = new List<byte[]>();
            string src = serverMapPathFunc("~/Uploads/apps/tax/19.1_sign_list.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            #region Header
            var generalInfo = req.Data.TryGetData("GENERAL_INFORMATION");
            var boardTextInfo = req.Data.TryGetData("APP_TAX_ALL_AMOUNT");

            int taxYear = 0;
            if (int.TryParse(boardTextInfo.Data.TryGetString("APP_TAX_YEAR", ""), out taxYear))
            {
                if (taxYear < 2300) taxYear += 543; // Convert to Bc.
                model.Add(new PDFFieldValue() { FieldName = "Year", Value = (taxYear % 100).ToString() });
            }

            model.Add(new PDFFieldValue() { FieldName = "LastReceiveNo", Value = boardTextInfo.Data.TryGetString("APP_TAX_OLD_NUMBER") });

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo.Data["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo.ThenGetStringData("CITIZEN_NAME"), generalInfo.ThenGetStringData("CITIZEN_LASTNAME"));
                model.Add(field);
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo.ThenGetStringData("COMPANY_NAME_TH");
                model.Add(field);
            }

            #region Address
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                var addrInfo = req.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION");

                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.ThenGetStringData("ADDRESS_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS") });
                if (!string.IsNullOrEmpty(addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                if (addrInfo.Data.ContainsKey("CITIZEN_EMAIL"))
                    model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.ThenGetStringData("CITIZEN_EMAIL") });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION");
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS") });
                if (!string.IsNullOrEmpty(addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                if (addrInfo.Data.ContainsKey("JURISTIC_EMAIL"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.ThenGetStringData("JURISTIC_EMAIL") });
                }
            }
            #endregion

            var store = req.Data["INFORMATION_STORE"];
            model.Add(new PDFFieldValue() { FieldName = "Store", Value = store.Data.TryGetString("INFORMATION_STORE_NAME_TH", "") });
            model.Add(new PDFFieldValue() { FieldName = "Agent", Value = boardTextInfo.Data.TryGetString("ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT", "") });
            
            #endregion

            #region Tag List  APP_TAX_BOARD_INFO_SECTION
            var set1 = GetAppTaxEntry(req, "1");
            var set2 = GetAppTaxEntry(req, "2");
            var set3 = GetAppTaxEntry(req, "3");

            int maxPerItem = 3;
            while (set1.Count > 0 || set2.Count > 0 | set3.Count > 0)
            {
                List<PDFFieldValue> contentModel = new List<PDFFieldValue>();
                contentModel.AddRange(model);

                int n = 0;
                while (set1.Count > 0 && n < maxPerItem)
                {
                    n++;
                    AppTaxEntry en = set1.Dequeue();
                    contentModel.Add(new PDFFieldValue() { FieldName = "Width1" + n, Value = en.Width });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Long1" + n, Value = en.Long });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Area1" + n, Value = en.Centimeter });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Qty1" + n, Value = en.Amount });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Message1" + n, Value = en.Text });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Location1" + n, Value = en.Location });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Remark1" + n, Value = en.Other });
                }

                n = 0;
                while (set2.Count > 0 && n < maxPerItem)
                {
                    n++;
                    AppTaxEntry en = set2.Dequeue();
                    contentModel.Add(new PDFFieldValue() { FieldName = "Width2" + n, Value = en.Width });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Long2" + n, Value = en.Long });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Area2" + n, Value = en.Centimeter });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Qty2" + n, Value = en.Amount });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Message2" + n, Value = en.Text });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Location2" + n, Value = en.Location });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Remark2" + n, Value = en.Other });
                }

                n = 0;
                while (set3.Count > 0 && n < maxPerItem)
                {
                    n++;
                    AppTaxEntry en = set3.Dequeue();
                    contentModel.Add(new PDFFieldValue() { FieldName = "Width3" + n, Value = en.Width });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Long3" + n, Value = en.Long });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Area3" + n, Value = en.Centimeter });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Qty3" + n, Value = en.Amount });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Message3" + n, Value = en.Text });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Location3" + n, Value = en.Location });
                    contentModel.Add(new PDFFieldValue() { FieldName = "Remark3" + n, Value = en.Other });
                }

                PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 11 };
                byte[] b = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, contentModel, cfg);
                files.Add(b);
            }
            #endregion
            
            var bytes = iTextPDFFormFieldsHelper.MergePDFFiles(files);
            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        private static Queue<AppTaxEntry> GetAppTaxEntry(ApplicationRequestEntity req, string option)
        {
            Queue<AppTaxEntry> entries = new Queue<AppTaxEntry>();

            var taxInfo = req.Data["APP_TAX_BOARD_SECTION"].Data;
            int count = int.Parse(taxInfo.TryGetString("APP_TAX_BOARD_SECTION_TOTAL", "0"));
            for (int i = 0; i < count; i++)
            {
                //if (taxInfo.TryGetString("APP_RADIO_SUBMIT_OPTION_" + i, "") != option) continue;
                if (taxInfo.TryGetString("APP_TAX_TYPE_OPTION_" + i, "") != option) continue;

                AppTaxEntry entry = new AppTaxEntry();
                //entry.Option = taxInfo.TryGetString("APP_RADIO_SUBMIT_OPTION_" + i, "");
                entry.Option = taxInfo.TryGetString("APP_TAX_TYPE_OPTION_" + i, "");
                entry.Amount = taxInfo.TryGetString("APP_TAX_AMOUNT_" + i, "");
                entry.Centimeter = taxInfo.TryGetString("APP_TAX_CENTIMETER_" + i, "");
                entry.Location = taxInfo.TryGetString("APP_TAX_LOCATION_" + i, "");
                entry.Long = taxInfo.TryGetString("APP_TAX_LONG_" + i, "");
                entry.Other = taxInfo.TryGetString("APP_TAX_OTHER_" + i, "");
                entry.Text = taxInfo.TryGetString("APP_TAX_TEXT_" + i, "");
                entry.Width = taxInfo.TryGetString("APP_TAX_WIDTH_" + i, "");

                entries.Enqueue(entry);
            }

            return entries;
        }

        private class AppTaxEntry
        {
            public string Option { get; set; }
            public string Width { get; set; }
            public string Long { get; set; }
            public string Centimeter { get; set; }
            public string Amount { get; set; }
            public string Text { get; set; }
            public string Location { get; set; }
            public string Other { get; set; }
            /*
             * 
             "APP_RADIO_SUBMIT_OPTION_0": "1",
					"APP_TAX_WIDTH_0": "1",
					"APP_TAX_LONG_0": "500",
					"APP_TAX_CENTIMETER_0": "500",
					"APP_TAX_AMOUNT_0": "1",
					"APP_TAX_TEXT_0": "input ข้อความหรือภาพหรือเครื่องหมายที่ปรากฏในป้ายโดยย่อ",
					"APP_TAX_LOCATION_0": "input สถานที่ติดตั้งป้ายและวันติดตั้ง(แสดงป้าย) ถนน,ตรอก,ซอย,แขวง,เขต สถานที่ใกล้เคียงหรือระหว่าง ก.ม.ที่ input สถานที่ติดตั้งป้ายและวันติดตั้ง(แสดงป้าย) ถนน,ตรอก,ซอย,แขวง,เขต สถานที่ใกล้เคียงหรือระหว่าง ก.ม.ที่ input สถานที่ติดตั้งป้ายและวันติดตั้ง(แสดงป้าย) ถนน,ตรอก,ซอย,แขวง,เขต สถานที่ใกล้เคียงหรือระหว่าง ก.ม.ที่",
					"APP_TAX_OTHER_0": "input หมายเหตุ",
					"ARR_IDX_0": "1",
					"IS_EDIT_0": "true",
					"CUSREQ_0": "true",
					"ARR_ITEM_ID_0": "1538755102357",
             */
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
