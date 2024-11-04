using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IInvoiceDetailService 
    {
        Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync();
        Task<bool> UpdateInvoiceDetail(Guid id, InvoiceDetail invoicedetailupdate);
        Task<InvoiceDetail> GetinvoiceDetailByIdAsync(Guid id);

    }
}
