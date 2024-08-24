using InterfaceAdapters_Adapters;
using InterfaceAdapters_Adapters.Dtos;
using System.Text.Json;

namespace FrameworksAndDrivers_ExternalService
{
    public class PostService : IExternalService<PostServiceDTO>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IEnumerable<PostServiceDTO>> GetContentAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PostServiceDTO>>(responseData, _options);
        }
    }
}
