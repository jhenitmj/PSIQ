using PSIQ.DataAccess;
using PSIQ.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    public class CadastroPController : Controller
    {

        public ActionResult Index()
        {
          
     
            var obj = new Paciente() { DtNascimento = System.DateTime.Now };
            return View(obj);
        }

        //public ActionResult Salvar(Paciente obj)
        //{
        //    //var usuarioLogado = new TerapeutaDAO().BuscarPorId(((Usuario)User).Cod);

        //    ////Chamando a classe de acesso ao banco de dados para buscar todos os registro salvos na tabela
        //    //usuarioLogado.Pacientes = new PacienteDAO().Atualizar(((Usuario)User).Cod);

        //    ////Retornando uma view chamada 'Index' com a lista de Pacientes carregados do banco de dados
        //    //return View(usuarioLogado);



        //}

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Fotos")))
                    Directory.CreateDirectory(Server.MapPath("~/Fotos"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Fotos"), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);
                    return Json(fi.Name);
                }

                return Json(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
