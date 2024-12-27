using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotifyMaster.Client.Models.ButtonVM;
using NotifyMaster.Common.Dtos;
using System.Text;

namespace NotifyMaster.Client.Controllers;

public class ButtonController : BaseController
{
    private readonly ILogger<ButtonController> _logger;

    private readonly string _baseUrl = "api/button";

    public ButtonController(HttpClient httpClient, IMapper mapper, ILogger<ButtonController> logger) 
        : base(httpClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);

            var buttonDtos = await ExecuteActionResultAsync<List<ButtonDto>>(response);
            var buttonVMs = _mapper.Map<List<ButtonViewModel>>(buttonDtos);

            return View(buttonVMs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            
            var buttonDto = await ExecuteActionResultAsync<ButtonDto>(response);
            var buttonVM = _mapper.Map<ButtonViewModel>(buttonDto);

            return View(buttonVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ButtonViewModel buttonVM)
    {
        try
        {
            var buttonDto = _mapper.Map<ButtonDto>(buttonVM);

            var content = new StringContent(
                JsonConvert.SerializeObject(buttonDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/edit", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
