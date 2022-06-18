using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class MedicineDepartmentController : ApiControllerBase
    {
        // GET: WebApiV2/MedicineDepartment
        [Route("Api/v2/MedicineDepartment/GetDepartment")]
        [HttpGet]
        public object GetDepartment(string ddlVal)
        {

            List<Select2Opt> options = new List<Select2Opt>();

            switch (ddlVal) 
            {
                case "PROFESSION":
                    options.Add(new Select2Opt() {
                        ID = "DEPT-001"
                        ,Text = "สาขากิจกรรมบำบัด"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-002"
                        ,Text = "สาขาการแก้ไขความผิดปกติของการสื่อความหมาย"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-003"
                        ,Text = "สาขาเทคโนโลยีหัวใจและทรวงอก"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-004"
                        ,Text = "สาขารังสีเทคนิค"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-005"
                        ,Text = "สาขาจิตวิทยาคลินิก"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-006"
                        ,Text = "สาขากายอุปกรณ์"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-007"
                        ,Text = "สาขาการแพทย์แผนจีน"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-008"
                        ,Text = "สาขาการกำหนดอาหาร"
                    });      
                    break;
                case "MEDICAL":
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-009"
                        ,Text = "แพทย์"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-010"
                        ,Text = "ทันตแพทย์"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-011"
                        ,Text = "พยาบาล"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-012"
                        ,Text = "กายภาพบำบัด"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-013"
                        ,Text = "เทคนิคการแพทย์"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-014"
                        ,Text = "การแพทย์แผนไทย"
                    });
                    options.Add(new Select2Opt()
                    {
                        ID = "DEPT-015"
                        ,Text = "การแพทย์แผนไทยประยุกต์"
                    });
                    break;
                default:
                    break;
            }

            return new { results = options.ToArray() };
        }

    }
}