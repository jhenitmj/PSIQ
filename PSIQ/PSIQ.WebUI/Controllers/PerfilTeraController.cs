using System.Web.Mvc;
using PSIQ.Models;
using PSIQ.DataAccess;
using System;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilTeraController : Controller
    {
        public ActionResult Index()
        {
            var usuarioLogado = new TerapeutaDAO().BuscarPorId(((Usuario)User).Cod);

            //Chamando a classe de acesso ao banco de dados para buscar todos os registro salvos na tabela
            usuarioLogado.Pacientes = new PacienteDAO().BuscarPorTerapeuta(((Usuario)User).Cod);

            //Retornando uma view chamada 'Index' com a lista de Pacientes carregados do banco de dados
            return View(usuarioLogado);
        }

        public ActionResult Chat(int pacienteId)
        {
            ViewBag.Usuario = new Paciente() { Cod = pacienteId };
            var lst = new PostDAO().BuscarPorUsuario(pacienteId);
            return View(lst);
        }

        public ActionResult EnviarMsg(Post obj)
        {
            obj.DataHora = DateTime.Now;
            obj.Terapeuta = new Terapeuta() { Cod = ((Usuario)User).Cod };

            new PostDAO().Inserir(obj);

            return RedirectToAction("Chat", "PerfilTera", new { @pacienteId = obj.IdUsuario });
        }
    }
}