using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFieldService
{
    Task<List<Field>> GetAllFieldsAsync();
    Task<Field> GetFieldByIdAsync(Guid id);
    Task<bool> CreateFieldAsync(Field field);
    Task<bool> UpdateFieldAsync(Guid id, Field updatedField);
    Task<bool> DeleteFieldAsync(Guid id);
}
