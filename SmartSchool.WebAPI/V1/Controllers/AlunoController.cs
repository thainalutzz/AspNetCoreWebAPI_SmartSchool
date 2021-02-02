using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <sumary>
    /// Versão 1 do controlador de Aluno
    /// </sumary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <sumary>
        ///
        /// </sumary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /*public List<Aluno> Alunos = new List<Aluno>(){
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
        };*/

        /// <sumary>
        /// Método responsável por retornar todos os alunos
        /// </sumary>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <sumary>
        /// Método responsável por retornar apenas um aluno através do Id
        /// </sumary>
        //api/aluno/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        /*//api/aluno/Laura
        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome));
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno/byId?id=2
        [HttpGet("byId")]
        public IActionResult GetByIdQueryString(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        //api/aluno/byName?Nome=Marta&Sobrenome=Kent
        [HttpGet("byName")]
        public IActionResult GetByNameQueryString(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a =>
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }*/

        //api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto alunoRegistrarDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoRegistrarDto);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoRegistrarDto.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado.");
        }

        //api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto alunoRegistrarDto)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _mapper.Map(alunoRegistrarDto, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoRegistrarDto.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado.");
        }

        //api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto alunoRegistrarDto)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _mapper.Map(alunoRegistrarDto, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoRegistrarDto.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado.");
        }

        //api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado.");
            }

            return BadRequest("Aluno não deletado.");
        }
    }
}