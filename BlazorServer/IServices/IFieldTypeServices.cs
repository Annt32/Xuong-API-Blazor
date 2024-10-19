using AppData.DTO.FieldType_DTO;
using AppData.Entities;
using BlazorServer.Models;

namespace BlazorServer.IServices
{
    public interface IFieldTypeServices
    {
        Task<List<FieldTypeDTO>> GetAllFieldType();
        Task<FieldTypeDTO> GetFieldByIdAsync(Guid id);
        Task<bool> CreateFieldAsync(FieldType field);
        Task<bool> UpdateFieldAsync(Guid id, FieldType updatedField);
        Task<bool> DeleteFieldAsync(Guid id);
    }

}

