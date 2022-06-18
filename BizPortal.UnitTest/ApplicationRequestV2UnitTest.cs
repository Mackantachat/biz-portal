using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB;
using System.Collections.Generic;
using System.Linq;


namespace BizPortal.UnitTest
{
    //[TestClass]
    //public class ApplicationRequestV2UnitTest
    //{
    //    private ActionApplicationRequestModel BuildActionAppModel(ApplicationStatusV2Enum status, string actionReply, bool replyFromOrg, string permitDeliveryTye, string statusBeforeCancel, string paymentMethod, string permitDeliveryAddress, string emsTrackingNumber, int totalFee)
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = new ActionApplicationRequestModel();
    //        actionAppRequestModel.Status = status;
    //        actionAppRequestModel.ActionReply = actionReply;
    //        actionAppRequestModel.ReplyFromOrg = replyFromOrg;
    //        actionAppRequestModel.PermitDeliveryType = permitDeliveryTye;
    //        actionAppRequestModel.StatusBeforeCancel = statusBeforeCancel;
    //        actionAppRequestModel.PaymentMethod = paymentMethod;
    //        actionAppRequestModel.PermitDeliveryAddress = permitDeliveryAddress;
    //        actionAppRequestModel.EMSTrackingNumber = emsTrackingNumber;
    //        actionAppRequestModel.TotalFee = totalFee;
    //        return actionAppRequestModel;
    //    }

    //    #region ขั้นตอนที่ 1
    //    [TestMethod]
    //    public void t_step1_user_create_app_request_and_agent_not_read()
    //    {
    //        var actionAppModel = BuildActionAppModel(ApplicationStatusV2Enum.WAITING, null, true, "", "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();

    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_WAITING_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ส่งไปขั้นตอนที่ 2 เจ้าหน้าที่เปิดอ่านคำร้องขอ");
    //    }

    //    [TestMethod]
    //    public void t_step1_case_user_cancel_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string actionReply = ApplicationActionReplyRequestEnum.CANCELED_BY_REQUESTOR.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = "";
    //        string statusBeforeCancel = ApplicationStatusV2Enum.WAITING.ToString();
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, statusBeforeCancel, "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.WAITING.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 ผู้ประกอบการขอทำการยกเลิกคำร้องขอ");
    //    }
    //    #endregion

    //    #region ขั้นตอนที่ 2
    //    [TestMethod]
    //    public void t001_step2_case_staff_reading_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = null;
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
            
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ส่งไปขั้นตอนที่ 2 เจ้าหน้าที่เปิดอ่านคำร้องขอ");
    //    }

    //    [TestMethod]
    //    public void t002_step2_case_staff_approve_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = ApplicationActionReplyRequestEnum.APPROVE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = null;
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CHECK_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ส่งไปขั้นตอนที่ 3 จุดที่ 1 กำลังพิจารณาอนุมัติคำขอ");
    //    }

    //    [TestMethod]
    //    public void t003_step2_case_staff_adjust_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 จุดที่ 3 แก้ไขข้อมูล/ส่งเอกสารเพิ่มเติม");
    //    }

    //    [TestMethod]
    //    public void t004_step2_case_staff_decline_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 ปฏิเสธ(หยุดการทำงานทันที)");
    //    }

    //    [TestMethod]
    //    public void t005_step2_case_user_adjust_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 จุดที่ 1 รอตรวจสอบคำขอและเอกสาร");
    //    }

    //    [TestMethod]
    //    public void t_step2_case_user_cancel_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string actionReply = ApplicationActionReplyRequestEnum.CANCELED_BY_REQUESTOR.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = "";
    //        string statusBeforeCancel = ApplicationStatusV2Enum.CHECK.ToString();
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, statusBeforeCancel, "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.CHECK.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 ผู้ประกอบการขอทำการยกเลิกคำร้องขอ");
    //    }

    //    #endregion

    //    #region ขั้นตอนที่ 3
    //    [TestMethod]
    //    public void t006_step3_case_staff_approve_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string actionReply = ApplicationActionReplyRequestEnum.APPROVE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_PENDING_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ส่งไปขั้นตอนที่ 4 จุดที่ 1 ชำระค่าธรรมเนียม/วางหลักประกัน");
    //    }

    //    [TestMethod]
    //    public void t007_step3_case_staff_adjust_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 3 จุดที่ 1 กำลังพิจารณาอนุมัติคำขอ");
    //    }

    //    [TestMethod]
    //    public void t008_step3_case_staff_reject_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye,"", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 3 ปฏิเสธ(หยุดการทำงานทันที)");
    //    }

    //    [TestMethod]
    //    public void t009_step3_case_user_adjust_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye,"", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 3 จุดที่ 1 กำลังพิจารณาอนุมัติคำขอ");
    //    }

    //    [TestMethod]
    //    public void t_step3_case_user_cancel_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string actionReply = ApplicationActionReplyRequestEnum.CANCELED_BY_REQUESTOR.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = "";
    //        string statusBeforeCancel = ApplicationStatusV2Enum.PENDING.ToString();
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, statusBeforeCancel, "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.PENDING.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 2 ผู้ประกอบการขอทำการยกเลิกคำร้องขอ");
    //    }
    //    #endregion
         
    //    #region ขั้นตอนที่ 4
    //    [TestMethod]
    //    public void t010_step4_case_staff_select_paid_fee_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string actionReply = ApplicationActionReplyRequestEnum.PAID_FEE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_PAY_FEE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ส่งไปขั้นตอนที่ 5 จุดที่ 1 จัดทำใบอนุญาต");
    //    }

    //    [TestMethod]
    //    public void t011_step4_case_staff_decline_app_request()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = true;
    //        string permitDeliveryTye = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 4 ปฏิเสธ(จบการทำงานทันที)");
    //    }

    //    [TestMethod]
    //    public void t012_step4_case_user_paid_fee_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = PermitDeliveryTypeValueConst.BY_MAIL;
    //        string paymentMethod = PaymentMethodValueConst.QR_CODE;
    //        string permitDeliveryAddress = "1/155 หมู่ 5";
    //        string statusBeforeCancel = "";
    //        var actionAppModel = BuildActionAppModel(status, actionReply, replyFromOrg, permitDeliveryTye, statusBeforeCancel, paymentMethod, permitDeliveryAddress, "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 4 และรอพนักงานทำงานต่อ");
    //    }

    //    [TestMethod]
    //    public void t013_step4_case_user_paid_fee_AtOSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = PermitDeliveryTypeValueConst.AT_OSS;
    //        string paymentMethod = PaymentMethodValueConst.BILL_PAYMENT;
    //        string ems = "";
    //        int totalFee = 1000;
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(status,actionReply,replyFromOrg,permitDeliveryTye,"",paymentMethod,"", ems, totalFee);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] == null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 4 และรอพนักงานทำงานต่อ");
    //    }

    //    [TestMethod]
    //    public void t014_step4_case_user_paid_fee_At_Owner_Org()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();
    //        bool replyFromOrg = false;
    //        string permitDeliveryTye = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        string paymentMethod = PaymentMethodValueConst.AT_OWNER_ORG;
    //        string ems = "";
    //        int totalFee = 1000;
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(status,actionReply,replyFromOrg,permitDeliveryTye,"",paymentMethod,"", ems, totalFee);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] == null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 4 และรอพนักงานทำงานต่อ");
    //    }

    //    [TestMethod]
    //    public void t_step4_case_user_select_ownerOrg_and_total_money_is_0()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE, "", false, PermitDeliveryTypeValueConst.AT_OWNER_ORG, "", "", "", "", 0);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_PAY_FEE_DT] != null);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] == null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอน ออกใบอนุญาต และ รอเจ้าหน้าที่ดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t_step4_case_user_select_oss_and_total_money_is_0()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE, "", false, PermitDeliveryTypeValueConst.AT_OSS, "", "", "", "", 0);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_PAY_FEE_DT] != null);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] == null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอน ออกใบอนุญาต และ รอเจ้าหน้าที่ดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t_step4_case_user_select_byMail_and_total_money_is_0()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE, "", false, PermitDeliveryTypeValueConst.BY_MAIL, "", "", "บ้านเหมา", "", 0);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_PAY_FEE_DT] != null);
    //        result.Add(dict[RequestFormStaffConstKey.PERMIT_DELIVERY_ADDRESS] == null);

    //        var oneResult = result.Distinct();
    //        bool isInCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isInCorrect);
    //    }

    //    [TestMethod]
    //    public void t_step4_case_agent_reply_adjust()//เคสนี้ยังไม่มี แต่ทำเผื่อไว้อนาคตเผื่อมี
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE, ApplicationActionReplyRequestEnum.ADJUST.ToString(), true, PermitDeliveryTypeValueConst.AT_OWNER_ORG,"", PaymentMethodValueConst.AT_OWNER_ORG, "", "", 1000);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ขั้นตอนที่ 4 พนักงานส่งให้ผู้ประกอบการแก้ไขช่องทางการชำระเงินและออกใบอนุญาต");
    //    }
    //    #endregion

    //    #region ขั้นตอนที่ 5
    //    [TestMethod]
    //    public void t015_step5_case_staff_select_sent_by_mail()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.SENT_APPLICATION_BY_EMAIL.ToString(),true,PermitDeliveryTypeValueConst.BY_MAIL,"","","", "APE002349",1000);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect, "ขั้นตอนทั้งหมด เสร็จสิ้น");
    //    }

    //    [TestMethod]
    //    public void t016_step5_case_staff_select_decline_by_mail()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.DECLINE.ToString(),true, PermitDeliveryTypeValueConst.BY_MAIL,"","","","",1000);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 แล้วสถานะย่อยเป็น ปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t017_step5_case_staff_select_tell_user_AT_OWNER_ORG()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.TELL_USER_TO_GET_APPLICATION.ToString(),true, PermitDeliveryTypeValueConst.AT_OWNER_ORG,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นรอผู้ประกอบการดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t018_step5_case_staff_select_user_got_app_AT_OWNER_ORG()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.USER_GOT_APPLICATION.ToString(),true, PermitDeliveryTypeValueConst.AT_OWNER_ORG,"","","","",1000);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นเสร็จสิ้น");
    //    }

    //    [TestMethod]
    //    public void t019_step5_case_staff_select_decline_AT_OWNER_ORG()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.DECLINE.ToString(),true, PermitDeliveryTypeValueConst.AT_OWNER_ORG,"","","","",1000);

    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t019_step5_case_staff_select_sent_to_oss_AT_OSS()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.SENT_APPLICATION_TO_OSS.ToString(),true, PermitDeliveryTypeValueConst.AT_OSS,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นรอเจ้าหน้าทีดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t020_step5_case_staff_select_tell_user_AT_OSS()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.TELL_USER_TO_GET_APPLICATION.ToString(),true, PermitDeliveryTypeValueConst.AT_OSS,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นรอผู้ประกอบการดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t021_step5_case_staff_select_user_got_app_AT_OSS()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.USER_GOT_APPLICATION.ToString(),true, PermitDeliveryTypeValueConst.AT_OSS,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นเสร็จสิ้น");
    //    }

    //    [TestMethod]
    //    public void t022_step5_case_staff_select_decline_app_AT_OSS()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.DECLINE.ToString(),true, PermitDeliveryTypeValueConst.AT_OSS,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 และสถานะย่อยจะเป็นปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t022_step5_case_staff_select_user_got_app_AT_OSS()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.USER_GOT_APPLICATION.ToString(), true, PermitDeliveryTypeValueConst.AT_OSS, "", "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 เสร็จสิ้นแบบผู้ประกอบการมารับที่ OSS");
    //    }

    //    [TestMethod]
    //    public void t022_step5_case_staff_select_user_got_app_BY_MAIL()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.SENT_APPLICATION_BY_EMAIL.ToString(),true, PermitDeliveryTypeValueConst.BY_MAIL,"","","", "APE002349",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 เสร็จสิ้นแบบส่งใบทางไปษณีย์");
    //    }

    //    [TestMethod]
    //    public void t022_step5_case_staff_select_user_got_app_AT_OWNER_ORG()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.USER_GOT_APPLICATION.ToString(),true, PermitDeliveryTypeValueConst.AT_OWNER_ORG,"","","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 เสร็จสิ้นแบบผู้ประกอบการมารับที่สำนักงาน");
    //    }

    //    [TestMethod]
    //    public void t023_step5_case_agent_select_sent_app_to_user_by_mail()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationActionReplyRequestEnum.SENT_APPLICATION_BY_EMAIL.ToString(),true, PermitDeliveryTypeValueConst.BY_MAIL,"","","", "APE002349",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add(dict[RequestFormStaffConstKey.APPROVE_CREATE_LICENSE_DT] != null);
    //        result.Add(!string.IsNullOrEmpty((string)dict[RequestFormStaffConstKey.EMS]));

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "อยู่ที่ขั้นตอนที่ 5 เสร็จสิ้นแบบส่งทางไปรษณีและส่งเลขพัสดุไปสแดงที่หน้าจอผู้ประกอบการ");
    //    }
    //    #endregion

    //    #region เจ้าหน้าที่ทำการตรวจสอบการขอยกเลิกของผู้ประกอบการ

    //    [TestMethod]
    //    public void t_step2_case_agent_approve_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.APPROVE_CANCEL.ToString(),true,"", ApplicationStatusV2Enum.CHECK.ToString(),"","","",1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.CHECK.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และ เจ้าหน้าที่ approve ให้");
    //    }

    //    [TestMethod]
    //    public void t_step1_case_agent_approve_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.APPROVE_CANCEL.ToString(), true, "", ApplicationStatusV2Enum.WAITING.ToString(), "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.WAITING.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และ เจ้าหน้าที่ approve ให้");
    //    }

    //    [TestMethod]
    //    public void t_step3_case_agent_approve_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.APPROVE_CANCEL.ToString(), true, "", ApplicationStatusV2Enum.PENDING.ToString(), "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == ApplicationStatusV2Enum.PENDING.ToString());

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และ เจ้าหน้าที่ approve ให้");
    //    }

    //    [TestMethod]
    //    public void t_step2_case_agent_decline_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString(), true, "", ApplicationStatusV2Enum.CHECK.ToString(), "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == "");

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และเจ้าหน้าที่ไม่ยอม approve");
    //    }


    //    [TestMethod]
    //    public void t_step3_case_agent_decline_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString(), true, "", ApplicationStatusV2Enum.PENDING.ToString(), "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == "");

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และเจ้าหน้าที่ไม่ยอม approve");
    //    }

    //    [TestMethod]
    //    public void t_step1_case_agent_decline_cancel()
    //    {
    //        ActionApplicationRequestModel actionAppRequestModel = BuildActionAppModel(ApplicationStatusV2Enum.REJECTED, ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString(), true, "", ApplicationStatusV2Enum.WAITING.ToString(), "", "", "", 1000);
    //        var dict = ApplicationRequestTransactionEntity.GetStatusAndStatusOther(actionAppRequestModel);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[RequestFormStaffConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((string)dict[RequestFormStaffConstKey.STATUS_BEFORE_CANCEL] == "");

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ผู้ประกอบการขอทำการยกเลิกคำร้องขอ และเจ้าหน้าที่ไม่ยอม approve");
    //    }
    //    #endregion
    //}
}
