using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotifyMaster.Client.Models.MessageReminderVM;
using NotifyMaster.Common.Dtos;
using System.Text;

namespace NotifyMaster.Client.Controllers;

public class MessageReminderController : BaseController
{
    private readonly ILogger<MessageReminderController> _logger;

    private readonly string _baseUrl = "api/messagereminder";

    public MessageReminderController(HttpClient httpClient, IMapper mapper, ILogger<MessageReminderController> logger) 
        : base(httpClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);

            var dtos = await ExecuteActionResultAsync<List<MessageReminderDto>>(response);
            var vms = _mapper.Map<List<MessageReminderViewModel>>(dtos);

            return View(vms);
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

            var dto = await ExecuteActionResultAsync<MessageReminderDto>(response);
            var vm = _mapper.Map<MessageReminderViewModel>(dto);

            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MessageReminderViewModel vm)
    {
        try
        {
            if (string.IsNullOrEmpty(vm.VideoUrl))
            {
                vm.VideoUrl = "";
            }

            var dto = _mapper.Map<MessageReminderDto>(vm);

            var content = new StringContent(
                JsonConvert.SerializeObject(dto),
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
