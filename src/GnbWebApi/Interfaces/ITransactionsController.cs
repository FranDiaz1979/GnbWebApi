using Microsoft.AspNetCore.Mvc;

namespace WebApi.Interfaces
{
    public interface ITransactionsController
    {
        Task<IActionResult> Get();
        Task<IActionResult> GetBySku(string sku);
    }
}