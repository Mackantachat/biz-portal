using BizPortal.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using BizPortal.Models.Base;
using System.Data.Entity.Validation;

namespace BizPortal.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        private string _currentUserID;
        private int? _currentLanguageID;

        public string CurrentUserID { get { return this._currentUserID; } }
        public int? CurrentLanguageID { get { return this._currentLanguageID; } }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationTranslation> ApplicationTranslations { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<ApplicationStatusTranslation> ApplicationStatusTranslations { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleTagMapping> ArticleTagMappings { get; set; }
        public virtual DbSet<ArticleTranslation> ArticleTranslations { get; set; }
        public virtual DbSet<ArticleView> ArticleViews { get; set; }
        public virtual DbSet<ArticleApplicationMapping> ArticleApplicationMappings { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual DbSet<CategoryView> CategoryViews { get; set; }
        //public virtual DbSet<Company> Companies { get; set; }
        //public virtual DbSet<CompanyTranslation> CompanyTranslations { get; set; }
        public virtual DbSet<DbdTitle> DbdTitles { get; set; }
        public virtual DbSet<JuristicApplicationStatusRequest> JuristicApplicationStatusRequests { get; set; }
        public virtual DbSet<JuristicApplicationStatusRequestLog> JuristicApplicationStatusRequestLogs { get; set; }
        public virtual DbSet<JuristicRequestView> JuristicRequestViews { get; set; }
        public virtual DbSet<JuristicRequestLogView> JuristicRequestLogViews { get; set; }
        
        public virtual DbSet<FileUpload> FileUploads { get; set; }
        public virtual DbSet<MoiPostalCode> MoiPostalCodes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LabourRegisStatus> LabourRegisStatus { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationTranslation> OrganizationTranslations { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionTranslation> SectionTranslations { get; set; }
        public virtual DbSet<SectionView> SectionViews { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TSICcategory> TSICcategories { get; set; }
        public virtual DbSet<TSICsubCategory> TSICsubCategories { get; set; }
        public virtual DbSet<TSICgroup> TSICgroups { get; set; }
        public virtual DbSet<TSICsubGroup> TSICsubGroups { get; set; }
        public virtual DbSet<TSICcode> TSICcodes { get; set; }
        public virtual DbSet<SSOMinimumWage> SSOMinimumWages { get; set; }
        public virtual DbSet<MemberService> MemberServices { get; set; }
        public virtual DbSet<MemberServiceArea> MemberServiceAreas { get; set; }
        public virtual DbSet<MemberManageService> MemberManageServices { get; set; }
        public virtual DbSet<PaymentCenter> PaymentCenters { get; set; }
        public virtual DbSet<PaymentHomeCostCenter> PaymentHomeCostCenters { get; set; }
        public virtual DbSet<PaymentCatalog> PaymentCatalogs { get; set; }

        public virtual DbSet<DBDCommerceOffice> DBDCommerceOffices { get; set; }
        public virtual DbSet<DBDCommerceObjective> DBDCommerceObjectives { get; set; }
        public virtual DbSet<SerivceCategoryList> SerivceCategoryList { get; set; }
        public virtual DbSet<PermitSummaryReport> PermitSummaryReports { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessGroup> BusinessGroups { get; set; }

        public virtual DbSet<SigningPerson> SigningPersons { get; set; }
        public virtual DbSet<SigningPosition> SigningPositions { get; set; }
        public virtual DbSet<SigningExtendedData> SigningExtendedDatas { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            // http://stackoverflow.com/questions/4596371/what-are-the-downsides-to-turning-off-proxycreationenabled-for-ctp5-of-ef-code-f
            /*
             * Dynamic proxies are used for change tracking and lazy loading. When WCF tries to serialize object, related context is usually closed and disposed but serialization of navigation properties will automatically trigger lazy loading (on closed context) => exception.
             * 
             * If you turn off lazy loading you will need to use eager loading for all navigation properties you want to use (Include on ObjectQuery). Tracking changes doesn't work over WCF it works only for modification of entity which is attached to ObjectContext.
             */
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = false;

            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var iden = HttpContext.Current.User.Identity;
                this._currentUserID = iden.GetUserId();
            }
            this._currentLanguageID = Languages.Where(o => o.TwoLetterISOLanguageName.ToLower() == System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName).Select(o => o.LanguageID).SingleOrDefault(); ;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().Property(e => e.Order).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Properties<decimal>().Configure(prop => prop.HasPrecision(18, 8));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public int SaveChanges(bool resolvePersonalize)
        {
            if (resolvePersonalize)
                ResolvePersonalizeObject();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex);
            }
        }

        public void ResolvePersonalizeObject()
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;

            // http://stackoverflow.com/questions/9178956/how-to-get-freshly-added-entities-from-repository
            var addedObjects = objectContext.ObjectStateManager
                                    .GetObjectStateEntries(EntityState.Added)
                                    .Select(o => o.Entity)
                                    .ToList();

            foreach (var obj in addedObjects)
            {
                Type objType = obj.GetType();

                if (objType.IsSubclassOf(typeof(ManipulateModel)))
                {
                    ManipulateModel model = obj as ManipulateModel;
                    model.CreatedUserID = CurrentUserID;
                    model.CreatedDate = DateTime.Now; //.ToUniversalTime();

                    if (model.IsDeleted)
                    {
                        model.DeletedUserID = CurrentUserID;
                        model.DeletedDate = DateTime.Now; //.ToUniversalTime();
                    }
                }
            }

            var modifiedObjects = objectContext.ObjectStateManager
                                        .GetObjectStateEntries(EntityState.Modified)
                                        .Select(o => o.Entity)
                                        .ToList();
            foreach (var obj in modifiedObjects)
            {
                Type objType = obj.GetType();

                if (objType.IsSubclassOf(typeof(ManipulateModel)))
                {
                    ManipulateModel model = obj as ManipulateModel;

                    if (model.IsDeleted)
                    {
                        model.DeletedUserID = CurrentUserID;
                        model.DeletedDate = DateTime.Now; //.ToUniversalTime();

                        var entry = Entry(model);
                        entry.Property(o => o.UpdatedUserID).IsModified = false;
                        entry.Property(o => o.UpdatedDate).IsModified = false;
                    }
                    else
                    {
                        model.UpdatedUserID = CurrentUserID;
                        model.UpdatedDate = DateTime.Now; //.ToUniversalTime();
                    }
                }
            }
        }

        #region [Methods]
        public void ArticleSetUnlist(int[] ids, bool unlist)
        {
            List<Article> articles = Articles.Where(o => !o.IsDeleted && ids.Contains(o.ArticleID)).ToList();

            foreach (var article in articles)
            {
                article.Unlisted = unlist;
                Entry<Article>(article).State = EntityState.Modified;
            }
        }

        public void ArticleSetOrderList(int[] ids, int[] order)
        {
            List<Article> articles = Articles.Where(o => !o.IsDeleted && ids.Contains(o.ArticleID)).ToList();

            foreach (var article in articles)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (article.ArticleID == ids[i])
                    {
                        article.Ordering = order[i];
                        Entry<Article>(article).State = EntityState.Modified;
                        break;
                    }
                }
            }
        }

        public void ArticleDelete(int[] ids)
        {
            List<Article> articles = Articles.Where(o => !o.IsDeleted && ids.Contains(o.ArticleID)).ToList();

            foreach (var article in articles)
            {
                article.IsDeleted = true;
                Entry<Article>(article).State = EntityState.Modified;
            }
        }
        #endregion

        #region [FileUpload]
        public FileUpload CreateFileUploadData(string systemFileName, string fileName, string absPath,
            string contentType, long contentLength, bool isTemporary)
        {
            FileUpload fUpload = new FileUpload();
            fUpload.FileSysName = systemFileName;
            fUpload.FileName = fileName;
            fUpload.AbsolutePath = absPath;
            fUpload.ContentType = contentType;
            fUpload.ContentLength = contentLength;
            fUpload.TemporaryStatus = isTemporary;

            FileUploads.Add(fUpload);
            SaveChanges();

            return fUpload;
        }

        public FileUpload UpdateFileUploadData(string systemFileName, string absPath,
            long contentLength, bool isTemporary)
        {
            FileUpload fUpload = null;

                fUpload = FileUploads.Where(f => f.FileSysName == systemFileName && !f.IsDeleted)
                                .OrderByDescending(f => f.CreatedDate).FirstOrDefault();

                if (fUpload != null)
                {
                    fUpload.AbsolutePath = absPath;
                    fUpload.ContentLength = contentLength;
                    fUpload.TemporaryStatus = isTemporary;

                    Entry(fUpload).State = EntityState.Modified;

                    SaveChanges();
                }

            return fUpload;
        }
        #endregion
    }
}
