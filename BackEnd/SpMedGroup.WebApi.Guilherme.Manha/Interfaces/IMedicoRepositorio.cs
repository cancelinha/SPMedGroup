using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Interfaces
{
    interface IMedicoRepositorio
    {
        List<Medico> Listar();
        Medico BuscarMedico(int Id);
        void Cadastrar(Medico medico);
        void Alterar(Medico medico);
        void Deletar(int id);
    }
}
