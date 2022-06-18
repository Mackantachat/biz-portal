using BizPortal.Models;
using BizPortal.Utils;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using EGA.EGA_Development.Util.MailV2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class LabourRegisStatusController : ApiControllerBase
    {
        /*
        // GET: api/ApplicationStatus
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApplicationStatus/5
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST: api/ApplicationStatus
        [HttpPost]
        [Route("api/LabourRegisStatus")]
        public ResponseData<object> LabourRegisStatus(LabourRegisStatus model)
        {
            ModelState.Remove("ApplicationID");
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            model.ApplicationID = int.Parse(ConfigurationManager.AppSettings["LABOUR_EFORM_APPID"]);
            JuristicApplicationStatusRequestSubmitViewModel request = new JuristicApplicationStatusRequestSubmitViewModel()
            {
                JuristicID = model.JuristicID,
                FileIDs = new List<FileMetadata>(),
                ApplicationID = model.ApplicationID,
                Remark = model.RefCode,
                CustomAppStatusID = model.Status ? (int)ApplicationStatusEnum.COMPLETED : (int)ApplicationStatusEnum.REJECTED,
                IsSubmit = true,
                DisableSendingEmail = true
            };

            DateTime transDate = new UnixTimestamp(model.TransactionTimestamp);
            request.CustomTransactionDate = transDate.ToLocalTime();

            var response = request.Save();
            if (response.Type == ResultDataType.Success)
            {
                var juristic = DB.Users.Where(w => w.JuristicID == model.JuristicID).SingleOrDefault();
                string body = @"
<p>เรียน {0}</p>
<p>
ตามที่ท่านได้ส่งสำเนาข้อบังคับเกี่ยวกับการทำงาน ตาม มาตรา 108 แห่งพระราชบัญญัติคุ้มครองแรงงาน พ.ศ. 2541 ให้กรมสวัสดิการและคุ้มครองแรงงาน ตามรหัสอ้างอิง <strong>{1}</strong> นั้น กรมสวัสดิการและคุ้มครองแรงงานได้รับสำเนาข้อบังคับเกี่ยวกับการทำงาน ฉบับดังกล่าวแล้ว
</p>
<p>&nbsp;</p>";
                body = string.Format(body, juristic.FullName, model.RefCode);
                //EmailHelper.SendEmail(model.Email, "แจ้งผลการส่งสำเนาข้อบังคับเกี่ยวกับการทำงาน", true, body, true);
                EmailHelper.SendEmailV2(new MailAddress[] { new MailAddress { Name = model.Email, Address = model.Email } }, 
                    "แจ้งผลการส่งสำเนาข้อบังคับเกี่ยวกับการทำงาน", body, true);
            }
            else if (!DB.Users.Where(w => w.JuristicID == model.JuristicID).Any())
            {
                if (!DB.LabourRegisStatus.Where(o => o.JuristicID == model.JuristicID && o.RefCode == model.RefCode).Any())
                {
                    // ถ้า Failed และไม่มี User อยู่ในระบบ ให้เพิ่มข้อมูลใส่ตารางเก็บรอไว้ก่อน หากอนาคต User สมัครสมาชิก ให้นำข้อมูลนี้ไป Mapping เลย
                    DB.LabourRegisStatus.Add(model);
                    DB.SaveChanges();

                    response.Type = ResultDataType.Success;
                    response.Message = Resources.ApplicationStatusRequests.MSG_ADD_SUCCESS;
                }
                else
                {
                    response.Message = Resources.ApplicationStatusRequests.MSG_DUPLICATE_REF_ID;
                }

            }

            return response;

        }

        /*
        // PUT: api/ApplicationStatus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApplicationStatus/5
        public void Delete(int id)
        {
        }
        */
    }
}
