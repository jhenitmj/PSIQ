using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace PSIQ.Models
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Cod { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
    }
}
