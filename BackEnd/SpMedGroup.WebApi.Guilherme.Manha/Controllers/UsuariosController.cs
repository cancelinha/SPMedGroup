using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Repositorios;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SpMedGroup.WebApi.Guilherme.Manha.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public UsuariosController()
        {
            UsuarioRepositorio = new UsuarioRepositorio();
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> listaUsuarios = UsuarioRepositorio.listaUsuarios();
                return Ok(listaUsuarios);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                UsuarioRepositorio.Cadastrar(usuario);

                return Ok(new
                {
                    mensagem = "Usuário Cadastrado"
                });
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR, MEDICO")]
        public IActionResult Alterar(Usuario usuario)
        {
            try
            {
                UsuarioRepositorio.Alterar(usuario);
                return Ok(UsuarioRepositorio.listaUsuarios());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Remove(ctx.Usuario.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

        // Lista um único Usuario especifico
        [Authorize (Roles = "ADMINISTRADOR")]
        [HttpGet("{usuarioId}")]
        public IActionResult GetUsuario(int usuarioId)
        {
            try
            {
                Usuario usuarioBuscado = UsuarioRepositorio.BuscarUsuario(usuarioId);

                if (usuarioBuscado == null)
                {
                    return NotFound(new { mensagem = "Usuário não encontrado" });
                }

                return Ok(usuarioBuscado);
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}