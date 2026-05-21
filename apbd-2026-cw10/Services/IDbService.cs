namespace apbd_2026_cw10.Services;

public interface IDbService
{
    Task<IEnumerable<GetPcsDto>> GetAllResponse();
    Task<PcWithComponentsDto> GetByIdResponse(int id);
    Task<GetPcsDto> CreatePcAsync(CreateOrUpdatePcDto dto);
    Task UpdatePcAsync(int id, CreateOrUpdatePcDto dto);
    Task DeletePcAsync(int id);
}