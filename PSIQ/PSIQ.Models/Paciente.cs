using System;

namespace PSIQ.Models
{
    public class Paciente : Usuario
    {
        public string CPF { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Descricao { get; set; }
        public Terapeuta Terapeuta { get; set; }
        public Estado Estado { get; set; }
        public Diagnostico Diagnostico { get; set; }
    }
}