using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class Terapeuta
    {
        public int Cod { get; set; }
        public int CRP { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DtNasciento { get; set; }
        public string Foto { get; set; }
        public string CaminhoFoto { get; set; }
    }
}