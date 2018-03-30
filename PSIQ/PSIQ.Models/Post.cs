using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Post
    {
        public int Cod             { get; set; }
        public Terapeuta Terapeuta { get; set; }
        public Paciente Paciente   { get; set; }
        public DateTime DataHora   { get; set; }
        public string   Mensagem   { get; set; }

    }
}
