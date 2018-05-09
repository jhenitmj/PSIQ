using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSIQ.Models
{
    public class Terapeuta : Usuario
    {
        [Required]
        public string CRP { get; set; }
        [Required]
        public DateTime DtNascimento { get; set; }

        public List<Paciente> Pacientes { get; set; }

        public Terapeuta()
        {
            this.Pacientes = new List<Paciente>();
        }
    }
}