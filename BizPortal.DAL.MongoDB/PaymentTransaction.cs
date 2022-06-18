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
    public class PaymentTransaction : Entity
    {

        #region[Entity]

        public string IdentityId { get; set; }

        public Guid ApplicationRequestId { get; set; }

        public string ApplicationRequestNumber { get; set; }

        public string CostCenterCode { get; set; }

        public string PaymentGatewayRef1 { get; set; }

        public string PaymentGatewayRef2 { get; set; }

        public string PaymentGatewayRef3 { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime? ConfirmPaidDate { get; set; } // Reconcile date

        #endregion

        #region[Func]
        public static void Init()
        {

        }

        public static PaymentTransaction Get(string cgdRef1)
        {
            var repo = MongoFactory.GetPaymentTransactionCollection().AsQueryable();
            var transaction = repo.Where(o => o.PaymentGatewayRef1 == cgdRef1).FirstOrDefault();
            return transaction;
        }

        public static PaymentTransaction Get(string identityID, Guid applicationRequestID)
        {
            var repo = MongoFactory.GetPaymentTransactionCollection().AsQueryable();
            var transaction = repo.Where(o => o.IdentityId == identityID && o.ApplicationRequestId == applicationRequestID && o.Status != (int)CGDPaymentStatus.Failed).OrderByDescending(o=>o.UpdatedDate).FirstOrDefault();
            return transaction;
        }

        public static IQueryable<PaymentTransaction> List()
        {
            return MongoFactory.GetPaymentTransactionCollection().AsQueryable();
        }

        public static void Add(PaymentTransaction model)
        {
            model.Create();
        }

        public void Create()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            MongoFactory.GetPaymentTransactionCollection().InsertOne(this);
        }

        public void Update()
        {
            var collection = MongoFactory.GetPaymentTransactionCollection();

            if (this.UpdatedDate == null || this.UpdatedDate == DateTime.MinValue) 
            {
                this.UpdatedDate = DateTime.Now;
            }

            collection.Update(this);
        }

        public void Delete()
        {
            var repo = MongoFactory.GetPaymentTransactionCollection();
            repo.Delete(this);
        }

        #endregion
    }
}
