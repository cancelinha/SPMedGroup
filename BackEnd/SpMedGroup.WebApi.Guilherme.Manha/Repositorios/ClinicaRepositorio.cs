using Microsoft.EntityFrameworkCore;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Repositorios
{
    public class ClinicaRepositorio : IClinicaRepositorio
    {
        public List<Clinica> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Clinica.Include(C => C.Medico).ToList();
            }
        }

        public void Cadastrar (Clinica clinica)
        {
            using(SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Clinica.Add(clinica);
                ctx.SaveChanges();
            }
        }

        public void Deletar (int id)
        {
            using (SpMedGroupContext ctx= new SpMedGroupContext())
            {
                ctx.Clinica.Remove(ctx.Clinica.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Clinica clinica)
        {
            using(SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Clinica clinicaExiste = ctx.Clinica.Find(clinica.Id);
                clinicaExiste.Id = clinica.Id;
                ctx.Clinica.Update(clinica);
                ctx.SaveChanges();
            }
        }
    }
}
