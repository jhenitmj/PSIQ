using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class CadTeraController : Controller
    {
        public ActionResult Index()
        {
            var obj = new Usuario() { DataNasc = DateTime.Now };
            return View(obj);
        }

        public ActionResult Salvar(Usuario obj)
        {
            if (ModelState.IsValid)
            {
                new UsuarioDAO().Inserir(obj);
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "CadTera");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Fotos")))
                    Directory.CreateDirectory(Server.MapPath("~/Fotos"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Fotos"), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);
                    return Json(fi.Name);
                }

                return Json(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}