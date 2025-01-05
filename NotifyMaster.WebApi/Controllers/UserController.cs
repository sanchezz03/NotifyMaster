using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Application.Services.Interfaces;

namespace NotifyMaster.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetList()
    {
        var userDtos = await _userService.GetUserDtosAsync();

        if (userDtos.IsNullOrEmpty())
        {
            return Ok(new List<UserDto>());
        }

        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> Get(long userId)
    {
        var userDto = await _userService.GetUserDtoAsync(userId);

        if (userDto == null)
        {
            return Ok(new UserDto());
        }

        return Ok(userDto);
    }
}
