using Microsoft.AspNetCore.Mvc;

namespace WebApi.Interfaces
{
    public interface IRateController
    {
        Task<IActionResult> Get();
    }
}