using Entities;
using Infraestructure;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApiClient _apiClient;
        private readonly IRepository _repository;

        public TransactionService(IApiClient apiClient, IRepository repository)
        {
            _apiClient = apiClient;
            _repository = repository;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsysnc()
        {
            IEnumerable<Transaction>? transactionList;

            var response = await _apiClient.GetAsync("transactions.json");

            if (response.IsSuccessStatusCode)
            {
                transactionList = await response.Content.ReadFromJsonAsync<IEnumerable<Transaction>>();
                if (transactionList is not null)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    _ = _repository.SetAsync("transaction", cadena ?? string.Empty);
                }
            }
            else
            {
                string cadena = await _repository.GetAsync("transaction");
                transactionList = JsonSerializer.Deserialize<IEnumerable<Transaction>>(cadena);
            }

            if (transactionList is null)
            {
                return Enumerable.Empty<Transaction>();
            }

            return transactionList;
        }


        public async Task<TransactionTotal> GetBySkuAsync(string sku)
        {
            var transactionRepository = new RedisRepository();
            IEnumerable<Transaction>? transactions;
            decimal total = 0;

            var response = await _apiClient.GetAsync("transactions.json");

            if (response.IsSuccessStatusCode)
            {
                transactions = await response.Content.ReadFromJsonAsync<IEnumerable<Transaction>>();
                if (transactions is not null)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    _ = transactionRepository.SetAsync("transaction", cadena ?? string.Empty);
                }
            }
            else
            {
                string cadena = await transactionRepository.GetAsync("transaction");
                transactions = JsonSerializer.Deserialize<IEnumerable<Transaction>>(cadena);
            }

            transactions = transactions?.Where(x => x.Sku == sku);

            if (transactions is null || !transactions.Any())
            {
                return new TransactionTotal();
            }

            var rateService = new RateService(_apiClient, _repository);
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

            var transactionTotal = new TransactionTotal()
            {
                Sku = sku,
                Amount = total,
            };

            return transactionTotal;
        }            
    }
}


