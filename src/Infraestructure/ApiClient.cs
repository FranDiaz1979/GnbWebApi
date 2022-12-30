namespace Infraestructure
{
    public class ApiClient : HttpClient, IApiClient
    {
        public ApiClient()
        {
            this.BaseAddress = new Uri("http://localhost:5074/AuxiliarApi/");
        }

        public new async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (this.BaseAddress is null)
            {
                throw new HttpRequestException("Base Adress is null");
            }

            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            try
            {
                var response = await base.GetAsync(new Uri(this.BaseAddress, requestUri));
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}