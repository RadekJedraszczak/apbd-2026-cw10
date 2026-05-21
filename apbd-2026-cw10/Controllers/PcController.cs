using apbd_2026_cw10.Data;
using apbd_2026_cw10.Exceptions;
using apbd_2026_cw10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PcController : ControllerBase
{
    private readonly IDbService _dbService;
    public PcController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pcs = await _dbService.GetAllResponse();
        return Ok(pcs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var pc = await _dbService.GetByIdResponse(id);
            return Ok(pc);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdatePcDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPc = await _dbService.CreatePcAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdPc.Id }, createdPc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateOrUpdatePcDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _dbService.UpdatePcAsync(id, dto);
            
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _dbService.DeletePcAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}