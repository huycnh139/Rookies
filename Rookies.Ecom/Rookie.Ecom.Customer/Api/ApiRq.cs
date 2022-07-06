namespace Rookie.Ecom.Customer.Api
{
    public class ApiRq
    { 
        public HttpClient ApiRequest()
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7055/api/");
                return client;
            }
    }
}
