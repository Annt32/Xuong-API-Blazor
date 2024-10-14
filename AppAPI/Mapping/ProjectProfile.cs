using AppData.DTO;
using AppData.DTO.User_RoleDto;
using AppData.Entities;
using AutoMapper;

namespace AppAPI.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {

            CreateMap<Attendance, AttendDto>().ReverseMap();
            CreateMap<User, WebUser>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
