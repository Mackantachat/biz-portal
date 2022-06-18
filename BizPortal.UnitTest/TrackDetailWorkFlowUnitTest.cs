using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.Service;
using BizPortal.DAL.MongoDB;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.UnitTest
{
    //[TestClass]
    //public class TrackDetailWorkFlowUnitTest
    //{
    //    [TestMethod]
    //    public void t1_step1_show_cancel_by_requestor_form()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_CANCEL_BY_REQUESTOR] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 1 แสดง form cancel_by_requestor");
    //    }

    //    [TestMethod]
    //    public void t1_step2_show_cancel_by_requestor_form()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_CANCEL_BY_REQUESTOR] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 2 แสดง form cancel_by_requestor");
    //    }

    //    [TestMethod]
    //    public void t1_step3_show_cancel_by_requestor_form()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_CANCEL_BY_REQUESTOR] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 3 แสดง form cancel_by_requestor");
    //    }

    //    [TestMethod]
    //    public void t1_case_request_app_first_time_and_agant_not_read()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = "";
    //        string actionReply = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 1 รอเจ้าหน้าที่เปิดอ่าน");
    //    }

    //    [TestMethod]
    //    public void t2_case_request_app_first_time_and_agant_not_read()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = null;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 1 รอเจ้าหน้าที่เปิดอ่าน");
    //    }

    //    [TestMethod]
    //    public void t_case_request_app_first_time_but_agent_readed()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 2 เจ้าหน้าที่อ่านี่ยละเอียดแล้ว");
    //    }

    //    [TestMethod]
    //    public void t_case_step2_user_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่2 รอผู้ประกอบการดำเนินการและ ActionReply == ADJUST");
    //    }

    //    [TestMethod]
    //    public void t_case_step2_user_cannot_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่2 รอผู้ประกอบการดำเนินการและ ActionReply != ADJUST");
    //    }

    //    [TestMethod]
    //    public void t_case_step2_agent_working()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขัน้ตอนที่ 2 รอเจ้าหน้าที่ดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t_case_step3_agent_working()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 3 รอเจ้าหน้าที่ดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t_case_step3_user_working_cannot_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.EMPTY.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 3 รอผู้ประกอบการดำเนินการและ ActionReply != ADJUST");
    //    }

    //    [TestMethod]
    //    public void t_case_step3_user_working_can_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 3 รอผู้ประกอบการดำเนินการและ ActionReply == ADJUST");
    //    }

    //    [TestMethod]
    //    public void t_case_step4_user_working_can_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 รอผู้ประกอบการดำเนินการและ ActionReply == ADJUST");
    //    }

    //    [TestMethod]
    //    public void t_case_step4_user_working_cannot_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 รอเจ้าหนาที่ดำเนินการ");
    //    }

    //    [TestMethod]
    //    public void t_case_step4_user_got_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 ถูกเจ้าหน้าที่ปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t_case_step4_user_working_but_confirm_paid_fee_when_payment_is_AtOwnerOrg()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        string paymentMethod = PaymentMethodValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, paymentMethod);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 รอเจ้าหนาที่ดำเนินการ แต่แสดงปุ่มดูรายละเอียด");
    //    }

    //    [TestMethod]
    //    public void t_case_step4_user_working_but_confirm_paid_fee_when_payment_is_AtOSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        string paymentMethod = PaymentMethodValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, paymentMethod);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 รอเจ้าหนาที่ดำเนินการ แต่แสดงปุ่มดูรายละเอียด");
    //    }
    //    [TestMethod]
    //    public void t_case_step4_user_working_but_paid_fee()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();
    //        string permitDeliveryType = "";
    //        string paymentMethod = "";
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, paymentMethod);

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 4 รอเจ้าหนาที่ดำเนินการ แต่แสดงปุ่มดูรายละเอียด");
    //    }

    //    [TestMethod]
    //    public void t_case_step1_user_got_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 1 ถูกเจ้าหน้าที่ปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t_case_step2_user_got_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 2 ถูกเจ้าหน้าที่ปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t_case_step3_user_got_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, "", "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 2 ถูกเจ้าหน้าที่ปฏิเสธ");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_user_work_and_permitDelivery_is_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_work_and_permitDelivery_is_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_reject_and_permitDelivery_is_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีแดง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_approve_and_permitDelivery_is_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มขอแสดงความยินดีแบบส่งทางไปรษณีย์");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_approve_and_permitDelivery_is_AT_OWNER_ORG()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มขอแสดงความยินดีแบบให้ผู้ประกอบการมารับที่สำนักงานเขตดุสิต กรุงเทพมหานคร");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_reject_and_permitDelivery_is_AT_OWNER_ORG()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มขอแสดงความยินดีแบบให้ผู้ประกอบการมารับที่สำนักงานเขตดุสิต กรุงเทพมหานคร");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_work_and_permitDelivery_is_AT_OWNER_ORG()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_user_work_and_permitDelivery_is_AT_OWNER_ORG()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_user_work_and_permitDelivery_is_AT_OSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_work_and_permitDelivery_is_AT_OSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีเหลือง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_reject_and_permitDelivery_is_AT_OSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.REJECT);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์มใดเลย แต่แสดงข้อมูลจาก status other และ icon สีแดง");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_html_when_agent_approve_and_permitDelivery_is_AT_OSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มยินดีของ At Oss");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_COMPETED_BY_MAIL()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.COMPLETED;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.BY_MAIL;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มยินดีของ BY_MAIL");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_COMPETED_AT_OWNER_ORG()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.COMPLETED;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OWNER_ORG;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มยินดีของ At OWNER ORG");
    //    }

    //    [TestMethod]
    //    public void t_case_step5_COMPETED_AT_OSS()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.COMPLETED;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";
    //        string permitDeliveryType = PermitDeliveryTypeValueConst.AT_OSS;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.COMPLETED);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.DONE);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 แสดงฟอร์มยินดีของ At OSS");
    //    }

    //    [TestMethod]
    //    public void t_case_user_cancel_and_waiting_agent_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.REJECTED;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL;
    //        string actionReply = "";
    //        string permitDeliveryType = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.REJECTED);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 5 ไม่แสดงฟอร์ม");
    //    }

    //    [TestMethod]
    //    public void t_step1_case_agent_decline_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();
    //        string permitDeliveryType = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.WAITING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 1 แสดงฟอร์มให้ user ทำงานต่อไปปกติ");
    //    }

    //    [TestMethod]
    //    public void t_step2_case_agent_decline_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();
    //        string permitDeliveryType = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.CHECK);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 2 แสดงฟอร์มให้ user ทำงานต่อไปปกติ");
    //    }

    //    [TestMethod]
    //    public void t_step3_case_agent_decline_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();
    //        string permitDeliveryType = null;
    //        var dict = TrackService.CheckingWorkFlowStatusForView(status, statusOther, actionReply, permitDeliveryType, "");

    //        List<bool> result = new List<bool>();
    //        result.Add(dict[CheckingWorkFlowConstKey.STATUS].ToEnumValue() == ApplicationStatusV2Enum.PENDING);
    //        result.Add((string)dict[CheckingWorkFlowConstKey.STATUS_OTHER] == ApplicationStatusOtherValueConst.WAITING_USER_WORKING);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_RESPONSE_FORM] == true);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.SHOW_PAY_FEE_FORM] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_BY_MAIL] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OWNER_ORG] == false);
    //        result.Add((bool)dict[CheckingWorkFlowConstKey.CONGRAT_FORM_AT_OSS] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ขั้นตอนที่ 3 แสดงฟอร์มให้ user ทำงานต่อไปปกติ");
    //    }

    //    [TestMethod]
    //    public void t_calculate_relative_time()
    //    {
    //        DateTime chatTime = new DateTime(2018, 09, 23, 17, 03, 59);
    //        string textTimeAgo = ChatService.CalculateTimeAgo(chatTime);

    //        Assert.IsTrue(!string.IsNullOrEmpty(textTimeAgo));
    //    }
    //}
}
