#nullable enable

namespace Library.WebUI.Abstracts;

public interface IHttpRepository<TDto> where TDto: class
{
    Task<TDto?> GetAsync (string uri);

    Task<IEnumerable<TDto>> GetAllAsync (string uri);

    Task<HttpResponseMessage> CreateAsync (string uri, TDto entity);

    Task<HttpResponseMessage> UpdateAsync (string uri, TDto entity, string id);

    Task<HttpResponseMessage> SetStateAsync(string uri, string state);

    Task<HttpResponseMessage> DeleteAsync (string uri);
}
