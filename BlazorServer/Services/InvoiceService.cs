using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BlazorServer.Services
{
    public class InvoiceService : IInvoiceServices
    {
        private readonly HttpClient _httpClient;
        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Invoice>> GetAllInvoiceAsync()
        {
            string requestURL = "https://localhost:7143/api/Invoice/InvoiceGetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Invoice>>(response);
        }

        public async Task<bool> CancelInvoiceAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/invoice/cancel-invoice/{id}";
            var request = new HttpRequestMessage(HttpMethod.Put, requestURL);
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Invoice/invoice-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }
    }
}
