using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IInvoiceServices
    {
        Task<List<Invoice>> GetAllInvoiceAsync();
        Task<bool> DeleteInvoiceAsync(Guid id);
        Task<bool> CancelInvoiceAsync(Guid id);
    }
}