using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace PSIQ.Models
{
    public class Usuario : ICustomPrincipal
    {
        public int Cod { get; set; }
        public string Nome { get; set; }
        public TIPO_USUARIO Tipo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Foto { get; set; }

        public string CPF { get; set; }
        public string CRP { get; set; }
        public DateTime DataNasc { get; set; }
        public string Descricao { get; set; }
        public Usuario Terapeuta { get; set; }
        public Estado Estado { get; set; }

        public List<PacienteXDiagnostico> Diagnosticos { get; set; }
        public List<Post> Posts { get; set; }
        public List<Usuario> Pacientes { get; set; }

        [ScriptIgnore]
        [IgnoreDataMember]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Email, "Usuario");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Usuario()
        {
            this.Pacientes = new List<Usuario>();
            this.Diagnosticos = new List<PacienteXDiagnostico>();
            this.Posts = new List<Post>();
        }

        public Usuario(string myEmail)
        {
            Identity = new GenericIdentity(myEmail, "Usuario");
        }
    }
}
