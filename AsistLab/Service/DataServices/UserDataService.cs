using AutoMapper;
using Common.Domains;
using Common.Dtos;
using Common.Options;
using Microsoft.Extensions.Options;
using Repository.Repositories.Interfaces;
using Service.Infrastructure;

namespace Service.DataServices;

public class UserDataService
{
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly JwtOption _jwtOptions;
    private readonly IMapper _mapper;

    public UserDataService(
        IUserRepository userRepository,
        IFriendRepository friendRepository,
        IOptions<JwtOption> jwtOptions,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _jwtOptions = jwtOptions.Value;
        _mapper = mapper;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByLoginAsync(dto.Login);

        if (user == null)
            throw new Exception("This user does not exist");

        if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Password is not correct");
        
        var token = JwtTokenGenerator.GenerateJwtToken(user, _jwtOptions);
        return token;
    }
    
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var userExists = await _userRepository.UserWithThisLoginExistsAsync(dto.Login);
        if (userExists)
            throw new Exception("User with this login already exists");

        var user = _mapper.Map<User>(dto);
        user.CreatedOn = DateTime.UtcNow;
        user.PasswordHash = PasswordHasher.GenerateHash(dto.Password);

        await _userRepository.AddAsync(user);
        var token = JwtTokenGenerator.GenerateJwtToken(user, _jwtOptions);
        return token;
    }
    
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var dtos = _mapper.Map<List<UserDto>>(users);

        return dtos;
    }
    
    public async Task AddFriend(int userId, int friendId)
    {
        var friend = new Friend { SourceUserId = userId, TargetUserId = friendId };
        await _friendRepository.AddAsync(friend);
    }
}