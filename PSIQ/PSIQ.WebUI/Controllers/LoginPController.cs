﻿using PSIQ.DataAccess;
using PSIQ.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PSIQ.WebUI.Controllers
{
    public class LoginPController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Usuario obj)
        {
            var usuarioLogado = new UsuarioDAO().LogarP(obj);

            if (usuarioLogado == null)
            {
                return View("Index");
            }

            var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
            FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

            return RedirectToAction("Index", "PerfilPacienteP");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "LoginP");
        }
    }
}