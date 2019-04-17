using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Interfaces
{
    interface IConsultaRepositorio
    {
        void Cadastrar(Consulta consulta);
        void Deletar(int id);
        void Alterar(Consulta consulta , int Id);
        List<Consulta> BuscarConsulta(int idrecebido, string tipousuario);
    }
}
