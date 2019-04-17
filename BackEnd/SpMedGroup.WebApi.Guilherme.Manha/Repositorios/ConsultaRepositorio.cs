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
                    listaConsultaBuscada = ctx.Consulta.ToList();
                }
                else if (tipousuario == "MEDICO")
                {
                    listaConsultaBuscada = ctx.Consulta.ToList().FindAll(c => c.IdMedico == idrecebido);
                }
                else
                {
                    listaConsultaBuscada = ctx.Consulta.ToList().FindAll(c => c.IdProntuario == idrecebido);
                }
                                 
                return listaConsultaBuscada;
            }
        }


    }
}
