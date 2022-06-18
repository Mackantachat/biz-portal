using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public class RenderDataResult
    {
        public List<DataRowsDisplay<DataGroupDisplay>> Rows { get; set; }

        public RenderDataResult()
        {
            Rows = new List<DataRowsDisplay<DataGroupDisplay>>();
        }
    }

    public class DataRowsDisplay<T>
    {
        public int ColumnsPerRow { get; set; }
        public List<T> DataGroupDisplays { get; set; }
        public bool FixedColumnNumber { get; internal set; }

        public DataRowsDisplay()
        {
            ColumnsPerRow = 1;
            DataGroupDisplays = new List<T>();
        }
    }

    public class DataGroupDisplay
    {
        public string GroupCode { get; set; }

        public string GroupDisplayText { get; set; }

        public List<DataRowsDisplay<DataDisplay>> Rows { get; set; }
        public int ColumnNumber { get; internal set; }

        public DataGroupDisplay()
            : base()
        {
            Rows = new List<DataRowsDisplay<DataDisplay>>();
        }
    }

    public class DataDisplay
    {
        public bool HideLabel { get; set; }

        public string DataCode { get; set; }

        public string DisplayLabel { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }

        public string Info { get; set; }
        /// <summary>
        /// For OrderList type
        /// </summary>
        public List<DataList> List { get; set; }

        public DisplayType DisplayType { get; set; }
    }

    public class DataList
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public bool Checked { get; set; }
    }
}
