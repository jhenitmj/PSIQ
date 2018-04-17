using System.Web.Mvc;
using PSIQ.Models;
using PSIQ.DataAccess;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilTeraController : Controller
    {
        public ActionResult Index()
        {
            //Chamando a classe de acesso ao banco de dados para buscar todos os registro salvos na tabela
            var lst = new PacienteDAO().BuscarPorTerapeuta(((Usuario)User).Cod);

            //Retornando uma view chamada 'Index' com a lista de Cidades carregados do banco de dados
            return View(lst);
        }

        public ActionResult Chat(int pacienteId)
        {
            var lst = new PostDAO().BuscarPorUsuario(pacienteId);
            return View(lst);
        }
    }
}