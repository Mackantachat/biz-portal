using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace BizPortal.ViewModels
{
    public class DataTableSearch
    {
        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    public class DataTablesColumn
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "orderable")]
        public bool Orderable { get; set; }

        [JsonProperty(PropertyName = "search")]
        public DataTableSearch Search { get; set; }

        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }
    }

    public class DataTablesOrder
    {
        [JsonProperty(PropertyName = "column")]
        public int Column { get; set; }

        [JsonProperty(PropertyName = "dir")]
        public string Dir { get; set; }
    }

    public abstract class DataTables
    {
        private string _lang = "th";

        public string SearchKeyword { get; set; }

        public string Lang
        {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value.ToLower();
            }
        }

        [JsonProperty(PropertyName = "columns")]
        public DataTablesColumn[] Columns { get; set; }

        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "order")]
        public DataTablesOrder[] Order { get; set; }

        [JsonProperty(PropertyName = "search")]
        public DataTableSearch Search { get; set; }

        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        public IQueryable<T> GenerateSearchQuery<T>(IQueryable<T> query, string defaultColumn, string defaultDir = "asc")
            where T : class
        {
            if (Order != null && Order.Length > 0)
            {
                string orderBy = "";
                for (int i = 0; i < Order.Length; i++)
                {
                    var o = Order[i];
                    DataTablesColumn col = Columns[o.Column];

                    string name = col.Name;
                    if (string.IsNullOrEmpty(name))
                        name = col.Data;
                    if (i == 0)
                        orderBy = string.Format("{0} {1}", name, o.Dir);
                    else
                        orderBy = string.Format("{0}, {1} {2}", orderBy, name, o.Dir);

                }
                query = query.OrderBy(orderBy.Replace("'", "''"));
            }
            else
                query = query.OrderBy(string.Format("{0} {1}", defaultColumn, defaultDir).Replace("'", "''"));

            query = query.Skip(Start).Take(Length);

            return query;
        }
    }

    public class DatatableDisplay
    {
        public bool canView { get; set; }
        public bool canEdit { get; set; }
        public bool canEditVisible { get; set; }
        public bool canDelete { get; set; }
        public bool canDeleteVisible { get; set; }

        public bool canSuspend { get; set; }
        public bool canSuspendVisible { get; set; }

        public bool canEnable { get; set; }
        public bool canEnableVisible { get; set; }

        public bool canApprove { get; set; }
        public bool canApproveVisible { get; set; }

        public bool canPrint { get; set; }
        public bool canPrintVisible { get; set; }

        public bool canDuplicate { get; set; }
        public bool canDuplicateVisible { get; set; }

        public bool canChangeOwner { get; set; }
        public bool canChangeOwnerVisible { get; set; }
    }
}
