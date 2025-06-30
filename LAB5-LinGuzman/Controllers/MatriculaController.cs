using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly IRepository<Matricula> _matriculaRepo;
        private readonly IUnitOfWork _unitOfWork;

        public MatriculaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _matriculaRepo = _unitOfWork.Repository<Matricula>();
        }

        // Obtener todas las matrículas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matriculas = await _matriculaRepo.GetAllAsync();
            return Ok(matriculas);
        }

        // Obtener matrícula por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matricula = await _matriculaRepo.GetByIdAsync(id);
            if (matricula == null)
                return NotFound();

            return Ok(matricula);
        }

        // Crear nueva matrícula
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Matricula matricula)
        {
            await _matriculaRepo.InsertAsync(matricula);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = matricula.IdMatricula }, matricula);
        }

        // Actualizar matrícula existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
        {
            var existing = await _matriculaRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.IdEstudiante = matricula.IdEstudiante;
            existing.IdCurso = matricula.IdCurso;
            existing.Semestre = matricula.Semestre;

            _matriculaRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // Eliminar una matrícula
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var matricula = await _matriculaRepo.GetByIdAsync(id);
            if (matricula == null)
                return NotFound();

            _matriculaRepo.Delete(matricula);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
