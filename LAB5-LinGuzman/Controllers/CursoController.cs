using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly IRepository<Curso> _cursoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CursoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cursoRepo = _unitOfWork.Repository<Curso>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cursos = await _cursoRepo.GetAllAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var curso = await _cursoRepo.GetByIdAsync(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Curso curso)
        {
            await _cursoRepo.InsertAsync(curso);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Curso curso)
        {
            var existing = await _cursoRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Nombre = curso.Nombre;
            existing.Descripcion = curso.Descripcion;
            existing.Creditos = curso.Creditos;

            _cursoRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoRepo.GetByIdAsync(id);
            if (curso == null)
                return NotFound();

            _cursoRepo.Delete(curso);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
