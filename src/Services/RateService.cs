using Entities;
using Infraestructure;
using Pipelines.Sockets.Unofficial.Arenas;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services
{
    public class RateService : IRateService
    {
        private readonly IApiClient _apiClient;
        private readonly IRepository _repository;

        public RateService(IApiClient apiClient, IRepository repository)
        {
            _apiClient = apiClient;
            _repository = repository;
        }

        public async Task<IEnumerable<RateEntity>> GetAllAsync()
        {
            var response = await _apiClient.GetAsync("rates.json");

            IEnumerable<RateEntity>? rates;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                rates = await content.ReadFromJsonAsync<IEnumerable<RateEntity>>();
                if (rates is not null)
                {
                    string cadena = await content.ReadAsStringAsync();
                    _ = _repository.SetAsync("rates", cadena ?? string.Empty);
                }
            }
            else
            {
                string cadena = await _repository.GetAsync("rates");
                rates = JsonSerializer.Deserialize<IEnumerable<RateEntity>>(cadena);
            }

            if (rates is null)
            {
                return Enumerable.Empty<RateEntity>();
            }

            return rates;
        }

        public async Task<decimal> AmountToEur(decimal amount, string currency)
        {
            if (currency == "EUR")
            {
                return amount;
            }

            var listRates = await GetAllAsync();
            decimal rateEur;
            if (listRates.Any())
            {
                var firstRate = listRates.FirstOrDefault(x => x.From == currency && x.To == "EUR");
                if (firstRate is null)
                {
                    return 0;
                }

                rateEur = firstRate.Rate;
            }
            else
            {
                var repository = new RedisRepository();
                string cadena = await repository.GetAsync("rates");
                listRates = JsonSerializer.Deserialize<IEnumerable<RateEntity>>(cadena);
                var rate = listRates?.FirstOrDefault(x=>x.From==currency && x.To=="EUR");
                
                if (rate is null)
                {
                    return 0;
                }
                rateEur = rate.Rate;
            }

            return amount * rateEur;
        }
    }
}