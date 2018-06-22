using System.Web.Mvc;
using PSIQ.Models;
using PSIQ.DataAccess;
using System;
using System.Linq;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PerfilTeraController : Controller
    {
        public ActionResult Index()
        {
            var usuarioLogado = new UsuarioDAO().BuscarPorCod(((Usuario)User).Cod);

            //Chamando a classe de acesso ao banco de dados para buscar todos os registro salvos na tabela
            usuarioLogado.Pacientes = new UsuarioDAO().BuscarPorTerapeuta(((Usuario)User).Cod);

            //Retornando uma view chamada 'Index' com a lista de Pacientes carregados do banco de dados
            return View(usuarioLogado);
        }

        public ActionResult Chat(int pacienteId)
        {
            ViewBag.Usuario = new Usuario() { Cod = pacienteId };
            var lst = new PostDAO().BuscarPorPaciente(pacienteId);
            return View(lst);
        }

        public ActionResult EnviarMsg(Post obj)
        {
            obj.DataHora = DateTime.Now;
            obj.Usuario = new Usuario() { Cod = ((Usuario)User).Cod };

            new PostDAO().Inserir(obj);

            return RedirectToAction("Chat", "PerfilTera", new { @pacienteId = obj.Paciente.Cod });
        }

        public ActionResult Buscar(string campoTexto)
        {
            usuarioLogado.Pacientes = new UsuarioDAO().BuscarPorTerapeuta(((Usuario)User).Cod).Where(p => p.Nome.Contains(campoTexto)).ToList();
            return View("Index", usuarioLogado);
        }
    }
}