using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IInvoiceDetailServices
    {
        Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync();
        Task<bool> DeleteInvoiceDetailAsync(Guid id);
        Task<bool> CancelInvoiceDetailAsync(Guid id);
    }
}