using System.Globalization;
using apbd_2026_cw10.Data;
using apbd_2026_cw10.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _dbContext;

    public DbService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPcsDto>> GetAllResponse()
    {
        var pcs = await _dbContext.PCs.ToListAsync();
        return pcs.Select(p => new GetPcsDto()
        {
            Id = p.Id,
            Name = p.Name,
            Weight = p.Weight,
            Warranty = p.Warranty,
            CreatedAt = p.CreatedAt,
            Stock = p.Stock
        });
    }

    public async Task<PcWithComponentsDto> GetByIdResponse(int id)
    {
        var pc = await _dbContext.PCs
            .Where(p => p.Id == id)
            .Select(p => new PcWithComponentsDto()
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock,
                PcComponents = p.PcComponents.Select(pc => new GetPcComponentsDto()
                {
                    Amount = pc.Amount,
                    Component = new GetComponentDto
                    {
                        Code = pc.Component.Code,
                        Name = pc.Component.Name,
                        Description = pc.Component.Description,
                        Manufacturer = new GetManufactureDto()
                        {
                            Id = pc.Component.Manufacturer.Id,
                            Abbreviation = pc.Component.Manufacturer.Abbreviation,
                            FullName = pc.Component.Manufacturer.FullName,
                            FoundationDate = pc.Component.Manufacturer.FoundationDate
                        },
                        Type = new GetTypeDto
                        {
                            Id = pc.Component.Types.Id,
                            Abbreviation = pc.Component.Types.Abbreviation,
                            Name = pc.Component.Types.Name
                        }
                    }
                }).FirstOrDefault()
            });
        if (pc == null)
        {
            throw new NotFoundException();
        }
        return pc;
    }
}