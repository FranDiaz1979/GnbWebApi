using Domain;
using Infraestructure;
using Newtonsoft.Json.Linq;
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
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<RateDto>>();
            //result = JToken.Parse(result).ToString();
            //
            var ratesRepository = new RatesRepository();
            await ratesRepository.SetAsync("1", "agua");

            var resultado= await ratesRepository.GetAsync("1");



            return result;
        }
    }
}