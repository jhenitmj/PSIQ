using PSIQ.DataAccess;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilPacientePController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Chat(int terapeutaId)
        {
            var lst = new PostDAO().BuscarPorUsuario(terapeutaId);
            return View(lst);
        }
    }
}