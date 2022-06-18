using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using BizPortal.ViewModels;
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using BizPortal.ViewModels.Report.Page1;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using System.Configuration;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.V2;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using BizPortal.Models;

namespace BizPortal.Areas.Manage.Controllers
{
    public class ReportController : ManageControllerBase
    {
        public ActionResult ReportOverall()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.REPORT_OVERALL.GetEnumStringValue();
            ViewBag.CurrentLanguage = CurrentCulture;

            return View();
        }

        public ActionResult ReportStatistics()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.REPORT_STATISTICS.GetEnumStringValue();
            ViewBag.CurrentLanguage = CurrentCulture;

            return View();
        }

        public ActionResult ReportRole()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.REPORT_STATISTICS.GetEnumStringValue();
            ViewBag.CurrentLanguage = CurrentCulture;


            //var entities = (from a in AspNetRoles
            //                join b in AspNetUserRoles on a.Id equals b.RoleId
            //                join c in AspNetUsers on b.UserId equals c.Id
            //                join d in Organizations on c.OrgCode equals d.OrgCode
            //                orderby a.Order ascending
            //                select new { UserId = c.Id, RoleName = a.Name, RoleDescription = a.Description, c.FullName, OrgName = d.OrgSysName });
            //var x = from r in DB.Roles select r.
            //entities = (from u in DB.Users.Where(x => x.OrgCode==null && x.Roles.Count>0)



            List<ApplicationRole> roles = (from r in DB.Roles
                        select r).ToList();

            //List<ReportAdminDataTable> entities = (from u in DB.Users.Where(x => x.Roles.Count > 0)
            //                                       select new ReportAdminDataTable { UserId = u.Id, Roles = u.Roles.ToList(), FullName = u.FullName, OrgName = u.Organization.OrgSysName }).ToList();

            List<ReportAdminDataTable> entities = (from u in DB.Users.Where(x => x.Roles.Count > 0 &&  !string.IsNullOrEmpty(x.FullName) )
                                                   select new ReportAdminDataTable { UserId = u.Id, RoleId= u.Roles.FirstOrDefault().RoleId, Roles = u.Roles.ToList(), FullName = u.FullName, OrgName = u.Organization.OrgSysName }).ToList();


            ViewBag.Roles = roles;

            return View(entities);

            //return View();
        }

        public ActionResult ReportOnProcessRequest()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.REPORT_ON_PROCESS_REQUEST.GetEnumStringValue();
            ViewBag.CurrentLanguage = CurrentCulture;

            return View();
        }

        public class Apps_Model
        {
            public string Request_Number { get; set; }  //เลขที่คำร้อง
            public string Request_Name { get; set; } //ชื่อใบอนุญาต
            public string ID_Card { get; set; }  //บัตรประชาชน/นิติบุคคล
            public string Request_By { get; set; }  //ชื่อ
            public string Area { get; set; }  //จังหวัด
            public string Request_Date { get; set; }  //วันที่ขอ
            //public DateTime? Finish_Date { get; set; }  //วันที่คาดว่าจะแล้วเสร็จ
            public string Request_Status { get; set; } //สถานะ
            public string Year { get; set; }
            public string Month { get; set; }
            public string OrgName { get; set; }
            public string IdentityType { get; set; }
            public string Province { get; set; }
            public string Amphor { get; set; }
            public decimal? Fee { get; set; }
            public string ExpectedFinishDate { get; set; }
        }
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }


                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }


                    columnIndex++;
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                // removed ignored columns  
                for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                {
                    if (i == 0 && showSrNo)
                    {
                        continue;
                    }
                    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                    {
                        workSheet.DeleteColumn(i + 1);
                    }
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = "";
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = package.GetAsByteArray();
            }

            return result;
        }

        public static byte[] ExportExcel<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        }
        [HttpGet]
        public FileContentResult Export_to_CSV()
        {
            var result = new DataTablesResult<ApplicationRequestSearchResultViewModel>();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var status = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
            };

            List<Apps_Model> Apps = new List<Apps_Model>();
            //var query = MongoFactory.GetApplicationRequestCollection().AsQueryable().Take(50).Where(a=>a.ApplicationID == 480);
            var collection = MongoFactory.GetApplicationRequestCollection();
            var repo = collection.AsQueryable();
            var query = repo.AsQueryable();
            //db = db.Where(a=>Names.Any)

            if (isAdmin)
            {
                // all
            }
            else if (isOrgAdmin)
            {
                var serviceIds = DB.MemberManageServices
                                   .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                   .Select(e => e.ApplicationID)
                                   .ToList();

                // ถ้าไม่ set member manage service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานตัวเองเป็นค่าตั้งต้น
                if (serviceIds.Count > 0)
                {
                    query = query.Where(e => serviceIds.Contains(e.ApplicationID));
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }
            else if (isOrgAgent)
            {
                var services = DB.MemberServices
                                 .Include(e => e.MemberServiceAreas)
                                 .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                 .ToList();

                // ถ้าไม่ set member service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานของตัวเองเป็นค่าตั้งต้น
                if (services.Count > 0)
                {
                    var builder = Builders<ApplicationRequestEntity>.Filter;
                    var filters = new List<FilterDefinition<ApplicationRequestEntity>>();

                    foreach (var service in services)
                    {
                        var filter = builder.Where(e => e.ApplicationID == service.ApplicationID);
                        var filterAreas = new List<FilterDefinition<ApplicationRequestEntity>>();

                        foreach (var area in service.MemberServiceAreas)
                        {
                            if (area.ProvinceID > 0)
                            {
                                var filterArea = builder.Where(e => e.ProvinceID == area.ProvinceID);

                                if (area.DistrictID > 0)
                                {
                                    filterArea &= builder.Where(e => e.AmphurID == area.DistrictID);

                                    if (area.SectionID > 0)
                                    {
                                        filterArea &= builder.Where(e => e.TumbolID == area.SectionID);
                                    }
                                }

                                filterAreas.Add(filterArea);
                            }
                        }

                        if (filterAreas.Count > 0)
                        {
                            filter &= builder.Or(filterAreas);
                        }

                        filters.Add(filter);
                    }

                    query = collection.Find(builder.Or(filters)).ToList().AsQueryable();
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }
            foreach (var x in query)
            {
                //new Technology { Request_ID = x.ApplicationRequestNumber, Request_Name = x.ApplicationRequestNumber };
                Apps.Add(new Apps_Model
                {
                    Request_Number = x.ApplicationRequestNumber,
                    Request_Name = x.PermitName,
                    ID_Card = x.IdentityID,
                    Request_By = x.IdentityName,
                    Area = x.Province == null ? "-" : x.Province + " " + x.Amphur,
                    Request_Date = x.CreatedDate.ToShortDateString(),
                    //Finish_Date = x.ExpectedFinishDate,
                    Request_Status = x.Status.ToString() == "CHECK" ? "ตรวจสอบคำขอเบื้องต้น" :
                                        x.Status.ToString() == "PENDING" ? "อยู่ระหว่างการพิจารณา" :
                                        x.Status.ToString() == "APPROVED_WAITING_PAY_FEE" ? "อนุมัติแล้วรอชำระค่าธรรมเนียม" :
                                        x.Status.ToString() == "PAID_FEE_CREATING_LICENSE" ? "ออกใบอนุญาต" :
                                        x.Status.ToString() == "REJECTED" ? "ยกเลิกการดำเนินการ" :
                                        x.Status.ToString() == "COMPLETED" ? "ยื่นเรื่องเสร็จสมบูรณ์" :
                                        x.Status.ToString() == "FAILED" ? "ยื่นคำร้องไม่สำเร็จ" :
                                        x.Status.ToString() == "CANCELED" ? "ผู้ประกอบการยกเลิก" : "status",
                    Year = x.CreatedDate.Year.ToString(),
                    Month = x.CreatedDate.Month.ToString(),
                    OrgName = x.OrgNameTH,
                    IdentityType = x.IdentityType.ToString() == "Juristic" ? "นิติบุคคล" : "บุคคลธรรมดา",
                    Province = x.Province == null ? "-" : x.Province,
                    Amphor = x.Amphur == null ? "-" : x.Amphur,
                    Fee = x.Fee == null ? 0 : x.Fee,
                    ExpectedFinishDate = x.ExpectedFinishDate == null ? " " : x.ExpectedFinishDate.ToString(),
                });
                //new Technology() { Request_ID = "asdf", Request_Name = "Bill" };
            }
            //List<Apps_Model> technologies = Apps;
            string[] columns = { "Request_Number", "Request_Name", "ID_Card", "Request_By", "Area", "Request_Date", "Request_Status",
                                 "Year","Month","OrgName","IdentityType","Province","Amphor","Fee","ExpectedFinishDate"};
            byte[] filecontent = ExportExcel(Apps, "Apps_Request", true, columns);

            return File(filecontent, ExcelContentType, "Permit_Reports.xlsx");

        }

        public class Excel_Model
        {
            public string Request_Number { get; set; }  //เลขที่คำร้อง
            public string Business_Type { get; set; } //
            public string ID_Card { get; set; }  //
            public string Request_By { get; set; }  //ชื่อ
        }
        public ActionResult DownloadExcel()
        {
            var result = new DataTablesResult<ApplicationRequestSearchResultViewModel>();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var status = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
            };

            var collection = MongoFactory.GetApplicationRequestCollection();
            var repo = collection.AsQueryable();
            var query = repo.AsQueryable();

            if (isAdmin)
            {
                // all
            }
            else if (isOrgAdmin)
            {
                var serviceIds = DB.MemberManageServices
                                   .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                   .Select(e => e.ApplicationID)
                                   .ToList();

                // ถ้าไม่ set member manage service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานตัวเองเป็นค่าตั้งต้น
                if (serviceIds.Count > 0)
                {
                    query = query.Where(e => serviceIds.Contains(e.ApplicationID));
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }
            else if (isOrgAgent)
            {
                var services = DB.MemberServices
                                 .Include(e => e.MemberServiceAreas)
                                 .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                 .ToList();

                // ถ้าไม่ set member service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานของตัวเองเป็นค่าตั้งต้น
                if (services.Count > 0)
                {
                    var builder = Builders<ApplicationRequestEntity>.Filter;
                    var filters = new List<FilterDefinition<ApplicationRequestEntity>>();

                    foreach (var service in services)
                    {
                        var filter = builder.Where(e => e.ApplicationID == service.ApplicationID);
                        var filterAreas = new List<FilterDefinition<ApplicationRequestEntity>>();

                        foreach (var area in service.MemberServiceAreas)
                        {
                            if (area.ProvinceID > 0)
                            {
                                var filterArea = builder.Where(e => e.ProvinceID == area.ProvinceID);

                                if (area.DistrictID > 0)
                                {
                                    filterArea &= builder.Where(e => e.AmphurID == area.DistrictID);

                                    if (area.SectionID > 0)
                                    {
                                        filterArea &= builder.Where(e => e.TumbolID == area.SectionID);
                                    }
                                }

                                filterAreas.Add(filterArea);
                            }
                        }

                        if (filterAreas.Count > 0)
                        {
                            filter &= builder.Or(filterAreas);
                        }

                        filters.Add(filter);
                    }

                    query = collection.Find(builder.Or(filters)).ToList().AsQueryable();
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }



            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "เลขที่ใบคำร้อง"; //ApplicationRequestNumber
            Sheet.Cells["B1"].Value = "ประเภทธุรกิจ"; //BusinessName
            Sheet.Cells["C1"].Value = "บริการ"; //PermitName
            Sheet.Cells["D1"].Value = "เลขที่บัตรประชาชน/นิติบุคคล";  //IdentityID
            Sheet.Cells["E1"].Value = "ชื่อผู้ขอใบอนุญาต"; //IdentityName
            Sheet.Cells["F1"].Value = "วันที่เขียนใบคำขอ"; //CreatedDate
            Sheet.Cells["G1"].Value = "สถานะ"; //Status
            Sheet.Cells["H1"].Value = "ปี";
            Sheet.Cells["I1"].Value = "เดือน";
            Sheet.Cells["J1"].Value = "ชื่อหน่วยงาน"; //OrgNameTH
            Sheet.Cells["K1"].Value = "ประเภทผู้ขออนุญาต"; //IdentityType
            Sheet.Cells["L1"].Value = "จังหวัดของผู้ขออนุญาต";
            Sheet.Cells["M1"].Value = "อำเภอของผู้ขออนุญาต";
            Sheet.Cells["N1"].Value = "จังหวัดของสถานประกอบการ";
            Sheet.Cells["O1"].Value = "อำเภอของสถานประกอบการ";
            Sheet.Cells["P1"].Value = "ค่าธรรมเนียม";
            Sheet.Cells["Q1"].Value = "วันที่คาดว่าสำเร็จ";
            Sheet.Cells["R1"].Value = "วันที่อนุมัติใบอนุญาต";
            Sheet.Cells["S1"].Value = "วันที่ยกเลิกรายการคำขอ";
            Sheet.Cells["T1"].Value = "ขออนุญาตด้วยตนเอง/มอบอำนาจ";

            int row = 2;
            string C_Pr1 = "";
            string C_Am1 = "";
            string J_Pr1 = "";
            string J_Am1 = "";
            string CJ_P = "";
            string CJ_A = "";
            string Request_by = "";
            string Requestor = "";
            foreach (var item in query)
            {

                if(item.IdentityType.ToString() == "Citizen")
                {
                    
                    try
                    { 
                        C_Pr1 = item.Data["CITIZEN_ADDRESS_INFORMATION"].Data["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"].ToString();
                        C_Am1 = item.Data["CITIZEN_ADDRESS_INFORMATION"].Data["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"].ToString();
                    }
                    catch(Exception ex)
                    { 
                        C_Pr1 = "-";
                        C_Am1 = "-";
                    }
                    CJ_P = C_Pr1;
                    CJ_A = C_Am1;
                }
                else if (item.IdentityType.ToString() == "Juristic")
                {
                    try
                    { 
                        J_Pr1 = item.Data["JURISTIC_ADDRESS_INFORMATION"].Data["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"].ToString();
                        J_Am1 = item.Data["JURISTIC_ADDRESS_INFORMATION"].Data["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"].ToString();
                    }
                    catch (Exception ex)
                    { 
                        J_Pr1 = "-";
                        J_Am1 = "-";
                    }
                    CJ_P = J_Pr1;
                    CJ_A = J_Am1;
                }
                else
                {
                    CJ_P = "-";
                    CJ_A = "-";
                }

                try
                {
                    Request_by = item.Data["REQUESTOR_INFORMATION__HEADER"].Data["REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION"].ToString();
                }
                catch(Exception ex)
                {
                    Request_by = "-";
                }

                try
                {
                    Requestor = item.Data["GENERAL_INFORMATION"].Data["INFORMATION__REQUEST_AS_OPTION"].ToString();
                }
                catch (Exception ex)
                {
                    Requestor = "-";
                }

                Sheet.Cells[string.Format("A{0}", row)].Value = item.ApplicationRequestNumber;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.BusinessName;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.PermitName;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.IdentityID;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.IdentityName;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.CreatedDate.ToShortDateString();
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Status.ToString() == "CHECK" ? "ตรวจสอบคำขอเบื้องต้น" :
                                                                item.Status.ToString() == "PENDING" ? "อยู่ระหว่างการพิจารณา" :
                                                                item.Status.ToString() == "APPROVED_WAITING_PAY_FEE" ? "อนุมัติแล้วรอชำระค่าธรรมเนียม" :
                                                                item.Status.ToString() == "PAID_FEE_CREATING_LICENSE" ? "ออกใบอนุญาต" :
                                                                item.Status.ToString() == "REJECTED" ? "ยกเลิกการดำเนินการ" :
                                                                item.Status.ToString() == "COMPLETED" ? "ยื่นเรื่องเสร็จสมบูรณ์" :
                                                                item.Status.ToString() == "FAILED" ? "ยื่นคำร้องไม่สำเร็จ" :
                                                                item.Status.ToString() == "CANCELED" ? "ผู้ประกอบการยกเลิก" : "status";
                Sheet.Cells[string.Format("H{0}", row)].Value = item.CreatedDate.Year;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.CreatedDate.Month;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.OrgNameTH;
                Sheet.Cells[string.Format("K{0}", row)].Value = Requestor;
                Sheet.Cells[string.Format("L{0}", row)].Value = CJ_P;
                Sheet.Cells[string.Format("M{0}", row)].Value = CJ_A;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.Province;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.Amphur;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.Fee;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.ExpectedFinishDate.ToString();
                Sheet.Cells[string.Format("R{0}", row)].Value = item.Status.ToString() == "COMPLETED" ? item.UpdatedDate.ToShortDateString() : "-";
                Sheet.Cells[string.Format("S{0}", row)].Value = item.Status.ToString() == "FAILED" ? item.UpdatedDate.ToShortDateString() : "-";
                Sheet.Cells[string.Format("T{0}", row)].Value = Request_by == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? "ขออนุญาตด้วยตนเอง" : 
                                                                Request_by == "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE" ? "มอบอำนาจ" : "-";
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();

            return View();
        }

    }
}