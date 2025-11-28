using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using NexaSoft.Club.Infrastructure.ConfigSettings;

namespace NexaSoft.Club.Infrastructure.Services
{
    using NexaSoft.Club.Application.Storages;
    using NexaSoft.Club.Domain.ServicesModel.Reniec;

    public class ReniecService : IReniecService
    {
        private readonly HttpClient _httpClient;
        private readonly string _bearerToken;
        private readonly string _baseUrl;

        public ReniecService(HttpClient httpClient, ReniecOptions options)
        {
            _httpClient = httpClient;
            _bearerToken = options.ApiKey;
            _baseUrl = options.Url;
        }

        public async Task<ReniecDniResponse?> GetDniInfoAsync(string dni)
        {
            var url = _baseUrl.EndsWith("/") ? _baseUrl + "dni" : _baseUrl + "/dni";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var body = new { dni = dni };
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializar el modelo de respuesta
            var apiResponse = JsonConvert.DeserializeObject<ApiReniecDniResponse>(responseContent);
            return apiResponse?.Data;
        }

        public async Task<ReniecRucResponse?> GetRucInfoAsync(string ruc)
        {
            var url = _baseUrl.EndsWith("/") ? _baseUrl + "ruc" : _baseUrl + "/ruc";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var body = new { ruc = ruc };
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializar el modelo de respuesta
            var apiResponse = JsonConvert.DeserializeObject<ApiReniecRucResponse>(responseContent);
            return apiResponse?.Data;
        }

        private sealed class ApiReniecDniResponse
        {
            public bool Success { get; set; }
            public ReniecDniResponse? Data { get; set; }
        }

        private sealed class ApiReniecRucResponse
        {
            public bool Success { get; set; }
            public ReniecRucResponse? Data { get; set; }
        }
    }
}
