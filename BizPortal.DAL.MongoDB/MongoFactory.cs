using MongoDB.Driver;
using System.Configuration;

namespace BizPortal.DAL.MongoDB
{
    public static class MongoFactory
    {
        public static IMongoDatabase GetDatabase()
        {
            string conString = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var _client = new MongoClient(conString);
            var _dbName = MongoUrl.Create(conString).DatabaseName;
            return _client.GetDatabase(_dbName);
        }

        public static IMongoCollection<ApplicationRequestEntity> GetApplicationRequestCollection()
        {
            return GetDatabase().GetCollection<ApplicationRequestEntity>("ApplicationRequest");
        }

        public static IMongoCollection<FileGroupEntity> GetFileGroupCollection()
        {
            return GetDatabase().GetCollection<FileGroupEntity>("FileGroup");
        }

        public static IMongoCollection<FileMetadataEntity> GetFileMetadataCollection()
        {
            return GetDatabase().GetCollection<FileMetadataEntity>("FileMetadata");
        }

        public static IMongoCollection<FormSectionGroup> GetFormSectionGroupCollection()
        {
            return GetDatabase().GetCollection<FormSectionGroup>("FormSectionGroup");
        }

        public static IMongoCollection<FormSectionGroup> GetFormSectionGroupRevisionCollection()
        {
            return GetDatabase().GetCollection<FormSectionGroup>("FormSectionGroupRevision");
        }

        public static IMongoCollection<FormSectionRow> GetFormSectionRowCollection()
        {
            return GetDatabase().GetCollection<FormSectionRow>("FormSectionRow");
        }

        public static IMongoCollection<FormSectionRow> GetFormSectionRowRevisionCollection()
        {
            return GetDatabase().GetCollection<FormSectionRow>("FormSectionRowRevision");
        }

        public static IMongoCollection<FormSection> GetFormSectionCollection()
        {
            return GetDatabase().GetCollection<FormSection>("FormSection");
        }

        public static IMongoCollection<FormSection> GetFormSectionRevisionCollection()
        {
            var ud = new UpdateDefinitionBuilder<FormSection>();
            var uds = ud.Set(x => x.RevisionCode, 100);
            uds = uds.Set(x => x.RevisionName, "");
            return GetDatabase().GetCollection<FormSection>("FormSectionRevision");
        }

        public static IMongoCollection<FormConfigRevision> GetFormConfigRevisionCollection()
        {
            return GetDatabase().GetCollection<FormConfigRevision>("FormConfigRevision");
        }

        public static IMongoCollection<SingleFormTransaction> GetSingleFormTransactionCollection()
        {
            return GetDatabase().GetCollection<SingleFormTransaction>("SingleFormTransaction");
        }

        public static IMongoCollection<SingleFormRequestEntity> GetSingleFormRequestCollection()
        {
            return GetDatabase().GetCollection<SingleFormRequestEntity>("SingleFormRequest");
        }

        public static IMongoCollection<SingleFormAppFile> GetSingleFormAppFileCollection()
        {
            return GetDatabase().GetCollection<SingleFormAppFile>("SingleFormAppFile");
        }

        public static IMongoCollection<SingleFormFileList> GetSingleFormFileListCollection()
        {
            return GetDatabase().GetCollection<SingleFormFileList>("SingleFormFileList");
        }
        
        public static IMongoCollection<QuizGroup> GetQuizGroupCollection()
        {
            return GetDatabase().GetCollection<QuizGroup>("QuizGroup");
        }

        public static IMongoCollection<QuizSectionRow> GetQuizSectionRowCollection()
        {
            return GetDatabase().GetCollection<QuizSectionRow>("QuizSectionRow");
        }

        public static IMongoCollection<QuizAppMapping> GetQuizAppMappingCollection()
        {
            return GetDatabase().GetCollection<QuizAppMapping>("QuizAppMapping");
        }

        public static IMongoCollection<SmartQuiz> GetSmartQuizCollection()
        {
            return GetDatabase().GetCollection<SmartQuiz>("SmartQuiz");
        }

        public static IMongoCollection<Holiday> GetHolidayCollection()
        {
            return GetDatabase().GetCollection<Holiday>("Holiday");
        }

        public static IMongoCollection<NDIDLoginTransaction> GetNDIDLoginTransactionCollection()
        {
            return GetDatabase().GetCollection<NDIDLoginTransaction>("NDIDLoginTransaction");
        }

        public static IMongoCollection<AnonymousQuizAnswer> GetAnonymousQuizAnswerCollection()
        {
            return GetDatabase().GetCollection<AnonymousQuizAnswer>("AnonymousQuizAnswer");
        }

        public static IMongoCollection<AppPrefillAnswer> GetAppPrefillAnswerCollection()
        {
            return GetDatabase().GetCollection<AppPrefillAnswer>("AppPrefillAnswer");
        }

        public static IMongoCollection<ActivityLog> GetActivityLogCollection()
        {
            return GetDatabase().GetCollection<ActivityLog>("ActivityLog");
        }

        public static IMongoCollection<InitLog> GetInitLogCollection()
        {
            return GetDatabase().GetCollection<InitLog>("InitLog");
        }

        public static IMongoCollection<ReceiptRunningTransaction> GetReceiptRunningTransactionCollection()
        {
            return GetDatabase().GetCollection<ReceiptRunningTransaction>("ReceiptRunningTransaction");
        }

        public static IMongoCollection<PaymentTransaction> GetPaymentTransactionCollection()
        {
            return GetDatabase().GetCollection<PaymentTransaction>("PaymentTransaction");
        }

        public static IMongoCollection<SingleFormPreFillData> GetSingleFormPreFillDataCollection()
        {
            return GetDatabase().GetCollection<SingleFormPreFillData>("SingleFormPreFillData");
        }
        
        public static IMongoCollection<EDocumentEntity> GetEDocumentCollection()
        {
            return GetDatabase().GetCollection<EDocumentEntity>("EDocument");
        }

        public static ReplaceOneResult Update<TDocument>(this IMongoCollection<TDocument> coll, TDocument doc)
            where TDocument : Entity
        {
            var filter = Builders<TDocument>.Filter.Eq(o => o.Id, doc.Id);
            return coll.ReplaceOne(filter, doc);
        }

        public static DeleteResult Delete<TDocument>(this IMongoCollection<TDocument> coll, string id)
            where TDocument : Entity
        {
            var filter = Builders<TDocument>.Filter.Eq(o => o.Id, id);
            return coll.DeleteOne(filter);
        }

        public static DeleteResult Delete<TDocument>(this IMongoCollection<TDocument> coll, TDocument doc)
            where TDocument : Entity
        {
            var filter = Builders<TDocument>.Filter.Eq(o => o.Id, doc.Id);
            return coll.DeleteOne(filter);
        }
    }
}
