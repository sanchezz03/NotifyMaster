using AutoMapper;
using Flurl.Http;
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

    public MessageReminderController(IFlurlClient flurlClient, IMapper mapper, ILogger<MessageReminderController> logger)
           : base(flurlClient, mapper)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = await _flurlClient.Request(_baseUrl).GetJsonAsync<List<MessageReminderDto>>();
        var vms = _mapper.Map<List<MessageReminderViewModel>>(dtos);

        return View(vms);
    }

    public async Task<IActionResult> Edit(long id)
    {
        var dto = await _flurlClient.Request(_baseUrl, id).GetJsonAsync<MessageReminderDto>();
        var vm = _mapper.Map<MessageReminderViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MessageReminderViewModel vm)
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

        var response = await _flurlClient.Request($"{_baseUrl}/edit").PutJsonAsync(dto);

        if (response.StatusCode == 200)
        {
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
}
