using InventoryManager.Api.DTOs;
using InventoryManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _service;

        public ProvidersController(IProviderService service)
        {
            _service = service;
        }

        // GET api/providers
        [HttpGet]
        public async Task<ActionResult<List<ProviderDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET api/providers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProviderDto>> GetById(int id)
        {
            var provider = await _service.GetByIdAsync(id);
            return provider is null ? NotFound() : Ok(provider);
        }

        // POST api/providers
        [HttpPost]
        public async Task<ActionResult<ProviderDto>> Create(SaveProviderDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ProviderId }, created);
        }

        // PUT api/providers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SaveProviderDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        // DELETE api/providers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
