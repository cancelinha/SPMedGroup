using Microsoft.EntityFrameworkCore;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Repositorios
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        public void Cadastrar(Consulta consulta)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Add(consulta);
                ctx.SaveChanges();
            }
        }

        //public List<Consulta> Listar()
        //{
        //    using (SpMedGroupContext ctx = new SpMedGroupContext())
        //    {
        //        return ctx.Consulta.ToList();
        //    }

        //}

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Remove(ctx.Consulta.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Consulta consulta, int Id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Consulta consultaExiste = ctx.Consulta.Find(Id);
                consultaExiste.Descricao = consulta.Descricao;
                ctx.Consulta.Update(consultaExiste);
                ctx.SaveChanges();
            }
        }

               public List<Consulta> BuscarConsulta(int idrecebido, string tipousuario)
        {
            List<Consulta> listaConsultaBuscada = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                if (tipousuario == "ADMINISTRADOR")
                {
                    listaConsultaBuscada = ctx.Consulta.Include(c => c.IdMedicoNavigation).Include(x => x.IdStatusNavigation).ToList();
                }
                else if (tipousuario == "MEDICO")
                {
                    Medico medicoBuscado = ctx.Medico.ToList().Find(c => c.IdUsuario == idrecebido  );
                    listaConsultaBuscada = ctx.Consulta.Where(c => c.IdMedico == medicoBuscado.Id).ToList();
                }
                else
                {
                    Prontuario prontuarioBuscado = ctx.Prontuario.ToList().Find(c => c.IdUsuario == idrecebido);
                    listaConsultaBuscada = ctx.Consulta.Where(c => c.IdProntuario == prontuarioBuscado.Id).ToList();
                   
                }
                                 
                return listaConsultaBuscada;
            }
        }


    }
}
