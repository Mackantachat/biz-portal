using BizPortal.Utils.JsonConverter;
using BizPortal.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.ViewModels
{
    public class SendAlertEmailViewModel
    {
        public string fullname { get; set; }
        public string email { get; set; }
        public string msgbody { get; set; }
    }
}
