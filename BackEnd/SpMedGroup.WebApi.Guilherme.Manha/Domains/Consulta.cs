using System;
using System.Collections.Generic;

namespace SpMedGroup.WebApi.Guilherme.Manha.Domains
{
    public partial class Consulta
    {
        public int Id { get; set; }
        public int? IdProntuario { get; set; }
        public int? IdMedico { get; set; }
        public DateTime Data { get; set; }
        public int? IdStatus { get; set; }
        public string Descricao { get; set; }

        public Medico IdMedicoNavigation { get; set; }
        public Prontuario IdProntuarioNavigation { get; set; }
        public Status IdStatusNavigation { get; set; }
    }
}
