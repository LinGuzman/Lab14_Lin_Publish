using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly IRepository<Materia> _materiaRepo;
        private readonly IUnitOfWork _unitOfWork;

        public MateriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _materiaRepo = _unitOfWork.Repository<Materia>();
        }

        // Obtener todas las materias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materias = await _materiaRepo.GetAllAsync();
            return Ok(materias);
        }

        // Obtener materia por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var materia = await _materiaRepo.GetByIdAsync(id);
            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        // Crear nueva materia
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Materia materia)
        {
            await _materiaRepo.InsertAsync(materia);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = materia.IdMateria }, materia);
        }

        // Actualizar materia existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Materia materia)
        {
            var existing = await _materiaRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Nombre = materia.Nombre;
            existing.Descripcion = materia.Descripcion;
            existing.IdCurso = materia.IdCurso;

            _materiaRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // Eliminar una materia
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var materia = await _materiaRepo.GetByIdAsync(id);
            if (materia == null)
                return NotFound();

            _materiaRepo.Delete(materia);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
