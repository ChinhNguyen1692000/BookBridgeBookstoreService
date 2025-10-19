using BookstoreService.Application.Interface;
using BookstoreService.Application.Models;
using BookstoreService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookstoreService.Api.Controllers
{
    [ApiController]
    [Route("api/bookstores")]
    public class BookstoreController : ControllerBase
    {
        private readonly IBookstoreService _service;

        public BookstoreController(IBookstoreService service)
        {
            _service = service;
        }

        [HttpGet("/api/healthz")]
        public IActionResult HealthCheck() => Ok("Healthy");

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            var list = await _service.GetAllAsync(pageNo, pageSize);
            return Ok(list);
        }

        // POST: api/bookstores
        [HttpPost]
        [Consumes("multipart/form-data")] // <-- Nhận form-data
        public async Task<IActionResult> Create([FromForm] BookstoreCreateRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/bookstores/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookstore = await _service.GetByIdAsync(id);
            if (bookstore == null) return NotFound();
            return Ok(bookstore);
        }

        // PUT: api/bookstores
        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] BookstoreUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/bookstores/ban/{id}
        [HttpDelete("ban/{id}")]
        public async Task<IActionResult> Ban(int id)
        {
            try
            {
                var result = await _service.Ban(id);
                return Ok(new { banned = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetByOwnerId(string ownerId)
        {
            var result = await _service.GetByOwnerId(ownerId);
            return Ok(result);
        }
    }
}
