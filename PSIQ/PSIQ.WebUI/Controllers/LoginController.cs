using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PSIQ.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Terapeuta obj)
        {
            var usuario = new TerapeutaDAO().Logar(obj.Email, obj.Senha);

            if (usuario == null)
            {
                return View("Index");
            }

            var userData = new JavaScriptSerializer().Serialize(usuario);
            FormsAuthenticationUtil.SetCustomAuthCookie(usuario.Email, userData, false);

            return RedirectToAction("Index", "PerfilTera");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}