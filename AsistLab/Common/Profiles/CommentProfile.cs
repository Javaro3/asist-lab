using AutoMapper;
using Common.Domains;
using Common.Dtos;

namespace Common.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
    }
}