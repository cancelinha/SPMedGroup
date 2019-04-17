using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Interfaces
{
    interface IProntuarioRepositorio
    {
        List<Prontuario> Listar();
        void Cadastrar(Prontuario prontuario);
        void Deletar(int id);
        void Alterar(Prontuario prontuario);
        Prontuario BuscarProntuario(int prontuarioId);
    }
}
