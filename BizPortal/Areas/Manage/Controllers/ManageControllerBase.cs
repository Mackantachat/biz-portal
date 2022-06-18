using BizPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Manage.Controllers
{
    [Authorize]
    public abstract class ManageControllerBase : BizPortal.Controllers.ControllerBase
    {
    }
}