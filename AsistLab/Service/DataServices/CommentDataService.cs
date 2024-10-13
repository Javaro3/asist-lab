using AutoMapper;
using Common.Domains;
using Common.Dtos;
using Repository.Repositories.Interfaces;

namespace Service.DataServices;

public class CommentDataService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    
    public CommentDataService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<CommentDto> AddCommentAsync(CommentDto commentDto)
    {
        var model = _mapper.Map<Comment>(commentDto);
        await _commentRepository.AddAsync(model);

        var newComment = await _commentRepository.GetByIdAsync(model.Id);
        var dto = _mapper.Map<CommentDto>(newComment);

        return dto;
    }
}