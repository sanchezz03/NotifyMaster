using AutoMapper;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using NotifyMaster.Client.Models.UserVM;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Client.Controllers;

public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;

    private readonly string _baseUrl = "api/user";

    public UserController(IFlurlClient flurlClient, IMapper mapper, ILogger<UserController> logger)
          : base(flurlClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userDtos = await _flurlClient.Request(_baseUrl).GetJsonAsync<List<UserDto>>();
        var userVMs = _mapper.Map<List<UserViewModel>>(userDtos);

        return View(userVMs);
    }

    public async Task<IActionResult> Details(long userId)
    {
        var userDto = await _flurlClient.Request(_baseUrl, userId).GetJsonAsync<UserDto>();

        if (userDto == null)
        {
            return NotFound("User not found");
        }

        var userDetailVm = _mapper.Map<UserDetailViewModel>(userDto);

        return View(userDetailVm);
    }
}
