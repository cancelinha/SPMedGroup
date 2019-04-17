using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Interfaces
{
    interface IClinicaRepositorio
    {
        void Cadastrar(Clinica clinica);
        void Alterar(Clinica clinica);
        void Deletar(int id);
        List<Clinica> Listar();
    }
}
