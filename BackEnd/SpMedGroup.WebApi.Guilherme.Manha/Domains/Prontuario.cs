using System;
using System.Collections.Generic;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class Prontuario
    {
        public Prontuario()
        {
            Consultas = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Endereco { get; set; }
        public int? IdUsuario { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
    }
}
