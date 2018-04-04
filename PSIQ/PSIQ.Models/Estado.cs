using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.Models
{
    public class Estado
    {
        public int Cod { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public string Nome { get; set; }
    }
}
