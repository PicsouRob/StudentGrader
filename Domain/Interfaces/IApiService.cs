
namespace Domain.Interfaces
{
    public interface IApiService
    {
        Task<T> GetTAsync<T>(string endpoint);

        Task PostAsync<T>(string endpoint, T data);
    }
}

