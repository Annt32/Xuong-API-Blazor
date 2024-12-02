using AppAPI.Repositories;
using AppData.DTO;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IRepository<Invoice> _respoitory;
        private readonly IMapper _mapper;
        public InvoiceController(IRepository<Invoice> respoitory, IMapper mapper)
        {
            _respoitory = respoitory;
            _mapper = mapper;
        }
        [HttpGet("InvoiceGetAll")]
        public IActionResult GetAll()
        {
            var invoicelist = _respoitory.GetAll();
            return Ok(invoicelist.Select(x => _mapper.Map<InvoiceDTO>(x)).ToList());
        }

		[HttpGet("invoice-get-id/{id}")]
		public IActionResult GetInvoiceById(Guid id)
		{
			var invoice = _respoitory.GetById(id);
			if (invoice == null)
			{
				return NotFound("invoice not found");
			}
			return Ok(invoice);
		}
		[HttpPut("invoice-put/{id}")]
        public IActionResult UpdateUserInvoice(Guid id, [FromBody] InvoiceDTO invoiceDTO)
        {
            try
            {
                var entity = _respoitory.GetById(id);
                if (entity == null)
                {
                    return Ok("Id Null");
                }

                entity.Notes = invoiceDTO.Notes;
                _respoitory.ModifileUpdate(entity);
                return Ok("Update success");
            }
            catch (Exception)
            {

                throw;
            }
        }

		[HttpPost("invoice-create")]
		public IActionResult CreateInvoice([FromBody] InvoiceDTO invoiceDTO)
		{
			try
			{
				var invoice = _mapper.Map<Invoice>(invoiceDTO);
				invoice.IdInvoice = Guid.NewGuid();
				invoice.CreatedAt = DateTime.Now;
				invoice.UpdatedAt = DateTime.Now;

				_respoitory.Add(invoice);
				return Ok("Invoice created successfully");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error creating invoice: {ex.Message}");
			}
		}

		[HttpDelete("invoice-delete/{id}")]
		public IActionResult DeleteInvoice(Guid id)
		{
			try
			{
				var invoice = _respoitory.GetById(id);
				if (invoice == null)
				{
					return NotFound("Invoice not found");
				}

				_respoitory.Remove(invoice);
				return Ok("Invoice deleted successfully");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error deleting invoice: {ex.Message}");
			}
		}

	}
}
