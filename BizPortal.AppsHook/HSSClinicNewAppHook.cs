﻿using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps.HSSStandard;
using BizPortal.ViewModels.SingleForm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mapster;
using BizPortal.Utils.Helpers;
namespace BizPortal.AppsHook
{
    public class HSSClinicNewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            var canExportStatus = new ApplicationStatusV2Enum[]
            {
                ApplicationStatusV2Enum.COMPLETED
            };

            return canExportStatus.Contains(status);
        }

        public override string GenerateRequestData(Guid applicationrequestId)
        {
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var exportRequest = new HSSExportData();
            exportRequest.FormRevisionCode = request.FormRevisionCode;
            exportRequest.FormRevisionName = request.FormRevisionName;
            exportRequest.ApplicationRequestVersion = request.ApplicationRequestVersion;
            exportRequest.IdentityID = request.IdentityID;
            exportRequest.IdentityName = request.IdentityName;
            exportRequest.IdentityType = request.IdentityType.ToString();
            exportRequest.ApplicationID = request.ApplicationID;
            exportRequest.RequestBatchID = request.RequestBatchID.ToString();
            exportRequest.Fee = request.Fee;
            exportRequest.EMSFee = request.EMSFee.ToString();
            exportRequest.DueDateForPayFee = request.DueDateForPayFee;
            exportRequest.Duration = request.Duration;
            exportRequest.ProvinceID = request.ProvinceID;
            exportRequest.AmphurID = request.AmphurID;
            exportRequest.TumbolID = request.TumbolID;
            exportRequest.Province = request.Province;
            exportRequest.Amphur = request.Amphur;
            exportRequest.Tumbol = request.Tumbol;
            exportRequest.SourceIPAddress = request.SourceIPAddress;
            exportRequest.OrgCode = request.OrgCode;
            exportRequest.OrgNameTH = request.OrgNameTH;
            exportRequest.OrgAddress = request.OrgAddress;
            exportRequest.PermitName = request.PermitName;
            exportRequest.BusinessId = request.BusinessId;
            exportRequest.BusinessName = request.BusinessName;
            exportRequest.AppSysName = request.AppSysName;
            exportRequest.CreatedDate = request.CreatedDate;
            exportRequest.UpdatedDate = request.UpdatedDate;
            exportRequest.ExpectSLADate = request.ExpectSLADate.ToString();
            exportRequest.UpdatedDateByRequestor = request.UpdatedDateByRequestor;
            exportRequest.UpdatedDateByAgent = request.UpdatedDateByAgent;
            exportRequest.UpdatedByAgent = request.UpdatedByAgent;
            exportRequest.LastRequestorUpdateEmail = request.LastRequestorUpdateEmail;
            exportRequest.isViewed = request.isViewed;
            exportRequest.Status = request.Status.ToString();
            exportRequest.StatusOther = request.StatusOther;
            exportRequest.StatusRemark = request.StatusRemark;
            exportRequest.IsAgentCheckUserCorrection = request.IsAgentCheckUserCorrection;
            exportRequest.StatusBeforeCancel = request.StatusBeforeCancel;
            exportRequest.ApplicationRequestNumberAgent = request.ApplicationRequestNumberAgent;
            exportRequest.ActionReply = request.ActionReply;
            exportRequest.PermitDeliveryAddress = request.PermitDeliveryAddress;
            exportRequest.PermitDeliveryType = request.PermitDeliveryType;
            exportRequest.EMSFeePaymentType = request.EMSFeePaymentType;
            exportRequest.PaymentMethod = request.PaymentMethod;
            exportRequest.PaymentMethodEnabledChoice = request.PaymentMethodEnabledChoice;
            exportRequest.PaymentMethodOrgDetail = request.PaymentMethodOrgDetail;
            exportRequest.PaymentMethodOrgAddress = request.PaymentMethodOrgAddress;
            exportRequest.PaymentMethodOrgTel = request.PaymentMethodOrgTel;
            exportRequest.BillPaymentTypeForPaymentMethod = request.BillPaymentTypeForPaymentMethod;
            exportRequest.PermitDeliveryTypeEnabledChoice = request.PermitDeliveryTypeEnabledChoice;
            exportRequest.PermitDeliveryOrgDetail = request.PermitDeliveryOrgDetail;
            exportRequest.PermitDeliveryOrgAddress = request.PermitDeliveryOrgAddress;
            exportRequest.PermitDeliveryOrgTel = request.PermitDeliveryOrgTel;
            exportRequest.EMSTrackingNumber = request.EMSTrackingNumber;
            exportRequest.WaitingApproveDateTime = request.WaitingApproveDateTime;
            exportRequest.CheckApproveDateTime = request.CheckApproveDateTime;
            exportRequest.PendingApproveDateTime = request.PendingApproveDateTime;
            exportRequest.PaidFeeApproveDateTime = request.PaidFeeApproveDateTime;
            exportRequest.CreateLicenseApproveDateTime = request.CreateLicenseApproveDateTime;
            exportRequest.RejectDateTime = request.RejectDateTime;
            exportRequest.NoDocument = request.NoDocument;
            exportRequest.TransactionCode = request.TransactionCode;
            exportRequest.TransactionDate = request.TransactionDate;
            exportRequest.DataFiltered = request.DataFiltered;
            exportRequest.DataExcluded = request.DataExcluded;
            exportRequest.Remark = request.Remark;
            exportRequest.RequestedFiles = request.RequestedFiles;
            // public List<object> GovFiles = request.
            exportRequest.RequestedFiles = request.RequestedFiles;
            exportRequest.EPermitFiles = request.EPermitFiles;
            //  public List<object> BillPaymentFiles = request.
            exportRequest.PermitCanBeDeliveredOnPayment = request.PermitCanBeDeliveredOnPayment;
            exportRequest.UserCanGetAppDate = request.UserCanGetAppDate;
            exportRequest.UserCanGetAppDateEnd = request.UserCanGetAppDateEnd;
            exportRequest.ExpectedFinishDate = request.ExpectedFinishDate;
            exportRequest.LastUpdatedFrom = request.LastUpdatedFrom;
            exportRequest.isDone = request.isDone;
            exportRequest.ApplicationRequestID = request.ApplicationRequestID.ToString();
            exportRequest.ApplicationRequestNumber = request.ApplicationRequestNumber;
            exportRequest.ApplicationRequestRunningNumber = request.ApplicationRequestRunningNumber;
            exportRequest.Chats = request.Chats;
            var generalInfo = request.Data.TryGetData("GENERAL_INFORMATION");
            var juristicAddressInfo = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION");
            var commiteeInfo = request.Data.TryGetData("COMMITTEE_INFORMATION");
            var citizenAddressInfo = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION");
            var currentAddress = request.Data.TryGetData("CURRENT_ADDRESS");
            var requestorInfo = request.Data.TryGetData("REQUESTOR_INFORMATION__HEADER");
            var infoStore = request.Data.TryGetData("INFORMATION_STORE");
            var clinicOwnedOparetor = request.Data.TryGetData("APP_CLINIC_OWNED_OPARETOR_SECTION");
            var clinicOwnedConfirm = request.Data.TryGetData("APP_CLINIC_OWNED_CONFIRM_SECTION");
            var clinicLicenseInfo = request.Data.TryGetData("APP_CLINIC_LICENSE_INFO_SECTION");
            var clinicLicenseDetail = request.Data.TryGetData("APP_CLINIC_LICENSE_DETAIL_SECTION");
            var clinicLicenseInfo2 = request.Data.TryGetData("APP_CLINIC_LICENSE_INFO_SECTION_2");
            var clinicPlanInfo = request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION");
            var clinicInfo = request.Data.TryGetData("APP_CLINIC_INFO_SECTION");



            #region GENERAL_INFORMATION
            exportRequest.Data = new HSSAppData();
            exportRequest.Data.GENERAL_INFORMATION = new GENERAL_INFORMATION();
            exportRequest.Data.GENERAL_INFORMATION.Data = new GENERAL_INFORMATION_DATA();
            exportRequest.Data.GENERAL_INFORMATION.GroupName = generalInfo.GroupName;
            exportRequest.Data.GENERAL_INFORMATION.Visible = generalInfo.Visible;
            exportRequest.Data.GENERAL_INFORMATION.Data.INFORMATION_HEADER__REQUEST_DATE = generalInfo.ThenGetStringData("INFORMATION_HEADER__REQUEST_DATE");
            exportRequest.Data.GENERAL_INFORMATION.Data.INFORMATION_HEADER__REQUEST_AT = generalInfo.ThenGetStringData("INFORMATION_HEADER__REQUEST_AT");
            exportRequest.Data.GENERAL_INFORMATION.Data.INFORMATION__REQUEST_AS_OPTION = generalInfo.ThenGetStringData("INFORMATION__REQUEST_AS_OPTION");
            exportRequest.Data.GENERAL_INFORMATION.Data.COMPANY_NAME_TH = generalInfo.ThenGetStringData("COMPANY_NAME_TH");
            exportRequest.Data.GENERAL_INFORMATION.Data.COMPANY_NAME_EN = generalInfo.ThenGetStringData("COMPANY_NAME_EN");
            exportRequest.Data.GENERAL_INFORMATION.Data.GENERAL_INFORMATION__JURISTIC_TYPE = generalInfo.ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE");
            exportRequest.Data.GENERAL_INFORMATION.Data.REGISTER_DATE = generalInfo.ThenGetStringData("REGISTER_DATE");
            exportRequest.Data.GENERAL_INFORMATION.Data.CHECKBOX_SHOW_COMMITTEE_INFORMATION = generalInfo.ThenGetStringData("CHECKBOX_SHOW_COMMITTEE_INFORMATION");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_CITIZEN_TITLE = generalInfo.ThenGetStringData("DROPDOWN_CITIZEN_TITLE");
            exportRequest.Data.GENERAL_INFORMATION.Data.CITIZEN_NAME = generalInfo.ThenGetStringData("CITIZEN_NAME");
            exportRequest.Data.GENERAL_INFORMATION.Data.CITIZEN_LASTNAME = generalInfo.ThenGetStringData("CITIZEN_LASTNAME");
            exportRequest.Data.GENERAL_INFORMATION.Data.GENERAL_INFORMATION__CITIZEN_AGE = generalInfo.ThenGetStringData("GENERAL_INFORMATION__CITIZEN_AGE");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY = generalInfo.ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY");
            exportRequest.Data.GENERAL_INFORMATION.Data.IDENTITY_ID = generalInfo.ThenGetStringData("IDENTITY_ID");
            exportRequest.Data.GENERAL_INFORMATION.Data.GENERAL_EMAIL = generalInfo.ThenGetStringData("GENERAL_EMAIL");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_CITIZEN_TITLE_TEXT = generalInfo.ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT = generalInfo.ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL = generalInfo.ThenGetStringData("AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL = generalInfo.ThenGetStringData("AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT = generalInfo.ThenGetStringData("AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT = generalInfo.ThenGetStringData("AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_CITIZEN_TITLE = generalInfo.ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_CITIZEN_TITLE_EN = generalInfo.ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE_EN");
            exportRequest.Data.GENERAL_INFORMATION.Data.CITIZEN_FIRSTNAME_EN = generalInfo.ThenGetStringData("CITIZEN_FIRSTNAME_EN");
            exportRequest.Data.GENERAL_INFORMATION.Data.CITIZEN_LASTNAME_EN = generalInfo.ThenGetStringData("CITIZEN_LASTNAME_EN");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE = generalInfo.ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE");
            exportRequest.Data.GENERAL_INFORMATION.Data.BIRTH_DATE = generalInfo.ThenGetStringData("BIRTH_DATE");
            exportRequest.Data.GENERAL_INFORMATION.Data.BIRTH_DATE_AGE = generalInfo.ThenGetStringData("BIRTH_DATE_AGE");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_CITIZEN_TITLE_TEXT = generalInfo.ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.AJAX_DROPDOWN_CITIZEN_TITLE_EN_TEXT = generalInfo.ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE_EN_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT = generalInfo.ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_CITIZEN_TITLE_EN = generalInfo.ThenGetStringData("DROPDOWN_CITIZEN_TITLE_EN");
            exportRequest.Data.GENERAL_INFORMATION.Data.DROPDOWN_CITIZEN_TITLE_EN_TEXT = generalInfo.ThenGetStringData("DROPDOWN_CITIZEN_TITLE_EN_TEXT");

            #endregion
            var ownerType = HSSUtility.GetOwnerType().FirstOrDefault(x => x.Value == exportRequest.Data.GENERAL_INFORMATION.Data.INFORMATION__REQUEST_AS_OPTION).Key.ToString();


            if (ownerType == "1")
            {
                #region CITIZEN_ADDRESS_INFORMATION
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION = new CITIZEN_ADDRESS_INFORMATION();
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data = new CITIZEN_ADDRESS_INFORMATION_DATA();
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.GroupName = citizenAddressInfo.GroupName;
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Visible = citizenAddressInfo.Visible;
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_MOO_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_SOI_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_BUILDING_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_ROOMNO_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_FLOOR_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_ROAD_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_PROVINCE_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_AMPHUR_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_TUMBOL_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_POSTCODE_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_TELEPHONE_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_FAX_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT = citizenAddressInfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT = citizenAddressInfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT = citizenAddressInfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.CHECKBOX_SHOW_CITIZEN_ADDRESS_INFORMATION = citizenAddressInfo.ThenGetStringData("CHECKBOX_SHOW_CITIZEN_ADDRESS_INFORMATION");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_MOBILE_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_MOBILE_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.ADDRESS_EMAIL_CITIZEN_ADDRESS = citizenAddressInfo.ThenGetStringData("ADDRESS_EMAIL_CITIZEN_ADDRESS");
                exportRequest.Data.CITIZEN_ADDRESS_INFORMATION.Data.CITIZEN_EMAIL = citizenAddressInfo.ThenGetStringData("CITIZEN_EMAIL");

                #endregion
            }
            else
            {
                #region JURISTIC_ADDRESS_INFORMATION
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION = new JURISTIC_ADDRESS_INFORMATION();
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data = new JURISTIC_ADDRESS_INFORMATION_DATA();
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.GroupName = juristicAddressInfo.GroupName;
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Visible = juristicAddressInfo.Visible;
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_MOO_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_SOI_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_ROAD_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT = juristicAddressInfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT = juristicAddressInfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT = juristicAddressInfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_FAX_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS");
                exportRequest.Data.JURISTIC_ADDRESS_INFORMATION.Data.ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS = juristicAddressInfo.ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS");

                #endregion
                #region COMMITTEE_INFORMATION
                exportRequest.Data.COMMITTEE_INFORMATION = new COMMITTEE_INFORMATION();
                exportRequest.Data.COMMITTEE_INFORMATION.Data = new COMMITTEE_INFORMATION_DATA();
                exportRequest.Data.COMMITTEE_INFORMATION.GroupName = commiteeInfo.GroupName;
                exportRequest.Data.COMMITTEE_INFORMATION.Visible = commiteeInfo.Visible;
                int commiteeTotal = commiteeInfo.ThenGetIntData("COMMITTEE_INFORMATION_TOTAL");
                exportRequest.Data.COMMITTEE_INFORMATION.Data.COMMITTEE_INFORMATION_TOTAL = commiteeTotal;
                if (commiteeTotal > 0)
                {
                    var commiteeList = new List<COMMITTEE>();
                    for (int i = 0; i < commiteeTotal; i++)
                    {
                        var commitee = new COMMITTEE()
                        {
                            ARR_IDX = commiteeInfo.ThenGetStringData("ARR_IDX_" + i),
                            JURISTIC_COMMITTEE_NUMBER = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_NUMBER_" + i),
                            JURISTIC_COMMITTEE_TITLE = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_TITLE_" + i),
                            JURISTIC_COMMITTEE_NAME = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i),
                            JURISTIC_COMMITTEE_LASTNAME = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i),
                            JURISTIC_COMMITTEE_CITIZEN_ID = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i),
                            COMMITTEE_INFORMATION_CITIZEN_ID = commiteeInfo.ThenGetStringData("COMMITTEE_INFORMATION_CITIZEN_ID_" + i),
                            JURISTIC_COMMITTEE_NATIONALITY_OPTION = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_NATIONALITY_OPTION_" + i),
                            DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT = commiteeInfo.ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT_" + i),
                            JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i),
                            JURISTIC_COMMITTEE_IS_LAWYER_LICENSE_DUEDATE = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_IS_LAWYER_LICENSE_DUEDATE_" + i),
                            DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE = commiteeInfo.ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE_" + i),
                            JURISTIC_COMMITTEE_ACCOUNTING_LICENSE_ID = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_ACCOUNTING_LICENSE_ID_" + i),
                            JURISTIC_COMMITTEE_ACCOUNTING_DUE_DATE = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_ACCOUNTING_DUE_DATE_" + i),
                            COMMITTEE_INFORMATION_PASSPORT_NUMBER = commiteeInfo.ThenGetStringData("COMMITTEE_INFORMATION_PASSPORT_NUMBER-" + i),
                            IS_EDIT = commiteeInfo.ThenGetStringData("IS_EDIT_" + i),
                            CUSREQ = commiteeInfo.ThenGetStringData("CUSREQ_" + i),
                            JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION__RADIO_TEXT = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION__RADIO_TEXT_" + i),
                            JURISTIC_COMMITTEE_NATIONALITY_OPTION__RADIO_TEXT = commiteeInfo.ThenGetStringData("JURISTIC_COMMITTEE_NATIONALITY_OPTION__RADIO_TEXT_" + i),
                            DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE_TEXT = commiteeInfo.ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE_TEXT_" + i),
                            AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL = commiteeInfo.ThenGetStringData("AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_" + i),
                            AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL = commiteeInfo.ThenGetStringData("AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_" + i),
                            AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT = commiteeInfo.ThenGetStringData("AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT_" + i),
                            AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT = commiteeInfo.ThenGetStringData("AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT_" + i),

                        };
                        commiteeList.Add(commitee);
                    }
                    exportRequest.Data.COMMITTEE_INFORMATION.Data.Commitees = commiteeList;
                }
                #endregion
            }


            #region CURRENT_ADDRESS
            exportRequest.Data.CURRENT_ADDRESS = new CURRENT_ADDRESS();
            exportRequest.Data.CURRENT_ADDRESS.Data = new CURRENT_ADDRESS_DATA();
            exportRequest.Data.CURRENT_ADDRESS.GroupName = currentAddress.GroupName;
            exportRequest.Data.CURRENT_ADDRESS.Visible = currentAddress.Visible;
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_MOO_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_MOO_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_SOI_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_SOI_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_BUILDING_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_BUILDING_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_ROOMNO_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_ROOMNO_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_FLOOR_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_FLOOR_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_ROAD_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_ROAD_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_POSTCODE_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_POSTCODE_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_TELEPHONE_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_TELEPHONE_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_TELEPHONE_EXT_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_FAX_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_FAX_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_MOBILE_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_MOBILE_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_EMAIL_CURRENT_INFORMATION__ADDRESS = currentAddress.ThenGetStringData("ADDRESS_EMAIL_CURRENT_INFORMATION__ADDRESS");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS_TEXT = currentAddress.ThenGetStringData("ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS_TEXT");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS_TEXT = currentAddress.ThenGetStringData("ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS_TEXT");
            exportRequest.Data.CURRENT_ADDRESS.Data.ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS_TEXT = currentAddress.ThenGetStringData("ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS_TEXT");
            exportRequest.Data.CURRENT_ADDRESS.Data.CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS_CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE = currentAddress.ThenGetStringData("CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS_CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE");

            #endregion

            exportRequest.Data.REQUESTOR_INFORMATION__HEADER = new REQUESTOR_INFORMATION__HEADER();
            exportRequest.Data.REQUESTOR_INFORMATION__HEADER.GroupName = requestorInfo.GroupName;
            exportRequest.Data.REQUESTOR_INFORMATION__HEADER.Visible = requestorInfo.Visible;
            exportRequest.Data.REQUESTOR_INFORMATION__HEADER.CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION = requestorInfo.ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
            #region INFORMATION_STORE
            exportRequest.Data.INFORMATION_STORE = new INFORMATION_STORE();
            exportRequest.Data.INFORMATION_STORE.Data = new INFORMATION_STORE_DATA();
            exportRequest.Data.INFORMATION_STORE.GroupName = infoStore.GroupName;
            exportRequest.Data.INFORMATION_STORE.Visible = infoStore.Visible;
            exportRequest.Data.INFORMATION_STORE.Data.INFORMATION_STORE_NAME_TH = infoStore.ThenGetStringData("INFORMATION_STORE_NAME_TH");
            exportRequest.Data.INFORMATION_STORE.Data.INFORMATION_STORE_NAME_EN = infoStore.ThenGetStringData("INFORMATION_STORE_NAME_EN");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_MOO_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_SOI_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_ROAD_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_FAX_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_LAT_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_LNG_INFORMATION_STORE__ADDRESS = infoStore.ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.SEARCH_TEXT_GOOGLE_MAP = infoStore.ThenGetStringData("SEARCH_TEXT_GOOGLE_MAP");
            exportRequest.Data.INFORMATION_STORE.Data.CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION = infoStore.ThenGetStringData("CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION");
            exportRequest.Data.INFORMATION_STORE.Data.INFORMATION_STORE_HEALTH_OTHER = infoStore.ThenGetStringData("INFORMATION_STORE_HEALTH_OTHER");
            exportRequest.Data.INFORMATION_STORE.Data.INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE = infoStore.ThenGetStringData("INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT = infoStore.ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT = infoStore.ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.Data.INFORMATION_STORE.Data.ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT = infoStore.ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.Data.INFORMATION_STORE.Data.INFORMATION_STORE_TSICID = infoStore.ThenGetStringData("INFORMATION_STORE_TSICID");
            exportRequest.Data.INFORMATION_STORE.Data.AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE = infoStore.ThenGetStringData("AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE");
            exportRequest.Data.INFORMATION_STORE.Data.CHECKBOX_SHOW_INFORMATION_STORE_NAME = infoStore.ThenGetStringData("CHECKBOX_SHOW_INFORMATION_STORE_NAME");
            exportRequest.Data.INFORMATION_STORE.Data.CHECKBOX_SHOW_INFORMATION_STORE_ADDRESS = infoStore.ThenGetStringData("CHECKBOX_SHOW_INFORMATION_STORE_ADDRESS");
            exportRequest.Data.INFORMATION_STORE.Data.AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE_TEXT = infoStore.ThenGetStringData("AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE_TEXT");

            #endregion

            #region clinicOwnedOparetor
            int ownedTotal = clinicOwnedOparetor.ThenGetIntData("APP_CLINIC_OWNED_OPARETOR_SECTION_TOTAL");
            exportRequest.Data.APP_CLINIC_OWNED_OPARETOR_SECTION = new APP_CLINIC_OWNED_OPARETOR_SECTION();
            exportRequest.Data.APP_CLINIC_OWNED_OPARETOR_SECTION.Data = new APP_CLINIC_OWNED_OPARETOR_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_OWNED_OPARETOR_SECTION.Data.APP_CLINIC_OWNED_OPARETOR_SECTION_TOTAL = ownedTotal;
            if (ownedTotal > 0)
            {
                var ownedList = new List<Owned>();
                for (int i = 0; i < ownedTotal; i++)
                {
                    var owned = new Owned()
                    {
                        APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_AGE = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_AGE_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_ID = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_ID_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_PASSPORT = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_PASSPORT_" + i),
                        ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        ADDRESS_EMAIL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_EMAIL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSE_NUMBER = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSE_NUMBER_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSING_DATE = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSING_DATE_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_DIPLOMA = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_DIPLOMA_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_PLACE_NAME = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_PLACE_NAME_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_LICENSE_NUMBER = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_LICENSE_NUMBER_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_OTHER = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_OTHER_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_OTHER = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_OTHER_" + i),
                        ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS = clinicOwnedOparetor.ThenGetStringData("ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_DAY_TIME_WOKING = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_DAY_TIME_WOKING_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_QUIT_WOKING_DATE = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_QUIT_WOKING_DATE_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_CONFIRM_WOKING = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_CONFIRM_WOKING_" + i),
                        ARR_IDX = clinicOwnedOparetor.ThenGetStringData("ARR_IDX_" + i),
                        IS_EDIT = clinicOwnedOparetor.ThenGetStringData("IS_EDIT_" + i),
                        CUSREQ = clinicOwnedOparetor.ThenGetStringData("CUSREQ_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION__RADIO_TEXT = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION__RADIO_TEXT_" + i),
                        APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION__RADIO_TEXT = clinicOwnedOparetor.ThenGetStringData("APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION__RADIO_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY_TEXT_" + i),
                        ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT_" + i),
                        ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT_" + i),
                        ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE_TEXT = clinicOwnedOparetor.ThenGetStringData("DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE_TEXT_" + i),
                        ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT_" + i),
                        ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT_" + i),
                        ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT = clinicOwnedOparetor.ThenGetStringData("ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT_" + i),
                        ARR_ITEM_ID = clinicOwnedOparetor.ThenGetStringData("ARR_ITEM_ID_" + i),
                    };
                    ownedList.Add(owned);
                }
                exportRequest.Data.APP_CLINIC_OWNED_OPARETOR_SECTION.Data.Owners = ownedList;
            }
            #endregion

            exportRequest.Data.APP_CLINIC_OWNED_CONFIRM_SECTION = new APP_CLINIC_OWNED_CONFIRM_SECTION();
            exportRequest.Data.APP_CLINIC_OWNED_CONFIRM_SECTION.Data = new APP_CLINIC_OWNED_CONFIRM_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_OWNED_CONFIRM_SECTION.GroupName = clinicOwnedConfirm.GroupName;
            exportRequest.Data.APP_CLINIC_OWNED_CONFIRM_SECTION.Visible = clinicOwnedConfirm.Visible;
            exportRequest.Data.APP_CLINIC_OWNED_CONFIRM_SECTION.Data.APP_CLINIC_OWNED_CONFIRM_SECTION_CONFIRM_TRUE_CLINIC_CHECKED_TRUE = clinicOwnedConfirm.ThenGetStringData("APP_CLINIC_OWNED_CONFIRM_SECTION_CONFIRM_TRUE_CLINIC_CHECKED_TRUE");

            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION = new APP_CLINIC_LICENSE_INFO_SECTION();
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION.Data = new APP_CLINIC_LICENSE_INFO_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION.GroupName = clinicLicenseInfo.GroupName;
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION.Visible = clinicLicenseInfo.Visible;
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION.Data.APP_CLINIC_LICENSE_INFO_SECTION_TYPE = clinicLicenseInfo.ThenGetStringData("APP_CLINIC_LICENSE_INFO_SECTION_TYPE");
            #region clinicLicenseDetail
            exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION = new APP_CLINIC_LICENSE_DETAIL_SECTION();
            exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION.Data = new APP_CLINIC_LICENSE_DETAIL_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION.GroupName = clinicLicenseInfo.GroupName;
            exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION.Visible = clinicLicenseInfo.Visible;
            int licenseTotal = clinicLicenseDetail.ThenGetIntData("APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL");
            exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION.Data.APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL = licenseTotal;
            if (licenseTotal > 0)
            {
                var licenseList = new List<License>();
                for (int i = 0; i < licenseTotal; i++)
                {
                    var license = new License()
                    {
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_" + i),
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_" + i),
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_" + i),
                        ARR_IDX = clinicLicenseDetail.ThenGetStringData("ARR_IDX_" + i),
                        IS_EDIT = clinicLicenseDetail.ThenGetStringData("IS_EDIT_" + i),
                        CUSREQ = clinicLicenseDetail.ThenGetStringData("CUSREQ_" + i),
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_TEXT = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_TEXT = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_TEXT_" + i),
                        DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_TEXT = clinicLicenseDetail.ThenGetStringData("DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_TEXT_" + i),
                        ARR_ITEM_ID = clinicLicenseDetail.ThenGetStringData("ARR_ITEM_ID_" + i),
                    };
                    licenseList.Add(license);
                }
                exportRequest.Data.APP_CLINIC_LICENSE_DETAIL_SECTION.Data.Licenses = licenseList;
            }
            #endregion


            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION_2 = new APP_CLINIC_LICENSE_INFO_SECTION_2();
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION_2.Data = new APP_CLINIC_LICENSE_INFO_SECTION_2_DATA();
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION_2.GroupName = clinicLicenseInfo2.GroupName;
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION_2.Visible = clinicLicenseInfo2.Visible;
            exportRequest.Data.APP_CLINIC_LICENSE_INFO_SECTION_2.Data.APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE_INFO_CHECKED_TRUE = clinicLicenseInfo2.ThenGetStringData("APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE_INFO_CHECKED_TRUE");
            #region clinicPlanInfo
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION = new APP_CLINIC_PLAN_INFO_SECTION();
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data = new APP_CLINIC_PLAN_INFO_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.GroupName = clinicPlanInfo.GroupName;
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Visible = clinicPlanInfo.Visible;
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_XRAY_ROOM = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_XRAY_ROOM");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ARTIFICIAL_ROOM = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ARTIFICIAL_ROOM");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_TYPE_OPTION = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_TYPE_OPTION");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_BOOTHS = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_BOOTHS");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_FLOORS = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_FLOORS");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_AREA = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_AREA");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_WIDTH = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_WIDTH");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_LENGTH = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_LENGTH");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_HIGH = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_HIGH");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_PROFESSIONALS = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_PROFESSIONALS");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_CONFIRM_TRUE = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_CONFIRM_TRUE");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_SMALL_ROOM = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_SMALL_ROOM");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_MAJOR_ROOM = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_MAJOR_ROOM");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ACUPUNCTURE_ROOM = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ACUPUNCTURE_ROOM");
            exportRequest.Data.APP_CLINIC_PLAN_INFO_SECTION.Data.APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER = clinicPlanInfo.ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER");

            #endregion


            //clinicInfo
            exportRequest.Data.APP_CLINIC_INFO_SECTION = new APP_CLINIC_INFO_SECTION();
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data = new APP_CLINIC_INFO_SECTION_DATA();
            exportRequest.Data.APP_CLINIC_INFO_SECTION.GroupName = clinicInfo.GroupName;
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Visible = clinicInfo.Visible;
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE = clinicInfo.ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE");
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE = clinicInfo.ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE");
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.APP_CLINIC_INFO_SECTION_OTHER = clinicInfo.ThenGetStringData("APP_CLINIC_INFO_SECTION_OTHER");
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.APP_CLINIC_INFO_SECTION_CONFIRM_CONFIRM_TRUE_FIRST = clinicInfo.ThenGetStringData("APP_CLINIC_INFO_SECTION_CONFIRM_CONFIRM_TRUE_FIRST");
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_TEXT = clinicInfo.ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_TEXT");
            exportRequest.Data.APP_CLINIC_INFO_SECTION.Data.DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE_TEXT = clinicInfo.ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE_TEXT");


            return JsonConvert.SerializeObject(exportRequest, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });
        }

        //ใบขออนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทไม่รับผู้ป่วยไว้ค้างคืน) บุคคลธรรมดา
        public override JObject GenerateELicenseData(Guid applicationrequestId)
        {
            var data = (object)null;
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var files = request.UploadedFiles.Where(e => e.Description == "APP_CLINIC_OWNED_FILE_SECTION").FirstOrDefault();
            var phofilePhotoMeta = files.Files.Where(e => e.FileTypeCode.Contains("APP_CLINIC_OWNED_PHOTO3M")).FirstOrDefault().Adapt<BizPortal.ViewModels.V2.FileMetadata>();

            var licenseNumber = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("Identifier");
            var identityName = request.IdentityName;
            var createDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            var practitionerPhoto = Convert.ToBase64String(phofilePhotoMeta.GetBytes());
            var practitionerPhotoContentType = "image/jpg";
            var practitionerPhotoUrl = "";
            var practitionerPhotoSize = phofilePhotoMeta.FileSize;
            var practitionerPhotoTitle = phofilePhotoMeta.FileName;
            var practitionerQualificationId = "";
            var practitionerQualificationType = "";
            var practitionerQualificationStart = "";
            var professionallicenseNumber = "";
            var characteristicsSanatorium = request.Data.TryGetData("APP_CLINIC_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_TEXT");
            var typeMedicalFacility = request.Data.TryGetData("APP_CLINIC_LICENSE_INFO_SECTION").ThenGetStringData("APP_CLINIC_LICENSE_INFO_SECTION_TYPE");
            var clinicName = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
            var organizationAddressNumber = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
            var organizationMoo = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
            var organizationSoi = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
            var organizationStreet = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
            var organizationCity = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
            var organizationCityValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationDistrict = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
            var organizationDistrictValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationState = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
            var organizationStateValue = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            var organizationPostalCode = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            var organizationCountry = "TH";
            var organizationPhone = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS");
            var organizationFax = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");
            var organizationEmail = request.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
            var permitStartDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitStartDate");
            var permitEndDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndDate");
            var services = request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_XRAY_ROOM") == true ? "ห้องเอกซเรย์," : string.Empty;
            services = services + (request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ARTIFICIAL_ROOM") == true ? "ห้องไตเทียม," : string.Empty);
            services = services + (request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_SMALL_ROOM") == true ? "ห้องผ่าตัดเล็ก," : string.Empty);
            services = services + (request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_MAJOR_ROOM") == true ? "ห้องผ่าตัดใหญ่," : string.Empty);
            services = services + (request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ACUPUNCTURE_ROOM") == true ? "ห้องฝังเข็ม," : string.Empty);
            services = services + (request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetBooleanData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER") == true ? request.Data.TryGetData("APP_CLINIC_PLAN_INFO_SECTION").ThenGetStringData("APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT") + "," : string.Empty);
            services = services.Length > 0 ? services.Substring(0, services.Length - 1) : services;
            int totalDate = 0;
            var getOpenServicesDate = request.Data.TryGetData("APP_CLINIC_LICENSE_DETAIL_SECTION").Data;
            var openServicesDate = string.Empty;
            if (int.TryParse(getOpenServicesDate["APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL"], out totalDate))
            {
                for (int i = 0; i < totalDate; i++)
                {
                    openServicesDate = openServicesDate + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_TEXT_" + i] + " " + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_TEXT_" + i] + "-" + getOpenServicesDate["DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_TEXT_" + i] + ",";
                }
                openServicesDate = openServicesDate.Substring(0, openServicesDate.Length - 1);
            }
            var permitEndYear = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitEndYear");
            var permitDate = request.Data.TryGetData("ELICENSE_INFORMATION").ThenGetStringData("PermitDate");

            switch (request.IdentityType)
            {
                case UserTypeEnum.Citizen:
                    data = new
                    {
                        //ประกอบกิจการสถานพยาบาลคลีนิค
                        DocumentReference = new
                        {
                            extension = new object[]
                            {
                                new {
                                    attr_url = "https://oid.estandard.or.th",
                                    valueOid = new  {
                                    attr_id = "2.16.764.1.4.100.16.5.1.1"
                                    }
                                },
                                new {
                                    attr_url = "VersionNumber",
                                    valueString = new {
                                        attr_value = "1.0"
                                    }
                                }
                            },
                            identifier = new
                            {
                                attr_id = licenseNumber.ToThaiDigit()
                            },
                            status = new
                            {
                                attr_value = "current"
                            },
                            type = new
                            {
                                coding = new
                                {
                                    code = new
                                    {
                                        attr_value = "ส.พ.7"
                                    }
                                }
                            },
                            subject = new
                            {
                                display = new
                                {
                                    attr_value = identityName.ToThaiDigit()
                                }
                            },
                            date = new
                            {
                                attr_value = createDate.ToThaiDigit()
                            },
                            author = new
                            {
                                practitioner = new
                                {
                                    qualification = new
                                    {
                                        identifier = new
                                        {
                                            attr_id = "เลขที่ใบประกอบวิชาชีพ",
                                            value = new
                                            {
                                                attr_value = professionallicenseNumber.ToThaiDigit()
                                            }
                                        },
                                        period = new object[]
                                        {
                                            new
                                            {
                                                attr_value = practitionerQualificationStart.ToThaiDigit()
                                            }
                                        }

                                    }
                                },
                                organization = new
                                {
                                    type = new
                                    {
                                        text = new
                                        {
                                            attr_value = "ประเภทสถานประกอบการ",
                                            extension = new object[]
                                            {
                                                new {
                                                    attr_url = "จำนวนเตียง",
                                                    valueQuantity = new {
                                                        attr_id = ""              //จำนวนเตียง ถ้ามี  ปล.ใน e-form ไม่มี
                                                    }
                                                },
                                                new {
                                                    attr_url = "ประเภทของสถานพยาบาล",
                                                    valueString = new {
                                                        attr_value = typeMedicalFacility.ToThaiDigit()
                                                    }
                                                },
                                                new {
                                                    attr_url = "ลักษณะของสถานพยาบาล",
                                                    valueString = new {
                                                        attr_value = characteristicsSanatorium.ToThaiDigit()
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    name = new
                                    {
                                        attr_value = clinicName.ToThaiDigit()
                                    },
                                    address = new
                                    {
                                        text = "",
                                        line = new object[] {
                                            new {
                                                attr_id = "No.",
                                                attr_value = organizationAddressNumber.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Moo",
                                                attr_value = organizationMoo.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Soi",
                                                attr_value = organizationSoi.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Street",
                                                attr_value = organizationStreet.ToThaiDigit()
                                            },

                                        },
                                        city = new
                                        {
                                            attr_value = organizationCity,
                                            attr_valueString = organizationCityValue
                                        },
                                        district = new
                                        {
                                            attr_value = organizationDistrict,
                                            attr_valueString = organizationDistrictValue
                                        },
                                        state = new
                                        {
                                            attr_value = organizationState,
                                            attr_valueString = organizationStateValue
                                        },
                                        postalCode = new
                                        {
                                            attr_value = organizationPostalCode
                                        },
                                        country = new
                                        {
                                            attr_value = organizationCountry
                                        }
                                    }
                                },
                                healthcareService = new
                                {
                                    display = new
                                    {
                                        attr_value = services   // บริการที่จัดให้มีเพิ่มเติม ถ้ามี
                                    }
                                },
                                telecom = new object[]
                                {
                                    new {
                                        system = new {
                                            attr_value = "phone"
                                        },
                                        value  = new {
                                            attr_value = organizationPhone.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                    new {
                                        system = new {
                                            attr_value = "fax"
                                        },
                                        value  = new {
                                            attr_value = organizationFax.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                     new {
                                        system = new {
                                            attr_value = "email"
                                        },
                                        value  = new {
                                            attr_value = organizationEmail.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                },
                                availableTime = new
                                {
                                    daysOfWeek = new
                                    {
                                        extension = new
                                        {
                                            attr_url = "วันและเวลาที่เปิดทำการ",
                                            valueString = new
                                            {
                                                attr_value = openServicesDate.ToThaiDigit()
                                            }
                                        }
                                    }
                                }
                            },
                            relatesTo = new    // อ้างอิงถึงเลขที่ใบอนุญาตเดิม 
                            {
                                code = new  { 
                                    //ประเภทของความสัมพันธ์ที่เอกสารนี้มีกับใบอนุญาตเดิม 
                                },
                                target = new {
                                    display = ""   //ใบอนุญาตเดิมเลขที่ 
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitDate.ToThaiDigit() //วันเดือนปีที่ออกเอกสาร มีผลการใช้งานเอกสาร
                                    },
                                    end = new
                                    {
                                        attr_value = "๓๑ ธันวาคม พ.ศ. " + permitEndYear.ToThaiDigit()  //วันเดือนปีที่ใบอนุญาตหมดอายุ
                                    }
                                }
                            }
                        }
                    };
                    break;
                case UserTypeEnum.Juristic:
                    data = new
                    {
                        //ประกอบกิจการสถานพยาบาลคลีนิค
                        DocumentReference = new
                        {
                            extension = new object[]
                            {
                                new {
                                attr_url = "https://oid.estandard.or.th",
                                valueOid = new  {
                                    attr_id = "2.16.764.1.4.100.16.5.1.1"
                                    }
                                },
                                new {
                                    attr_url = "VersionNumber",
                                    valueString = new {
                                        attr_value = "1.0"
                                    }
                                }
                            },
                            identifier = new
                            {
                                attr_id = licenseNumber.ToThaiDigit()
                            },
                            status = new
                            {
                                attr_value = "current"
                            },
                            type = new
                            {
                                coding = new
                                {
                                    code = new
                                    {
                                        attr_value = "ส.พ.7"
                                    }
                                }
                            },
                            subject = new
                            {
                                display = new
                                {
                                    attr_value = identityName.ToThaiDigit()
                                }
                            },
                            date = new
                            {
                                attr_value = createDate.ToThaiDigit()
                            },
                            author = new
                            {
                                practitioner = new
                                {
                                    qualification = new
                                    {
                                        identifier = new
                                        {
                                            attr_id = "เลขที่ใบประกอบวิชาชีพ",
                                            value = new
                                            {
                                                attr_value = professionallicenseNumber.ToThaiDigit()
                                            }
                                        },
                                        period = new object[]
                                        {
                                            new
                                            {
                                                attr_value = practitionerQualificationStart.ToThaiDigit()
                                            }
                                        }

                                    }
                                },
                                organization = new
                                {
                                    type = new
                                    {
                                        text = new
                                        {
                                            attr_value = "ประเภทสถานประกอบการ",
                                            extension = new object[]
                                            {
                                                new {
                                                    attr_url = "จำนวนเตียง",
                                                    valueQuantity = new {
                                                        attr_id = ""              //จำนวนเตียง ถ้ามี  ปล.ใน e-form ไม่มี
                                                    }
                                                },
                                                new {
                                                    attr_url = "ประเภทของสถานพยาบาล",
                                                    valueString = new {
                                                        attr_value = typeMedicalFacility.ToThaiDigit()
                                                    }
                                                },
                                                new {
                                                    attr_url = "ลักษณะของสถานพยาบาล",
                                                    valueString = new {
                                                        attr_value = characteristicsSanatorium.ToThaiDigit()
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    name = new
                                    {
                                        attr_value = clinicName.ToThaiDigit()
                                    },
                                    address = new
                                    {
                                        text = "",
                                        line = new object[] {
                                            new {
                                                attr_id = "No.",
                                                attr_value = organizationAddressNumber.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Moo",
                                                attr_value = organizationMoo.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Soi",
                                                attr_value = organizationSoi.ToThaiDigit()
                                            },
                                            new {
                                                attr_id = "Street",
                                                attr_value = organizationStreet.ToThaiDigit()
                                            },

                                        },
                                        city = new
                                        {
                                            attr_value = organizationCity.ToThaiDigit(),
                                            attr_valueString = organizationCityValue.ToThaiDigit()
                                        },
                                        district = new
                                        {
                                            attr_value = organizationDistrict.ToThaiDigit(),
                                            attr_valueString = organizationDistrictValue.ToThaiDigit()
                                        },
                                        state = new
                                        {
                                            attr_value = organizationState.ToThaiDigit(),
                                            attr_valueString = organizationStateValue.ToThaiDigit()
                                        },
                                        postalCode = new
                                        {
                                            attr_value = organizationPostalCode.ToThaiDigit()
                                        },
                                        country = new
                                        {
                                            attr_value = organizationCountry.ToThaiDigit()
                                        }
                                    }
                                },
                                healthcareService = new
                                {
                                    display = new
                                    {
                                        attr_value = services.ToThaiDigit()  // บริการที่จัดให้มีเพิ่มเติม ถ้ามี
                                    }
                                },
                                telecom = new object[]
                                {
                                    new {
                                        system = new {
                                            attr_value = "phone"
                                        },
                                        value  = new {
                                            attr_value = organizationPhone.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                    new {
                                        system = new {
                                            attr_value = "fax"
                                        },
                                        value  = new {
                                            attr_value = organizationFax.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                     new {
                                        system = new {
                                            attr_value = "email"
                                        },
                                        value  = new {
                                            attr_value = organizationEmail.ToThaiDigit()
                                        },
                                        use = new {
                                            attr_value = "work"
                                        }
                                    },
                                },
                                availableTime = new
                                {
                                    daysOfWeek = new
                                    {
                                        extension = new
                                        {
                                            attr_url = "วันและเวลาที่เปิดทำการ",
                                            valueString = new
                                            {
                                                attr_value = openServicesDate.ToThaiDigit()
                                            }
                                        }
                                    }
                                }
                            },
                            relatesTo = new    // อ้างอิงถึงเลขที่ใบอนุญาตเดิม 
                            {
                                code = new { 
                                    //ประเภทของความสัมพันธ์ที่เอกสารนี้มีกับใบอนุญาตเดิม 
                                },
                                target = new object[] {
                                    new {
                                        display = ""   //ใบอนุญาตเดิมเลขที่ หาที่เก็บไม่เจอ
                                    }
                                }
                            },
                            context = new
                            {
                                period = new
                                {
                                    start = new
                                    {
                                        attr_value = permitDate.ToThaiDigit() //วันเดือนปีที่ออกเอกสาร มีผลการใช้งานเอกสาร
                                    },
                                    end = new
                                    {
                                        attr_value = "๓๑ ธันวาคม พ.ศ. " + permitEndYear.ToThaiDigit()  //วันเดือนปีที่ใบอนุญาตหมดอายุ
                                    }
                                }
                            }
                        }
                    };
                    break;
            }

            return JObject.FromObject(data);
        }
    }
}

