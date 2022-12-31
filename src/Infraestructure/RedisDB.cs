namespace Infraestructure
{
    using StackExchange.Redis;
    
    public static class RedisDB
    {
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection = 
            new(() =>
                ConnectionMultiplexer.Connect("localhost")
            );

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }
    }
}