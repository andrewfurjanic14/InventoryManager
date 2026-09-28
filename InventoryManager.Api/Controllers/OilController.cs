using InventoryManager.Shared.Dtos;
using InventoryManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace InventoryManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OilsController : ControllerBase
    {
        private readonly IOilService _service;

        public OilsController(IOilService service)
        {
            _service = service;
        }

        // GET api/oils
        [HttpGet]
        public async Task<ActionResult<List<OilDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET api/oils/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OilDto>> GetById(int id)
        {
            var oil = await _service.GetByIdAsync(id);
            return oil is null ? NotFound() : Ok(oil);
        }

        // POST api/oils
        [HttpPost]
        public async Task<ActionResult<OilDto>> Create(SaveOilDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.OilId }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT api/oils/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SaveOilDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return updated ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/oils/5
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
