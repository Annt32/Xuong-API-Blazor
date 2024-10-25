using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IInvoiceServices
    {
        Task<List<Invoice>> GetAllInvoiceAsync();
        Task<bool> UpdateUserInvoice(Guid id, Invoice invoiceupdate );
		Task<Invoice> GetinvoiceByIdAsync(Guid id);


	}
}
