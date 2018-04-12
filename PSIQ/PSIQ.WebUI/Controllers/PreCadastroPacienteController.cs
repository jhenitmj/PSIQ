using PSIQ.DataAccess;
using PSIQ.Models;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class PreCadastroPacienteController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            ViewBag.Diagnostico = new DiagnosticoDAO().BuscarTodos();
            return View();
        }

        public ActionResult Salvar(Paciente obj)
        {
            new PacienteDAO().Inserir(obj);

            //Redirecionando para a tela de listagem de Cidades
            return RedirectToAction("Index", "PerfilTera");
        }
    }
}