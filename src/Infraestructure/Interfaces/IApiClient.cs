namespace Infraestructure
{
    public interface IApiClient
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}