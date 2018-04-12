using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilPacienteTController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}