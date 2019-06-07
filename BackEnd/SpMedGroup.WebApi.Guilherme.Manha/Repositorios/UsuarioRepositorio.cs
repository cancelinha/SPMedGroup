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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog = SpMedicalGroup; user id=sa; pwd=132";

        public List<Usuario> listaUsuarios()
        {
            string QuerySelect = @"SELECT U.IDUSUARIO AS ID_USUARIO,TU.NOME AS TIPO_USUARIO,TU.ID AS ID_TIPO_USUARIO, U.EMAIL,U.SENHA FROM USUARIO U JOIN TIPO_USUARIO TU ON U.ID_TIPO_USUARIO = TU.ID";
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.IdUsuario = Convert.ToInt32(sdr["ID_USUARIO"]);
                        usuario.IdTipoUsuario = Convert.ToInt32(sdr["ID_TIPO_USUARIO"]);
                        usuario.IdTipoUsuarioNavigation = new TipoUsuario();
                        usuario.IdTipoUsuarioNavigation.Nome = sdr["TIPO_USUARIO"].ToString();
                        usuario.Senha = sdr["SENHA"].ToString();
                        usuario.Email = sdr["EMAIL"].ToString();
                        listaUsuarios.Add(usuario);
                    }
                }

                return listaUsuarios;
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Add(usuario);
                ctx.SaveChanges();
            }
        }

        public Usuario BuscarEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string QuerySelect = @"select u.idusuario, u.email, u.senha, u.id_tipo_usuario, tu.nome as nometipousuario 
                        from usuario u inner join TIPO_USUARIO tu on tu.ID = u.ID_TIPO_USUARIO
                        where email = @email and senha = @senha";

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        Usuario usuario = new Usuario();

                        while (sdr.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(sdr["idusuario"]);
                            usuario.Email = sdr["email"].ToString();
                            usuario.IdTipoUsuarioNavigation = new TipoUsuario();
                            usuario.IdTipoUsuarioNavigation.Nome = sdr["nometipousuario"].ToString();
                        }
                        return usuario;
                    }
                }
                return null;
            }
        }
        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Remove(ctx.Usuario.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Usuario usuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Usuario usuarioExiste = ctx.Usuario.Find(usuario.IdUsuario);

                usuarioExiste.IdUsuario = usuario.IdUsuario;
                ctx.Usuario.Update(usuario);
                ctx.SaveChanges();
            }
        }

        public Usuario BuscarUsuario(int usuarioId)
        {
            Usuario usuarioBuscado = new Usuario();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                usuarioBuscado = ctx.Usuario.Find(usuarioId);
            }

            return usuarioBuscado;
        }
    }
}
