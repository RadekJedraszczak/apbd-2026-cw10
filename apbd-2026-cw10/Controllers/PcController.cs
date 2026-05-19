using apbd_2026_cw10.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PcController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public PcController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var pcs = await _dbContext.PCs.ToListAsync();
        return Ok(_dbContext);
    }
}