using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using SpMedGroup.WebApi.Guilherme.Manha.Repositorios;
using SpMedGroup.WebApi.Guilherme.Manha.ViewModel;

namespace SpMedGroup.WebApi.Guilherme.Manha.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public LoginController()
        {
            UsuarioRepositorio = new UsuarioRepositorio();
        }

        [HttpPost]
        public IActionResult Post(LoginViewModel login)
        {
            try
            {
                Usuario usuarioBuscado = UsuarioRepositorio.BuscarEmailSenha(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    return NotFound(new
                    {
                        mensagem = "Email ou senha inválido"
                    });
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuarioNavigation.Nome.ToString()),
                    new Claim("TipoUsuario", usuarioBuscado.IdTipoUsuarioNavigation.Nome.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("spmedgroup-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "SpMedGroup.WebApi",
                    audience: "SpMedGroup.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}