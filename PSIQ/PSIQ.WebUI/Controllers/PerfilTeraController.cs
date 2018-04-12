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
            var lst = new PacienteDAO().BuscarTodos();

            //Retornando uma view chamada 'Index' com a lista de Cidades carregados do banco de dados
            return View(lst);
        }


    }
}