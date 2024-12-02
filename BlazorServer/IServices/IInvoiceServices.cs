using AppData.Entities;

namespace BlazorServer.IServices
{
    public interface IInvoiceServices
    {
        Task<List<Invoice>> GetAllInvoiceAsync();
        Task<List<User>> GetAllUser();
        Task<List<InvoiceDetail>> GetAllInvoiceDetailAsync();
        Task<List<FieldShift>> GetAllFieldShiftAsync();
        Task<List<Field>> GetAllShiftAsync();
        Task<List<Shift>> GetFieldByTypeAsync();
        Task<bool> UpdateUserInvoice(Guid id, Invoice invoiceupdate );
		Task<Invoice> GetinvoiceByIdAsync(Guid id);
		Task<bool> CreateInvoiceAsync(Invoice invoice);
		Task<bool> DeleteInvoiceAsync(Guid id);

	}
}
