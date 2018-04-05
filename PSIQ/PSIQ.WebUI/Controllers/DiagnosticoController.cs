using PSIQ.DataAccess;
using PSIQ.Models;
using System.Web.Mvc;



namespace PSIQ.WebUI.Controllers
{
    public class DiagnosticoController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salvar(Diagnostico obj)
        {
            new DiagnosticoDAO().Inserir(obj);
            return Index();
        }
    }
}