using System;
using System.Collections.Generic;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedGroup.WebApi.Guilherme.Manha.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> listaUsuarios();
        Usuario BuscarEmailSenha(string email, string senha);
        Usuario BuscarUsuario(int usuarioId);
        void Cadastrar(Usuario usuario);
        void Alterar(Usuario usuario);
        void Deletar(int id);
        
    }
}
