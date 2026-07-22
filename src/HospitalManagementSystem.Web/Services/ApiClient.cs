namespace HospitalManagementSystem.Web.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadAsStringAsync();
    }

}
