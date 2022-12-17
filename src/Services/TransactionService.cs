using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class TransactionService
    {
        public static async Task<IEnumerable<TransactionDto>> GetListAsysnc()
        {
            using var client = new HttpClient();
            //var result = await client.GetFromJsonAsync<IEnumerable<TransactionDto>>("http://localhost:5074/AuxiliarApi/transactions.json");
            var response = await client.GetAsync("http://localhost:5074/AuxiliarApi/transactions.json");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionDto>>();
            //result= JToken.Parse(result);
            return result;
        }

        public static async Task<IEnumerable<TransactionDto>> GetBySkuAsync(string sku)
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

            return transactions;
        }
    }
}
