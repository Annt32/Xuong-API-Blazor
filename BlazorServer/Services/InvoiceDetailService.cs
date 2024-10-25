using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BlazorServer.Services
{
    public class InvoiceDetailService : IInvoiceDetailServices
    {
        private readonly HttpClient _httpClient;
        public InvoiceDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync()
        {
            string requestURL = "https://localhost:7143/api/InvoiceDetail/InvoiceDetailGetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<InvoiceDetail>>(response);
        }

        public async Task<bool> CancelInvoiceDetailAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/invoicedetail/cancel-invoicedetail/{id}";
            var request = new HttpRequestMessage(HttpMethod.Put, requestURL);
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInvoiceDetailAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Invoicedetail/invoicedetail-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }
    }
}
