using AutoMapper;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using NotifyMaster.Client.Models.ButtonVM;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Client.Controllers;

public class ButtonController : BaseController
{
    private readonly ILogger<ButtonController> _logger;

    private readonly string _baseUrl = "api/button";

    public ButtonController(IFlurlClient flurlClient, IMapper mapper, ILogger<ButtonController> logger) 
        : base(flurlClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var buttonDtos = await _flurlClient.Request(_baseUrl).GetJsonAsync<List<ButtonDto>>();
        var buttonVMs = _mapper.Map<List<ButtonViewModel>>(buttonDtos);

        return View(buttonVMs);
    }

    public async Task<IActionResult> Edit(long id)
    {
        var buttonDto = await _flurlClient.Request(_baseUrl, id).GetJsonAsync<ButtonDto>();
        var buttonVM = _mapper.Map<ButtonViewModel>(buttonDto);

        return View(buttonVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ButtonViewModel buttonVM)
    {
        var buttonDto = _mapper.Map<ButtonDto>(buttonVM);

        var response = await _flurlClient.Request($"{_baseUrl}/edit")
                                         .PutJsonAsync(buttonDto);

        if (response.StatusCode == 200)
        {
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
}
