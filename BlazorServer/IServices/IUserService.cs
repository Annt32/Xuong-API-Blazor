using AppData.DTO.User_RoleDto;

namespace BlazorServer.IServices
{
    public interface IUserService
    {
        Task<bool> CreateUsersAsync(WebUser value);
        Task<bool> DeleteUsersAsync(Guid id);
        Task<List<WebUser>> GetAllUsersAsync();
        Task<WebUser> GetUsersByIdAsync(Guid id);
        Task<bool> ModifileUpdateUsersAsync(WebUser value);
        Task<bool> UpdateUsersAsync(Guid id, WebUser updatedValue);
    }
}