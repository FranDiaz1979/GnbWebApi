namespace Infraestructure
{
    using StackExchange.Redis;

    public class RedisRepository : IRepository
    {
        private readonly IDatabase _redisDB;

        public RedisRepository()
        {
            _redisDB = RedisDB.Connection.GetDatabase();
        }

        public async Task SetAsync(string key, string value)
        {
            await _redisDB.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            return (await _redisDB.StringGetAsync(key)).ToString();
        }
    }
}