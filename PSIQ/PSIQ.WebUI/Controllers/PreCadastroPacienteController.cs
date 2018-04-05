using PSIQ.DataAccess;
using PSIQ.Models;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class PreCadastroPacienteController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Estado = new EstadoDAO().BuscarTodos();
            ViewBag.Diagnostico = new DiagnosticoDAO().BuscarTodos();
            return View();
        }



        public ActionResult Salvar(Paciente obj)
        {
            //Chamando a classe de acesso ao banco de dados para inserir o novo registro
            new PacienteDAO().Inserir(obj);

            //Redirecionando para a tela de listagem de Cidades
            return RedirectToAction("Index", "PreCadastroPaciente");
        }
    }
}