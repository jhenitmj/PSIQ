using PSIQ.DataAccess;
using PSIQ.Models;
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
            var lst = new PostDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult Enviar(Post obj)
        {
            var lstUsuarios = new List<int>() { 1, 2 };
            obj.DataHora = DateTime.Now;
            obj.Usuario = new Usuario() { Cod = lstUsuarios[new Random().Next(lstUsuarios.Count)] };
            new PostDAO().Inserir(obj);

            return RedirectToAction("Index", "Home");
        }
    }
}