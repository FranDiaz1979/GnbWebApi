namespace Infraestructure
{ 
    public interface IRepository
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
    }
}