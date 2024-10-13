using AppData.Entities;
using BlazorServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFieldService
{
    Task<List<FieldViewModel>> GetAllFieldsAsync();
    Task<FieldViewModel> GetFieldByIdAsync(Guid id);
    Task<bool> CreateFieldAsync(Field field);
    Task<bool> UpdateFieldAsync(Guid id, Field updatedField);
    Task<bool> DeleteFieldAsync(Guid id);
}

