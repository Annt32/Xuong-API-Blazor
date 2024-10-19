using AppData.Entities;
using BlazorServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShiftService
{
    Task<List<ShiftViewModel>> GetAllShiftsAsync();
    Task<ShiftViewModel> GetShiftByIdAsync(Guid id);
    Task<bool> CreateShiftAsync(Shift shift);
    Task<bool> UpdateShiftAsync(Guid id, Shift updatedShift);
    Task<bool> DeleteShiftAsync(Guid id);
}
