using AddressBook.Application.DTOs.JobDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class JobsController : ControllerBase
    {
        private readonly IJobService _service;

        public JobsController(IJobService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _service.GetByIdAsync(id);

            if (job == null)
                return NotFound();

            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobCreateDTO dto)
        {
            var newId = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobUpdateDTO dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
