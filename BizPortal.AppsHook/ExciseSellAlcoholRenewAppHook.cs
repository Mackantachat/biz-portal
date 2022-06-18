using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.StandardApi;

namespace BizPortal.AppsHook
{
    class ExciseSellAlcoholRenewAppHook : StoreBaseAppHook
    {
        public override bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return true;
            }
        }

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }


        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {

            //TODO: Generate pdf here
            string src = serverMapPathFunc("~/Uploads/apps/exise/alcohol/alcohol_permit_template.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            // พื้นที่สาขา
            //model.Add(new PDFFieldValue() { FieldName = "Branch", Value = "???" });

            #region 1. ข้อมูลผู้ประสงค์จะขายสุรา

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", req.Data["GENERAL_INFORMATION"].Data["DROPDOWN_CITIZEN_TITLE_TEXT"], req.Data["GENERAL_INFORMATION"].Data["CITIZEN_NAME"], req.Data["GENERAL_INFORMATION"].Data["CITIZEN_LASTNAME"]);
                model.Add(field);
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = req.Data["GENERAL_INFORMATION"].Data["COMPANY_NAME_TH"];
                model.Add(field);
            }
            

            string identity = req.Data["GENERAL_INFORMATION"].Data["IDENTITY_ID"];
            int digit = 0;
            for (int i = 0; i < identity.Length && digit <= 13; i++)
            {
                if (!IsNumeric(identity.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Identity" + digit, Value = identity.Substring(i, 1), FontSize = 13 });

            }

            string excise = req.Data["SELL_ALCOHOL_INFO"].Data["SELL_ALCOHOL_EXCISE_ID"];
            digit = 0;
            for (int i = 0; i < excise.Length && digit <= 17; i++)
            {
                if (!IsNumeric(excise.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Excise" + digit, Value = excise.Substring(i, 1), FontSize = 13 });

            }

            var addrInfo = req.Data["INFORMATION_STORE"].Data;
            model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"] });

            model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"] });
            if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"]))
                model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"];

            model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["INFORMATION_STORE_EMAIL"] });
            model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo["ADDRESS_FAX_INFORMATION_STORE__ADDRESS"] });

            /*
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_CITIZEN_ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_CITIZEN_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"]))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["CITIZEN_EMAIL"] });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["JURISTIC_EMAIL"] });
            }
            */

            #endregion

            #region 2. วัตถุประสงค์ของการยื่นคำขอรับใบอนุญาต

            var alcoholInfo = req.Data["SELL_ALCOHOL_INFO"].Data;

            // ขายสุราประเภท 1
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"]))
                model.Add(new PDFFieldValue() { FieldName = "Option21", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"]) 
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1"))
                model.Add(new PDFFieldValue() { FieldName = "Option211", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2"))
                model.Add(new PDFFieldValue() { FieldName = "Option212", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY"))
                model.Add(new PDFFieldValue() { FieldName = "Option213", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__OTHER"))
                model.Add(new PDFFieldValue() { FieldName = "Option214", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            // ขายสุราประเภท 2
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"]))
                model.Add(new PDFFieldValue() { FieldName = "Option22", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_FREE"))
                model.Add(new PDFFieldValue() { FieldName = "Option221", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED"))
                model.Add(new PDFFieldValue() { FieldName = "Option222", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED")
            {
                string vat = alcoholInfo["SELL_ALCOHOL_VAT_ID"];
                digit = 0;
                for (int i = 0; i < vat.Length && digit <= 17; i++)
                {
                    if (!IsNumeric(vat.Substring(i, 1))) continue;

                    digit++;
                    model.Add(new PDFFieldValue() { FieldName = "Vat" + digit, Value = vat.Substring(i, 1), FontSize = 13 });
                }
            }

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED"))
                model.Add(new PDFFieldValue() { FieldName = "Option223", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            // ลักษณะของสถานประกอบการขายสุรา
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_PRODUCER"]))
                model.Add(new PDFFieldValue() { FieldName = "Option31", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_DUTY_FREE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option32", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_RESTAURANT"]))
                model.Add(new PDFFieldValue() { FieldName = "Option33", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_DEPARTMENT_STORE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option34", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_ASSOCIATION"]))
                model.Add(new PDFFieldValue() { FieldName = "Option35", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_PUB"]))
                model.Add(new PDFFieldValue() { FieldName = "Option36", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_CONVENIENT_STORE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option37", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_OTHER"]))
                model.Add(new PDFFieldValue() { FieldName = "Option38", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            model.Add(new PDFFieldValue() { FieldName = "Option38Other", Value = alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_OTHER_INFO"] });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_BROTHEL"]))
                model.Add(new PDFFieldValue() { FieldName = "Option39", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_GROCERY"]))
                model.Add(new PDFFieldValue() { FieldName = "Option310", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            
            #endregion
            


            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        private static string CountDocumentText(ApplicationRequestEntity req, string fileTypeCode)
        {
            int count = 0;
            if (req.UploadedFiles != null)
            {
                foreach (var fg in req.UploadedFiles)
                {
                    if (fg.Files == null) continue;

                    count += fg.Files.Count(o => o.FileTypeCode == fileTypeCode);
                }
            }

            return count.ToString();
        }

        private static bool IsTrueValue(string data)
        {
            return ("" + data).ToLower() == "true";
        }

        private static bool IsNumeric(string str)
        {
            int i = 0;
            return int.TryParse(str, out i);
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            var tResult = base.Invoke(stage, model, appHookInfo, ref request);

            var standardData = GenerateStandardFormData(model, request);
            var files = new List<StandardApiFileInfo>();
            foreach (var upload in request.UploadedFiles)
            {
                foreach (var f in upload.Files)
                {
                    if (f.Extras != null && f.Extras.ContainsKey("PREDOC") && f.Extras["PREDOC"].ToString() == "true")
                    {
                        //TODO: Get predoc file here
                        //files.Add(file);
                    }
                    else
                    {
                        var fileMetaModel = new BizPortal.ViewModels.V2.FileMetadata
                        {
                            FileID = f.FileID,
                            FileName = f.FileName,
                            ContentType = f.ContentType,
                            Extras = new Dictionary<string, string>()
                        };
                        if (f.Extras != null)
                        {
                            foreach (var extra in f.Extras)
                            {
                                fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                            }
                        }
                        var file = new StandardApiFileInfo
                        {
                            Name = f.FileTypeCode,
                            Content = fileMetaModel.GetBytes(),
                            FileName = f.FileName,
                            ContentType = f.ContentType
                        };
                        files.Add(file);
                    }
                }

            }
            var result = SendStandardAPIRequest("http://localhost:45598/th/test/TestStandardApi", standardData, files);
            return tResult;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
