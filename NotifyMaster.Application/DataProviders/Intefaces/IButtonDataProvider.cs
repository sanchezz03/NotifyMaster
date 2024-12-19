using NotifyMaster.Application.Dtos;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IButtonDataProvider
{
    Task<ButtonDto> GetAsync(long id);
}
