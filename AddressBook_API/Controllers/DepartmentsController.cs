using AddressBook.Application.DTOs.DepartmentDTOs;
using AddressBook.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dept = await _departmentService.GetByIdAsync(id);

            if (dept == null)
                return NotFound();

            return Ok(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int newId = await _departmentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _departmentService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
