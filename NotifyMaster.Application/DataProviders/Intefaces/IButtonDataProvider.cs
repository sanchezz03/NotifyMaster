using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IButtonDataProvider
{
    Task<ButtonDto> GetAsync(long id);
    Task<List<ButtonDto>> GetListAsync();
    Task Update(ButtonDto buttonDto);
}
