using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IServices<T> where T : class
    {
        Task<List<T>> GetAllFieldsAsync();
        Task<T> GetFieldByIdAsync(Guid id);
        Task<bool> CreateFieldAsync(T value);
        Task<bool> UpdateFieldAsync(Guid id, T updatedValue);
        Task<bool> DeleteFieldAsync(Guid id);
        Task<bool> ModifileUpdateFieldAsync(T value);
    }
}
