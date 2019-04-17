using System;
using System.Collections.Generic;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
