using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net.Http.Json;

namespace Infraestructure
{
    public class ApiClient : HttpClient, IApiClient
    {
        public ApiClient()
        {
            this.BaseAddress = new Uri("http://localhost:5074/AuxiliarApi/");
        }

        new public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (this.BaseAddress is null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return await base.GetAsync(new Uri(this.BaseAddress, requestUri));
        }
    }
}
