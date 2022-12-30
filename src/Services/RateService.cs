using Entities;
using Infraestructure;
using System.Net.Http.Json;

namespace Services
{
    public class RateService : IRateService
    {
        private readonly IApiClient _apiClient;

        public RateService(IApiClient apiClient)
        {
            this._apiClient = apiClient;
        }

        public async Task<IEnumerable<RateEntity>> GetAllAsync()
        {
            var response = await _apiClient.GetAsync("rates.json");

            var rateRepository = new RateRepository();
            IEnumerable<RateEntity>? listRates;
            if (response.IsSuccessStatusCode)
            {
                listRates = await response.Content.ReadFromJsonAsync<IEnumerable<RateEntity>>();
                if (listRates is not null)
                {
                    await rateRepository.Refresh(listRates);
                }
            }
            else
            {
                listRates = await rateRepository.GetAllAsync();
            }

            if (listRates is null)
            {
                return Enumerable.Empty<RateEntity>();
            }

            return listRates;
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
                var ratesRepository = new RateRepository();
                string valor = await ratesRepository.GetAsync(currency + "EUR");
                if (string.IsNullOrEmpty(valor))
                {
                    return 0;
                }
                rateEur = Convert.ToDecimal(valor);
            }

            return amount * rateEur;
        }
    }
}