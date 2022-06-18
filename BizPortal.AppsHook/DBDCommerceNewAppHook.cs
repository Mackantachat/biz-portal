using BizPortal.DAL.MongoDB;
using BizPortal.Integrated;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps.DBDStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class DBDCommerceNewAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 50;
        }

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = true;

            if (currentSectionGroup == "APP_ELECTRONIC_COMMERCIAL")
            {                
                if (model.IdentityType == UserTypeEnum.Juristic)
                {
                    // Frontis: Store juristic type in APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_JURISTIC_TYPE, it will be used in display condition.
                    var reqSection = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION", SectionType.Form);
                    if (reqSection.FormData.TryGetString("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_JURISTIC_TYPE", "") == "")
                    {
                        var profile = IdentityHelper.GetJuristicProfile(model.IdentityID);
                        string juristicType = profile["JuristicType"].DefaultString();
                        reqSection.FormData.AddOrUpdate("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_JURISTIC_TYPE", juristicType);
                    }
                }

            }

            return result;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            var post = new CommerceDataService()
                            {
                                bizReqNo = model.ApplicationRequestID.ToString(),
                                bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                reqNo = model.ApplicationRequestNumber,
                                commerceRegistInfo = new CommerceRegistInfo()
                                {
                                    budgetAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INVESTMENT_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_INVESTMENT_SECTION_AMOUNT"),
                                    // closeDate = null,
                                    commerceNameEN = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                                    commerceNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                    commerceNo = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    commerceStatus = 1,
                                    // delistDate = "25620219",
                                    // model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                    officeCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE"),
                                    ownerType = DBDUtility.GetOwnerType().FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION__REQUEST_AS_OPTION")).Key.ToString(),
                                    //reasonClose = "",
                                    //reasonCloseCode = "",
                                    registerDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_DATE"),
                                    // registerNo = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    // registrarCode = "50120",
                                    //registrarFirstname = "ทะเบียน",
                                    // registrarLastname = "รอบคอบ",
                                    //registrarTitle = "นาย",
                                    //Remarks = "",
                                    startDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION_DATE"),
                                    headOffice = new HeadOffice()
                                    {
                                        amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"),
                                        buildingTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                        buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                        houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                        moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                        postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                        provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"),
                                        roadTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                        roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                                        soiTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                        telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                        tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                                        villageTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_VILLAGE_INFORMATION_STORE__ADDRESS"),
                                        email = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),
                                    },
                                }
                            };
                            #region [Objective]

                            var str = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE");
                            JArray parsedArray = JArray.Parse(str);
                            var objectiveList = new List<Objective>();
                            int j = 1;
                            foreach (JObject parsedObject in parsedArray.Children<JObject>())
                            {
                                var objective = new Objective();
                                foreach (JProperty parsedProperty in parsedObject.Properties())
                                {

                                    string propertyName = parsedProperty.Name;
                                    if (propertyName.Equals("TYPE_TEXT"))
                                    {
                                        objective.description = (string)parsedProperty.Value;

                                    }
                                    if (propertyName.Equals("TYPE"))
                                    {
                                        objective.objectiveCode = (string)parsedProperty.Value;

                                    }
                                }
                                objective.seqNo = j++;
                                objectiveList.Add(objective);
                            }
                            post.commerceRegistInfo.objectives = objectiveList.ToList();

                            #endregion
                            #region [WebSite]
                            var is_electronic = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE_OPTION");

                            if (is_electronic == "ELECTRONIC")
                            {
                                post.commerceRegistInfo.isElectronic = "Y";
                                //headoffice EN
                                post.commerceRegistInfo.headOffice.buildingEN = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("ADDRESS_EN_BUILDING_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN");
                                // post.commerceRegistInfo.headOffice.villageEN = "";
                                post.commerceRegistInfo.headOffice.soiEN = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("ADDRESS_EN_SOI_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN"); ;
                                post.commerceRegistInfo.headOffice.roadEN = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("ADDRESS_EN_ROAD_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN");

                                var webSite = new ECommerceWebsite()
                                {

                                    //email = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("[APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_EMAIL"),
                                    fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("[ADDRESS_EN_FAX_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN"),


                                    Telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("[ADDRESS_EN_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN"),
                                    typeOfBussiness = new List<TypeOfBussiness>()
                                    {
                                        new TypeOfBussiness()
                                        {
                                            description = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE_TEXT"),
                                            seqNo = 1,
                                            typeOfBussinessCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE"),
                                        }
                                    },
                                    webBudget = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_BUDGET"),
                                    //websiteURL = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME"),
                                };
                                var orderMethods = new List<OrderMethod>();

                                int i = 1;
                                foreach (OrderMethodCode method in (OrderMethodCode[])Enum.GetValues(typeof(OrderMethodCode)))
                                {
                                    bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_" + method);
                                    if (is_chk)
                                    {
                                        string description = method.GetEnumStringValue();
                                        int orderMethodCode = (int)method;
                                        if (OrderMethodCode.OTHER == method)
                                        {
                                            description = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_" + method + "_TEXT");
                                        }
                                        var order = new OrderMethod()
                                        {
                                            description = description,
                                            orderMethodCode = orderMethodCode.ToString("D2"),

                                            seqNo = i++
                                        };
                                        orderMethods.Add(order);
                                    }
                                }

                                webSite.orderMethods = orderMethods.ToList();
                                var deliveryMethods = new List<DeliveryMethod>();
                                i = 1;
                                foreach (DeliveryMethodCode method in (DeliveryMethodCode[])Enum.GetValues(typeof(DeliveryMethodCode)))
                                {
                                    bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_" + method);
                                    if (is_chk)
                                    {
                                        string description = method.GetEnumStringValue();
                                        int code = (int)method;
                                        if (DeliveryMethodCode.OTHER == method)
                                        {
                                            description = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_" + method + "_TEXT");
                                        }
                                        var delivery = new DeliveryMethod()
                                        {

                                            deliverMethodCode = code.ToString("D2"),

                                            description = description,
                                            seqNo = i++
                                        };
                                        deliveryMethods.Add(delivery);
                                    }
                                }

                                webSite.deliveryMethods = deliveryMethods.ToList();
                                var paymentMethods = new List<PaymentMethod>();
                                i = 1;
                                foreach (PaymentMethodCode method in (PaymentMethodCode[])Enum.GetValues(typeof(PaymentMethodCode)))
                                {
                                    bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_" + method);
                                    if (is_chk)
                                    {
                                        string description = method.GetEnumStringValue();
                                        int code = (int)method;
                                        if (PaymentMethodCode.OTHER == method)
                                        {
                                            description = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_" + method + "_TEXT");
                                        }
                                        var payment = new PaymentMethod()
                                        {
                                            paymentMethodCode = code.ToString("D2"),

                                            description = description,
                                            seqNo = i++
                                        };
                                        paymentMethods.Add(payment);
                                    }
                                }
                                webSite.paymentMethods = paymentMethods.ToList();
                                webSite.email = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_EMAIL");
                                webSite.urlChannel = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA_OPTION");
                                foreach (URLC_CODE url in (URLC_CODE[])Enum.GetValues(typeof(URLC_CODE)))
                                {
                                    int code = (int)url;
                                    if (webSite.urlChannel == code.ToString("D2"))
                                    {
                                        webSite.websiteURL = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_" + url);

                                    }
                                }

                                post.commerceRegistInfo.webSite = webSite;

                            }
                            else
                            {
                                post.commerceRegistInfo.isElectronic = "N";
                                post.commerceRegistInfo.otherInfo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_OTHER_NON_ELECTRONIC_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_OTHER_NON_ELECTRONIC_SECTION_OTHER");
                            }
                            #endregion
                            #region [Transfer]
                            var is_transfer = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION_IS_TRANSFER_OPTION");

                            // bool is_transfer = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION_IS_TRANSFER_OPTION");
                            if (is_transfer == "YES")
                            {
                                var transfer = new Transfer()
                                {
                                    //titleCode = "001",

                                    amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    oldCommerceName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_COMMERCIAL_NAME"),
                                    postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    refCommerce = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_COMMERCIAL_NUMBER"),
                                    refRegisterNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_REQUEST_NUMBER"),
                                    road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    //transfererAge = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                    //transfererBirthDate = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                    transferDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_DATE"),
                                    transfererFirstName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_FIRSTNAME"),
                                    transfererIdentityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ID_CARD"),
                                    transfererLastName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_LASTNAME"),
                                    transfererNation = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_NATIONALITY"),
                                    //transferRece = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                    transferReason = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_CAUSE"),
                                    transfererTitle = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_TITLE"),
                                    tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                    village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_ADDRESS"),
                                };
                                post.commerceRegistInfo.transfer = transfer;
                            }
                            #endregion
                            #region [Attachs]
                            var attachList = new List<Attach>();
                            int file_index = 1;
                            foreach (FileGroup group in model.UploadedFiles)
                            {
                                foreach (var item in group.Files)
                                {
                                    var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;

                                    string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                    var attach = new Attach()
                                    {
                                        base64Sting = item.GetBased64String(),
                                        contentType = item.ContentType,
                                        description = description,
                                        fileName = item.FileName,
                                        fileSize = item.FileSize.ToString(),
                                        fileType = fileType,
                                        fileId = item.FileID,
                                        seqNo = file_index++.ToString()
                                    };
                                    attachList.Add(attach);
                                }

                            }
                            post.commerceRegistInfo.attachs = attachList.ToList();


                            #endregion
                            #region [Branches]
                            var brancheTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_TOTAL"));
                            if (brancheTotal > 0)
                            {
                                var brancheList = new List<Branch>();
                                for (int i = 0; i < brancheTotal; i++)
                                {
                                    var branche = new Branch()
                                    {
                                        amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        // information = "-",// model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("" + i),
                                        moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                        soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                        village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION_ADDRESS_" + i),
                                    };
                                    brancheList.Add(branche);
                                }
                                post.commerceRegistInfo.branchs = brancheList.ToList();
                            }
                            #endregion
                            #region [Agents]
                            var agentTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_TOTAL"));
                            if (agentTotal > 0)
                            {
                                var agentList = new List<Agent>();
                                for (int i = 0; i < agentTotal; i++)
                                {
                                    var agent = new Agent()
                                    {
                                        agentName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_NAME_" + i),
                                        amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("" + i),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("" + i),
                                        houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                        soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_ฎ)ธ๘APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                        village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION_ADDRESS_" + i),
                                    };
                                    agentList.Add(agent);
                                }
                                post.commerceRegistInfo.agents = agentList.ToList();
                            }
                            #endregion
                            #region [Managers]
                            var managerTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TOTAL"));
                            if (managerTotal > 0)
                            {
                                var managerList = new List<Manager>();
                                for (int i = 0; i < managerTotal; i++)
                                {
                                    var manager = new Manager()
                                    {
                                        age = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetIntData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_BIRTH_DATE_AGE_" + i),
                                        amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        titleCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TITLE_" + i),
                                        nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY_TEXT_" + i),
                                        nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY_" + i),
                                        birthDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_BIRTH_DATE_" + i),
                                        building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),

                                        buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        //fristNameEN = "-",
                                        firstNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_FIRSTNAME_" + i),
                                        houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),


                                        lastNameEN = "-",
                                        lastNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_LASTNAME_" + i),
                                        moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),

                                        postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                        soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),

                                        telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                        tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),

                                        village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS_" + i),
                                    };
                                    if (manager.nationCode == "TH")
                                    {
                                        manager.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ID_CARD_" + i).Replace("-", "");
                                    }
                                    else
                                    {
                                        manager.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                    }
                                    managerList.Add(manager);
                                }
                                post.commerceRegistInfo.managers = managerList.ToList();
                            }
                            #endregion

                            if (post.commerceRegistInfo.ownerType == "1")
                            {
                                var owner = new Owner()
                                {
                                    age = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetIntData("BIRTH_DATE_AGE"),
                                    amphurCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS"),
                                    birthDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateTHStringData("BIRTH_DATE"),
                                    building = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS"),
                                    buildingFloor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS"),
                                    fax = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS"),
                                    firstNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_FIRSTNAME_EN"),
                                    firstNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME"),
                                    houseNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                    identityID = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    lastNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN"),
                                    lastNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME"),
                                    moo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                    nationCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY"),
                                    postCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS"),
                                    provinceCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS"),
                                    race = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE"),
                                    road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                    roomNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS"),
                                    soi = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                    telephone = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS"),
                                    titleCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE"),
                                    tumbonCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS"),
                                    village = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_CITIZEN_ADDRESS"),
                                    email = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_CITIZEN_ADDRESS"),
                                };
                                post.commerceRegistInfo.owner = owner;
                            }
                            #region [Juristic]
                            if (post.commerceRegistInfo.ownerType == "2")
                            {
                                var owner = new Owner()
                                {
                                    age = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetIntData("BIRTH_DATE_AGE"),
                                    amphurCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                                    birthDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateTHStringData("REGISTER_DATE"),
                                    building = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                    buildingFloor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                    fax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS"),
                                    firstNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN"),
                                    firstNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH"),
                                    houseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                    identityID = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    lastNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN"),
                                    lastNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME"),
                                    moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                    nationCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY"),
                                    postCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                    provinceCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                                    race = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE"),
                                    road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                    roomNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"),
                                    soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                    telephone = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                                    titleCode = DBDUtility.GetJuristicTitle(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE")),
                                    tumbonCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                    village = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                                    email = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS"),

                                };
                                post.commerceRegistInfo.owner = owner;
                                #region [Partner]
                                var partnerTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_TOTAL"));
                                if (partnerTotal > 0)
                                {
                                    var partnerList = new List<Partner>();
                                    for (int i = 0; i < partnerTotal; i++)
                                    {
                                        var partner = new Partner()
                                        {
                                            age = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetIntData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_BIRTH_DATE_AGE_" + i),
                                            amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            titleCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_TITLE_" + i),
                                            nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_NATIONALITY_TEXT_" + i),
                                            nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_NATIONALITY_" + i),
                                            birthDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_BIRTH_DATE_" + i),
                                            building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            investAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_AMOUNT_" + i),
                                            //investCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_INVEST_TYPE_" + i),
                                            investCode = DBDUtility.GetInvestCode().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_INVEST_TYPE_TEXT_" + i)).Key.ToString("D2"),
                                            fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            firstName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_FIRSTNAME_" + i),
                                            firstNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_FIRSTNAME_" + i),
                                            houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            status = "1",
                                            // identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ID_CARD_" + i).Replace("-", ""),
                                            //lastNameEN = "-",
                                            lastNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_LASTNAME_" + i),
                                            moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),

                                            postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            race = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_RACE_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                            soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),

                                            telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                            tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),

                                            village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i),
                                        };
                                        if (partner.nationCode == "TH")
                                        {
                                            partner.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_ID_CARD_" + i).Replace("-", "");
                                        }
                                        else
                                        {
                                            partner.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                        }
                                        partnerList.Add(partner);
                                    }
                                    post.commerceRegistInfo.partners = partnerList.ToList();
                                }


                                #endregion
                                #region [Shareholder]
                                var shareholderTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2_TOTAL"));
                                if (shareholderTotal > 0)
                                {
                                    var shareholderList = new List<Shareholder>();
                                    for (int i = 0; i < shareholderTotal; i++)
                                    {
                                        var shareholder = new Shareholder()
                                        {
                                            nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2_NATIONALITY_TEXT_" + i),
                                            nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2_NATIONALITY_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2").ThenGetIntData("ARR_IDX_" + i),
                                            shareAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2_SHARE_" + i),
                                            shareValue = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SHARE_BATH"),
                                        };
                                        shareholderList.Add(shareholder);
                                    }
                                    post.commerceRegistInfo.shareholders = shareholderList.ToList();
                                }

                                #endregion
                            }
                            #endregion
                            #region [Warehouse]
                            var warehouseTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_TOTAL"));
                            if (warehouseTotal > 0)
                            {
                                var warehouseList = new List<Warehouse>();
                                for (int i = 0; i < warehouseTotal; i++)
                                {
                                    var warehouse = new Warehouse()
                                    {
                                        amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        //information = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("" + i),
                                        moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                        soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION_ADDRESS_" + i),
                                    };
                                    warehouseList.Add(warehouse);
                                }
                                post.commerceRegistInfo.warehouses = warehouseList.ToList();
                            }
                            #endregion

                            // Model data
                            string regisUrl = ConfigurationManager.AppSettings["DBD_COMMERCE_REGIS_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }); // Serialize model data to JSON

                            // API Exception
                            Api.OnCheckingApplicationError += (ex) =>
                            {
                                result.Exception = ex;
                                var egaEx = ex as EGAWSAPIException;
                                if (egaEx != null)
                                {
                                    var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                    result.Message = egaEx.ResponseData["Message"].ToString();
                                }
                                else
                                {
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                    result.Message = ex.Message;
                                }
                            };

                            // Call API
                            var apiResult = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                            if (apiResult != null)
                            {
                                if (apiResult.HasValues)
                                {
                                    if (apiResult["messageCode"].ToString() == "2000" && !string.IsNullOrEmpty(apiResult["processId"].ToString()))
                                    {
                                        Dictionary<string, string> respData = new Dictionary<string, string>()
                                        {
                                            { "processId", apiResult["processId"].ToString() }
                                        };

                                        if (request.Data.ContainsKey("DBD_RESPONSE_DATA"))
                                        {
                                            request.Data.Remove("DBD_RESPONSE_DATA");
                                        }
                                        request.Data.Add("DBD_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                        {
                                            GroupName = "DBD_RESPONSE_DATA",
                                            Data = respData
                                        });
                                        result.Success = true;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", result.Success.ToString(), jsonPost);
                                    }
                                    else
                                    {
                                        string error = "messageCode : " + apiResult["messageCode"].ToString() + ",messageDesc : " + apiResult["messageDesc"].ToString();

                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                        throw new Exception(error);
                                    }
                                }
                                else
                                {
                                    string error = "No value";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }
                            }
                            else
                            {
                                string error = "Unable to request to " + regisUrl + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        break;
                    case AppsStage.ApiUpdate:
                        if (model.Extras != null)
                        {
                            var registNo = model.Extras.Where(o => o.Key == "registNo").FirstOrDefault();
                            if (!registNo.IsDefault())
                            {
                                request = request.AddExtraData("APP_ELECTRONIC_COMMERCIAL_EXTRAS", "APP_ELECTRONIC_COMMERCIAL_REGIST_NO", registNo.Value.ToString());
                                request = request.AddExtraData("APP_ELECTRONIC_COMMERCIAL_EXTRAS", "APP_ELECTRONIC_COMMERCIAL_COMMERCE_NO", model.IdentityID);
                            }
                        }
                        result.Success = true;
                        break;
                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                var post = new CommerceAddAttach()
                                {
                                    bizReqNo = model.ApplicationRequestID.ToString(),
                                    reqNo = model.ApplicationRequestNumber,
                                    bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                    //remark = model.Remark.ToString(),
                                };
                                #region [Attachs]
                                var attachList = new List<Attach>();
                                // int file_index = 1;
                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    foreach (var item in requestedFiles.Files)
                                    {

                                        // var seqNo = item.Extras.ContainsKey("SEQNO") ? item.Extras["SEQNO"].ToString() : string.Empty;
                                        var fileTypeDesc = item.Extras.ContainsKey("FILETYPEDESC") ? item.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                        var fileType = item.Extras.ContainsKey("FILETYPE") ? item.Extras["FILETYPE"].ToString() : string.Empty;
                                        var fileTypeCode = DBDUtility.GetFileTypeRef().FirstOrDefault(x => x.Key.ToString() == fileType).Value.ToString();
                                        var description = ResourceHelper.GetResourceWordWithDefault(fileTypeCode, "Apps_SingleForm_Filelist", fileTypeCode).Replace(": ({0} {1} {2})", "");
                                        var fileIdOld = item.Extras.ContainsKey("FILEID") ? item.Extras["FILEID"].ToString() : string.Empty;
                                        //string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                        var attach = new Attach()
                                        {
                                            base64Sting = item.GetBased64String(),
                                            contentType = item.ContentType,
                                            description = fileTypeDesc == string.Empty ? description : fileTypeDesc,
                                            fileName = item.FileName,
                                            fileSize = item.FileSize.ToString(),
                                            fileType = fileType.ToString(),//fileType,
                                            fileId = fileIdOld == string.Empty ? item.FileID : fileIdOld,
                                            //seqNo = seqNo.ToString()
                                        };
                                        attachList.Add(attach);
                                    }

                                }
                                post.attachs = attachList.ToList();


                                #endregion
                                string regisUrl = ConfigurationManager.AppSettings["DBD_COMMERCE_UPDATE_WS_URL"];
                                var jsonPost = JsonConvert.SerializeObject(post,
                                    Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    }); // Serialize model data to JSON
                                //jsonPost = null;
                                // API Exception
                                Api.OnCheckingApplicationError += (ex) =>
                                {
                                    result.Exception = ex;
                                    var egaEx = ex as EGAWSAPIException;
                                    if (egaEx != null)
                                    {
                                        var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                        result.Message = egaEx.ResponseData["Message"].ToString();
                                    }
                                    else
                                    {
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                        result.Message = ex.Message;
                                    }
                                };
                                // Call API
                                var apiResult = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                                if (apiResult != null)
                                {
                                    if (apiResult.HasValues)
                                    {
                                        if (apiResult["messageCode"].ToString() == "2000" && !string.IsNullOrEmpty(apiResult["processId"].ToString()))
                                        {
                                            Dictionary<string, string> respData = new Dictionary<string, string>()
                                            {
                                                { "processId", apiResult["processId"].ToString() }
                                            };

                                            if (request.Data.ContainsKey("DBD_RESPONSE_DATA"))
                                            {
                                                request.Data.Remove("DBD_RESPONSE_DATA");
                                            }
                                            request.Data.Add("DBD_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                            {
                                                GroupName = "DBD_RESPONSE_DATA",
                                                Data = respData
                                            });
                                            result.Success = true;
                                        }
                                        else
                                        {
                                            string error = "messageCode : " + apiResult["messageCode"].ToString() + ",messageDesc : " + apiResult["messageDesc"].ToString();
                                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                            throw new Exception(error);
                                        }
                                    }
                                    else
                                    {
                                        string error = "No value";
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                        throw new Exception(error);
                                    }
                                }
                                else
                                {
                                    string error = "Unable to request to " + regisUrl + ".";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }
                            }
                            else
                            {
                                result.Success = true;
                            }
                        }
                        break;
                    case AppsStage.None:
                    case AppsStage.AgentUpdate:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
                result.Success = false;
            }

            return result;
        }
    }
}
