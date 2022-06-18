using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BizPortal.DAL.MongoDB
{
    public class QuizAppMapping : Entity
    {
        public static void Init()
        {
            /*var db = MongoFactory.GetQuizAppMappingCollection();
            if (db.AsQueryable().Count() == 0)
            {
                QuizAppMapping[] items = new QuizAppMapping[]
                {
                    new QuizAppMapping()
                    {
                        QuizGroup = "UTILITIES",
                        AppSystemName = "แบบฟอร์มขอใช้ไฟฟ้า",
                        DisplayConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO"),
                            new QuizAnswer("UTILITY_SERVICES_ELECTRIC", "true")
                        },
                        OnlineRequestAllowedConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("HAS_HOME_NUMBER", new string[] { "Yes" }, "VAL_HOME_NUMBER_REQUIRED"),
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO"),
                            new QuizAnswer("UTILITY_SERVICES_ELECTRIC", "true"),
                            new QuizAnswer("INSTALL_TYPE", new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
                        }
                    },
                    new QuizAppMapping()
                    {
                        QuizGroup = "UTILITIES",
                        AppSystemName = "แบบฟอร์มขอใช้น้ำประปา",
                        DisplayConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO"),
                            new QuizAnswer("UTILITY_SERVICES_WATER", "true")
                        },
                        OnlineRequestAllowedConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("HAS_HOME_NUMBER", new string[] { "Yes" }, "VAL_HOME_NUMBER_REQUIRED"),
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO"),
                            new QuizAnswer("UTILITY_SERVICES_WATER", "true"),
                            new QuizAnswer("INSTALL_TYPE", new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
                        }
                    },
                    new QuizAppMapping()
                    {
                        QuizGroup = "UTILITIES",
                        AppSystemName = "ขอใช้บริการโทรศัพท์พื้นฐาน และอินเทอร์เน็ต",
                        DisplayConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_PHONE", "true")
                        },
                        OnlineRequestAllowedConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("HAS_HOME_NUMBER", new string[] { "Yes" }, "VAL_HOME_NUMBER_REQUIRED"),
                            new QuizAnswer("METRO_OR_PROVINCE", "METRO", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_PHONE", "true"),
                            new QuizAnswer("INSTALL_TYPE", new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
                        }
                    },
                    new QuizAppMapping()
                    {
                        QuizGroup = "UTILITIES",
                        AppSystemName = "แบบฟอร์มขอใช้ไฟฟ้า (ภูมิภาค)",
                        DisplayConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("METRO_OR_PROVINCE", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_ELECTRIC", "true")
                        },
                        OnlineRequestAllowedConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("HAS_HOME_NUMBER", new string[] { "Yes" }, "VAL_HOME_NUMBER_REQUIRED"),
                            new QuizAnswer("METRO_OR_PROVINCE", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_ELECTRIC", "true"),
                            new QuizAnswer("INSTALL_TYPE", new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
                        }
                    },
                    new QuizAppMapping()
                    {
                        QuizGroup = "UTILITIES",
                        AppSystemName = "แบบฟอร์มขอใช้น้ำประปา (ภูมิภาค)",
                        DisplayConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("METRO_OR_PROVINCE", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_WATER", "true")
                        },
                        OnlineRequestAllowedConditions = new QuizAnswer[]
                        {
                            new QuizAnswer("HAS_HOME_NUMBER", new string[] { "Yes" }, "VAL_HOME_NUMBER_REQUIRED"),
                            new QuizAnswer("METRO_OR_PROVINCE", "PROVINCE"),
                            new QuizAnswer("UTILITY_SERVICES_WATER", "true"),
                            new QuizAnswer("INSTALL_TYPE", new string[] { "PERMANENT", "TEMPORARILY" })
                        }
                    }
                };

                db.InsertMany(items);
            }*/
        }

        public string QuizGroup { get; set; }

        public string AppSystemName { get; set; }

        public QuizAnswer[] DisplayConditions { get; set; }

        public QuizAnswer[] OnlineRequestAllowedConditions { get; set; }

        public AppWarning[] Warnings { get; set; }

        public static List<QuizAppMapping> GetQuizAppMappings(string groupName)
        {
            var db = MongoFactory.GetQuizAppMappingCollection().AsQueryable();
            var mappings = db.Where(o => o.QuizGroup == groupName.ToUpper()).ToList();
            return mappings;
        }
    }
    public class AppWarning
    {
        public string Message { get; set; }
        public QuizAnswer[] Conditions { get; set; }
    }

    public class QuizAnswerGroup : QuizAnswer
    {
        
        public QuizAnswerGroup(bool isOr, QuizAnswer[] answers)
            : base("", null)
        {
            IsOr = isOr;
            Answers = answers;
        }
    }

    public class QuizAnswerFactory
    {
        public static QuizAnswer AnswerChecked(SmartQuiz.Question q, SmartQuiz.Choice c)
        {
            return new QuizAnswer(q.SystemName + "_" + c.SystemName, "on");
        }
    }

    public class QuizAnswer
    {
        public const string UserIdentityType = "User::IdentityType";
        public string Key { get; set; }

        public string[] Values { get; set; }

        public string InvalidAnswerReason { get; set; }
        public bool IsOr { get; set; }
        public QuizAnswer[] Answers { get; set; }
        public QuizAnswer(string key, params string[] values)
            : this(key, values, "")
        {

        }

        public QuizAnswer(string key, string[] values, string invalidAnswerReason)
        {
            Key = key;
            Values = values;
            InvalidAnswerReason = invalidAnswerReason;
        }
    }
}
