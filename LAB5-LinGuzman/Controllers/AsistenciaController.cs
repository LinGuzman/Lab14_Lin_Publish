using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IRepository<Asistencia> _asistenciaRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AsistenciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _asistenciaRepo = _unitOfWork.Repository<Asistencia>();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var asistencias = await _asistenciaRepo.GetAllAsync();
            return Ok(asistencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asistencia = await _asistenciaRepo.GetByIdAsync(id);
            if (asistencia == null)
                return NotFound();

            return Ok(asistencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asistencia asistencia)
        {
            await _asistenciaRepo.InsertAsync(asistencia);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = asistencia.IdAsistencia }, asistencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Asistencia asistencia)
        {
            var existing = await _asistenciaRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.IdEstudiante = asistencia.IdEstudiante;
            existing.IdCurso = asistencia.IdCurso;
            existing.Fecha = asistencia.Fecha;
            existing.Estado = asistencia.Estado;

            _asistenciaRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var asistencia = await _asistenciaRepo.GetByIdAsync(id);
            if (asistencia == null)
                return NotFound();

            _asistenciaRepo.Delete(asistencia);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
