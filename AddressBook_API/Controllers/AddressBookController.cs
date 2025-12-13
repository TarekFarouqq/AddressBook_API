using AddressBook.Application.DTOs.AddressBookDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService _service;

        public AddressBookController(IAddressBookService service)
        {
            _service = service;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] AddressBookSearchQuery query)
        {
            var result = await _service.SearchAsync(query);
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            foreach (var item in result.Items)
            {
                if (item.PhotoFileName != null)
                { 
                    item.PhotoFileName = $"{baseUrl}/{item.PhotoFileName}";
                }
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddressBookCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new AddressBookCreateDto
            {
                FullName = request.FullName,
                JobId = request.JobId,
                DepartmentId = request.DepartmentId,
                MobileNumber = request.MobileNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Email = request.Email
            };

            Stream? photoStream = null;
            string? originalFileName = null;

            if (request.Photo != null && request.Photo.Length > 0)
            {
                photoStream = request.Photo.OpenReadStream();
                originalFileName = request.Photo.FileName;
            }

            var id = await _service.CreateAsync(dto, photoStream, originalFileName);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddressBookReadDto dto)
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

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var bytes = await _service.ExportToExcelAsync();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "addressbook.xlsx");
        }
    }
}
