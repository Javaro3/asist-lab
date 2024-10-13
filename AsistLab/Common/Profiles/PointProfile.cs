using AutoMapper;
using Common.Domains;
using Common.Dtos;

namespace Common.Profiles;

public class PointProfile : Profile
{
    public PointProfile()
    {
        CreateMap<Point, PointDto>();
        CreateMap<PointDto, Point>();
    }
}