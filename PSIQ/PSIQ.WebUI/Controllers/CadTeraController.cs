using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class CadTeraController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salvar(Terapeuta obj)
        {
            new TerapeutaDAO().Inserir(obj);

            return RedirectToAction("Index", "Login");
        }
    }
}