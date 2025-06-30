using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly IGenericRepository<Estudiante> _estudianteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EstudianteController(IGenericRepository<Estudiante> estudianteRepository, IUnitOfWork unitOfWork)
        {
            _estudianteRepository = estudianteRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _estudianteRepository.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estudiante = await _estudianteRepository.GetByIdAsync(id);
            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Estudiante estudiante)
        {
            await _estudianteRepository.InsertAsync(estudiante);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Estudiante estudiante)
        {
            var existing = await _estudianteRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Nombre = estudiante.Nombre;
            existing.Edad = estudiante.Edad;
            existing.Direccion = estudiante.Direccion;
            existing.Telefono = estudiante.Telefono;
            existing.Correo = estudiante.Correo;

            _estudianteRepository.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _estudianteRepository.GetByIdAsync(id);
            if (estudiante == null)
                return NotFound();

            _estudianteRepository.Delete(estudiante);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
