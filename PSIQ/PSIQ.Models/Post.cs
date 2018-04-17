using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSIQ.Models
{
    public class Post
    {
        public int Cod { get; set; }
        public Terapeuta Terapeuta { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }

        public int IdUsuario
        {
            get
            {
                if (this.Terapeuta != null && this.Terapeuta.Cod > 0)
                    return this.Terapeuta.Cod;
                if (this.Paciente != null && this.Paciente.Cod > 0)
                    return this.Paciente.Cod;
                return 0;
            }
        }

        public string TipoUsuario
        {
            get
            {
                if (this.Terapeuta != null && this.Terapeuta.Cod > 0)
                    return "TERAPEUTA";
                if (this.Paciente != null && this.Paciente.Cod > 0)
                    return "PACIENTE";
                return string.Empty;
            }
        }

        public string NomeUsuario
        {
            get
            {
                if (this.Terapeuta != null && !string.IsNullOrWhiteSpace(this.Terapeuta.Nome))
                    return this.Terapeuta.Nome;
                if (this.Paciente != null && !string.IsNullOrWhiteSpace(this.Paciente.Nome))
                    return this.Paciente.Nome;
                return string.Empty;
            }
        }

        public string FotoUsuario
        {
            get
            {
                if (this.Terapeuta != null && !string.IsNullOrWhiteSpace(this.Terapeuta.Foto))
                    return this.Terapeuta.Foto;
                if (this.Paciente != null && !string.IsNullOrWhiteSpace(this.Paciente.Foto))
                    return this.Paciente.Foto;
                return "perfil-blog.png";
            }
        }
    }
}
