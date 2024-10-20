using AppData.Entities;
using BlazorServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ShiftService : IShiftService
{
    private readonly HttpClient _httpClient;

    public ShiftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ShiftViewModel>> GetAllShiftsAsync()
    {
        string requestURL = "https://localhost:7143/api/Shift/shifts-get";
        var response = await _httpClient.GetStringAsync(requestURL);
        var shifts = JsonConvert.DeserializeObject<List<Shift>>(response);

        // Chuyển đổi sang ShiftViewModel
        var viewModels = shifts.Select(s => new ShiftViewModel
        {
            IdShift = s.IdShift,
            ShiftName = s.ShiftName,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Status = s.Status,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            CreatedBy = s.CreatedBy,
            UpdatedBy = s.UpdatedBy
        }).ToList();

        return viewModels;
    }

    public async Task<ShiftViewModel> GetShiftByIdAsync(Guid id)
    {
        string requestURL = $"https://localhost:7143/api/Shift/shifts-get-id/{id}";
        var response = await _httpClient.GetStringAsync(requestURL);
        var shift = JsonConvert.DeserializeObject<Shift>(response);

        return new ShiftViewModel
        {
            IdShift = shift.IdShift,
            ShiftName = shift.ShiftName,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Status = shift.Status,
            CreatedAt = shift.CreatedAt,
            UpdatedAt = shift.UpdatedAt,
            CreatedBy = shift.CreatedBy,
            UpdatedBy = shift.UpdatedBy
        };
    }

    public async Task<bool> CreateShiftAsync(Shift shift)
    {
        string requestURL = "https://localhost:7143/api/Shift/shifts-post";
        var jsonContent = JsonConvert.SerializeObject(shift);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateShiftAsync(Guid id, Shift updatedShift)
    {
        string requestURL = $"https://localhost:7143/api/Shift/shifts-put/{id}";
        var jsonContent = JsonConvert.SerializeObject(updatedShift);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestURL, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteShiftAsync(Guid id)
    {
        string requestURL = $"https://localhost:7143/api/Shift/shifts-delete/{id}";
        var response = await _httpClient.DeleteAsync(requestURL);
        return response.IsSuccessStatusCode;
    }
}
