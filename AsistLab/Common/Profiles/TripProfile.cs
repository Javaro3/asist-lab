using AutoMapper;
using Common.Domains;
using Common.Dtos;

namespace Common.Profiles;

public class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<Trip, EditTripDto>();
        CreateMap<EditTripDto, Trip>()
            .ForMember(dest => dest.ExpectedStartTime, s => s.MapFrom(e => DateTime.SpecifyKind(e.ExpectedStartTime, DateTimeKind.Utc)))
            .ForMember(dest => dest.ExpectedFinishTime, s => s.MapFrom(e => DateTime.SpecifyKind(e.ExpectedFinishTime, DateTimeKind.Utc)))
            .ForMember(dest => dest.Images, s => s.MapFrom(e => new List<Image>()));
        
        CreateMap<Trip, TripDto>()
            .ForMember(dest => dest.Duration, s => s.MapFrom(e => e.RealFinishTime - e.RealStartTime));
    }
}