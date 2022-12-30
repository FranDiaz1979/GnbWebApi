namespace Infraestructure
{
    using Entities;
    using StackExchange.Redis;
    using System.Collections.Generic;

    public class TransactionRepository
    {
        private readonly IDatabase _redisDB;

        public TransactionRepository()
        {
            _redisDB = RedisDB.Connection.GetDatabase();
        }

        public async Task Refresh(IEnumerable<TransactionTotal> transactions)
        {
            if (transactions.Any())
            {
                foreach (var transaction in transactions)
                {
                    await SetAsync(transaction.Sku, transaction.Amount.ToString());
                }
            }
        }

        public async Task SetAsync(string key, string value)
        {
            await _redisDB.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            return (await _redisDB.StringGetAsync(key)).ToString();
        }

        public async Task<IEnumerable<RateEntity>> GetAllAsync()
        {
            using ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true");
            IServer server = redis.GetServer("localhost", 6379);
            List<RateEntity> rateList = new List<RateEntity>();

            foreach (var key in server.Keys())
            {
                if (key.ToString().Length == 6)
                {
                    string value = (await _redisDB.StringGetAsync(key)).ToString();
                    var rate = new RateEntity()
                    {
                        From = key.ToString().Substring(0, 3),
                        To = key.ToString().Substring(3, 3),
                        Rate = decimal.Parse(value ?? string.Empty),
                    };
                    rateList.Add(rate);
                }
            }

            return rateList;
        }
    }
}