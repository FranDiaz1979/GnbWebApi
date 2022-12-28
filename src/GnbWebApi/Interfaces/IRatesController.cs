using Microsoft.AspNetCore.Mvc;

namespace WebApi.Interfaces
{
    public interface IRatesController
    {
        Task<IActionResult> Get();
    }
}