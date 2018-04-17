using System;
using System.ComponentModel.DataAnnotations;

namespace PSIQ.Models
{
    public class Terapeuta : Usuario
    {
        [Required]
        public string CRP { get; set; }
        [Required]
        public DateTime DtNascimento { get; set; }
    }
}