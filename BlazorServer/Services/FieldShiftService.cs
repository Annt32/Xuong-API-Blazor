using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BlazorServer.Services
{
	public class FieldShiftService : IFieldShiftService
	{
		private readonly HttpClient _httpClient;
		public FieldShiftService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> CreateFieldshiftAsync(FieldShift fieldshift)
		{
			string requestURL = "https://localhost:7143/api/Fieldshift/fieldshift-post";
			var jsonContent = JsonConvert.SerializeObject(fieldshift);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(requestURL, content);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteFieldshiftAsync(Guid id)
		{
			string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-delete/{id}";
			var response = await _httpClient.DeleteAsync(requestURL);
			return response.IsSuccessStatusCode;
		}

		public async Task<List<FieldShift>> GetAllFieldshiftAsync()
		{
			string requestURL = "https://localhost:7143/api/FieldShift/fieldshift-get";
			var response = await _httpClient.GetStringAsync(requestURL);
			return JsonConvert.DeserializeObject<List<FieldShift>>(response);
		}

		public async Task<FieldShift> GetFieldshiftByIdAsync(Guid id)
		{
			string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-get-id/{id}";
			var response = await _httpClient.GetStringAsync(requestURL);
			return JsonConvert.DeserializeObject<FieldShift>(response);
		}

		public async Task<bool> UpdateFieldshiftAsync(Guid id, FieldShift updatedFieldShift)
		{
			string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-put/{id}";
			var jsonContent = JsonConvert.SerializeObject(updatedFieldShift);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(requestURL, content);
			return response.IsSuccessStatusCode;
		}
	}
}
