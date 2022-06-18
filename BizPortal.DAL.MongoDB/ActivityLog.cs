using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class ActivityLog : Entity
    {
        public string Path { get; set; }

        public string Type { get; set; }

        public DateTime CreateDate { get; set; }

        public object Data { get; set; }

        public void Create()
        {
            try
            {
                CreateDate = DateTime.Now;
                var repo = MongoFactory.GetActivityLogCollection().AsQueryable();
                MongoFactory.GetActivityLogCollection().InsertOne(this);
            }
            catch (Exception)
            {

            }
        }

        public void Delete()
        {
            var repo = MongoFactory.GetActivityLogCollection();
            repo.Delete(this);
        }
    }
}
