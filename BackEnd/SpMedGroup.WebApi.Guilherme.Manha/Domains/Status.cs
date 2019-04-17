using System;
using System.Collections.Generic;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class Status
    {
        public Status()
        {
            Consultas = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        public string Situacao { get; set; }

        public ICollection<Consulta> Consultas { get; set; }
    }
}
