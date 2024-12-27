using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NotifyMaster.Client.Controllers;

public class BaseController : Controller
{
    protected readonly HttpClient _httpClient;
    protected IMapper _mapper;

    public BaseController(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;   
    }

    protected async Task<T?> ExecuteActionResultAsync<T>(HttpResponseMessage? response)
    {
        return await ExecuteAsync<T>(response);
    }

    #region Private methods

    protected async Task<T?> ExecuteAsync<T>(HttpResponseMessage? response)
    {
        if (response == null)
        {
            return default;
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return default;
        }

        try
        {
            var content = await response.Content.ReadAsStringAsync();
            var deserializedData = JsonConvert.DeserializeObject<T>(content);

            if (deserializedData == null)
            {
                return default;
            }

            return deserializedData;
        }
        catch (JsonException ex)
        {
            return default;
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    #endregion
}
