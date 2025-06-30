using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluacioneController : ControllerBase
    {
        private readonly IRepository<Evaluacione> _evaluacionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EvaluacioneController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _evaluacionRepo = _unitOfWork.Repository<Evaluacione>();
        }

        // Obtener todas las evaluaciones
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evaluaciones = await _evaluacionRepo.GetAllAsync();
            return Ok(evaluaciones);
        }

        // Obtener evaluación por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evaluacion = await _evaluacionRepo.GetByIdAsync(id);
            if (evaluacion == null)
                return NotFound();

            return Ok(evaluacion);
        }

        // Crear nueva evaluación
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Evaluacione evaluacion)
        {
            await _evaluacionRepo.InsertAsync(evaluacion);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = evaluacion.IdEvaluacion }, evaluacion);
        }

        // Actualizar evaluación existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Evaluacione evaluacion)
        {
            var existing = await _evaluacionRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.IdEstudiante = evaluacion.IdEstudiante;
            existing.IdCurso = evaluacion.IdCurso;
            existing.Calificacion = evaluacion.Calificacion;
            existing.Fecha = evaluacion.Fecha;

            _evaluacionRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // Eliminar una evaluación
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var evaluacion = await _evaluacionRepo.GetByIdAsync(id);
            if (evaluacion == null)
                return NotFound();

            _evaluacionRepo.Delete(evaluacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
