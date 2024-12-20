using AutoMapper;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Infrastructure.Repositories;

namespace NotifyMaster.Application.DataProviders;

public class ButtonDataProvider : IButtonDataProvider
{
    private readonly ButtonRepository _buttonRepository;
    private readonly IMapper _mapper;

    public ButtonDataProvider(ButtonRepository buttonRepository, IMapper mapper)
    {
        _buttonRepository = buttonRepository;
        _mapper = mapper;
    }
    public async Task<ButtonDto> GetAsync(long id)
    {
        var button = await _buttonRepository.GetByIdAsync(id);

        return _mapper.Map<ButtonDto>(button);  
    }
}
