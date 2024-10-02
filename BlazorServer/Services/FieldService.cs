using Appdata;
using Newtonsoft.Json; // Để sử dụng JsonConvert
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class FieldService : IFieldService
{
    private readonly HttpClient _httpClient;

    public FieldService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Field>> GetAllFieldsAsync()
    {
        string requestURL = "https://localhost:7143/api/Field/fields-get";
        var response = await _httpClient.GetStringAsync(requestURL);
        return JsonConvert.DeserializeObject<List<Field>>(response);
    }

    public async Task<Field> GetFieldByIdAsync(Guid id)
    {
        string requestURL = $"https://localhost:7143/api/Field/fields-get-id/{id}";
        var response = await _httpClient.GetStringAsync(requestURL);
        return JsonConvert.DeserializeObject<Field>(response);
    }

    public async Task<bool> CreateFieldAsync(Field field)
    {
        string requestURL = "https://localhost:7143/api/Field/fields-post";
        var jsonContent = JsonConvert.SerializeObject(field);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateFieldAsync(Guid id, Field updatedField)
    {
        string requestURL = $"https://localhost:7143/api/Field/fields-put/{id}";
        var jsonContent = JsonConvert.SerializeObject(updatedField);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFieldAsync(Guid id)
    {
        string requestURL = $"https://localhost:7143/api/Field/fields-delete/{id}";
        var response = await _httpClient.DeleteAsync(requestURL);
        return response.IsSuccessStatusCode;
    }
}
