using InventoryManager.Shared.Dtos;
using InventoryManager.Api.Services;
using InventoryManager.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OilBatchesController : ControllerBase
    {
        private readonly IOilBatchService _service;

        public OilBatchesController(IOilBatchService service)
        {
            _service = service;
        }

        // GET api/oilbatches
        // GET api/oilbatches?oilId=3
        // GET api/oilbatches?status=Active
        // GET api/oilbatches?oilId=3&status=Active
        [HttpGet]
        public async Task<ActionResult<List<OilBatchDto>>> GetAll(
            [FromQuery] int? oilId, [FromQuery] BatchStatus? status)
        {
            return Ok(await _service.GetAllAsync(oilId, status));
        }

        // GET api/oilbatches/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OilBatchDto>> GetById(int id)
        {
            var batch = await _service.GetByIdAsync(id);
            return batch is null ? NotFound() : Ok(batch);
        }

        // POST api/oilbatches
        [HttpPost]
        public async Task<ActionResult<OilBatchDto>> Create(CreateOilBatchDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.BatchId }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/oilbatches/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateOilBatchDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return updated ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/oilbatches/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
