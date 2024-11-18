#nullable enable

using System.Net.Http.Json;
using System.Text.Json;
using Library.WebUI.Abstracts;
using Library.WebUI.Model;
using MudBlazor;

namespace Library.WebUI.Services
{
    public class HttpRepository<TDto>(IHttpClientFactory httpClientFactory,
                                      JsonSerializerOptions serializerOptions,
                                      ISnackbar Snackbar) : IHttpRepository<TDto> where TDto : class
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("backend");

        public async Task<HttpResponseMessage> CreateAsync (string uri, TDto entity)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(uri, entity);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync (string uri)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(uri);
            return response;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync (string uri)
        {
            IEnumerable<TDto>? result = await httpClient.GetFromJsonAsync<IEnumerable<TDto>>(uri, serializerOptions);
            return result ?? [];
        }

        public async Task<TDto?> GetAsync (string uri)
        {
            TDto? result = await httpClient.GetFromJsonAsync<TDto> (uri, serializerOptions);
            return result;
        }

        public async Task<HttpResponseMessage> SetStateAsync (string uri, string state)
        {
            HttpResponseMessage response = await httpClient.PutAsync($"{uri}", null);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync (string uri, TDto entity, string id)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{uri}/{id}", entity);
            return response;
        }
    }
}
