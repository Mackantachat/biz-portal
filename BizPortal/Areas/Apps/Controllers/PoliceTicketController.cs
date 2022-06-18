using BizPortal.Utils.Annotations;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Web.Mvc;
using BizPortal.ViewModels.Apps.PoliceTicket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "Citizen")]
    public class PoliceTicketController : AppsControllerBase
    {

        // GET: Apps/PoliceTicket
        public ActionResult Index()
        {
            var model = new PoliceTicketViewModel();

            try
            {
                RestClient client = new RestClient("https://omega.govchannel.go.th");
                RestRequest request = new RestRequest($"/ptm/get_all_ticket_list/1709900381574/", Method.GET);
                request.AddHeader("Authorization", ConfigurationValues.OmegaAuth);

                IRestResponse response = client.Execute(request);
                if (response != null && response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                {
                    model = JsonConvert.DeserializeObject<PoliceTicketViewModel>(response.Content);

                    if (model.Code == -1)
                    {
                        model.Message = "ไม่สามารถเชื่อมต่อ Service ได้";
                    }
                }
                else
                {
                    model.Message = "ไม่สามารถเชื่อมต่อ Service ได้";
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }


            return View(model);
        }
    }
}