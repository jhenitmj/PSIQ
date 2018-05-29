using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class PreCadastroPacienteController : Controller
    {
        public ActionResult Index()
        {
            var obj = new Usuario() { DataNasc = DateTime.Now };
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            return View(obj);
        }

        public ActionResult Salvar(Usuario obj)
        {
            obj.Terapeuta = new Usuario() { Cod = ((Usuario)User).Cod };

            new UsuarioDAO().Inserir(obj);

            return RedirectToAction("Index", "PerfilTera");
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