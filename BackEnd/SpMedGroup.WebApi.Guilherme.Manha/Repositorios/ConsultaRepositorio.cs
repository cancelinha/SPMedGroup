using Microsoft.EntityFrameworkCore;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Repositorios
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=SPMEDICALGROUP; user=sa; pwd=132";
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

        public List<Consulta> ListarConsultas(int idrecebido, string tipousuario)
        {


            string QuerySelect = @"SELECT M.ID AS ID_MEDICO,S.ID AS ID_STATUS,P.ID AS ID_PRONTUARIO, C.ID, P.NOME AS PRONTUARIO, M.NOME AS MEDICO, C.DATA, S.SITUACAO AS SITUACAO, C.DESCRICAO FROM CONSULTAS C JOIN PRONTUARIO P ON C.ID_PRONTUARIO = P.ID JOIN MEDICO M ON C.ID_MEDICO = M.ID JOIN STATUS S ON C.ID_STATUS = S.ID;";

            if (tipousuario == "MEDICO")
            {
                QuerySelect += " WHERE ID_MEDICO =" + idrecebido;
            }
            else if (tipousuario == "PACIENTE")
            {
                QuerySelect += " WHERE ID_PRONTUARIO =" + idrecebido;
            }

                
            List<Consulta> listaConsultas = new List<Consulta>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Consulta consulta = new Consulta();
                        consulta.Id = Convert.ToInt32(sdr["ID"]);
                        consulta.IdMedico = Convert.ToInt32(sdr["ID_MEDICO"]);
                        consulta.IdProntuarioNavigation = new Prontuario();
                        consulta.IdProntuarioNavigation.Nome = sdr["PRONTUARIO"].ToString();
                        consulta.IdProntuario= Convert.ToInt32(sdr["ID_PRONTUARIO"]);
                        consulta.IdMedicoNavigation = new Medico();
                        consulta.IdMedicoNavigation.Nome = sdr["MEDICO"].ToString();
                        consulta.IdStatus= Convert.ToInt32(sdr["ID_STATUS"]);
                        consulta.IdStatusNavigation = new Status();
                        consulta.IdStatusNavigation.Situacao = sdr["SITUACAO"].ToString();
                        consulta.Data = Convert.ToDateTime(sdr["DATA"]);
                        consulta.Descricao = sdr["DESCRICAO"].ToString();                     
                        listaConsultas.Add(consulta);
                    }
                }
            }

            return listaConsultas;
        }
    }
}
