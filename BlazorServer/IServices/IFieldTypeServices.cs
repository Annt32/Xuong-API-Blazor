using AppData.DTO.FieldType_DTO;
using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IFieldTypeServices
    {
        Task<List<FieldTypeDTO>> GetAllFieldType();

    }
}
