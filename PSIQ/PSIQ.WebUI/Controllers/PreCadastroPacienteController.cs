using AutoMapper;
using PSIQ.DataAccess;
using PSIQ.Models;
using PSIQ.WebUI.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PSIQ.WebUI.Controllers
{
    [Authorize]
    public class PreCadastroPacienteController : Controller
    {
        public ActionResult Index()
        {
            var obj = new PaciViewModel() { DataNasc = DateTime.Now };
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            return View(obj);
        }

        public ActionResult Salvar(PaciViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var u = Mapper.Map<PaciViewModel, Usuario>(obj);
                u.Terapeuta = new Usuario() { Cod = ((Usuario)User).Cod };
                u.Tipo = TIPO_USUARIO.PACIENTE;

                if (!IsValidCPF(obj.CPF))
                {
                    ViewBag.MsgErro = "CPF inválido!";
                    return View("Index");
                }

                if (!ValidarEmail(obj.Email))
                {
                    ViewBag.MsgErro = "E-mail inválido!";
                    return View("Index");
                }

                new UsuarioDAO().Inserir(u);

                return RedirectToAction("Index", "PerfilTera");
            }

            return RedirectToAction("Index", "PreCadastroPaciente");
        }

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

        private bool IsValidCPF(string cpf)
        {
            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public bool ValidarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;
            if (!email.Contains("@") || !email.Contains("."))
                return false;
            string[] strCamposEmail = email.Split(new String[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length != 2)
                return false;
            if (strCamposEmail[0].Length < 3)
                return false;
            if (!strCamposEmail[1].Contains("."))
                return false;
            strCamposEmail = strCamposEmail[1].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length < 2)
                return false;
            if (strCamposEmail[0].Length < 1)
                return false;
            return true;
        }

    }
}

