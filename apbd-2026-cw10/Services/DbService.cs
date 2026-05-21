using System.Globalization;
using apbd_2026_cw10.Data;
using apbd_2026_cw10.Entities;
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
        return await _dbContext.PCs
            .Select(p => new GetPcsDto()
        {
            Id = p.Id,
            Name = p.Name,
            Weight = p.Weight,
            Warranty = p.Warranty,
            CreatedAt = p.CreatedAt,
            Stock = p.Stock
        })
            .ToListAsync();
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
                }).ToList()
            })
            .FirstOrDefaultAsync();
        if (pc == null)
        {
            throw new NotFoundException();
        }
        return pc;
    }
    
    

    public async Task<PCs> AddAsync(PCs pc)
    {
        _dbContext.PCs.Add(pc);
        await _dbContext.SaveChangesAsync();
        return pc;
    }

    public async Task UpdateAsync(PCs pc)
    {
        _dbContext.PCs.Update(pc);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(PCs pc)
    {
        _dbContext.PCs.Remove(pc);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GetPcsDto> CreatePcAsync(CreateOrUpdatePcDto dto)
    {
        var pc = new PCs
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };
        
        await _dbContext.PCs.AddAsync(pc);

        await _dbContext.SaveChangesAsync();

        return new GetPcsDto()
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task UpdatePcAsync(int id, CreateOrUpdatePcDto dto)
    {
        var pc = await _dbContext.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
        {
            throw new NotFoundException();
        }
        
        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePcAsync(int id)
    {
        var pc = await _dbContext.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
        {
            throw new NotFoundException();
        }
        
        _dbContext.PCs.Remove(pc);
        await _dbContext.SaveChangesAsync();
    }
}