using AutoMapper;
using Common.Domains;
using Common.Dtos;

namespace Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, LoginDto>();
        CreateMap<User, RegisterDto>();
        CreateMap<LoginDto, User>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}