using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public interface IApiClient
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
