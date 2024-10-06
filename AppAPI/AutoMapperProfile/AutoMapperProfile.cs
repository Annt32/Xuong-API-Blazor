using AppData.DTO.FieldType_DTO;
using AppData.Entities;
using AutoMapper;

namespace AppAPI.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<FieldTypeDTO, FieldType>().ReverseMap();
            CreateMap<FieldTypeUpdateRequest, FieldType>();
            CreateMap<FieldTypeDelRequest, FieldType>();
        }
    }
}
