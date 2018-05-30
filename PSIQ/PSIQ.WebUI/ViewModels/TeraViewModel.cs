using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSIQ.WebUI.ViewModels
{
    public class TeraViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Foto { get; set; }
        public string CRP { get; set; }
        public DateTime? DataNasc { get; set; }
    }
}