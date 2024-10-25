using AppData.DTO; // Sử dụng DTO thay vì thực thể
using AppData.DTO.Field_DTO;
using AppData.DTO.FieldType_DTO;
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

        // Tạo mới FieldShift bằng DTO
        public async Task<bool> CreateFieldshiftAsync(FieldShiftDTO fieldshift)
        {
            string requestURL = "https://localhost:7143/api/FieldShift/fieldshift-post";

            // Gửi request tới API
            var response = await _httpClient.PostAsJsonAsync(requestURL, fieldshift);

            // Kiểm tra nếu request không thành công
            if (!response.IsSuccessStatusCode)
            {
                // Đọc nội dung phản hồi từ API
                var errorContent = await response.Content.ReadAsStringAsync();

                // Log hoặc hiển thị lỗi từ API (tùy thuộc vào cách bạn xử lý log)
                Console.WriteLine($"Error: {response.StatusCode}, Message: {errorContent}");

                // Bạn cũng có thể tùy chọn throw exception nếu cần
                throw new Exception($"Request failed with status code: {response.StatusCode}. Details: {errorContent}");
            }

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

		public async Task<List<FieldTypeDTO>> GetAllFieldTypeAsync()
		{
            var response = await _httpClient.GetFromJsonAsync<List<FieldTypeDTO>>("https://localhost:7143/api/FieldType/get-all");

            return response;
		}

		public async Task<List<Shift>> GetAllShiftAsync()
		{
            var response = await _httpClient.GetFromJsonAsync<List<Shift>>("https://localhost:7143/api/Shift/shifts-get");

            return response;
		}

		public async Task<List<FieldDTO>> GetFieldByTypeAsync(Guid idtype)
		{
			var response = await _httpClient.GetFromJsonAsync<List<FieldDTO>>("https://localhost:7143/api/Field/fields-get");

            var lst = response.Where(x => x.FieldTypeId == idtype).ToList();

            return lst;
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
