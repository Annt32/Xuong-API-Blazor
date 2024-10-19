using AppData.DTO.FieldType_DTO;
using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class FieldTypeService : IFieldTypeServices
{
    private readonly HttpClient _httpClient;

    public FieldTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FieldTypeDTO>> GetAllFieldType()
    {
        // Lấy danh sách FieldType từ API
        string requestURL = "https://localhost:7143/api/FieldType/get-all";
        var response = await _httpClient.GetStringAsync(requestURL);
        var fieldTypes = JsonConvert.DeserializeObject<List<FieldTypeDTO>>(response);

        return fieldTypes;
    }

    public async Task<FieldTypeDTO> GetFieldByIdAsync(Guid id)
    {
        // Lấy FieldType theo ID từ API
        string requestURL = $"https://localhost:7143/api/FieldType/get-id/{id}";
        var response = await _httpClient.GetStringAsync(requestURL);
        var fieldType = JsonConvert.DeserializeObject<FieldTypeDTO>(response);

        return fieldType;
    }

    public async Task<bool> CreateFieldAsync(FieldType field)
    {
        // Tạo mới FieldType
        string requestURL = "https://localhost:7143/api/FieldType/create";
        var jsonContent = JsonConvert.SerializeObject(field);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateFieldAsync(Guid id, FieldType updatedField)
    {
        // Cập nhật FieldType theo ID
        string requestURL = $"https://localhost:7143/api/FieldType/update/{updatedField.Id}";
        var jsonContent = JsonConvert.SerializeObject(updatedField);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFieldAsync(Guid id)
    {
        // Xóa FieldType theo ID
        string requestURL = $"https://localhost:7143/api/FieldType/delete/{id}";
        var response = await _httpClient.DeleteAsync(requestURL);
        return response.IsSuccessStatusCode;
    }
}
