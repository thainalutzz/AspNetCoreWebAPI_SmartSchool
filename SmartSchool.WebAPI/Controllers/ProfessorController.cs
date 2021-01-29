using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        //api/Professor/byId
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O Professor não foi encontrado.");

            var ProfessorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(ProfessorDto);
        }

        /*//api/Professor/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var Professor = _repo.GetProfessorById(id, false);
            if(Professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(Professor);
        }

        //api/Professor/nome
        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {
            var Professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if(Professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(Professor);
        }*/

        //api/Professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _mapper.Map<Professor>(professorRegistrarDto);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{professorRegistrarDto.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não cadastrado.");
        }

        //api/Professor
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(professorRegistrarDto, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{professorRegistrarDto.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado.");
        }

        //api/Professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(professorRegistrarDto, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{professorRegistrarDto.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado.");
        }

        //api/Professor
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado.");
            }

            return BadRequest("Professor não deletado.");
        }
    }
}