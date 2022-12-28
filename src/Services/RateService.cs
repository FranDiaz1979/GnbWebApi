using Domain;
using Infraestructure;
using Newtonsoft.Json.Linq;
using Services;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Services
{
    public class RateService : IRateService
    {
        public async Task<IEnumerable<RateDto>> GetListAsync()
        {
            using var client = new HttpClient();
            //var result = await client.GetFromJsonAsync<IEnumerable<RateDto>>("http://localhost:5074/AuxiliarApi/rates.json");
            var response = await client.GetAsync("http://localhost:5074/AuxiliarApi/rates.json");
            response.EnsureSuccessStatusCode();

            IEnumerable<RateDto> listRates = await response.Content.ReadFromJsonAsync<IEnumerable<RateDto>>();
            var ratesRepository = new RatesRepository();
            ratesRepository.Refresh(listRates);

            //result = JToken.Parse(result).ToString();

            return listRates;
        }

        public async Task<decimal> AmountToEur(decimal amount, string currency)
        {
            if (currency == "EUR")
            {
                return amount;
            }

            var listRates = await GetListAsync();
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
                var ratesRepository = new RatesRepository();
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