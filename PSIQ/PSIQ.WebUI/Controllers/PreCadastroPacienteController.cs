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
            var obj = new Paciente() { DtNascimento = DateTime.Now };
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            return View(obj);
        }

        public ActionResult Salvar(Paciente obj)
        {
            obj.Terapeuta = new Terapeuta() { Cod = ((Usuario)User).Cod };

            new PacienteDAO().Inserir(obj);

            return RedirectToAction("Index", "PerfilTera");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Images")))
                    Directory.CreateDirectory(Server.MapPath("~/Images"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(f.FileName));
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