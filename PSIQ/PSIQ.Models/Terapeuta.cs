using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Terapeuta
    {
        public string   Nome        { get; set; }
        public int      CRP         { get; set; }
        public string   Senha       { get; set; }
        public DateTime DtNasciento { get; set; }
        public int      Idade       { get; set; }
        public int      COD         { get; set; }
        public Pacientes Paciente   { get; set; }

    }
}
