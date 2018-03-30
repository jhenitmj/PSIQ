using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Paciente
    {
        public int COD { get; set; }
        public int CPF { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DtNasciento { get; set; }
        public string Foto { get; set; }
        public string Caminho_foto { get; set; }
        public string DescricaoD { get; set; }
        public Estado Estado { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public Terapeuta   Terapeuta   { get; set; }
    }
}
