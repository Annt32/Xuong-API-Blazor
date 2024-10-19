using AppData.Entities;
using BlazorServer.Models;
using Newtonsoft.Json;
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

    public async Task<List<FieldViewModel>> GetAllFieldsAsync()
    {
        // Lấy danh sách Field từ API
        string fieldRequestURL = "https://localhost:7143/api/Field/fields-get";
        var fieldResponse = await _httpClient.GetStringAsync(fieldRequestURL);
        var fields = JsonConvert.DeserializeObject<List<Field>>(fieldResponse);

        // Lấy danh sách FieldType từ API
        string fieldTypeRequestURL = "https://localhost:7143/api/FieldType/get-all"; // URL giả định
        var fieldTypeResponse = await _httpClient.GetStringAsync(fieldTypeRequestURL);
        var fieldTypes = JsonConvert.DeserializeObject<List<FieldType>>(fieldTypeResponse);

        // Chuyển đổi từ Field sang FieldViewModel và nạp thông tin từ FieldType
        var viewModels = fields.Select(f =>
        {
            var fieldType = fieldTypes.FirstOrDefault(ft => ft.Id == f.FieldTypeId);

            return new FieldViewModel
            {
                IdField = f.IdField,
                FieldName = f.FieldName,
                Status = f.Status,
                FieldTypeId = f.FieldTypeId,
                FieldTypeName = fieldType != null ? fieldType.Name : "Không xác định",
                FieldTypePrice = fieldType != null ? fieldType.Price : 0,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt,
                CreatedBy = f.CreatedBy,
                UpdatedBy = f.UpdatedBy
            };
        }).ToList();

        return viewModels;
    }

    public async Task<FieldViewModel> GetFieldByIdAsync(Guid id)
    {
        // Lấy Field từ API
        string fieldRequestURL = $"https://localhost:7143/api/Field/fields-get-id/{id}";
        var fieldResponse = await _httpClient.GetStringAsync(fieldRequestURL);
        var field = JsonConvert.DeserializeObject<Field>(fieldResponse);

        // Lấy FieldType từ API
        string fieldTypeRequestURL = "https://localhost:7143/api/FieldType/get-all";
        var fieldTypeResponse = await _httpClient.GetStringAsync(fieldTypeRequestURL);
        var fieldTypes = JsonConvert.DeserializeObject<List<FieldType>>(fieldTypeResponse);

        var fieldType = fieldTypes.FirstOrDefault(ft => ft.Id == field.FieldTypeId);

        return new FieldViewModel
        {
            IdField = field.IdField,
            FieldName = field.FieldName,
            Status = field.Status,
            FieldTypeId = field.FieldTypeId,
            FieldTypeName = fieldType != null ? fieldType.Name : "Không xác định",
            FieldTypePrice = fieldType != null ? fieldType.Price : 0,
            CreatedAt = field.CreatedAt,
            UpdatedAt = field.UpdatedAt,
            CreatedBy = field.CreatedBy,
            UpdatedBy = field.UpdatedBy
        };
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
