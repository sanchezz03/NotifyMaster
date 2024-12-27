namespace NotifyMaster.Client.Extensions;

public static class HttpClientExtension
{
    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"http://localhost:8000/");

        return services
            .AddSingleton<HttpClient>(httpClient);
    }
}
