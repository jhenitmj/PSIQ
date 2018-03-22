using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Post
    {
        public int Cod { get; set;}
        public DateTime Data { get; set; }
        public string Conteudo { get; set; }
        public Edicao Edicao { get; set; }
        public Comentario Comentario { get; set; }
        public Curtida Curtida { get; set; }

    }
}
