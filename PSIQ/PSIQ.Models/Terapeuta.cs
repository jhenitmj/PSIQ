using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Web.Script.Serialization;

namespace PSIQ.Models
{
    public class Terapeuta : ICustomPrincipal
    {
        public int Cod { get; set; }
        public int CRP { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Foto { get; set; }
        public string CaminhoFoto { get; set; }

        [ScriptIgnore]
        [IgnoreDataMember]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Email, "CustomPrincipal");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Terapeuta()
        {
            //Identity = new GenericIdentity(this.Email, "Terapeuta");
        }

        public Terapeuta(string email)
        {
            Identity = new GenericIdentity(email, "Terapeuta");
        }
    }
}