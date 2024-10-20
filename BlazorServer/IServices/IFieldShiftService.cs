﻿using AppData.DTO; // Sử dụng DTO

namespace BlazorServer.IServices
{
    public interface IFieldShiftService
    {
        Task<List<FieldShiftDTO>> GetAllFieldshiftAsync();
        Task<FieldShiftDTO> GetFieldshiftByIdAsync(Guid id);
        Task<bool> CreateFieldshiftAsync(FieldShiftDTO fieldshift);
        Task<bool> UpdateFieldshiftAsync(Guid id, FieldShiftDTO updatedFieldShift);
        Task<bool> DeleteFieldshiftAsync(Guid id);
    }
}
