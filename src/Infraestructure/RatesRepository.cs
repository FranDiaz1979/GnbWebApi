namespace Infraestructure
{
    using StackExchange.Redis;
    using Domain;
    using System.Runtime.CompilerServices;
    using System;

    public class RatesRepository
    {
        private readonly IDatabase _redisDB;

        public RatesRepository()
        {
            _redisDB = RedisDB.Connection.GetDatabase();
        }

        public async Task Refresh(IEnumerable<RateDto> rates)
        {
            if (rates.Any())
            {
                foreach (var rate in rates)
                {
                    SetAsync(
                        rate.From + rate.To, rate.Rate.ToString());
                }
            }           
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
