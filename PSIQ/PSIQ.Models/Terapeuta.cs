using System;

namespace PSIQ.Models
{
    public class Terapeuta : Usuario
    {
        public int CRP { get; set; }
        public DateTime DtNascimento { get; set; }
    }
}