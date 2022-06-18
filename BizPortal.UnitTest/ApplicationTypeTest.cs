using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB;
using BizPortal.DAL;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class ApplicationTypeTest
    {
        [TestMethod]
         
        public void TestMethod1()
        {
            Task<List<ApplicationRequestEntity>> Mongo_result = new Task<List<ApplicationRequestEntity>>(() =>
                {
                    return MongoFactory.GetDatabase().GetCollection<ApplicationRequestEntity>("ApplicationRequest").Find(_ => true).ToList();
                }
            );
            Mongo_result.Start();
            //var Mongo_result = MongoFactory.GetDatabase().GetCollection<ApplicationRequestEntity>("ApplicationRequest").Find(_ => true).ToList();

            int count = 1;
            int count_NEW= 0;
            int count_RENEW = 0;
            int count_EDIT = 0;
            int count_CANCEL = 0;

            using (var db = new ApplicationDbContext())
            {
                // var user = db.Users.Where(e => e.CitizenID == citizenId).FirstOrDefault();
                var Sql_result = db.Applications.ToList();
                Mongo_result.Wait();
                foreach (var app_sql  in Sql_result )
                {
                   
                    var appRequestEntityList = Mongo_result.Result.Where(e => e.ApplicationID == app_sql.ApplicationID).ToList();


                    foreach (var appRequestEntity in appRequestEntityList)
                    {
                        if (app_sql.ApplicationType == "NEW")
                        {
                            appRequestEntity.ApplicationType = app_sql.ApplicationType;
                            appRequestEntity.Update();

                            count_NEW = count_NEW + 1;
                        }


                        if (app_sql.ApplicationType == "EDIT")
                        {
                            appRequestEntity.ApplicationType = app_sql.ApplicationType;
                            appRequestEntity.Update();
                            count_EDIT = count_EDIT + 1;
                        }

                        if (app_sql.ApplicationType == "CANCEL")
                        {
                            appRequestEntity.ApplicationType = app_sql.ApplicationType;
                            appRequestEntity.Update();
                            count_CANCEL = count_CANCEL + 1;
                        }

                        if (app_sql.ApplicationType == "RENEW")
                        {
                            appRequestEntity.ApplicationType = app_sql.ApplicationType;
                            appRequestEntity.Update();

                            count_RENEW = count_RENEW + 1;
                        }


                    }

                }

                var result_NEW = count_NEW;
                var result_RENEW = count_RENEW;
                var result_EDIT = count_EDIT;
                var result_CANCEL = count_CANCEL;
            }

          
        }

     
    }
}
