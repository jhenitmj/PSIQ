using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class PacienteXDiagnostico
    {
        public int Cod { get; set; }

        public DateTime DataHora { get; set; }

        public Paciente Paciente { get; set; }

        public Diagnostico Diagnostico { get; set; }

        public string Descricao { get; set; }
    }
}
