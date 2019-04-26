using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using SpMedGroup.WebApi.Guilherme.Manha.Repositorios;

namespace SpMedGroup.WebApi.Guilherme.Manha.Controllers
{   [Produces ("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProntuarioController : ControllerBase
    {
        private IProntuarioRepositorio ProntuarioRepositorio { get; set; }

        public ProntuarioController()
        {
            ProntuarioRepositorio = new ProntuarioRepositorio();
        }

        [Authorize(Roles = "ADMINISTRADOR , MEDICO")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ProntuarioRepositorio.Listar());
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // TODO não está cadastrando consultas. Tem que arrumar. 
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(Prontuario prontuario)
        {
            try
            {
                ProntuarioRepositorio.Cadastrar(prontuario);
                return Ok(new {mensagem = "Prontuario Cadastrado!"});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }       
        }

        [Authorize(Roles = "ADMINISTRADOR, MEDICO")]
        [HttpPut]
        public IActionResult Alterar(Prontuario prontuario)
        {
            try
            {
                ProntuarioRepositorio.Alterar(prontuario);
                return Ok(new { mensagem = "Prontuário Modificado!" });
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                using (SpMedGroupContext ctx = new SpMedGroupContext())
                {
                    ctx.Prontuario.Remove(ctx.Prontuario.Find(id));
                    ctx.SaveChanges();
                }
                return Ok();
            }
            catch(SystemException ex) {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR, PACIENTE")]
        [HttpGet("/buscar/{prontuarioId}")]
        public IActionResult Get(int prontuarioId)
        {
            try
            {
                Prontuario prontuarioBuscado = ProntuarioRepositorio.BuscarProntuario(prontuarioId);

                if (prontuarioBuscado == null)
                {
                    return NotFound(new { mensagem = "Prontuario não encontrado" });
                }

                return Ok(prontuarioBuscado);
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}