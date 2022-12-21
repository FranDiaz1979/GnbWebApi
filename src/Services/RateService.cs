using Domain;
using Infraestructure;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Services
{
    public class RateService
    {
        public static async Task<IEnumerable<RateDto>> GetListAsysnc() 
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

        internal async Task<decimal> AmountToEur(decimal amount, string currency)
        {
            var listRates = await GetListAsysnc();
            decimal rateEur;
            if (listRates.Any())
            {
                rateEur = listRates.First(x => x.From == currency && x.To == "EUR").Rate;
            }
            else { 
                var ratesRepository = new RatesRepository();
                string valor = await ratesRepository.GetAsync(currency + "EUR");
                rateEur = Convert.ToDecimal(valor);
            }

            return amount * rateEur;
        }
    }
}