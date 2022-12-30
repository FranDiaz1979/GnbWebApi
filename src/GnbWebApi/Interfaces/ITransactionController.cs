using Microsoft.AspNetCore.Mvc;

namespace WebApi.Interfaces
{
    public interface ITransactionController
    {
        Task<IActionResult> Get();

        Task<IActionResult> GetBySku(string sku);
    }
}