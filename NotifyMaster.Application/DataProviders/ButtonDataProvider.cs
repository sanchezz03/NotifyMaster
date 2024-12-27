using AutoMapper;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Core.Entities;
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

    public async Task<List<ButtonDto>> GetListAsync()
    {
        var buttons = await _buttonRepository.GetAllAsync();

        return _mapper.Map<List<ButtonDto>>(buttons);
    }

    public async Task Update(ButtonDto buttonDto)
    {
        var entity = await _buttonRepository.GetByIdAsync(buttonDto.Id);

        entity.Name = buttonDto.Name;

        await _buttonRepository.SaveChangesAsync();
    }
}
