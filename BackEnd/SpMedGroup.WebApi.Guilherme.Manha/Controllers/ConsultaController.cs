using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using SpMedGroup.WebApi.Guilherme.Manha.Repositorios;

namespace SpMedGroup.WebApi.Guilherme.Manha.SpMedGroup.WebApi.Guilherme.Manha.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        
        private IConsultaRepositorio ConsultaRepositorio { get; set; }
        public ConsultaController()
        {
            ConsultaRepositorio = new ConsultaRepositorio();
        }
        [Authorize(Roles= "ADMINISTRADOR, MEDICO, PACIENTE")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                 
                int idrecebido = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                string tipousuario = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value.ToString();

                List<Consulta> listaConsultas = ConsultaRepositorio.BuscarConsulta(idrecebido, tipousuario);
                return Ok(listaConsultas);
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(Consulta consulta)
        {
            try
            {
                ConsultaRepositorio.Cadastrar(consulta);
                return Ok(new { mensagem = "Consulta Cadastrada" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "ADMINISTRADOR, MEDICO")]
        public IActionResult Alterar( Consulta consulta, int Id)
        {
            try
            {
                ConsultaRepositorio.Alterar(consulta, Id);
                return Ok(new { mensagem = "Consulta Alterada" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINSITRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Remove(ctx.Consulta.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }


    }
}