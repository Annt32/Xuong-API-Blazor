using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BlazorServer.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        HttpClient _httpClient;
        public InvoiceDetailService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync()
        {
            string requestURL = "https://localhost:7143/api/InvoiceDetail/InvoiceDetailGetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<InvoiceDetail>>(response);
        }

        public async Task<InvoiceDetail> GetinvoiceDetailByIdAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/InvoiceDetail/invoicedetail-get-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<InvoiceDetail>(response);
        }

        public async Task<bool> UpdateInvoiceDetail(Guid id, InvoiceDetail invoicedetailupdate)
        {
            string requestURL = $"https://localhost:7143/api/InvoiceDetail/invoicedetail-put/{id}";
            var jsonContent = JsonConvert.SerializeObject(invoicedetailupdate);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

		public async Task<bool> CreateInvoiceDetailAsync(InvoiceDetail invoiceDetail)
		{
			string requestURL = "https://localhost:7143/api/InvoiceDetail/invoicedetail-create";
			var jsonContent = JsonConvert.SerializeObject(invoiceDetail);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(requestURL, content);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteInvoiceDetailAsync(Guid id)
		{
			string requestURL = $"https://localhost:7143/api/InvoiceDetail/invoicedetail-delete/{id}";
			var response = await _httpClient.DeleteAsync(requestURL);
			return response.IsSuccessStatusCode;
		}

	}
}
