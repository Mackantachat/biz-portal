using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IO = System.IO;


namespace BizPortal.Utils.Extensions
{
    [DataContract()]
    public class DataTableJSON<T> where T : class, new()
    {
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "sEcho")]
        public int Echo { get; set; }

        [DataMember(Name = "iTotalRecords")]
        public int TotalRecords { get; set; }

        [DataMember(Name = "iTotalDisplayRecords")]
        public int TotalDisplayRecords { get; set; }

        [DataMember(Name = "aaData")]
        public IList<T> Data { get; set; }
    }

    public class DataTableProperty
    {
        public string OrderBy { get; set; }
        public bool IsOrderAsc { get; set; }
        public int Echo { get; set; }
        public int StartRecord { get; set; }
        public int EndRecord { get; set; }

        public static DataTableProperty Get(FormCollection formcollection)
        {
            string orderBy = string.Empty;
            bool isOrderAsc = false;
            int echo = 0;
            int startRecord = 0;
            int endRecord = 25;

            if (formcollection["sEcho"] != null)
            {
                echo = Convert.ToInt32(formcollection["sEcho"]);

            }

            if (formcollection["sColumns"] != null && formcollection["iSortCol_0"] != null)
            {
                orderBy = formcollection["sColumns"].Split(',')[Convert.ToInt32(formcollection["iSortCol_0"])];
            }

            if (formcollection["sSortDir_0"] != null)
            {
                isOrderAsc = formcollection["sSortDir_0"] == "desc" ? false : true;
            }

            if (formcollection["iDisplayStart"] != null && formcollection["iDisplayLength"] != null)
            {
                startRecord = Convert.ToInt32(formcollection["iDisplayStart"]);
                endRecord = Convert.ToInt32(formcollection["iDisplayLength"]);
            }

            return new DataTableProperty
            {
                Echo = echo,
                OrderBy = orderBy,
                IsOrderAsc = isOrderAsc,
                StartRecord = startRecord,
                EndRecord = endRecord
            };
        }
    }

    /// <remarks>
    /// Based on the excellent stackoverflow answer:
    /// http://stackoverflow.com/a/263416/1039947
    /// </remarks>
    public class JsonDataTableResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        public JsonDataTableResult(Object data)
            : this(data, JsonRequestBehavior.DenyGet)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        /// <param name="behavior">Behavior</param>
        public JsonDataTableResult(Object data, JsonRequestBehavior behavior)
        {
            Data = data;
            Behavior = behavior;
        }

        /// <summary>
        /// Gets of set the behavior
        /// </summary>
        private JsonRequestBehavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public Object Data { get; private set; }

        /// <summary>
        /// Enables processing of the result of an action method by a 
        /// custom type that inherits from the ActionResult class. 
        /// </summary>
        /// <param name="context">The controller context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if ((Behavior == JsonRequestBehavior.DenyGet) && (context.HttpContext.Request.HttpMethod != "POST"))
            {
                throw new HttpException("Only POST method allowed.");
            }

            var serializer = new DataContractJsonSerializer(Data.GetType());

            string output;
            using (var ms = new IO.MemoryStream())
            {
                serializer.WriteObject(ms, Data);
                output = Encoding.UTF8.GetString(ms.ToArray());
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(output);
        }
    }
}
