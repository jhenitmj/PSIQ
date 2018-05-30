using AutoMapper;
using PSIQ.DataAccess;
using PSIQ.Models;
using PSIQ.WebUI.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class CadastroPController : Controller
    {
        public ActionResult Index()
        {
            if ((Usuario)HttpContext.User != null && ((Usuario)HttpContext.User).Tipo == TIPO_USUARIO.PACIENTE)
            {
                var u = new UsuarioDAO().BuscarPorCod(((Usuario)HttpContext.User).Cod);
                var obj = Mapper.Map<Usuario, PaciViewModel>(u);
                return View(obj);
            }
            else
            {
                var obj = new PaciViewModel() { DataNasc = DateTime.Now };
                return View(obj);
            }
        }

        public ActionResult Salvar(PaciViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var u = Mapper.Map<PaciViewModel, Usuario>(obj);
                u.Tipo = TIPO_USUARIO.PACIENTE;

                if (u != null && u.Cod > 0)
                    new UsuarioDAO().Atualizar(u);
                else
                    new UsuarioDAO().Inserir(u);

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
