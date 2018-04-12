using PSIQ.DataAccess;
using PSIQ.Models;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class CadastroPController : Controller
    {
        // GET: CadastroP
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salvar(Paciente obj)
        {
            new PacienteDAO().Inserir(obj);

            return RedirectToAction("Index", "Login");
        }
    }
}