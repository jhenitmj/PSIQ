using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class DiagnosticoController : Controller
    {
        public ActionResult Index(int pacienteId)
        {
            ViewBag.Diagnosticos = new DiagnosticoDAO().BuscarTodos();
            ViewBag.PacXDiags = new PacienteXDiagnosticoDAO().BuscarPorPaciente(pacienteId);
            var pxd = new PacienteXDiagnostico() { Paciente = new Paciente() { Cod = pacienteId } };
            return View(pxd);
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Salvar(Diagnostico obj)
        {
            new DiagnosticoDAO().Inserir(obj);
            return RedirectToAction("Index", "PerfilTera");
        }

        public ActionResult SalvarPacXDiag(PacienteXDiagnostico obj)
        {
            obj.DataHora = DateTime.Now;
            new PacienteXDiagnosticoDAO().Inserir(obj);
            ViewBag.PacXDiags = new PacienteXDiagnosticoDAO().BuscarPorPaciente(obj.Paciente.Cod);
            return RedirectToAction("Index", "Diagnostico", new { pacienteId = obj.Paciente.Cod });
        }
    }
}