using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Globalization;

namespace BizPortal.Utils.Extensions
{
    public class JsonDataContractActionResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        public JsonDataContractActionResult(Object data)
            : this(data, JsonRequestBehavior.DenyGet)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        /// <param name="behavior">Behavior</param>
        public JsonDataContractActionResult(Object data, JsonRequestBehavior behavior)
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
