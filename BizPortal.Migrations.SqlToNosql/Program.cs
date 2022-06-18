using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BizPortal.Migrations.SqlToNosql
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var oldRequests = context.JuristicApplicationStatusRequests
                .Where(o => !o.MigratedToMongoDB)
                .Include(o => o.Application)
                .ToList();
            foreach (var oldR in oldRequests)
            {
                var newR = new ApplicationRequestTransactionEntity()
                {
                    IdentityID = oldR.JuristicID,
                    IdentityType = UserTypeEnum.Juristic,
                    ApplicationID = oldR.ApplicationID,
                    OrgCode = oldR.Application.OrgCode,
                    CreatedDate = oldR.CreatedDate,
                    UpdatedDate = oldR.UpdatedDate ?? oldR.CreatedDate,
                    StatusOther = oldR.ApplicationStatusOther,
                    Remark = oldR.Remark,
                    TransactionCode = oldR.TransactionCode,
                    Data = new Dictionary<string, ApplicationRequestDataGroupEntity>()
                };

                if (oldR.ApplicationStatusID != null)
                {
                    if (oldR.ApplicationStatusID == 1 || oldR.ApplicationStatusID == 5)
                    {
                        newR.Status = ApplicationStatusV2Enum.PENDING;

                        if (oldR.ApplicationStatusID == 5)
                        {
                            newR.StatusOther = oldR.ApplicationStatusOther;
                        }
                    }
                    else if (oldR.ApplicationStatusID == 2 || oldR.ApplicationStatusID == 3)
                    {
                        newR.Status = ApplicationStatusV2Enum.REJECTED;
                    }
                    else if (oldR.ApplicationStatusID == 4)
                    {
                        newR.Status = ApplicationStatusV2Enum.COMPLETED;
                    }
                    else
                    {
                        newR.Status = ApplicationStatusV2Enum.DRAFT;
                    }
                }

                var user = context.Users.Where(o => o.JuristicID == oldR.JuristicID).FirstOrDefault();
                if (user != null)
                {
                    newR.IdentityType = EnumUtils.GetEnum<UserTypeEnum>(user.UserType, true);
                }

                // Legacy Datas
                DateTime transformedDate = DateTime.Now;
                ApplicationRequestDataGroupEntity legacy = new ApplicationRequestDataGroupEntity()
                {
                    Visible = false
                };
                legacy.Data.Add(ExtraKeyEnum.LEGACY_VERSION.ToString(), "1.0.0");
                legacy.Data.Add(ExtraKeyEnum.LEGACY_TRANSFORMED_DATE.ToString(), transformedDate.ToString());
                legacy.Data.Add(ExtraKeyEnum.JURISTIC_APP_STATUS_REQUEST_ID.ToString(), oldR.JuristicApplicationStatusRequestID.ToString());
                newR.Data.Add(ExtraKeyEnum.LEGACY.ToString(), legacy);

                oldR.MigratedToMongoDB = true;
                oldR.MigratedDate = transformedDate;
                oldR.MigratedRequestID = newR.Save().ApplicationRequestID;

                context.SaveChanges();
            }
        }
    }
}
