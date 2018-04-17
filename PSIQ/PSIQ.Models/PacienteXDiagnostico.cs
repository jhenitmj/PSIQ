using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class PacienteXDiagnostico
    {
        public DateTime DataHora { get; set; }

        public Paciente Paciente { get; set; }

        public Diagnostico Diagnostico { get; set; }
    }
}
