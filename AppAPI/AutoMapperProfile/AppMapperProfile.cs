using AppData.Entities;
using AutoMapper;
using AppData.DTO;
namespace AppAPI.AutoMapperProfile
{
	public class AppMapperProfile : Profile
	{
		public AppMapperProfile()
		{
			CreateMap<FieldShiftDTO, FieldShift>().ReverseMap();

		}
	}
}
