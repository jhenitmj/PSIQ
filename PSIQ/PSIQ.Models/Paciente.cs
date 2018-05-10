using System;
using System.Collections.Generic;

namespace PSIQ.Models
{
    public class Paciente : Usuario
    {
        public string CPF { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Descricao { get; set; }
        public Terapeuta Terapeuta { get; set; }
        public Estado Estado { get; set; }
        public List<PacienteXDiagnostico> Diagnosticos { get; set; }
        public List<Post> Posts { get; set; }


        public Paciente()
        {
            this.Diagnosticos = new List<PacienteXDiagnostico>();
            this.Posts = new List<Post>();
        }
    }
}