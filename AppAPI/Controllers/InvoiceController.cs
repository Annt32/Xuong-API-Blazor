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
                entity.Name = invoiceDTO.Name;
                entity.Email = invoiceDTO.Email;
                entity.PhoneNumber = invoiceDTO.PhoneNumber;
                entity.Notes = invoiceDTO.Notes;
                _respoitory.ModifileUpdate(entity);
                return Ok("Update success");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
