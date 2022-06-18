using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using BizPortal.Utils.Helpers;
using Newtonsoft.Json;

namespace BizPortal.DAL.MongoDB
{
    public class Holiday : Entity
    {
        public static void Init()
        {
            var holidayRepo = MongoFactory.GetHolidayCollection();
            var holidayObj = HolidayHelper.GetHoliday();

            if (holidayObj != null)
            {
                var holidays = JsonConvert.DeserializeObject<List<Holiday>>(holidayObj.ToString());

                foreach (var h in holidays)
                {
                    if (holidayRepo.AsQueryable().Where(x => x.Date == h.Date).FirstOrDefault() == null)
                    {
                        holidayRepo.InsertOne(h);
                    }
                }
            }
        }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get { return Name; } }
    }

}
