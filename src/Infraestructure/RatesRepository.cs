namespace Infraestructure
{
    using StackExchange.Redis;
    

    public class RatesRepository
    {
        private readonly IDatabase _redisDB;

        public RatesRepository() 
        {
            _redisDB = RedisDB.Connection.GetDatabase();
        }

        public void Refresh() 
        {
        }

        public async Task SetAsync(string key, string value)
        {
            await _redisDB.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _redisDB.StringGetAsync(key);
        }
    }
}
