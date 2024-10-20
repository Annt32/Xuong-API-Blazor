﻿using AppData.DTO; // Sử dụng DTO thay vì thực thể
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

        // Tạo mới FieldShift bằng DTO
        public async Task<bool> CreateFieldshiftAsync(FieldShiftDTO fieldshift)
        {
            string requestURL = "https://localhost:7143/api/Fieldshift/fieldshift-post";
            var jsonContent = JsonConvert.SerializeObject(fieldshift); // Sử dụng DTO để serialize
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        // Xóa FieldShift theo ID
        public async Task<bool> DeleteFieldshiftAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }

        // Lấy tất cả FieldShifts (sử dụng DTO)
        public async Task<List<FieldShiftDTO>> GetAllFieldshiftAsync()
        {
            string requestURL = "https://localhost:7143/api/FieldShift/fieldshift-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<FieldShiftDTO>>(response); // Deserialize về DTO
        }

        // Lấy FieldShift theo ID (sử dụng DTO)
        public async Task<FieldShiftDTO> GetFieldshiftByIdAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-get-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<FieldShiftDTO>(response); // Deserialize về DTO
        }

        // Cập nhật FieldShift (sử dụng DTO)
        public async Task<bool> UpdateFieldshiftAsync(Guid id, FieldShiftDTO updatedFieldShift)
        {
            string requestURL = $"https://localhost:7143/api/Fieldshift/fieldshift-put/{id}";
            var jsonContent = JsonConvert.SerializeObject(updatedFieldShift); // Sử dụng DTO để serialize
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }
    }
}
