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
    public class CadTeraController : Controller
    {
        public ActionResult Index()
        {
            var obj = new TeraViewModel() { DataNasc = DateTime.Now };
            return View(obj);
        }

        public ActionResult Salvar(TeraViewModel obj)
        {
            if (ModelState.IsValid)
            {
                

                if (!ValidarEmail(obj.Email))
                {
                    ViewBag.MsgErro = "E-mail inválido!";
                    return View("Index");
                }
                var u = Mapper.Map<TeraViewModel, Usuario>(obj);
                u.Tipo = TIPO_USUARIO.TERAPEUTA;
              
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
     

        public bool ValidarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;
            if (!email.Contains("@") || !email.Contains("."))
                return false;
            string[] strCamposEmail = email.Split(new String[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length != 2)
                return false;
            if (strCamposEmail[0].Length < 3)
                return false;
            if (!strCamposEmail[1].Contains("."))
                return false;
            strCamposEmail = strCamposEmail[1].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length < 2)
                return false;
            if (strCamposEmail[0].Length < 1)
                return false;
            return true;
        }
    }
}