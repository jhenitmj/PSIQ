using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class Paciente
    {
        public int Cod { get; set; }
        public int CPF { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Foto { get; set; }
        public string CaminhoFoto { get; set; }
        public string Descricao { get; set; }
        public Terapeuta Terapeuta { get; set; }
        public Estado Estado { get; set; }
        public Diagnostico Diagnostico { get; set; }
    }
}