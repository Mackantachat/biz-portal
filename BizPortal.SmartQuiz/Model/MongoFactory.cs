using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SmartQuiz.Model
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

        public static IMongoCollection<FormSectionRow> GetFormSectionRowCollection()
        {
            return GetDatabase().GetCollection<FormSectionRow>("FormSectionRow");
        }

        public static IMongoCollection<FormSection> GetFormSectionCollection()
        {
            return GetDatabase().GetCollection<FormSection>("FormSection");
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