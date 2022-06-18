using BizPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinqKit;
using BizPortal.Models;
using System.Data.Entity;

namespace BizPortal.Areas.WebApi.Controllers
{

    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_OPDC_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_ORG_ADMIN_NAME)]
    public class MembersController : ApiControllerBase
    {
        [HttpPost]
        [Route("Api/Members/Search")]
        public DataTablesResult<MemberViewModel> List(MemberDataTables model)
        {
            var result = new DataTablesResult<MemberViewModel>();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var query = DB.Users.Include(e=>e.Roles).AsExpandable();

            if (isOrgAdmin)
            {
                // สิทธิ์ org admin จะดู user ได้แค่ในหน่วยงานตัวเองและ user type ที่เป็นประชาชน กับเจ้าหน้าที่รัฐสิทธิ์ org agent เท่านั้น
                var userTypeCitizen = UserTypeEnum.Citizen.GetEnumStringValue();
                var orgAgentRoleId = DB.Roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_AGENT_NAME).Select(e => e.Id).FirstOrDefault();

                query = query.Where(e => (e.UserType == userTypeCitizen) || (e.OrgCode == OrganizationID && e.Roles.Any(a => a.RoleId == orgAgentRoleId)));
            }

            result.Draw = model.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); 

            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = PredicateBuilder.New<ApplicationUser>(true);

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                    predicate = predicate.And(o => o.UserName.Contains(keyword) || o.Email.Contains(keyword) || o.FullName.Contains(keyword) || o.Firstname.Contains(keyword)
                        || o.Lastname.Contains(keyword) || o.CitizenID.Contains(keyword) || o.PassportID.Contains(keyword) || o.JuristicID.Contains(keyword));
                }

                query = query.Where(predicate);
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                query = query.Where(o => o.Roles.Where(p => p.RoleId == model.Role).Any());
            }

            if (!string.IsNullOrEmpty(model.OrgCode))
            {
                query = query.Where(o => o.OrgCode == model.OrgCode);
            }

            if (!string.IsNullOrEmpty(model.UserType))
            {
                query = query.Where(o => o.UserType == model.UserType);
            }

            var selectedQuery = query.Select(o => new MemberViewModel()
            {
                Username = o.UserName,
                FullName = o.FullName,
                RoleIds = o.Roles.Select(p => p.RoleId),
                Id = o.Id,
                UserType = o.UserType,
                LockoutEndDateUtc = o.LockoutEndDateUtc,
                OrgName = o.Organization.OrganizationTranslations.Where(p => p.Language.TwoLetterISOLanguageName == "th").FirstOrDefault().OrgName
            });

            result.RecordsFiltered = query.Select(o => o.Id).Count();

            result.Data = model.GenerateSearchQuery(selectedQuery, "Username").ToList();

            if (result.RecordsFiltered > 0)
            {
                var roles = DB.Roles.Select(o => new { o.Id, o.Description }).ToArray();

                foreach (var item in result.Data)
                {
                    if (item.RoleIds != null && item.RoleIds.Count() > 0)
                    {
                        item.RoleDescriptions = roles.Where(o => item.RoleIds.Contains(o.Id)).Select(o => o.Description).ToArray();
                    }
                }
            }

            return result;
        }
    }
}
