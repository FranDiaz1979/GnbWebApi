using Entities;
using Infraestructure;
using System.Net.Http.Json;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApiClient _apiClient;

        public TransactionService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsysnc()
        {
            var response = await _apiClient.GetAsync("transactions.json");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Transaction>>();
            if (result is null)
            {
                return Enumerable.Empty<Transaction>();
            }
            return result;
        }

        public async Task<TransactionTotal> GetBySkuAsync(string sku)
        {
            var response = await _apiClient.GetAsync("transactions.json");
            decimal total = 0;

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Transaction>? transactions;

                transactions = await response.Content.ReadFromJsonAsync<IEnumerable<Transaction>>();
                transactions = transactions?.Where(x => x.Sku == sku);

                if (transactions is null)
                {
                    return new TransactionTotal();
                }

                var rateService = new RateService(_apiClient);
                foreach (var transaction in transactions)
                {
                    if (transaction.Currency == "EUR")
                    {
                        total += transaction.Amount;
                    }
                    else
                    {
                        total += await rateService.AmountToEur(transaction.Amount, transaction.Currency);
                    }
                }
            }
            else
            {
                var transactionRepository = new TransactionRepository();

                var transactionAmount = await transactionRepository.GetAsync(sku);
                if (transactionAmount is not null)
                {
                    total = decimal.Parse(transactionAmount);
                }
            }

            var transactionTotalDto = new TransactionTotal()
            {
                Sku = sku,
                Amount = total,
            };

            return transactionTotalDto;
        }
    }
}