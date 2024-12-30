using AutoMapper;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotifyMaster.Client.Controllers;

public class BaseController : Controller
{
    protected readonly IFlurlClient _flurlClient;
    protected IMapper _mapper;

    public BaseController(IFlurlClient flurlClient, IMapper mapper)
    {
        _flurlClient = flurlClient;
        _mapper = mapper;   
    }
}
