using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormFileConfig
    {
        
        /// <summary>
        /// The file is Optional or not (apply both ShowOnCondition and ShowMultiple)
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Condition for the file to be Optional
        /// </summary>
        public ConditionConfig OptionalCondition { get; set; }

        /// <summary>
        /// Condition for the file to be visible
        /// </summary>
        public ConditionConfig DisplayCondition { get; set; }
        public ShowMultipleConfig ShowMultiple { get; set; }

        
        public class ConditionConfig
        {
            public bool Not { get; set; }
            public bool IsOr { get; set; }
            public ConditionItem Condition { get; set; }
            public ConditionConfig[] InnerConditions { get; set; }
        }

        public static ConditionConfig RequestForAppConfig(string appSystemName, bool not = false)
        {
            return new ConditionConfig
            {
                Not = not,
                Condition = new ConditionItem
                {
                    Data = new SingleFormDataItem
                    {
                        SectionName = SingleFormDataItem.TransactionAppSectionName,
                        DataName = appSystemName,
                    },
                    CheckIfSectionExist = true,
                }
            };
        }

        public class ConditionItem
        {
            public SingleFormDataItem Data { get; set; }
            public string ExpectedValue { get; set; }
            public bool CheckIfSectionExist { get; set; }
            public bool CheckIfDataExist { get; set; }
        }

        

        public class ShowMultipleConfig
        {
            public bool BindToSection { get; set; }
            public bool AllowMultipleEqualToSectionItem { get; set; }
            public int AllowMultipleEqualToSectionItemAdjust { get; set; }
            public int AllowMultipleMinItem { get; set; }
            public string AddItemBtnText { get; set; }
        //     public string Note { get; set; }
        //public string Note_2 { get; set; }
        //public string Note_3 { get; set; }

            public string SectionName { get; set; }
            
            public SingleFormDataItem[] DataItems { get; set; }

            /// <summary>
            /// กรองเฉพาะบาง Item ใน Section ด้วย Field ใด
            /// </summary>
            public SingleFormDataItem FilterDataItem { get; set; }
            /// <summary>
            /// ใช้ร้วมกับ FilterDataItem กรองเฉพาะ Item ที่มีค่านี้
            /// </summary>
            public string FilterDataItemText { get; set; }

            public ConditionConfig AdvancedCondition { get; set; }

        }
    }
}
