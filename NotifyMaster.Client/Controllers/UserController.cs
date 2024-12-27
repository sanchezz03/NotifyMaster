using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotifyMaster.Client.Models.UserVM;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Client.Controllers;

public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;

    private readonly string _baseUrl = "api/user";

    public UserController(HttpClient httpClient, IMapper mapper, ILogger<UserController> logger)
        : base(httpClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);

            var userDtos = await ExecuteActionResultAsync<List<UserDto>>(response);
            var userVMs = _mapper.Map<List<UserViewModel>>(userDtos);

            return View(userVMs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> Details(long userId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{userId}");
        
        var userDto = await ExecuteActionResultAsync<UserDto>(response);

        if (userDto == null)
        {
            return NotFound("User not found");
        }

        var userDetailVm = _mapper.Map<UserDetailViewModel>(userDto);

        return View(userDetailVm);
    }
}
