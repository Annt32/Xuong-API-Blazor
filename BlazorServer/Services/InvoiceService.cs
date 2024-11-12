using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
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
        public async Task<List<User>> GetAllUser()
        {
            string requestURL = "https://localhost:7143/api/User/get-all";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<User>>(response);
        }
        public async Task<List<Invoice>> GetAllInvoiceAsync()
        {
            string requestURL = "https://localhost:7143/api/Invoice/InvoiceGetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Invoice>>(response);
        }

        public async Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync()
        {
            string requestURL = "https://localhost:7143/api/InvoiceDetail/InvoiceDetailGetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<InvoiceDetail>>(response);
        }

        public async Task<List<FieldShift>> GetAllFieldShiftAsync()
        {
            string requestURL = "https://localhost:7143/api/FieldShift/fieldshift-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<FieldShift>>(response);
        }

        public async Task<List<Field>> GetAllShiftAsync()
        {
            string requestURL = "https://localhost:7143/api/Field/fields-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Field>>(response);
        }

        public async Task<List<Shift>> GetFieldByTypeAsync()
        {
            string requestURL = "https://localhost:7143/api/Shift/shifts-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Shift>>(response);
        }

        public async Task<Invoice> GetinvoiceByIdAsync(Guid id)
		{
			string requestURL = $"https://localhost:7143/api/Invoice/invoice-get-id/{id}";
			var response = await _httpClient.GetStringAsync(requestURL);
			return JsonConvert.DeserializeObject<Invoice>(response);
		}

		public async Task<bool> UpdateUserInvoice(Guid id, Invoice invoiceupdate)
        {
            string requestURL = $"https://localhost:7143/api/Invoice/invoice-put/{id}";
            var jsonContent = JsonConvert.SerializeObject(invoiceupdate);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }
    }
}
