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

		[HttpPost("invoicedetail-create")]
		public IActionResult CreateInvoiceDetail([FromBody] InvoiceDetailDTO invoicedetailDTO)
		{
			try
			{
				var invoiceDetail = _mapper.Map<InvoiceDetail>(invoicedetailDTO);
				invoiceDetail.IdInvoiceDetail = Guid.NewGuid();
				invoiceDetail.CreatedAt = DateTime.Now;
				invoiceDetail.UpdatedAt = DateTime.Now;

				_respoitory.Add(invoiceDetail);
				return Ok("Invoice detail created successfully");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error creating invoice detail: {ex.Message}");
			}
		}

		[HttpDelete("invoicedetail-delete/{id}")]
		public IActionResult DeleteInvoiceDetail(Guid id)
		{
			try
			{
				var invoiceDetail = _respoitory.GetById(id);
				if (invoiceDetail == null)
				{
					return NotFound("Invoice detail not found");
				}

				_respoitory.Remove(invoiceDetail);
				return Ok("Invoice detail deleted successfully");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error deleting invoice detail: {ex.Message}");
			}
		}

	}

}
