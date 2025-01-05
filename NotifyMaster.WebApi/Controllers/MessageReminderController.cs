using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotifyMaster.Application.DataProviders;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageReminderController : ControllerBase
{
    private readonly IMessageReminderDataProvider _messageReminderDataProvider;

    public MessageReminderController(IMessageReminderDataProvider messageReminderDataProvider)
    {
        _messageReminderDataProvider = messageReminderDataProvider;
    }

    [HttpGet]
    public async Task<ActionResult<List<MessageReminderDto>>> GetList()
    {
        var dtos = await _messageReminderDataProvider.GetListReminderMessageDtoAsync();

        if (dtos.IsNullOrEmpty())
        {
            return Ok(new List<MessageReminderDto>());
        }

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MessageReminderDto>> Get(long id)
    {
        var dto = await _messageReminderDataProvider.GetAsync(id);

        if (dto == null)
        {
            return Ok(new MessageReminderDto());
        }

        return Ok(dto);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromBody] MessageReminderDto dto)
    {
        await _messageReminderDataProvider.Update(dto);

        return Ok();
    }
}
