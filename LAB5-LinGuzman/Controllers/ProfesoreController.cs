using LAB5_LinGuzman.Models;
using LAB5_LinGuzman.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoreController : ControllerBase
    {
        private readonly IRepository<Profesore> _profesorRepo;

        private readonly IUnitOfWork _unitOfWork;

        public ProfesoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _profesorRepo = _unitOfWork.Repository<Profesore>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profesores = await _profesorRepo.GetAllAsync();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profesor = await _profesorRepo.GetByIdAsync(id);
            if (profesor == null)
                return NotFound();

            return Ok(profesor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Profesore profesor)
        {
            await _profesorRepo.InsertAsync(profesor);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = profesor.IdProfesor }, profesor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Profesore profesor)
        {
            var existing = await _profesorRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Nombre = profesor.Nombre;
            existing.Correo = profesor.Correo;

            _profesorRepo.Update(existing);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profesor = await _profesorRepo.GetByIdAsync(id);
            if (profesor == null)
                return NotFound();

            _profesorRepo.Delete(profesor);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
