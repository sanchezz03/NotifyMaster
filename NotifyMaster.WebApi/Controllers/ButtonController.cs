using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ButtonController : ControllerBase
{
    private readonly IButtonDataProvider _buttonDataProvider;

    public ButtonController(IButtonDataProvider buttonDataProvider)
    {
        _buttonDataProvider = buttonDataProvider;
    }

    [HttpGet]
    public async Task<ActionResult<List<ButtonDto>>> GetList()
    {
        var buttonDtos = await _buttonDataProvider.GetListAsync();

        if (buttonDtos.IsNullOrEmpty())
        {
            return Ok(new List<ButtonDto>());
        }

        return Ok(buttonDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ButtonDto>> Get(long id)
    {
        var buttonDto = await _buttonDataProvider.GetAsync(id);

        if (buttonDto == null)
        {
            return Ok(new ButtonDto());
        }

        return Ok(buttonDto);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromBody] ButtonDto buttonDto)
    {
        await _buttonDataProvider.Update(buttonDto);

        return Ok();
    }
}
