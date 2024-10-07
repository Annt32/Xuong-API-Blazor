using AppData.DTO.FieldType_DTO;
using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;

namespace BlazorServer.Services
{
    public class FieldTypeServices : IFieldTypeServices
    {

        private readonly HttpClient _client;

        public FieldTypeServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<FieldTypeDTO>> GetAllFieldType()
        {
            var lst = new List<FieldTypeDTO>();
            var result = await _client.GetStringAsync("https://localhost:7143/api/FieldType/get-all");


            if (result != null)
            {
                lst = JsonConvert.DeserializeObject<List<FieldTypeDTO>>(result);
            }

            return lst;
            
        }
    }
}
