using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BizPortal.DAL.MongoDB
{
    public class QuizGroup : Entity
    {
        public static void Init()
        {
            var db = MongoFactory.GetQuizGroupCollection();
            if (db.AsQueryable().Count() == 0)
            {
                QuizGroup[] items = new QuizGroup[]
                {
                    new QuizGroup() { QuizGroupName = "UTILITIES", Ordering = 1 },
                };

                db.InsertMany(items);
            }
        }

        public string QuizGroupName { get; set; }

        public int Ordering { get; set; }


        public static QuizGroup GetQuizGroup(string groupName)
        {
            var db = MongoFactory.GetQuizGroupCollection().AsQueryable();
            var group = db.Where(o => o.QuizGroupName == groupName.ToUpper()).SingleOrDefault();
            return group;
        }
    }
}
