using BizPortal.ViewModels.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Annotations;
using System.Globalization;
using BizPortal.Utils.Helpers;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
    public class UtilityController : AppsControllerBase
    {
        // GET: Apps/Utility
        public ActionResult Index()
        {
            UserTypeEnum type = IdentityType;
            ViewBag.IdentityType = type;
            ViewBag.ModelNull = false;
            ViewBag.CitizenBd = true;
            UtilityForm model = null;
            ViewBag.PrefixTH = null;
            ViewBag.NameTH = null;
            ViewBag.LastnameTH = null;

            try
            {
                if (type == UserTypeEnum.Citizen)
                {
                    var profile = IdentityHelper.GetCitizenProfile(IdentityID);
                    if (profile != null && profile.HasValues)
                    {
                        model = new UtilityForm()
                        {
                            IdentityID = profile["CitizenID"].DefaultString(),
                            IdentityName = string.Format("{0}{1} {2}", profile["NameTH_Prefix"].DefaultString(), profile["NameTH_FirstName"].DefaultString(), profile["NameTH_SurName"].DefaultString()),
                            AddressID = profile["AddressID"].DefaultString(),
                            Address = profile["Address_No"].DefaultString(),
                            BuildingVillage = profile["Address_Alley"].DefaultString(),
                            Moo = profile["Address_Moo"].DefaultString(),
                            Soi = profile["Address_Soi"].DefaultString(),
                            Road = profile["Address_Road"].DefaultString(),
                            Tumbol = profile["Address_Tumbol"].DefaultString(),
                            Amphur = profile["Address_Amphur"].DefaultString(),
                            Province = profile["Address_Province"].DefaultString(),
                            ContactFirstName = profile["NameTH_FirstName"].DefaultString(),
                            ContactLastName = profile["NameTH_SurName"].DefaultString(),
                            ContactIdentityID = profile["CitizenID"].DefaultString()
                        };

                        DateTime birthDate = DateTime.MinValue;
                        if (DateTime.TryParseExact(profile["BirthDate"].DefaultString(), "yyyyMMdd", CultureInfo.CreateSpecificCulture("th"), DateTimeStyles.None, out birthDate))
                        {
                            model.BirthDate = birthDate.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th"));
                        }
                        else
                        {
                            ViewBag.CitizenBd = false;
                        }

                        ViewBag.PrefixTH = profile["NameTH_Prefix"].DefaultString();
                        ViewBag.NameTH = profile["NameTH_FirstName"].DefaultString();
                        ViewBag.LastnameTH = profile["NameTH_SurName"].DefaultString();
                    }
                    else
                    {
                        model = new UtilityForm()
                        {
                            IdentityID = IdentityID
                        };
                        ViewBag.ModelNull = true;
                    }
                }
                else if (type == UserTypeEnum.Juristic)
                {
                    var profile = IdentityHelper.GetJuristicProfile(IdentityID);

                    if (profile != null && profile.HasValues)
                    {
                        JuristicProfile juristic = null;
                        juristic = profile.ToObject<JuristicProfile>();
                        ViewBag.Juristic = juristic;
                        model = new UtilityForm()
                        {
                            IdentityID = juristic.JuristicID,
                            IdentityName = juristic.JuristicName_TH
                        };

                        var addr = juristic.AddressInformations.FirstOrDefault();
                        if (addr != null)
                        {
                            model.Address = addr.AddressNo;
                            model.BuildingVillage = string.Format("{0} {1}", addr.Building, addr.VillageName).Trim();
                            model.Floor = addr.Floor;
                            model.Moo = addr.Moo;
                            model.Soi = addr.Soi;
                            model.Road = addr.Road;
                            model.Tumbol = addr.Tumbol;
                            model.Amphur = addr.Ampur;
                            model.Province = addr.Province;
                        }
                    }
                    else
                    {
                        ViewBag.ModelNull = true;
                    }
                }
            }
            catch { ViewBag.ModelNull = true; }

            return View(model);
        }
    }
}