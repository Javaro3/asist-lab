using AutoMapper;
using Common.Domains;
using Common.Dtos;

namespace Common.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageDto>();
        CreateMap<ImageDto, Image>();
    }
}