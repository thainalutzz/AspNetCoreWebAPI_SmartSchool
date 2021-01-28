using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {        
        public readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        //api/Professor/byId
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(professor);
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
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado.");
        }

        //api/Professor
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof= _repo.GetProfessorById(id, false);
            if(prof == null) return BadRequest("Professor não encontrado.");
            
            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado.");
        }

        //api/Professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
            if(prof == null) return BadRequest("Professor não encontrado.");

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado.");
        }
        
        //api/Professor
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado.");

            _repo.Delete(professor);
            if(_repo.SaveChanges())
            {
                return Ok("Professor deletado.");
            }

            return BadRequest("Professor não deletado.");
        }
    }
}