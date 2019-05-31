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
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MedicoController : ControllerBase
    {
        private IMedicoRepositorio MedicoRepositorio { get; set; }

        public MedicoController()
        {
            MedicoRepositorio = new MedicoRepositorio();
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(MedicoRepositorio.Listar());
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(Medico medico)
        {
            try
            {
                MedicoRepositorio.Cadastrar(medico);
                return Ok(new { mensagem = "Medico Cadastrado com Sucesso !" });

            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut]
        public IActionResult Alterar(Medico medico)
        {
            try
            {
                MedicoRepositorio.Alterar(medico);
                return Ok(MedicoRepositorio.Listar());
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                MedicoRepositorio.Deletar(id);
                return Ok(MedicoRepositorio.Listar());
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Lista um único Medico especifico
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{Id}")]
        public IActionResult GetMedico(int Id)
        {
            try
            {
                Medico medicoBuscado = MedicoRepositorio.BuscarMedico(Id);
                if (medicoBuscado == null)
                {
                    return NotFound(new { mensagem = "Medico não encontrado!" });
                }
                return Ok(medicoBuscado);
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}