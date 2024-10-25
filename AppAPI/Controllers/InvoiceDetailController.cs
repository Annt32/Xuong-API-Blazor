using AppAPI.Repositories;
using AppData.DTO;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceDetailController : ControllerBase
	{
		private readonly IRepository<InvoiceDetail> _respoitory;
		private readonly IMapper _mapper;
		public InvoiceDetailController(IRepository<InvoiceDetail> respoitory, IMapper mapper)
		{
			_respoitory = respoitory;
			_mapper = mapper;
		}
		[HttpGet("InvoiceDetailGetAll")]
		public IActionResult GetAll()
		{
			var invoicedetaillist = _respoitory.GetAll();
			if (invoicedetaillist == null)
			{
				return NotFound();
			}
			return Ok(invoicedetaillist.Select(x => _mapper.Map<InvoiceDetailDTO>(x)).ToList());
		}
		[HttpGet("invoicedetail-get-id/{id}")]
		public IActionResult GetInvoiceDetailById(Guid id)
		{
			var invoicedetail = _respoitory.GetById(id);
			if (invoicedetail == null)
			{
				return NotFound("invoice not found");
			}
			return Ok(invoicedetail);
		}
		[HttpPut("invoicedetail-put/{id}")]
		public IActionResult UpdateInvoiceDetail(Guid id, [FromBody] InvoiceDetailDTO invoicedetailDTO)
		{
			try
			{
				var entity = _respoitory.GetById(id);
				if (entity == null)
				{
					return Ok("Id Null");
				}
				entity.IdFieldShift = invoicedetailDTO.IdFieldShift;


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
