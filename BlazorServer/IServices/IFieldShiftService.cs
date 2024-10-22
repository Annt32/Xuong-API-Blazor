using AppData.DTO;
using AppData.DTO.Field_DTO;
using AppData.DTO.FieldType_DTO;
using AppData.Entities; // Sử dụng DTO

namespace BlazorServer.IServices
{
    public interface IFieldShiftService
    {
        Task<List<FieldShiftDTO>> GetAllFieldshiftAsync();
        Task<FieldShiftDTO> GetFieldshiftByIdAsync(Guid id);
        Task<bool> CreateFieldshiftAsync(FieldShiftDTO fieldshift);
        Task<bool> UpdateFieldshiftAsync(Guid id, FieldShiftDTO updatedFieldShift);
        Task<bool> DeleteFieldshiftAsync(Guid id);

        //
        Task<List<FieldTypeDTO>> GetAllFieldTypeAsync();
        Task<List<Shift>> GetAllShiftAsync();
        Task<List<FieldDTO>> GetFieldByTypeAsync(Guid idtype);
    }
}
