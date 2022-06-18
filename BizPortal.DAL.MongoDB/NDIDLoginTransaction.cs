using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using BizPortal.ViewModels;
using System.Configuration;

namespace BizPortal.DAL.MongoDB
{
    public class NDIDLoginTransaction : Entity
    {

        #region[Entity]

        public string IdentityId { get; set; }

        public string RequestId { get; set; }

        public string ReferenceId { get; set; }

        public string LastStatus { get; set; }

        public string LastMessage { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<NDIDTransaction> Transactions { get; set; }

        #endregion

        #region[Func]
        public static void Init()
        {

        }

        public static NDIDLoginTransaction Get(string requestId)
        {
            var repo = MongoFactory.GetNDIDLoginTransactionCollection().AsQueryable();
            var transaction = repo.Where(o => o.RequestId == requestId).FirstOrDefault();
            return transaction;
        }

        public static void Add(string identityId, string referenceId, string requestId, string status, string message, object data)
        {
            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["NDIDLogTransaction"]))
                {
                    var login = new NDIDLoginTransaction
                    {
                        IdentityId = identityId,
                        ReferenceId = referenceId,
                        RequestId = requestId,
                        LastStatus = status,
                        LastMessage = message,
                        CreateDate = DateTime.Now,
                        Transactions = new List<NDIDTransaction>
                    {
                        new NDIDTransaction{
                            Type = NDIDTransactionType.RPRequest.ToString(),
                            Data = data,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        }
                    }
                    };

                    login.Create();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void Add(string requestId, NDIDTransactionType type, string status, string message, object data)
        {
            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["NDIDLogTransaction"]))
                {
                    var login = MongoFactory.GetNDIDLoginTransactionCollection().AsQueryable().Where(e => e.RequestId == requestId).FirstOrDefault();

                    if (login != null)
                    {
                        string[] s = new string[] { "rp_callback_rejected", "rp_callback_completed" };
                        login.LastStatus = s.Contains(login.LastStatus) ? login.LastStatus : status; // ถ้า completed หรือ rejected แล้ว last status จะไม่เปลี่ยน
                        login.LastMessage = message;
                        login.Transactions.Add(new NDIDTransaction
                        {
                            Type = type.ToString(),
                            Data = data,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        });

                        login.Update();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Create()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            MongoFactory.GetNDIDLoginTransactionCollection().InsertOne(this);
        }

        public void Update()
        {
            var collection = MongoFactory.GetNDIDLoginTransactionCollection();
            this.UpdateDate = DateTime.Now;
            collection.Update(this);
        }

        public void Delete()
        {
            var repo = MongoFactory.GetNDIDLoginTransactionCollection();
            repo.Delete(this);
        }

        #endregion
    }

    public class NDIDTransaction
    {
        public string Type { get; set; } // RPRequest, RPCallback, RPCheckStatus

        public object Data { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

}
