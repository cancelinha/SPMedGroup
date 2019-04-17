using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedGroup.WebApi.Guilherme.Manha.Domains;
using SpMedGroup.WebApi.Guilherme.Manha.Interfaces;
using SpMedGroup.WebApi.Guilherme.Manha.Repositorios;

namespace SpMedGroup.WebApi.Guilherme.Manha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClinicaController : ControllerBase
    {
        private IClinicaRepositorio ClinicaRepositorio { get; set; }

        public ClinicaController()
        {
            ClinicaRepositorio = new ClinicaRepositorio();
        }

        [Authorize (Roles = "ADMINISTRADOR")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ClinicaRepositorio.Listar());
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(Clinica clinica)
        {
            try
            {
                ClinicaRepositorio.Cadastrar(clinica);
                return Ok(new { mensagem ="Clínica Cadastrada"});
            }
            catch(SystemException ex)
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
                ClinicaRepositorio.Deletar(id);
                
                return Ok(new {mensagem= "Clínica Apagada" });
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}