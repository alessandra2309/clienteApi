using ClienteAPI.Dtos;
using ClienteAPI.Models;
using ClienteAPI.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;

namespace ClienteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
   
        [HttpPost]
        public IActionResult Postar([FromBody] ClienteDTO pes)
        {
            // Valida o CPF
            if (ValidarCpf.ValidaCPF(pes.Cpf) == false)
            {
                return BadRequest("cpf invalido");
            }

            var cliente = Salvamento.Salvar(pes);
            return Ok(cliente);

        }


        [HttpGet]

        public IActionResult Get()
        {

            return Ok(Salvamento.Percorrer());
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {

            return Ok(Salvamento.ProcurarPorId(id));
        }

        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] ClienteDTO pes)
        {

            if (pes == null)
            {
                return NotFound();
            }

            if (ValidarCpf.ValidaCPF(pes.Cpf) == false)
            {
                return BadRequest("cpf invalido");
            }

            var cliente = Salvamento.Atualizar(pes, id);
            return Ok(cliente);
           
        }

        [HttpDelete("{id}")]

        public IActionResult delete(int id)
        {
           
            var checar = Salvamento.Deletar(id);
            if (!checar)
            {
                return BadRequest("id Não achado");
            }

            return Ok(checar);
            
        }

    }    
}





