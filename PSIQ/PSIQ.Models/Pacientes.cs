using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Pacientes
    {   public string CPF { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
        public int Idade { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public string Senha { get; set; }
        public int Cod { get; set; }
        public DateTime DtNasciento { get; set; }
        public string DescricaoD { get; set; }

    }
}
