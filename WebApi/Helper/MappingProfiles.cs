using AutoMapper;
using WebApi.Dto;
using WebApi.Model;

namespace WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
        }
    }
}
