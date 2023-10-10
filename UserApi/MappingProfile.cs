using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Auth;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserForCreationDto,User>();

            CreateMap<UserForUpdateDto, User>();

            CreateMap<Role, RoleDto>();

            CreateMap<UserReg,UserIdentity>();


        }
    }

}
