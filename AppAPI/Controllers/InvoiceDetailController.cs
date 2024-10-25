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
        private readonly IRepository<InvoiceDetail> _InvoiceDetailRepo;
        private readonly IRepository<ServiceField> _ServiceFieldRepo;
        private readonly IRepository<DrinkDetail> _DrinkDetailRepo;
        private readonly IRepository<RentalEquipmentDetail> _RentalEquipmentDetailRepo;
        public InvoiceDetailController( 
            IRepository<InvoiceDetail> InvoiceDetailRepo, IRepository<ServiceField> ServiceFieldRepo,
            IRepository<DrinkDetail> DrinkDetailRepo, IRepository<RentalEquipmentDetail> RentalEquipmentDetailRepo)
        {
            _InvoiceDetailRepo = InvoiceDetailRepo;
            _ServiceFieldRepo = ServiceFieldRepo;
            _DrinkDetailRepo = DrinkDetailRepo;
            _RentalEquipmentDetailRepo = RentalEquipmentDetailRepo;
        }

        [HttpGet("InvoiceDetailGetAll")]
        public IActionResult GetAll()
        {
            var invoicelist = _InvoiceDetailRepo.GetAll();
            return Ok(invoicelist);
        }

        //Xóa lịch đặt sân chưa xác nhận
        [HttpDelete("invoicedetail-delete/{id}")]
        public IActionResult DeleteInvoiceDetail(Guid id)
        {
            try
            {
                var invoiceDetail = _InvoiceDetailRepo.GetById(id);
                if (invoiceDetail == null)
                {
                    return BadRequest("Không tìm thấy lịch đặt sân");
                }

                if (invoiceDetail.Status == 0) //Nếu lịch đặt sân là chưa xác nhận mới cho xóa
                {
                    //Tìm dịch vụ sân bóng của invoiceDetail
                    List<ServiceField> lstServiceField = _ServiceFieldRepo.Find(x => x.IdInvoiceDetail == invoiceDetail.IdInvoiceDetail).ToList();

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

                    //Xóa lịch đặt sân
                    _InvoiceDetailRepo.Remove(invoiceDetail);

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
        [HttpPut("cancel-invoiceDetail/{id}")]
        public IActionResult CancelInvoiceDetail(Guid id)
        {
            try
            {
                var entity = _InvoiceDetailRepo.GetById(id);
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
                _InvoiceDetailRepo.ModifileUpdate(entity);
                return Ok("Hủy lịch đặt sân thành công");
            }
            catch (Exception)
            {
                return BadRequest("Hủy lịch đặt sân thất bại");
            }
        }
    }
}
