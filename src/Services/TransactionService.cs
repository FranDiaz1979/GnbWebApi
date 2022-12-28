using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Infraestructure;

namespace Services
{

    public class TransactionService : ITransactionService
    {
        private readonly IApiClient _apiClient;

        public TransactionService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<TransactionDto>> GetListAsysnc()
        {
            var response = await _apiClient.GetAsync("transactions.json");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionDto>>();
            //result= JToken.Parse(result);
            return result;
        }

        public async Task<TransactionTotalDto> GetBySkuAsync(string sku)
        {
            using var client = new HttpClient();
            //var response = await client.GetFromJsonAsync<IEnumerable<TransactionDto>>("http://localhost:5074/AuxiliarApi/transactions.json");
            var response = await client.GetAsync("http://localhost:5074/AuxiliarApi/transactions.json");
            response.EnsureSuccessStatusCode();

            var transactions = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionDto>>();
            //string result = await response.Content.ReadAsStringAsync();
            //result = JToken.Parse(result).ToString();
            transactions = transactions.Where(x => x.Sku == sku);
            //if (!transactions.Any()) 
            //{
            //    return "No se han encontrado transacciones con el SKU '" + sku + "'";
            //}

            //var result = JToken.FromObject(transactions).ToString();
            decimal total = 0;
            var rateService = new RateService(_apiClient);
            foreach (var transaction in transactions)
            {

                if (transaction.Currency == "EUR")
                {
                    total+=transaction.Amount;
                }
                else
                {
                    total += await rateService.AmountToEur(transaction.Amount, transaction.Currency);
                }
            }

            var transactionTotalDto = new TransactionTotalDto()
            {
                Sku= sku,
                Amount= total,
            };

            return transactionTotalDto;
        }
    }
}
