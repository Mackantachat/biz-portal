using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.Service;
using BizPortal.DAL.MongoDB;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.UnitTest
{
    //[TestClass]
    //public class AgentWorkFlowUnitTest
    //{
    //    [TestMethod]
    //    public void t_step1_first_request_from_user()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = null;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step1_agent_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "จบการทำงานไม่แสดงกล่องต้อตอบ");
    //    }

    //    [TestMethod]
    //    public void t_step1_waiting_agent_read()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step1_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.WAITING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect);
    //    }

    //    [TestMethod]
    //    public void t_step2_waiting_agent_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step2_waiting_user_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step2_agent_action_reply_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step2_user_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect);
    //    }

    //    [TestMethod]
    //    public void t_step3_user_cancel_by_requestor()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE_CANCELED_BY_REQUESTOR.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsFalse(isCorrect);
    //    }

    //    [TestMethod]
    //    public void t_step3_waiting_agent_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step3_waiting_user_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step3_agent_action_reply_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PENDING;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดงฟอร์มจบการทำงาน");
    //    }

    //    [TestMethod]
    //    public void t_step4_waiting_user_paid_fee()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = null;

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step4_waiting_agent_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = null;

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step4_agent_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step4_agent_wait_user_work_and_action_is_adjust()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = ApplicationActionReplyRequestEnum.ADJUST.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step5_agent_decline()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.REJECT;
    //        string actionReply = ApplicationActionReplyRequestEnum.DECLINE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step5_agent_approve()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = ApplicationActionReplyRequestEnum.APPROVE.ToString();

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step5_waiting_agent_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step5_waiting_user_work()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดง form ให้เจ้าหน้าที่ตอบกลับ");
    //    }

    //    [TestMethod]
    //    public void t_step5_completed()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.COMPLETED;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดงสถานะเสร็จสิ้น");
    //    }

    //    [TestMethod]
    //    public void t_step_user_request_cancel()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.REJECTED;
    //        string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_APPROVE_CANCEL;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == true);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == true);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "แสดง form Approve cancel by requestor เพื่อยืนยันหรือปฏิเสธการขอยกเลิก");
    //    }

    //    [TestMethod]
    //    public void t_step_when_agnt_approve_cancel()
    //    {
    //        ApplicationStatusV2Enum status = ApplicationStatusV2Enum.REJECTED;
    //        string statusOther = ApplicationStatusOtherValueConst.DONE;
    //        string actionReply = "";

    //        var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //        List<bool> result = new List<bool>();

    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //        result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //        var oneResult = result.Distinct();
    //        bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //        Assert.IsTrue(isCorrect, "ไม่แสดงฟอร์มใดเลยจบ");
    //    }

    //    //[TestMethod]
    //    //public void t_when_user_not_pay_fee_hide_form_of_agent()
    //    //{
    //    //    ApplicationStatusV2Enum status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //    //    string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //    //    string actionReply = "";
    //    //    string permitDeli

    //    //    var dict = ApplicationStatusService.CheckingWorkFlowStatusForView(status, statusOther, actionReply);

    //    //    List<bool> result = new List<bool>();

    //    //    result.Add((bool)dict[RequestFormStaffConstKey.SHOW_REQUEST_FORM] == false);
    //    //    result.Add((bool)dict[RequestFormStaffConstKey.SHOW_APPROVE_CANCEL_FORM] == false);

    //    //    var oneResult = result.Distinct();
    //    //    bool isCorrect = oneResult.Count() == 1 && oneResult.FirstOrDefault() == true;

    //    //    Assert.IsTrue(isCorrect, "ไม่แสดงฟอร์มใดเลยจบ");
    //    //}
    //}
}
