using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormDataItem
    {
        public SingleFormDataItem()
        {
            SectionType = SectionTypeEnum.FormSection;
        }

        public enum SectionTypeEnum
        {
            FormSection,
            ArraySection
        }

        public const string TransactionAppSectionName = "TransactionApps";
        public enum DataItemType
        {
            SectionData,
            ItemRunningNo
        }
        public DataItemType Type { get; set; }
        public SectionTypeEnum SectionType { get; set; }
        public string SectionName { get; set; }
        public string DataName { get; set; }
        public bool BindDataValueToResourceText { get; set; } = false;
    }
}
