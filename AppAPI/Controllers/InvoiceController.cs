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
        private readonly IRepository<Invoice> _InvoiceRepo;
        private readonly IRepository<InvoiceDetail> _InvoiceDetailRepo;
        private readonly IRepository<ServiceField> _ServiceFieldRepo;
        private readonly IRepository<DrinkDetail> _DrinkDetailRepo;
        private readonly IRepository<RentalEquipmentDetail> _RentalEquipmentDetailRepo;
        private readonly IMapper _mapper;
        public InvoiceController(IRepository<Invoice> respoitory, IMapper mapper, 
            IRepository<InvoiceDetail> InvoiceDetailRepo, IRepository<ServiceField> ServiceFieldRepo,
            IRepository<DrinkDetail> DrinkDetailRepo, IRepository<RentalEquipmentDetail> RentalEquipmentDetailRepo)
        {
            _InvoiceRepo = respoitory;
            _InvoiceDetailRepo = InvoiceDetailRepo;
            _ServiceFieldRepo = ServiceFieldRepo;
            _DrinkDetailRepo = DrinkDetailRepo;
            _RentalEquipmentDetailRepo = RentalEquipmentDetailRepo;
            _mapper = mapper;
        }
        [HttpGet("InvoiceGetAll")]
        public IActionResult GetAll()
        {
            var invoicelist = _InvoiceRepo.GetAll();
            return Ok(invoicelist);
        }

        //Xóa lịch đặt sân chưa xác nhận
        [HttpDelete("invoice-delete/{id}")]
        public IActionResult DeleteInvoice(Guid id)
        {
            try
            {
                var invoice = _InvoiceRepo.GetById(id);
                if (invoice == null)
                {
                    return BadRequest("Không tìm thấy lịch đặt sân");
                }

                if (invoice.Status == 0) //Nếu lịch đặt sân là chưa xác nhận mới cho xóa
                {
                    //Tìm hóa đơn chi tiết của lịch đặt sân
                    List<InvoiceDetail> invoiceDetail = _InvoiceDetailRepo.Find(x => x.IdInvoice == invoice.IdInvoice).ToList();

                    //Tìm dịch vụ sân bóng của từng hóa đơn chi tiết
                    List<ServiceField> lstServiceField = new List<ServiceField>();
                    if (invoiceDetail != null && invoiceDetail.Any())
                    {
                        for (int i = 0; i < invoiceDetail.Count; i++)
                        {
                            List<ServiceField> lstServiceFieldsFound = _ServiceFieldRepo.Find(x => x.IdInvoiceDetail == invoiceDetail[i].IdInvoiceDetail).ToList();
                            if (lstServiceFieldsFound != null && lstServiceFieldsFound.Any() == true)
                            {
                                lstServiceField.AddRange(lstServiceFieldsFound);
                            }
                        }
                    }

                    //Tìm đồ thuê chi tiết của từng dịch vụ sân bóng
                    List<RentalEquipmentDetail> lstRentalEquipmentDetail = new List<RentalEquipmentDetail>();
                    if (lstServiceField != null && lstServiceField.Any())
                    {
                        for (int i = 0; i < lstServiceField.Count; i++)
                        {
                            List<RentalEquipmentDetail> lstRentalEquipmentDetailFound = _RentalEquipmentDetailRepo.Find(x => x.IdServiceField == lstServiceField[i].IdServiceField).ToList();
                            if (lstRentalEquipmentDetailFound != null && lstRentalEquipmentDetailFound.Any() == true)
                            {
                                lstRentalEquipmentDetail.AddRange(lstRentalEquipmentDetailFound);
                            }
                        }
                    }

                    //Tìm nước uống chi tiết của từng dịch vụ sân bóng
                    List<DrinkDetail> lstDrinkDetail = new List<DrinkDetail>();
                    if (lstServiceField != null && lstServiceField.Any())
                    {
                        for (int i = 0; i < lstServiceField.Count; i++)
                        {
                            List<DrinkDetail> lstDrinkDetailFound = _DrinkDetailRepo.Find(x => x.IdServiceField == lstServiceField[i].IdServiceField).ToList();
                            if (lstDrinkDetailFound != null && lstDrinkDetailFound.Any() == true)
                            {
                                lstDrinkDetail.AddRange(lstDrinkDetailFound);
                            }
                        }
                    }

                    //Xóa nước uống chi tiết
                    if (lstDrinkDetail != null && lstDrinkDetail.Any())
                    {
                        _DrinkDetailRepo.RemoveRange(lstDrinkDetail);
                    }

                    //Xóa đồ thuê chi tiết
                    if (lstRentalEquipmentDetail != null && lstRentalEquipmentDetail.Any())
                    {
                        _RentalEquipmentDetailRepo.RemoveRange(lstRentalEquipmentDetail);
                    }

                    //Xóa dịch vụ sân bóng
                    if (lstServiceField != null && lstServiceField.Any())
                    {
                        _ServiceFieldRepo.RemoveRange(lstServiceField);
                    }

                    //Xóa hóa đơn chi tiết của lịch đặt sân
                    if (invoiceDetail != null && invoiceDetail.Any())
                    {
                        _InvoiceDetailRepo.RemoveRange(invoiceDetail);
                    }

                    //Xóa lịch đặt sân
                    _InvoiceRepo.Remove(invoice);

                    return Ok("Xóa lịch đặt sân chưa xác nhận thành công");
                }
                else
                {
                    return BadRequest("Chỉ được phép xóa lịch đặt sân chưa xác nhận");
                }
            }
            catch (Exception)
            {
                return BadRequest("Xóa lịch đặt sân thất bại");
            }
        }

        //Hủy lịch đặt sân đã xác nhận
        [HttpPut("cancel-invoice/{id}")]
        public IActionResult CancelInvoice(Guid id)
        {
            try
            {
                var entity = _InvoiceRepo.GetById(id);
                if (entity == null)
                {
                    return BadRequest("Không tìm thấy lịch đặt sân");
                }

                if (entity.Status == 1) //Nếu trạng thái là đã xác nhận: 1
                {
                    entity.Status = 2; //Chuyển sang trạng thái là hủy: 2
                    entity.UpdatedAt = DateTime.Now;
                }
                else
                {
                    return BadRequest("Chỉ có thể hủy lịch đặt sân đã xác nhận");
                }
                _InvoiceRepo.ModifileUpdate(entity);
                return Ok("Hủy lịch đặt sân thành công");
            }
            catch (Exception)
            {
                return BadRequest("Hủy lịch đặt sân thất bại");
            }
        }
    }
}
