using System;
using System.Collections.Generic;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consultas = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        public string Crm { get; set; }
        public string Nome { get; set; }
        public int? IdEspecialidade { get; set; }
        public int? IdClinica { get; set; }
        public int? IdUsuario { get; set; }

        public Clinica IdClinicaNavigation { get; set; }
        public Especialidade IdEspecialidadeNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
    }
}
