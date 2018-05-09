using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilPacientePController : Controller
    {
        public ActionResult Index()
        {
            var usuarioLogado = new PacienteDAO().BuscarPorId(((Usuario)User).Cod);
            usuarioLogado.Posts = new PostDAO().BuscarPorUsuario(((Usuario)User).Cod);
            return View(usuarioLogado);
        }

        public ActionResult EnviarMsg(Post obj)
        {
            obj.DataHora = DateTime.Now;
            var usuarioLogado = new PacienteDAO().BuscarPorId(((Usuario)User).Cod);
            obj.Terapeuta = new Terapeuta() { Cod = usuarioLogado.Terapeuta.Cod };
            obj.Paciente = new Paciente() { Cod = usuarioLogado.Cod };

            new PostDAO().Inserir(obj);

            return RedirectToAction("Index", "PerfilPacienteP");
        }
    }
}