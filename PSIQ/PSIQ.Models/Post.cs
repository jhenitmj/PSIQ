using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class Post
    {
        public int Cod { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
    }
}
