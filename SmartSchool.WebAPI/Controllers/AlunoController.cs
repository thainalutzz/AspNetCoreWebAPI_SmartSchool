using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Almeida",
                Telefone = "123456789"
            },
            new Aluno(){
                Id = 2,
                Nome = "Marta",
                Sobrenome = "Kent",
                Telefone = "987654321"
            },
            new Aluno(){
                Id = 3,
                Nome = "Laura",
                Sobrenome = "Maria",
                Telefone = "123459876"
            },
        };
        public AlunoController(){ }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }        

        //api/aluno/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno/Laura
        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome));
            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno/byId?id=2
        [HttpGet("byId")]
        public IActionResult GetByIdQueryString(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno/byName?Nome=Marta&Sobrenome=Kent
        [HttpGet("byName")]
        public IActionResult GetByNameQueryString(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a =>
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );
            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        //api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        //api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
        
        //api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}