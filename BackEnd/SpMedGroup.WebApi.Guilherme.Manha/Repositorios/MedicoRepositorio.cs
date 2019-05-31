using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Repositorios
{
    public class MedicoRepositorio : IMedicoRepositorio
    {
        private string StringConexao = "Data Source=.\\SqlExpress; Initial Catalog= SPMEDICALGROUP; user id = sa; pwd = 132";

        public List<Medico> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                //.Include(C => C.Usuarios).
                return ctx.Medico.ToList();
            }
        }

        public void Cadastrar(Medico medico)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Medico.Add(medico);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Medico.Remove(ctx.Medico.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Medico medico)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Medico medicoExiste = ctx.Medico.Find(medico.Id);

                if
                (medicoExiste.Id == medico.Id)
                {
                    medicoExiste.Nome = medico.Nome;
                    medicoExiste.Crm = medico.Crm;
                    medicoExiste.IdEspecialidade = medico.IdEspecialidade;
                    medicoExiste.IdEspecialidadeNavigation = medico.IdEspecialidadeNavigation;
                    medicoExiste.IdClinica = medico.IdClinica;
                    medicoExiste.IdClinicaNavigation = medico.IdClinicaNavigation;
                    medicoExiste.IdUsuario = medico.IdUsuario;
                    medicoExiste.IdUsuarioNavigation = medico.IdUsuarioNavigation;

                    ctx.Medico.Update(medicoExiste);
                    ctx.SaveChanges();
                }

            }
        }

        public Medico BuscarMedico(int Id)
        {
            Medico medicoBuscado = new Medico();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                medicoBuscado = ctx.Medico.Find(Id);
            }
            return medicoBuscado;
        }

    }
}
